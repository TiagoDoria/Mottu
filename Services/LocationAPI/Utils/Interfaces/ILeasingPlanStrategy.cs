namespace LocationAPI.Utils.Interfaces
{
    public interface ILeasingPlanStrategy
    {
        public decimal CalculateLeasingValue(DateTime startDate, DateTime returnDate);
    }
}
