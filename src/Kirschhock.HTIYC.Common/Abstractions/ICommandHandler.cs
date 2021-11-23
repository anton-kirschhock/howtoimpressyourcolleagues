using MediatR;

namespace Kirschhock.HTIYC.Common.Abstractions
{
    public interface ICommandHandler<TCommand> : INotificationHandler<TCommand>
        where TCommand : ICommand
    { }

    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    { }
}