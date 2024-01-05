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
    public record CreateAccountTransactionCommand(AccountTransactionRequest Model) : IRequest<ApiResponse<AccountTransactionResponse>>;
    public record UpdateAccountTransactionCommand(int Id, AccountTransactionRequest Model) : IRequest<ApiResponse>;
    public record DeleteAccountTransactionCommand(int Id) : IRequest<ApiResponse>;

    public record GetAllAccountTransactionQuery() : IRequest<ApiResponse<List<AccountTransactionResponse>>>;
    public record GetAccountTransactionByIdQuery(int Id) : IRequest<ApiResponse<AccountTransactionResponse>>;
    public record GetAccountTransactionByParameterQuery(string TransferType, string Description, string ReferenceNumber) : IRequest<ApiResponse<List<AccountTransactionResponse>>>;
}
