using DigimonApp.Domain.Models;
using DigimonApp.Domain.Repositories;
using DigimonApp.Domain.Services;
using DigimonApp.Resources;
using DigimonApp.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace DigimonAppTests.Services
{
    public class UsersServiceTest
    {
        public Mock<IUsersRepository> usersRepositoryMock = new Mock<IUsersRepository>();
        public Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        public Mock<IConfiguration> configurationMock = new Mock<IConfiguration>();

        [Fact]
        public async void SaveUserWithSuccess()
        {
            var user = new User { Email = "email@email.com", Password = "password", Role = UserRoleEnum.BASIC };

            var usersService = new UsersService(usersRepositoryMock.Object, unitOfWork.Object, configurationMock.Object);
            var result = await usersService.SaveAsync(user, user.Password);

            Assert.Equal(user, result.User);
        }

        [Fact]
        public async void SaveUserWithExistentEmail()
        {
            var user = new User { Email = "email@email.com", Password = "password", Role = UserRoleEnum.BASIC };

            usersRepositoryMock
                .Setup(s => s.GetUserByEmailAsync(user.Email))
                .ReturnsAsync(user);

            var usersService = new UsersService(usersRepositoryMock.Object, unitOfWork.Object, configurationMock.Object);
            var result = await usersService.SaveAsync(user, user.Password);

            Assert.False(result.Success);
        }
    }
}
