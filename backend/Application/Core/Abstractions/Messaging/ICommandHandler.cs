using Domain.Core.Primitives;

namespace Application.Core.Abstractions.Messaging;

/// <summary>
/// Defines a handler for commands that do not return a value.
/// </summary>
/// <typeparam name="TCommand">The type of command to handle.</typeparam>
public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    /// <summary>
    /// Handles the specified command.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>Task containing the result.</returns>
    Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// Defines a handler for commands that return a value.
/// </summary>
/// <typeparam name="TCommand">The type of command to handle.</typeparam>
/// <typeparam name="TResponse">The type of the response returned by the command.</typeparam>
public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand
{
    /// <summary>
    /// Handles the specified command.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>Task containing the result.</returns>
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}
