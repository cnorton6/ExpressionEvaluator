namespace ExpressionEvaluator.Evaluatables
{
    public class Term : IEvaluatable
    {
        private decimal _value;

        public Term(decimal value)
        {
            _value = value;
        }

        public decimal Evaluate()
        {
            return _value;
        }
    }
}
