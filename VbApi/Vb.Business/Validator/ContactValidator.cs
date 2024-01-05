using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb.Schema;

public class CreateContactValidator : AbstractValidator<ContactRequest>
{
    public CreateContactValidator()
    {
        RuleFor(x => x.ContactType).NotEmpty().MaximumLength(30).WithName("Contact type");
        RuleFor(x => x.CustomerId).NotEmpty().WithName("Customer Id needed");
        RuleFor(x => x.Information).NotEmpty().MinimumLength(10).MaximumLength(100).WithMessage("Information about contact");
        RuleFor(x => x.IsDefault).NotEmpty();
    }
}
