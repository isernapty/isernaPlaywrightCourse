public class RateCalculator
{
    public interface IDateTimeProvider
    {
        DayOfWeek DayOfWeek();
    }
    public decimal GetPayRate(decimal baseRate, IDateTimeProvider dateTimeProvider)
    {
        return dateTimeProvider.DayOfWeek() ==
               DayOfWeek.Sunday ?
               baseRate * 1.25m :
               baseRate;
    }
}
