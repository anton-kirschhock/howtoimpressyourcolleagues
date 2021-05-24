
using MediatR;

namespace Kirschhock.HTIYC.Common.Abstractions
{
    public interface ICommand : INotification
    {
    }

    public interface ICommand<TResponse> : IRequest<TResponse>
    {

    }
}
