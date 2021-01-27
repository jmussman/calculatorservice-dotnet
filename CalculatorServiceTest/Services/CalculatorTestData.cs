// CalculatorTestData.cs
// Copyright © 2018-2021 Joel Mussman. All rights reserved.
//

using System;
using System.Collections.Generic;

namespace WonderfulWidgets.Services {

    public class CalculatorTestData {

        public static IEnumerable<object[]> LowRightNumbers() {

            return new List<object[]> {

                new object[] { 1, 0 },
                new object[] { 1, -1 }
            };
        }
    }
}
