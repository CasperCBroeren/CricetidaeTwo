namespace CricetidaeTwo.Infrastructure.DTO
{

    public record Availability(
         string StartDate,
         string EndDate,
         string Description,
         string Typename
    );

    public record BonusSegment(
         string Id,
         int? HqId,
         Availability Availability,
         Discount Discount,
         DiscountUnit DiscountUnit,
         IReadOnlyList<DiscountLabel> DiscountLabels,
         string PromotionType,
         string Subtitle,
         string Title,
         string Type,
         string Description,
         bool StoreOnly,
         string Typename,
         string ActivationStatus,
         string Category,
         IReadOnlyList<Image> Images,
         Price Price,
         IReadOnlyList<Product> Products,
         int ProductCount,
         string SalesUnitSize,
         object SmartLabel,
         bool Spotlight
    );

    public record Product(
        int Id,
        string Title,
        string DescriptionFull)
    {
        public string Link => $"https://www.ah.nl/producten/product/wi{Id}/{Title}";
    }

    public record Data(
         IReadOnlyList<BonusSegment> BonusSegments,
         ProductSearch ProductSearch
    );

    public record Discount(
         int? Type,
         string Title,
         string Description,
         IReadOnlyList<string> ExtraDescriptions,
         string Theme,
         string Typename
    );

    public record DiscountLabel(
         string Code,
         string DefaultDescription,
         double? Price,
         object ActualCount,
         int? Count,
         int? FreeCount,
         double? Amount,
         int? Percentage,
         string DeliveryType,
         string Unit,
         string Typename
    );

    public record DiscountUnit(
         int Count,
         string Typename
    );

    public record Image(
         string Url,
         string Title,
         int Width,
         int Height,
         string Typename
    );

    public record Now(
         double Amount,
         string Formatted,
         string Typename
    );

    public record Page(
         int TotalElements,
         string Typename
    );

    public record Price(
         object Label,
         Now Now,
         Was Was,
         string Typename
    );

    public record ProductSearch(
         Page Page,
         string Typename
    );

    public record AhDataFrame(
        Data Data
    );

    public record Was(
         double Amount,
         string Formatted,
         string Typename
    );


}
