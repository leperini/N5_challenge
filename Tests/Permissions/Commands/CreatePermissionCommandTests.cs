using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Generics.Interfaces;
using Domain.Wrappers;
using FluentAssertions;
using Moq;
using N5_challenge.Commands;
using Xunit;

namespace Tests.Permissions.Commands
{
    public class CreatePermissionCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public CreatePermissionCommandTests()
        {
            _unitOfWorkMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task ReturnSuccessResultId_WhenDtoIsValid()
        {

            // Arrange
            string employeeForename = "Will";
            string employeeSurname = "Smith";

            var command = new CreatePermissionCommand
            {
                EmployeeForename = employeeForename,
                EmployeeSurname = employeeSurname,
                Date = DateTime.Now,
                PermissionTypeId = 10
            };
            var mockPermissionResult = new Permission
            {
                EmployeeForename = employeeForename,
                EmployeeSurname = employeeSurname,
                Date = DateTime.Now
            };
            var mockDtoResult = new GetPermissionDto
            {
                EmployeeForename = employeeForename,
                EmployeeSurname = employeeSurname,
                PermissionDate = DateTime.Now,
                PermissionId = 1
            };

            _unitOfWorkMock.Setup(m => m._permissionRepository.AddAsync(It.IsAny<Permission>())).ReturnsAsync(mockPermissionResult);

            _unitOfWorkMock.Setup(m => m._searchPermissionRepository.AddAsync(It.IsAny<Permission>())).ReturnsAsync(mockPermissionResult);

            _mapperMock.Setup(m => m.Map<Permission>(It.IsAny<CreatePermissionCommand>()))
                .Returns(mockPermissionResult);

            _mapperMock.Setup(m => m.Map<GetPermissionDto>(It.IsAny<Permission>()))
                .Returns(mockDtoResult);
            var handler = new CreatePermissionCommandHanlder(_unitOfWorkMock.Object, _mapperMock.Object);

            // Act
            Response<GetPermissionDto> response = await handler.Handle(command, default);

            // Assert
            response.Successed.Should().BeTrue();
            response.Data.PermissionId.Should().Be(1);


        }

        [Fact]
        public async Task ReturnSuccessResultAndResponseDto_WhenDtoIsValid()
        {
            // Arrange
            string employeeForename = "Will";
            string employeeSurname = "Smith";

            var command = new CreatePermissionCommand
            {
                EmployeeForename = employeeForename,
                EmployeeSurname = employeeSurname,
                Date = DateTime.Now,
                PermissionTypeId = 10
            };
            var mockPermissionResult = new Permission
            {
                EmployeeForename = employeeForename,
                EmployeeSurname = employeeSurname,
                Date = DateTime.Now
            };
            var mockDtoResult = new GetPermissionDto
            {
                EmployeeForename = employeeForename,
                EmployeeSurname = employeeSurname,
                PermissionDate = DateTime.Now
            };

            _unitOfWorkMock.Setup(m => m._permissionRepository.AddAsync(It.IsAny<Permission>())).ReturnsAsync(mockPermissionResult);

            _unitOfWorkMock.Setup(m => m._searchPermissionRepository.AddAsync(It.IsAny<Permission>())).ReturnsAsync(mockPermissionResult);

            _mapperMock.Setup(m => m.Map<Permission>(It.IsAny<CreatePermissionCommand>()))
                .Returns(mockPermissionResult);

            _mapperMock.Setup(m => m.Map<GetPermissionDto>(It.IsAny<Permission>()))
                .Returns(mockDtoResult);

            var handler = new CreatePermissionCommandHanlder(_unitOfWorkMock.Object, _mapperMock.Object);

            // Act
            Response<GetPermissionDto> response = await handler.Handle(command, default);

            // Assert
            response.Successed.Should().BeTrue();
            response.Data.Should().NotBeNull();


        }
    }
}
