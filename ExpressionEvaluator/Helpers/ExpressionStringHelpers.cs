using System;
using System.Collections.Generic;

namespace ExpressionEvaluator.Helpers
{
    public static class ExpressionStringHelpers
    {
        public static string RemoveSpaces(string expression)
        {
            return expression.Replace(" ", string.Empty);
        }

        public static bool VerifyBrackets(string expression)
        {
            var parenCount = 0;
            for (var i = 0; i < expression.Length; ++i)
            {
                if (expression[i] == '(')
                {
                    ++parenCount;
                }
                else if (expression[i] == ')')
                {
                    --parenCount;
                }
            }

            return parenCount == 0;
        }

        public static string SetOperatorPrecedence(string expression, IList<char> operators)
        {
            try
            {
                for (int i = 0; i < expression.Length; ++i)
                {
                    if (operators.Contains(expression[i]))
                    {
                        // Get Left side of sub expression
                        if (expression[i - 1] == ')')
                        {
                            var index = GetLeftClosingBracketIndex(expression.Substring(0, i));
                            expression = expression.Insert(index, "(");
                            ++i;
                        }
                        else
                        {
                            var index = GetLeftTermStartIndex(expression.Substring(0, i));
                            expression = expression.Insert(index, "(");
                            ++i;
                        }


                        // Get right side of sub expression
                        if (expression[i + 1] == '(')
                        {
                            var index = GetRightClosingBracketIndex(expression.Substring(i + 1));
                            expression = expression.Insert(index + i + 1, ")");
                        }
                        else
                        {
                            var index = GetRightTermStartIndex(expression.Substring(i + 1));
                            expression = expression.Insert(index + i + 1, ")");
                        }
                    }
                }

                return expression;
            }
            catch
            {
                throw new ArgumentException("Expression has invalid syntax");
            }
        }

        public static int GetRightClosingBracketIndex(string expression)
        {
            var parenCount = 0;
            for (int i = 0; i < expression.Length; ++i)
            {
                if (expression[i] == '(')
                {
                    ++parenCount;
                }
                else if (expression[i] == ')')
                {
                    --parenCount;
                }

                if (parenCount == 0)
                {
                    return i;
                }
            }

            throw new ArgumentException("Expression is missing a closing parenthesis");
        }

        public static int GetLeftClosingBracketIndex(string expression)
        {
            var parenCount = 0;
            for (int i = expression.Length - 1; i >= 0; --i)
            {
                if (expression[i] == ')')
                {
                    ++parenCount;
                }
                else if (expression[i] == '(')
                {
                    --parenCount;
                }

                if (parenCount == 0)
                {
                    return i;
                }
            }

            throw new ArgumentException("Expression is missing a closing parenthesis");
        }

        public static int GetLeftTermStartIndex(string expression)
        {
            for (int i = expression.Length - 1; i >= 0; --i)
            {
                if (!char.IsDigit(expression[i]) && expression[i] != '.')
                {
                    return i + 1;
                }
            }

            return 0;
        }

        public static int GetRightTermStartIndex(string expression)
        {
            for (var i = 0; i < expression.Length; ++i)
            {
                if (!char.IsDigit(expression[i]) && expression[i] != '.')
                {
                    return i;
                }
            }

            return expression.Length;
        }
    }
}
