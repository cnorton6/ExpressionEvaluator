namespace ExpressionEvaluator.Operations
{
    class Multiplication : Operation
    {
        public override char Operator()
        {
            return '*';
        }

        public override decimal Evaluate(decimal left, decimal right)
        {
            return left * right;
        }
    }
}
