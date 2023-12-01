using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Generics.Interfaces;
using Domain.Models;
using Domain.Wrappers;
using FluentAssertions;
using Moq;
using N5_challenge.Queries;
using System.Linq.Expressions;
using Xunit;

namespace Tests.Permissions.Queries
{
    public class GetAllPermissionsQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetAllPermissionsQueryTests()
        {
            _unitOfWorkMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task ReturnSuccessData_WhenExistsItemsByPermissionTypeIdEquals2()
        {
            var mockPermissionsResultList = new List<Permission>
            {
                new Permission
                {
                    EmployeeForename = "Ariel",
                    EmployeeSurname = "Gomez"
                },
                new Permission
                {
                    EmployeeForename = "Alexis",
                    EmployeeSurname = "MacAllister"
                }
            };

            var mockDtoPermissionsResultList = new List<GetPermissionDto>
            {
                new GetPermissionDto
                {
                    EmployeeForename = "Ariel",
                    EmployeeSurname = "Ariel"
                },
                new GetPermissionDto
                {
                    EmployeeForename = "Alexis",
                    EmployeeSurname = "MacAllister"
                }
            };

            var query = new GetAllPermissionsQuery { PermissionTypeId = 2 };
            var handler = new GetAllPermissionsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

            var additionalProps = new Dictionary<string, int>
            {
                ["totalCount"] = 2
            };
            _unitOfWorkMock.Setup(m => m._permissionRepository.GetAllAsync(additionalProps,
                It.IsAny<Expression<Func<Permission, bool>>>(), It.IsAny<Func<IQueryable<Permission>, IOrderedQueryable<Permission>>>(), It.IsAny<PaginatorModel>(),
                null)).ReturnsAsync(mockPermissionsResultList);

            _mapperMock.Setup(m => m.Map<List<GetPermissionDto>>(It.IsAny<List<Permission>>()))
                .Returns(mockDtoPermissionsResultList);


            PageResponse<List<GetPermissionDto>> response = await handler.Handle(query, default);

            response.Data.Count.Should().Be(2);

        }

        [Fact]
        public async Task ReturnSuccessData_WhenNotExistsItems()
        {
            var mockPermissionsResultList = new List<Permission>{ };

            var mockDtoPermissionsResultList = new List<GetPermissionDto>{};

            var query = new GetAllPermissionsQuery { };
            var handler = new GetAllPermissionsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

            var additionalProps = new Dictionary<string, int>
            {
                ["totalCount"] = 0
            };
            _unitOfWorkMock.Setup(m => m._permissionRepository.GetAllAsync(additionalProps,
                It.IsAny<Expression<Func<Permission, bool>>>(), It.IsAny<Func<IQueryable<Permission>, IOrderedQueryable<Permission>>>(), It.IsAny<PaginatorModel>(),
                null)).ReturnsAsync(mockPermissionsResultList);

            _mapperMock.Setup(m => m.Map<List<GetPermissionDto>>(It.IsAny<List<Permission>>()))
                .Returns(mockDtoPermissionsResultList);


            PageResponse<List<GetPermissionDto>> response = await handler.Handle(query, default);

            response.Data.Should().BeEmpty();

        }
    }
}
