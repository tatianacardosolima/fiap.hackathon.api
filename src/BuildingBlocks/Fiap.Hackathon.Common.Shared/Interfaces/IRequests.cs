namespace Fiap.Hackathon.Common.Shared.Interfaces
{
    public interface IRequest
    {

    }
    public interface INewRequest: IRequest
    {

    }
    public interface IUpdRequest : IRequest
    {
        Guid Id { get; set; }

    }
}
