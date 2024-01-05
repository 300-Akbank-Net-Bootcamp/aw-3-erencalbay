using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb.Schema;

public class CreateEftTransactionValidator : AbstractValidator<EftTransactionRequest>
{
    public CreateEftTransactionValidator()
    {
        RuleFor(x => x.ReferenceNumber).NotEmpty().Length(10).WithMessage("Reference Number length must be 10");
        RuleFor(x => x.AccountId).NotEmpty().WithMessage("Account Id can not be empty");
        RuleFor(x => x.Amount).NotEmpty().WithName("Amount of transaction");
        RuleFor(x => x.TransactionDate).NotEmpty();
        RuleFor(x => x.Description).NotEmpty().WithName("Description of transaction");
        RuleFor(x => x.SenderAccount).NotEmpty().WithName("Sender Account");
        RuleFor(x => x.SenderIban).NotEmpty().Length(26).WithName("Sender IBAN needed 26 digit");
        RuleFor(x => x.SenderName).NotEmpty().MinimumLength(5).WithMessage("Sender name needed.");  
    }
}