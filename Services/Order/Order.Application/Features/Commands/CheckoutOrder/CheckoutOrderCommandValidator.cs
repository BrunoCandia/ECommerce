using FluentValidation;

namespace Order.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty()
                .WithMessage("UserName is required.")
                .MaximumLength(100)
                .WithMessage("UserName must not exceed 100 characters.");

            RuleFor(p => p.FirstName)
                .NotEmpty()
                .WithMessage("FirstName is required.")
                .MaximumLength(100)
                .WithMessage("FirstName must not exceed 100 characters.");

            RuleFor(p => p.LastName)
                .NotEmpty()
                .WithMessage("LastName is required.")
                .MaximumLength(100)
                .WithMessage("LastName must not exceed 100 characters.");

            RuleFor(p => p.EmailAddress)
                .NotEmpty()
                .WithMessage("EmailAddress is required.")
                .EmailAddress()
                .WithMessage("EmailAddress must be a valid email format.")
                .MaximumLength(100)
                .WithMessage("EmailAddress must not exceed 100 characters.");

            RuleFor(p => p.AddressLine)
                .NotEmpty()
                .WithMessage("AddressLine is required.")
                .MaximumLength(100)
                .WithMessage("AddressLine must not exceed 100 characters.");

            RuleFor(p => p.Country)
                .NotEmpty()
                .WithMessage("Country is required.")
                .MaximumLength(100)
                .WithMessage("Country must not exceed 100 characters.");

            RuleFor(p => p.State)
                .NotEmpty()
                .WithMessage("State is required.")
                .MaximumLength(100)
                .WithMessage("State must not exceed 100 characters.");

            RuleFor(p => p.ZipCode)
                .NotEmpty()
                .WithMessage("ZipCode is required.")
                .MaximumLength(20)
                .WithMessage("ZipCode must not exceed 20 characters.");

            RuleFor(p => p.TotalPrice)
                .NotEmpty()
                .WithMessage("TotalPrice is required.")
                .GreaterThan(0)
                .WithMessage("TotalPrice must be greater than zero.");

            RuleFor(p => p.CardName)
                .NotEmpty()
                .WithMessage("CardName is required.")
                .MaximumLength(100)
                .WithMessage("CardName must not exceed 100 characters.");

            RuleFor(p => p.CardNumber)
                .NotEmpty()
                .WithMessage("CardNumber is required.")
                .CreditCard()
                .WithMessage("CardNumber must be a valid credit card number.")
                .MaximumLength(20)
                .WithMessage("CardNumber must not exceed 20 characters.");

            // TODO: Review the expiration date validation logic
            RuleFor(p => p.Expiration)
                .NotEmpty()
                .WithMessage("Expiration is required.")
                .Matches(@"^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$")
                .WithMessage("Expiration must be in MM/YY or MM/YYYY format.")
                .MaximumLength(10)
                .WithMessage("Expiration must not exceed 10 characters.");

            RuleFor(p => p.Cvv)
                .NotEmpty()
                .WithMessage("Cvv is required.")
                .Matches(@"^\d{3,4}$")
                .WithMessage("Cvv must be a 3 or 4 digit number.")
                .MaximumLength(5)
                .WithMessage("Cvv must not exceed 5 characters.");
        }
    }
}
