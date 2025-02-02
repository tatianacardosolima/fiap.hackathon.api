namespace Fiap.Hackathon.Common.Shared.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
        DateTime? ModifiedAt { get; }
    }
}
