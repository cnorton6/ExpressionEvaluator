using System;

namespace ExpressionEvaluator
{
    public class ExpressionEvaluator
    {
        public static void Main(string[] args)
        {
            var evaluator = new Evaluator();
            var running = true;

            while (running)
            {
                Console.WriteLine("Enter an arithmetic expression or 'N' to exit: ");

                var expression = Console.ReadLine().ToLower();

                if (expression.Equals("n"))
                {
                    break;
                }

                try
                {
                    var arithmeticResult = evaluator.Evaluate(expression);

                    Console.WriteLine();
                    Console.WriteLine($"The result is: {arithmeticResult}");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
                catch (Exception)
                {
                    Console.WriteLine("An error occured, please try another expression.");
                }

                Console.WriteLine();
            }
        }
    }
}
