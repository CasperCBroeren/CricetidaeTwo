using CricetidaeTwo.Domain;
using CricetidaeTwo.Infrastructure.DTO;
using System.Net.Http.Json;

namespace CricetidaeTwo.Infrastructure
{
    public class AhService : IAhService
    {
        private const string endPoint = "gql";
        private const string dateFormat = "yyyy-MM-dd";
        private HttpClient httpClient;

        public AhService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://www.ah.nl/");
            this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("content-type", "application/json");
            this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "*/*");
            this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("client-name", "ah-bonus");
            this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("client-version", "3.380.0");
            this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("content-type", "application/json");

        }

        public async Task<IReadOnlyList<BonusProduct>> RetrieveBonusInfo(DateOnly periodStart, DateOnly periodEnd, CancellationToken cancellationToken = default)
        {
            var requestdata = new List<AhOperation>()
            {
                new AhOperation(
                    "bonusSegments",
                    new Variables("NEGATE_PREMIUM", false, periodStart.ToString(dateFormat), periodEnd.ToString(dateFormat)),
                    @"
query bonusSegments( $segmentType: BonusSegmentType, $hideVariants: Boolean, $periodStart: String, $periodEnd: String) {
bonusSegments(
            segmentType: $segmentType
            hideVariants: $hideVariants
            periodStart: $periodStart
            periodEnd: $periodEnd
        ) 
    {
        ...Segment
        __typename
    }
}
fragment Segment on BonusSegment {
        ...BaseSegment
        activationStatus
        category
        description
        discountUnit {
            count
            __typename
        }
  images {
    url
    title
    width
    height
    __typename
  }
  price {
    label
    now {
      amount
      formatted
      __typename
    }
    was {
      amount
      formatted
      __typename
    }
    __typename
  }
  products {
    id
    title
    __typename
  }
  productCount
  promotionType
  salesUnitSize 
  spotlight
  type
  __typename
}

fragment BaseSegment on BonusSegment {
  id
  hqId
  availability {
    startDate
    endDate
    description
    __typename
  }
  discount {
    type
    title
    description
    extraDescriptions
    theme
    __typename
  }
  discountUnit {
    count
    __typename
  }
  discountLabels {
    code
    defaultDescription
    price
    actualCount
    count
    freeCount
    amount
    percentage
    deliveryType
    unit
    __typename
  }
  promotionType
  subtitle
  title
  type
  description
  storeOnly
  __typename
}")
            };
            var httpResponse = await httpClient.PostAsJsonAsync(endPoint, requestdata, cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var dataFrames = await httpResponse.Content.ReadFromJsonAsync<List<AhDataFrame>>();
            return dataFrames?
                .SelectMany(x =>
                    x.Data.BonusSegments.Select(y => new BonusProduct(y.Id,
                                                                      y.Title,
                                                                      y.Discount,
                                                                      y.Price?.Now?.Amount ?? -1,
                                                                      y.Price?.Was?.Amount ?? -1,
                                                                      y.ProductCount,
                                                                      y.Products)))
                .ToList();
        }
    }
}