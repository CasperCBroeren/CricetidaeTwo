namespace CricetidaeTwo.Domain
{
    public record BonusProduct(string Id,
                               string Name,
                               Infrastructure.DTO.Discount DiscountLabel,
                               double Price,
                               double OriginalPrice,
                               int ProductCount,
                               IReadOnlyList<Infrastructure.DTO.Product> Products)
    {
        public double DeltaPrice => OriginalPrice - Price;
         
    }
}
