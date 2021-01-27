// MoqCalculatorTests.cs
// Copyright © 2018-2021 Joel Mussman. All rights reserved.
//
// These tests demonstrate using Moq. ICalculator and CalculatorService are being mocked.
// The mocks are called directly in the tests without intermediate test clients depending
// on them, because the focus is on the syntax of Moq.
//

using System;
using System.Linq;
using Moq;
using Xunit;

namespace WonderfulWidgets.Services {

    public class MoqCalculatorTests {

        private Mock<ICalculator> moqCalculator;
        private Mock<CalculatorService> moqCalculatorService;

        private void GivenICalculator() {

            moqCalculator = new Mock<ICalculator>();
        }

        private void GivenCalculatorService() {

            moqCalculatorService = new Mock<CalculatorService>();
        }

        [Fact]
        public void MoqCalculatorAddSuccessful() {

            GivenICalculator();

            moqCalculator.Setup(m => m.Add(1, 1000)).Returns(1001);

            Assert.Equal(1001, moqCalculator.Object.Add(1, 1000), 0);
        }

        [Fact]
        public void UnmockedMethodReturnsDefaultValue() {

            GivenICalculator();

            moqCalculator.Setup(m => m.Add(1, 1000)).Returns(1001);

            Assert.Equal(0, moqCalculator.Object.Add(1000, 1), 0);
        }

        [Fact]
        public void MoqCalculatorNumbersOutOfRangeThrowException() {

            GivenICalculator();

            moqCalculator.Setup(m => m.Add(0, 1000)).Throws(new ArgumentOutOfRangeException());

            Assert.Throws<ArgumentOutOfRangeException>(() => moqCalculator.Object.Add(0, 1000));
        }

        [Fact]
        public void MoqPassesParameters() {

            GivenICalculator();

            moqCalculator.Setup(m => m.Add(1000, 1)).Returns((double x, double y) => x + y);

            Assert.Equal(1001, moqCalculator.Object.Add(1000, 1));
        }

        [Fact]
        public async void MoqAsyncMethod() {

            GivenCalculatorService();

            moqCalculatorService.Setup(m => m.AddAsync(1, 1001)).ReturnsAsync(1001);

            Assert.Equal(1001.0, await moqCalculatorService.Object.AddAsync(1, 1000), 0);
        }

        [Fact]
        public void MoqAsyncThrowsException() {

            GivenCalculatorService();

            moqCalculatorService.Setup(m => m.AddAsync(0, 1001)).ThrowsAsync(new ArgumentOutOfRangeException());

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => moqCalculatorService.Object.AddAsync(0, 1000));
        }

        [Fact]
        public void CantMockNonPublicMethod() {

            GivenCalculatorService();

            DateTime start = DateTime.Now;

            moqCalculatorService.Setup(m => m.CantMoqThis(start, TimeSpan.Zero)).Returns(start);

            // Assert.Equal(start, moqCalculator.Add(start, TimeSpan.Zero)); // fails
        }

        [Fact]
        public void MoqPropertyGet() {

            GivenCalculatorService();

            moqCalculatorService.Setup(m => m.Radix).Returns(2);

            Assert.Equal(2, moqCalculatorService.Object.Radix);
        }

        [Fact]
        public void MoqPropertySet() {

            GivenCalculatorService();

            moqCalculatorService.SetupSet(m => m.Radix = 2);

            moqCalculatorService.Object.Radix = 2;

            moqCalculatorService.VerifySet(m => m.Radix = 2);
        }

        [Fact]
        public void MoqPropertyWithPersistence() {

            GivenCalculatorService();

            moqCalculatorService.SetupProperty(m => m.Radix);

            moqCalculatorService.Object.Radix = 8;
            Assert.Equal(8, moqCalculatorService.Object.Radix);
        }

        [Fact]
        public void MoqCalculatorServiceAcceptAnyNumbers() {

            GivenCalculatorService();

            moqCalculatorService.Setup(m => m.Add(It.IsAny<double>(), 1000)).Returns(1001);

            Assert.Equal(1001, moqCalculatorService.Object.Add(42, 1000), 0);
        }

        [Fact]
        public void MoqCalculatorServiceAcceptAnyRefs() {

            GivenCalculatorService();

            double p = 1;

            moqCalculatorService.Setup(m => m.Add(ref It.Ref<double>.IsAny, ref p)).Returns(1001);

            Assert.Equal(1001, moqCalculatorService.Object.Add(ref p, ref p), 0);
        }

        [Fact]
        public void MoqCalculatorServiceNumberRange() {

            GivenCalculatorService();

            moqCalculatorService.Setup(m => m.Add(It.Is<double>(d => d == 1), 1000)).Returns(1001);

            Assert.Equal(1001, moqCalculatorService.Object.Add(1, 1000), 0);
            Assert.Equal(0, moqCalculatorService.Object.Add(2, 1000), 0); // default returned
        }

        [Fact]
        public void MoqCalculatorServiceNumberLambda() {

            GivenCalculatorService();

            moqCalculatorService.Setup(m => m.Add(It.IsInRange<double>(1, 3, Range.Exclusive), 1000)).Returns(1002);

            Assert.Equal(1002, moqCalculatorService.Object.Add(2, 1000), 0);
            Assert.Equal(0, moqCalculatorService.Object.Add(1, 1000), 0); // default returned
        }

        [Fact]
        public void MoqCalculatorServiceMatchesRegex() {

            GivenCalculatorService();

            moqCalculatorService.Setup(m => m.Add(It.IsRegex("^[a-m]{3}$"), "xyz")).Returns("abcxyz");

            Assert.Equal("abcxyz", moqCalculatorService.Object.Add("abc", "xyz"));
            Assert.Null(moqCalculatorService.Object.Add("qed", "xyz")); // default returned
        }

        [Fact]
        public void MoqVerifyAddCalled() {

            GivenCalculatorService();

            moqCalculatorService.Setup(m => m.Add(1, 1000)).Returns(1001);

            double result = moqCalculatorService.Object.Add(1, 1001);

            moqCalculatorService.Verify(m => m.Add(1, 1001));
        }

        [Fact]
        public void MoqVerifyAddCalledOnce() {

            GivenCalculatorService();

            moqCalculatorService.Setup(m => m.Add(1, 1000)).Returns(1001);

            double result = moqCalculatorService.Object.Add(1, 1001);

            moqCalculatorService.Verify(m => m.Add(1, 1001), Times.Once);
            moqCalculatorService.Verify(m => m.Add(1, 1001), Times.Exactly(1));
            Times.
        }
    }
}
