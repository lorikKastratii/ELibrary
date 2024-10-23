using ELibrary.Orders.Application.Requests;
using FluentValidation;

namespace ELibrary.Orders.PublicApi.Validators
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("Request cannot be null");

            RuleFor(x => x.OrderItems)
                .NotNull()
                .NotEmpty()
                .WithMessage("Order items cannot be null");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("UserId must be greater than 0");
            
            RuleFor(x => x.ShippingCity)
                .NotNull()
                .NotEmpty()
                .WithMessage("ShippingCity must not be null or empty");

            RuleFor(x => x.ShippingAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("ShippingAddress must not be null or empty");

            RuleFor(x => x.ShippingPostalCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("ShippingPostalCode must not be null or empty");

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0)
                .WithMessage("TotalAmount must be greater than 0");
        }
    }
}
