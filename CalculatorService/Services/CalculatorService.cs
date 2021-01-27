// CalculatorService.cs
// Copyright © 2018-2021 Joel Mussman. All rights reserved.
//
// See the ICalculator interface for the requirements of how the Calculator works.
//

using System;
using System.Threading.Tasks;

namespace WonderfulWidgets.Services {

    public class CalculatorService : ICalculator {

        public virtual double Add(double x, double y) {

            ValidateArguments(x, y);

            return x + y;
        }

        public virtual double Subtract(double x, double y) {

            ValidateArguments(x, y);

            return x - y;
        }

        public virtual double Multiply(double x, double y) {

            ValidateArguments(x, y);

            return x * y;
        }

        public virtual double Divide(double x, double y) {

            ValidateArguments(x, y);

            return x / y;
        }

        public virtual int Modulus(double x, double y) {

            ValidateArguments(x, y);

            return (int)(x % y);
        }

        private void ValidateArguments(double x, double y) {

            if (x < 1 || x > 1000 || y < 1 || y > 1000) {

                throw new ArgumentOutOfRangeException();
            }
        }

        // Just for fun with Moq.

        public virtual async Task<double> AddAsync(double x, double y) {

            ValidateArguments(x, y);

            return await Task.Run(() => x + y);
        }

        public DateTime CantMoqThis(DateTime x, TimeSpan y) {

            return x + y;
        }

        public virtual int Radix { get; set; }

        public virtual double Add(ref double x, ref double y) {

            ValidateArguments(x, y);

            return x + y;
        }

        public virtual string Add(string x, string y) {

            return x + y;
        }
    }
}
