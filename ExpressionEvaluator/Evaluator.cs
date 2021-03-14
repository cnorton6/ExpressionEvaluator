using ExpressionEvaluator.Evaluatables;
using ExpressionEvaluator.Helpers;
using ExpressionEvaluator.Operations;
using System;
using System.Collections.Generic;

namespace ExpressionEvaluator
{
    public class Evaluator
    {
        private readonly Operation MultiplicationOperator = new Multiplication();
        private readonly Operation AdditionOperator = new Addition();

        public decimal Evaluate(string expression)
        {
            if (!ExpressionStringHelpers.VerifyBrackets(expression))
            {
                throw new ArgumentException("Expression has mismatched parenthesis");
            }

            var preparedExpression = PrepareExpression(expression);

            var expressionTree = GenerateEvaluatableExpression(preparedExpression);

            return expressionTree.Evaluate();
        }

        private string PrepareExpression(string expression)
        {
            expression = ExpressionStringHelpers.RemoveSpaces(expression);

            expression = ExpressionStringHelpers.SetOperatorPrecedence(expression, new List<char> { MultiplicationOperator.Operator() });

            return expression;
        }

        private IEvaluatable GenerateEvaluatableExpression(string expression)
        {
            try
            {
                for (var i = 0; i < expression.Length; ++i)
                {
                    var currentChar = expression[i];
                    if (char.IsDigit(currentChar) || currentChar == '.')
                    {
                        continue;
                    }
                    else
                    {
                        // Reached an operator
                        if (currentChar == '(')
                        {
                            var subExpression = GenerateEvaluatableExpression(expression.Substring(i + 1));

                            var closingBracketIndex = ExpressionStringHelpers.GetRightClosingBracketIndex(expression);

                            if (expression.Length > closingBracketIndex + 1)
                            {
                                var nextChar = expression[closingBracketIndex + 1];
                                var subResult = subExpression.Evaluate();
                                if (char.IsDigit(nextChar) || nextChar == '.')
                                {
                                    // TODO This can be assumed to be multiplication but needs to be dealt with before evaluation begins
                                    throw new ArgumentException("Expression is missing an operator between term and parenthesis");
                                }
                                else
                                {
                                    // next character is an operation
                                    return GenerateEvaluatableExpression(subResult + expression.Substring(closingBracketIndex + 1));
                                }
                            }

                            return subExpression;
                        }
                        else if (currentChar == ')')
                        {
                            return GenerateEvaluatableExpression(expression.Substring(0, i));
                        }

                        var right = GenerateEvaluatableExpression(expression.Substring(i + 1));
                        var left = GenerateEvaluatableExpression(expression.Substring(0, i));

                        if (currentChar == MultiplicationOperator.Operator())
                        {
                            // Multiplication
                            return new Expression(MultiplicationOperator, left, right);
                        }

                        if (currentChar == AdditionOperator.Operator())
                        {
                            // Addition
                            return new Expression(AdditionOperator, left, right);
                        }
                    }
                }

                // The remaining expression is a number that can be evaluated
                var value = decimal.Parse(expression);
                return new Term(value);
            }
            catch(ArgumentException e)
            {
                throw e;
            }
            catch
            {
                throw new ArgumentException("Expression has invalid syntax");
            }
        }
    }
}
