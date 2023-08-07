namespace CricetidaeTwo.Infrastructure.DTO
{
    public record Input(bool PreviouslyBought, int Size, string Bonus, string BonusPeriodEndDate, string BonusPeriodStartDate);

    public record AhOperation(string OperationName, Variables Variables, string Query);

    public record Variables(string SegmentType, bool HideVariants, string PeriodStart, string PeriodEnd);
}
