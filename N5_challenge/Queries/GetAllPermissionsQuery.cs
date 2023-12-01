using AutoMapper;
using Domain.DTOs;
using Domain.Generics.Interfaces;
using Domain.Helpers;
using Domain.Models;
using Domain.Wrappers;
using MediatR;

namespace N5_challenge.Queries
{
    public class GetAllPermissionsQuery: IRequest<PageResponse<List<GetPermissionDto>>>
    {

        #region "Pagination Params"
        
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion


        #region "Filter params"
        
        public int PermissionTypeId { get; set; }

        #endregion

        #region "Order Params"

        public string OrderBy { get; set; }

        #endregion


    }


    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, PageResponse<List<GetPermissionDto>>>
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public GetAllPermissionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PageResponse<List<GetPermissionDto>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var pageableParam = new PaginatorModel
            (
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                orderBy: request.OrderBy
            );

            Dictionary<string, int> additionalPropsFromRequest = new Dictionary<string, int>();

            var result = await _unitOfWork._permissionRepository.GetAllAsync(
                includes:p => p.PermissionType,
                additionalProps: additionalPropsFromRequest,
                pagination: pageableParam,
                filter: (f) => request.PermissionTypeId == 0 || f.PermissionType.Id == request.PermissionTypeId,
                orderBy: (ord) => string.IsNullOrEmpty(pageableParam.OrderBy) || pageableParam.OrderBy.Equals("ASC")
                            ? ord.OrderBy(p => p.Id)
                            : ord.OrderByDescending(p => p.Id)
            );

            List<GetPermissionDto> mapperList = _mapper.Map<List<GetPermissionDto>>(result);

            int count = additionalPropsFromRequest.GetValueOrDefault(EntitiesHelper.KEY_TOTAL_COUNT);
            return new PageResponse<List<GetPermissionDto>>(
                mapperList,
                request.PageNumber,
                request.PageSize,
                count
            );
        }
    }
}
