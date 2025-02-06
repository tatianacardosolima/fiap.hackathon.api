namespace Fiap.Hackathon.Common.Shared.Interfaces
{
    public interface IRequest
    {
        Guid Id { get; set; }
    }
    public interface INewRequest: IRequest
    {

    }
    
}
