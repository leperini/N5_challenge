using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Generics.Interfaces;
using Domain.Wrappers;
using MediatR;

namespace N5_challenge.Commands
{
    public class UpdatePermissionCommand : IRequest<Response<GetPermissionDto>>
    {
        public int Id { get; set; }
        
        public string EmployeeForename { get; set; }

        public string EmployeeSurname { get; set; }

        public DateTime Date { get; set; }
        public int PermissionTypeId { get; set; }

    }


    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, Response<GetPermissionDto>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePermissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Response<GetPermissionDto>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            
            var permissionForUpdate = _mapper.Map<Permission>(request);

            // database persistence
            var result = await _unitOfWork._permissionRepository.UpdateAsync(request.Id, permissionForUpdate);

            // elastic persistence
            await _unitOfWork._searchPermissionRepository.UpdateAsync(request.Id, permissionForUpdate);


            var permissionResult = _mapper.Map<GetPermissionDto>(result);

            return new Response<GetPermissionDto>
            {
                Data = permissionResult,
                Message = $"Permission Id: {request.Id} updated ok"
            };
        }
    }

}
