using System;
using Xunit;
using MyConsoleApp;
using System.Collections.Generic;
using System.Collections;
using Xunit.Abstractions;

namespace MyTest
{
    public class CalculatorTest
    {
        private readonly Calculator _calculator = new();

        private readonly ITestOutputHelper _output;

        public CalculatorTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void AddTestLog()
        {
            Assert.Equal(3, _calculator.Add(1,2));
            _output.WriteLine("This is Log");
            Console.WriteLine("Not Disp Log");
        }

        [Fact(DisplayName = "First Test")]
        public void AddTestFact()
        {
            Assert.Equal(3, _calculator.Add(1,2));
        }

        [Theory]
        [InlineData(1,2,3)]
        [InlineData(2,3,5)]
        public void AddTestTheory(int input1, int input2, int expected)
        {
            int actual = _calculator.Add(input1, input2);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> AddTestMemberData =>
            new List<object[]> 
            {
                new object[] { 1,2,3 },
                new object[] { 5,7,12 }
            };

        [Theory]
        [MemberData(nameof(AddTestMemberData))]
        public void AddTestMember(int a, int b, int expected)
        {
            Assert.Equal(expected, _calculator.Add(a, b));
        }

        [Theory]
        [ClassData(typeof(AddTestData))]
        public void AddTestClassData(int a, int b, int expected)
        {
            int result = _calculator.Add(a, b);
            Assert.Equal(expected, result);
        }
    }

    public class AddTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 10, 20, 30 };
            yield return new object[] { -5, 5, 0 };
            yield return new object[] { 100, 200, 300 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
