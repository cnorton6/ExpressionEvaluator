namespace ExpressionEvaluator.Operations
{
    class Addition : Operation
    {
        public override char Operator()
        {
            return '+';
        }

        public override decimal Evaluate(decimal left, decimal right)
        {
            return left + right;
        }
    }
}
