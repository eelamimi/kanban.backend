namespace Backend.Application.Abstractions;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
    new Task Handle(TCommand command, CancellationToken token);
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    new Task<TResponse> Handle(TCommand command, CancellationToken token);
}