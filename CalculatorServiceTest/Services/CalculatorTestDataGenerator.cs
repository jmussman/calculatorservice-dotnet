// CalculatorTestDataGenerator.cs
// Copyright © 2018-2021 Joel Mussman. All rights reserved.
//

using System;
using System.Collections;
using System.Collections.Generic;

namespace WonderfulWidgets.Services {

    public class CalculatorTestDataGenerator : IEnumerable<object[]> {

        public IEnumerator<object[]> GetEnumerator() {

            yield return new object[] { 1, 1001 };
            yield return new object[] { 1, 1002 };  // Unnecessary, just for show
            yield return new object[] { 1, 1003 };  // Unnecessary, just for show
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
