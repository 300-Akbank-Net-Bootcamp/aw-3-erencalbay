using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Cqrs
{
    public record CreateAccountCommand(AccountRequest Model) : IRequest<ApiResponse<AccountResponse>>;
    public record UpdateAccountCommand(int Id, AccountRequest Model) : IRequest<ApiResponse>;
    public record DeleteAccountCommand(int Id) : IRequest<ApiResponse>;

    public record GetAllAccountQuery() : IRequest<ApiResponse<List<AccountResponse>>>;
    public record GetAccountByIdQuery(int Id) : IRequest<ApiResponse<AccountResponse>>;
    public record GetAccountByParameterQuery(string currencyType, string accountName) : IRequest<ApiResponse<List<AccountResponse>>>;
}
