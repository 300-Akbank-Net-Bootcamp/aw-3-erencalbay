using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb.Schema;

public class CreateAccountValidator : AbstractValidator<AccountRequest>
{
	public CreateAccountValidator()
	{
		RuleFor(x => x.Name).NotEmpty().MinimumLength(5).MaximumLength(100).WithName("Name of the account");
		RuleFor(x => x.IBAN).NotEmpty().MinimumLength(26).MaximumLength(26).WithMessage("Account IBAN with 26 digit");
		RuleFor(x => x.Balance).NotEmpty().WithName("Customer balance");
		RuleFor(x => x.CurrencyType).NotEmpty().MinimumLength(10).WithName("Account type");
		RuleFor(x => x.AccountNumber).NotEmpty();
		RuleFor(x => x.CustomerId).NotEmpty().WithName("Customer primary id").WithMessage("customer id is needed");
	}
}
