using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Query
{
    public class EftTransactionQueryHandler :
    IRequestHandler<GetAllEftTransactionQuery, ApiResponse<List<EftTransactionResponse>>>,
    IRequestHandler<GetEftTransactionByIdQuery, ApiResponse<EftTransactionResponse>>,
    IRequestHandler<GetEftTransactionByParameterQuery, ApiResponse<List<EftTransactionResponse>>>
    {
        private readonly VbDbContext dbContext;
        private readonly IMapper mapper;

        public EftTransactionQueryHandler(VbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<EftTransactionResponse>>> Handle(GetAllEftTransactionQuery request, CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<EftTransaction>()
                .ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<EftTransaction>, List<EftTransactionResponse>>(list);
            return new ApiResponse<List<EftTransactionResponse>>(mappedList);
        }

        public async Task<ApiResponse<EftTransactionResponse>> Handle(GetEftTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<EftTransaction>()
                .FirstOrDefaultAsync(x => x.ReferenceNumber == request.Id.ToString(), cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<EftTransactionResponse>("Reference Number Not Found");
            }

            var mapped = mapper.Map<EftTransaction, EftTransactionResponse>(entity);
            return new ApiResponse<EftTransactionResponse>(mapped);
        }

        public async Task<ApiResponse<List<EftTransactionResponse>>> Handle(GetEftTransactionByParameterQuery request, CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<EftTransaction>()
                .Where(x =>
                x.SenderIban.ToUpper().Contains(request.SenderIban.ToUpper()) ||
                x.SenderAccount.ToUpper().Contains(request.SenderAccount.ToUpper()) ||
                x.SenderName.ToUpper().Contains(request.SenderName.ToUpper()) ||
                x.ReferenceNumber.ToUpper().Contains(request.ReferenceNumber.ToUpper()) ||
                x.Description.ToUpper().Contains(request.Description.ToUpper()))
                .ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<EftTransaction>, List<EftTransactionResponse>>(list);
            return new ApiResponse<List<EftTransactionResponse>>(mappedList);
        }
    }
}
