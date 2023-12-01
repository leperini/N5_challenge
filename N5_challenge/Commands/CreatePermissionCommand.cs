using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Generics.Interfaces;
using Domain.Wrappers;
using MediatR;

namespace N5_challenge.Commands
{
    public class CreatePermissionCommand: IRequest<Response<GetPermissionDto>>
    {

        public string EmployeeForename { get; set; }

        public string EmployeeSurname { get; set; }

        public DateTime Date { get; set; }

        public int PermissionTypeId { get; set; }


    }

    public class CreatePermissionCommandHanlder : IRequestHandler<CreatePermissionCommand, Response<GetPermissionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePermissionCommandHanlder(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<GetPermissionDto>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionForCreate = _mapper.Map<Permission>(request);
            
            // db persistence
            var result = await _unitOfWork._permissionRepository.AddAsync(permissionForCreate);
            
            // elastic persistence
            await _unitOfWork._searchPermissionRepository.AddAsync(permissionForCreate);

            var permissionResult = _mapper.Map<GetPermissionDto>(result);

            return new Response<GetPermissionDto>
            {
                Data = permissionResult,
                Message = "Permission created ok",
                Successed = true
            };
        }
    }
}
