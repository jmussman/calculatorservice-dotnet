// ICalculator.cs
// Copyright © 2018-2021 Joel Mussman. All rights reserved.
//
// This interface defines a calculator that is extremely simple in nature, but
// the simplicity makes it easy to follow the unit testing and Moq.
//
// The requirements of the calculator are:
// - There must be methods Add, Subtract, Multiply, Divide, and Modulous which take doubles.
// - The parameters must be in the range from 1 to 1000 inclusive.
// - The Radix property, Add with "ref", and Add with "string" exist to demonstrate Moq.

using System;

namespace WonderfulWidgets.Services {

    public interface ICalculator {

        double Add(double x, double y);
        double Subtract(double x, double y);
        double Multiply(double x, double y);
        double Divide(double x, double y);
        int Modulus(double x, double y);

        // Just for fun with Moq.

        int Radix { get; set; }
        double Add(ref double x, ref double y);
        string Add(string x, string y);
    }
}
