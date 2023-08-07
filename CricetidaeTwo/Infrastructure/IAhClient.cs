using CricetidaeTwo.Domain;

namespace CricetidaeTwo.Infrastructure
{
    public interface IAhService
    {        
        Task<IReadOnlyList<BonusProduct>> RetrieveBonusInfo(DateOnly periodStart, DateOnly periodEnd, CancellationToken cancellationToken = default);
    }
}