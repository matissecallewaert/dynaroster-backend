using Core.Entities.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task<int> DispatchDomainEvents(
            this IMediator mediator,
            DbContext dbContext,
            ILogger<WorkForceDbContext> logger,
            bool isPostEvent
        )
        {
            var domainEntities = dbContext
                .ChangeTracker.Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .Where(e => e.IsPostEvent == isPostEvent)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            if (!domainEvents.Any())
            {
                return 0;
            }

            logger.LogInformation("Dispatching {Count} domain events.", domainEvents.Count);
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }

            return domainEvents.Count;
        }
    }
}
