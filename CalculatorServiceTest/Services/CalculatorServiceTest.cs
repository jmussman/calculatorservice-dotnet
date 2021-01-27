// CalculatorServiceTest.cs
// Copyright © 2018-2021 Joel Mussman. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WonderfulWidgets.Services {

    public class CalculatorServiceTest {

        private CalculatorService calculatorService;

        private void GivenCalculatorService() {

            calculatorService = new CalculatorService();
        }

        // Single test.

        [Fact]
        public void AddsOneAndOneThousand() {

            GivenCalculatorService();

            Assert.Equal(1001, calculatorService.Add(1, 1000), 0);
        }

        // Single test of thrown exception.

        [Fact]
        public void RejectsZeroLeftNumberOutsideOfRange() {

            GivenCalculatorService();

            Assert.Throws<ArgumentOutOfRangeException>(() => calculatorService.Add(0, 1000));
        }


        // Data-drive inline test of numbers in the range.

        [Theory]
        [InlineData(1, 1000, 1001)]
        [InlineData(1000, 1, 1001)]
        public void AddsNumbersInRange(double x, double y, double result) {

            GivenCalculatorService();

            Assert.Equal(result, calculatorService.Add(x, y), 0);
        }

        // Property data test of parameters out of range (alternate form of assertion).

        [Theory]
        [MemberData(nameof(PropertyData))]
        public void RejectsBothNumbersOutsideOfRange(double x, double y) {

            GivenCalculatorService();

#pragma warning disable xUnit2015 // Do not use typeof expression to check the exception type
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => calculatorService.Add(x, y));
#pragma warning restore xUnit2015 // Do not use typeof expression to check the exception type
        }

        public static IEnumerable<object[]> PropertyData =>

            new List<object[]> {

                new object[] { 0, 1001 },
                new object[] { 1001, 0 }
            };

        // Method data test of parameters out of range.
    
        [Theory]
        [MemberData(nameof(LowLeftNumbers))]
        public void RejectsLowLeftNumbersOutsideOfRange(double x, double y) {

            GivenCalculatorService();

            Assert.Throws<ArgumentOutOfRangeException>(() => calculatorService.Add(x, y));
        }

        public static IEnumerable<object[]> LowLeftNumbers() {

            return new List<object[]> {

                new object[] { 0, 1000 },
                new object[] { -1, 1000 }
            };
        }

        // Method data test of parameters out of range.

        [Theory]
        [MemberData(nameof(HighLeftNumbers), parameters: 1)]
        public void RejectsHighLeftNumbersOutsideOfRange(double x, double y) {

            GivenCalculatorService();

            Assert.Throws<ArgumentOutOfRangeException>(() => calculatorService.Add(x, y));
        }

        public static IEnumerable<object[]> HighLeftNumbers(int count) {

            List<object[]> data = new List<object[]> {

                new object[] { 1001, 1 },
                new object[] { 1002, 1 },
                new object[] { 1003, 1 }
            };

            return data.Take(count);
        }

        // External class method data test of parameters out of range.

        [Theory]
        [MemberData(nameof(CalculatorTestData.LowRightNumbers), MemberType=typeof(CalculatorTestData))]
        public void RejectsLowRightNumbersOutsideOfRange(double x, double y) {

            GivenCalculatorService();

            Assert.Throws<ArgumentOutOfRangeException>(() => calculatorService.Add(x, y));
        }

        // External enumerable class generates parameters out of range.

        [Theory]
        [ClassData(typeof(CalculatorTestDataGenerator))]
        public void RejectsHighRightNumbersOutsideOfRange(double x, double y) {

            GivenCalculatorService();

            Assert.Throws<ArgumentOutOfRangeException>(() => calculatorService.Add(x, y));
        }

        // CSV and Excel
        // These theories have been left in an examples, but the CsvDataAttribute and
        // the ExcelDataAttribute are depcreated and no longer included in the xUnit
        // library. At one point, around 2018, stuff was moved to a separate project on
        // Github: https://github.com/xunit/samples.xunit. ExcelData is still listed on
        // the main xUnit site as being there, but it was removed at some point.
        //

        // [Theory]
        // [CsvData(@"Resources/LowLeftDataPoints.csv")]
        // public void RejectsLowLeftDataPointsFromCsv(double x, double y) {
        //
        //     GivenCalculatorService();
        //
        //    Assert.Throws<ArgumentOutOfRangeException>(() => calculatorService.Add(x, y));
        // }

        // [Theory]
        // [ExcelData(@"Resources/LowRightDataPoints.xlsx", "Select * from TestData")]
        // public void RejectsLowLeftDataPointsFromCsv(double x, double y) {
        //
        //    GivenCalculatorService();
        //
        //    Assert.Throws<ArgumentOutOfRangeException>(() => calculatorService.Add(x, y));
        // }
    }
}
