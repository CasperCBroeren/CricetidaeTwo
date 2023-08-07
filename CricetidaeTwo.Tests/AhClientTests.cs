using CricetidaeTwo.Infrastructure;
using Shouldly;
using Xunit.Abstractions;

namespace CricetidaeTwo.Tests
{
    public class AhClientTests
    {
        private readonly ITestOutputHelper output;

        public AhClientTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task GetBonusDataOfTheWeek_ShouldHaveMoreThan100Records()
        {
            // Arange
            using (var httpClient = new HttpClient())
            {
                var client = new AhService(httpClient);

                // Act
                var items = await client.RetrieveBonusInfo(DateOnly.FromDateTime(DateTime.Now.AddDays(-7)), DateOnly.FromDateTime(DateTime.Now));

                // Assert
                items.Count.ShouldBeGreaterThan(100);
                var maxBonus = items.MaxBy(x => x.DeltaPrice);
                output.WriteLine($"Max bonus is {maxBonus.Name} with {maxBonus.OriginalPrice} now for {maxBonus.Price}\n{maxBonus.Products.First().Link}");

            }
        }


        [Fact]
        public async Task GetBonusDataOfFirstWeekofYear_ShouldHaveMoreThan5Records()
        {
            // Arange
            using (var httpClient = new HttpClient())
            {
                var client = new AhService(httpClient);

                // Act
                var firstDayOfYear = new DateOnly(DateTime.Now.Year, 1, 7);
                var items = await client.RetrieveBonusInfo(firstDayOfYear.AddDays(-7), firstDayOfYear);

                // Assert
                items.Count.ShouldBeGreaterThan(5);
            }
        }
    }
}