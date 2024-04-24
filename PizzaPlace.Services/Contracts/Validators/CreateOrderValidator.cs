using FluentValidation;
using PizzaPlace.Services.Contracts.Requests;

namespace PizzaPlace.Services.Contracts.Validators;

public class CreateOrderValidator : AbstractValidator<CreateOrder>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CustomerName).NotEmpty();
        RuleFor(x => x.OrderLines).NotEmpty();
        RuleForEach(x => x.OrderLines)
            .Must(x => x.Quantity > 0)
            .WithMessage($"An order line must contain a {nameof(CreateOrder.OrderLine.Quantity)} greater than zero");
    }
}