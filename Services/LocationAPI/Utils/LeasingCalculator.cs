using LocationAPI.Utils.Interfaces;

namespace LocationAPI.Utils
{
    public class LeasingCalculator
    {
        private ILeasingPlanStrategy _strategy;

        public LeasingCalculator(ILeasingPlanStrategy strategy)
        {
            _strategy = strategy;
        }

        public decimal CalculateLeasingValue(DateTime startDate, DateTime returnDate)
        {
            return _strategy.CalculateLeasingValue(startDate, returnDate);
        }
    }
}
