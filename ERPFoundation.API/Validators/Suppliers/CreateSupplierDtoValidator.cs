using ERPFoundation.API.DTOs.Suppliers;
using FluentValidation;

namespace ERPFoundation.API.Validators.Suppliers;

public class CreateSupplierDtoValidator : AbstractValidator<CreateSupplierDto>
{
    public CreateSupplierDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(120).WithMessage("Name must not exceed 120 characters.");

        RuleFor(x => x.TaxId)
            .NotEmpty().WithMessage("Tax ID is required.")
            .Length(14).WithMessage("Tax ID must be exactly 14 characters long.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");
    }
}