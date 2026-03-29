using Ardalis.Result;
using FluentValidation;

namespace ApplicationCore.Commands;

public abstract class AbstractCommandHandler<TCommand, TResult>(
    IValidator<TCommand>? validator = null
) : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    public async Task<Result<TResult>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        if (validator is null)
        {
            return await HandleValidated(request, cancellationToken);
        }

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
        {
            return await HandleValidated(request, cancellationToken);
        }

        var errors = validationResult
            .Errors.Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))
            .ToList();

        return Result<TResult>.Invalid(errors);

    }

    protected abstract Task<Result<TResult>> HandleValidated(
        TCommand request,
        CancellationToken cancellationToken
    );
}