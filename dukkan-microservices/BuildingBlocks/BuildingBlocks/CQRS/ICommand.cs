namespace BuildingBlocks.CQRS;
using MediatR;


public interface ICommand : ICommand<Unit>;

public interface ICommand<out TResult> : IRequest<TResult>;