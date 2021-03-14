namespace ExpressionEvaluator.Operations
{
    public abstract class Operation
    {
        public abstract char Operator();
        public abstract decimal Evaluate(decimal left, decimal right);
    }
}
