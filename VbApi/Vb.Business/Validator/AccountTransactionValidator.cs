using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb.Schema;

public class CreateAccountTransactionValidator : AbstractValidator<AccountTransactionRequest>
{
    public CreateAccountTransactionValidator()
    {
        RuleFor(x => x.ReferenceNumber).NotEmpty().Length(10).WithMessage("Reference Number length must be 10");
        RuleFor(x => x.TransferType).NotEmpty().WithName("Type of transaction");
        RuleFor(x => x.AccountId).NotEmpty().WithMessage("Account Id can not be empty");
        RuleFor(x => x.Amount).NotEmpty().WithName("Amount of transaction");
        RuleFor(x => x.TransactionDate).NotEmpty();
        RuleFor(x => x.Description).NotEmpty().WithName("Description of transaction");

    }

}