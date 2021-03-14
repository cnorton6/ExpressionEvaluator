using NUnit.Framework;
using System;

namespace ExpressionEvaluator.Test
{
    public class EvaluatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("1", ExpectedResult = 1)]
        [TestCase("1 + 1", ExpectedResult = 2)]
        [TestCase("1 + 2 + 3", ExpectedResult = 6)]
        [TestCase("2 * 3 + 1", ExpectedResult = 7)]
        [TestCase("1 + 2 * 3", ExpectedResult = 7)]
        [TestCase("1 + 2 * (3 + 4)", ExpectedResult = 15)]
        [TestCase("1 + (2 * 3) + 4", ExpectedResult = 11)]
        [TestCase("(6 + 5) * (8 + 2)", ExpectedResult = 110)]
        public decimal Evaluate_ValidArithmeticString_ResultReturned(string expression)
        {
            // Arrange
            var evaluator = new Evaluator();

            // Act
            var result = evaluator.Evaluate(expression);

            // Assert
            return result;
        }

        [TestCase("(1")]
        [TestCase("1)")]
        [TestCase("(1))")]
        [TestCase("((1)")]
        [TestCase(")1")]
        [TestCase("1(")]
        [TestCase(")1(")]
        public void Evaluate_InvalidParenthesis_ArgumentExceptionThrown(string expression)
        {
            // Arrange
            var evaluator = new Evaluator();

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => evaluator.Evaluate(expression));
        }

        [TestCase("1*")]
        [TestCase("*1")]
        [TestCase("1+2+")]
        [TestCase("+1+2")]
        [TestCase("1++2")]
        [TestCase("1**2")]
        public void Evaluate_InvalidArithmeticExpression_ExceptionThrown(string expression)
        {
            // Arrange
            var evaluator = new Evaluator();

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => evaluator.Evaluate(expression), "Expression has invalid syntax");
        }
    }
}