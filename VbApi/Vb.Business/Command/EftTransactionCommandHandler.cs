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

namespace Vb.Business.Command
{
    public class EftTransactionCommandHandler :
        IRequestHandler<CreateEftTransactionCommand, ApiResponse<EftTransactionResponse>>,
        IRequestHandler<UpdateEftTransactionCommand, ApiResponse>,
        IRequestHandler<DeleteEftTransactionCommand, ApiResponse>

    {
        private readonly VbDbContext dbContext;
        private readonly IMapper mapper;

        public EftTransactionCommandHandler(VbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<EftTransactionResponse>> Handle(CreateEftTransactionCommand request, CancellationToken cancellationToken)
        {
            var checkIdentity = await dbContext.Set<EftTransaction>().Where(x =>
                x.ReferenceNumber == request.Model.ReferenceNumber).FirstOrDefaultAsync(cancellationToken);

            if (checkIdentity != null)
            {
                return new ApiResponse<EftTransactionResponse>($"{request.Model.ReferenceNumber} is used by another ref.");
            }

            var entity = mapper.Map<EftTransactionRequest, EftTransaction>(request.Model);
            entity.ReferenceNumber = new Random().Next(100000000, 999999999).ToString();

            var entityResult = await dbContext.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var mapped = mapper.Map<EftTransaction, EftTransactionResponse>(entityResult.Entity);
            return new ApiResponse<EftTransactionResponse>(mapped);
        }

        public async Task<ApiResponse> Handle(UpdateEftTransactionCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<EftTransaction>().Where(x =>
                x.ReferenceNumber == request.Id.ToString()).FirstOrDefaultAsync(cancellationToken);

            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }
            
            fromdb.SenderAccount = request.Model.SenderAccount;
            fromdb.SenderName = request.Model.SenderName;
            fromdb.SenderIban = request.Model.SenderIban;
            fromdb.Description = request.Model.Description;
            fromdb.TransactionDate = request.Model.TransactionDate;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteEftTransactionCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<EftTransaction>().Where(x =>
            x.ReferenceNumber == request.Id.ToString()).FirstOrDefaultAsync(cancellationToken);

            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }

            fromdb.IsActive = false;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }
    }
}
