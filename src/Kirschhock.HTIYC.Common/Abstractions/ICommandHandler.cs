using MediatR;

namespace Kirschhock.HTIYC.Common.Abstractions
{
    public interface ICommandHandler<TCommand>: INotificationHandler<TCommand>
        where TCommand: ICommand { }
}