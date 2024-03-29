﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Command;
public class AddressCommandHandler :
    IRequestHandler<CreateAddressCommand, ApiResponse<AddressResponse>>,
    IRequestHandler<UpdateAddressCommand, ApiResponse>,
    IRequestHandler<DeleteAddressCommand, ApiResponse>
    {
        private readonly VbDbContext dbContext;
        private readonly IMapper mapper;

        public AddressCommandHandler(VbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<AddressResponse>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {

            var entity = mapper.Map<AddressRequest, Address>(request.Model);

            var entityResult = await dbContext.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var mapped = mapper.Map<Address, AddressResponse>(entityResult.Entity);
            return new ApiResponse<AddressResponse>(mapped);
    }

        public async Task<ApiResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Address>()
                .Include(x => x.Customer)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return new ApiResponse("Address Record not found");
            }

                entity.Address1 = request.Model.Address1;
                entity.Address2 = request.Model.Address2;
                entity.City = request.Model.City;
                entity.Country = request.Model.Country;
                entity.County = request.Model.County;

                await dbContext.SaveChangesAsync(cancellationToken);
                return new ApiResponse();
    }

        public async Task<ApiResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Address>()
                .Include(x => x.Customer)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return new ApiResponse("Address Record not found");
            }

            entity.IsActive = false;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
    }
    }