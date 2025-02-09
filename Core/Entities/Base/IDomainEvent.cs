using MediatR;

namespace Core.Entities.Base;

public interface IDomainEvent : INotification
{
    bool IsPostEvent { get; }
}
