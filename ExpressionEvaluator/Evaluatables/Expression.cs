using ExpressionEvaluator.Operations;

namespace ExpressionEvaluator.Evaluatables
{
    public class Expression : IEvaluatable
    {
        private Operation _operation;
        private IEvaluatable _left;
        private IEvaluatable _right;

        public Expression(Operation operation, IEvaluatable left, IEvaluatable right)
        {
            _operation = operation;
            _left = left;
            _right = right;
        }

        public decimal Evaluate()
        {
            var left = _left.Evaluate();
            var right = _right.Evaluate();

            return _operation.Evaluate(left, right);
        }
    }
}
