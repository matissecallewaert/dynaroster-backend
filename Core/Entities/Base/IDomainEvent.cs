namespace WorkforcePlanner.Core.Entities.Base;

using MediatR;

public interface IDomainEvent : INotification
{
    bool IsPostEvent { get; }
}
