using DigimonApp.Domain.Models;
using DigimonApp.Domain.Repositories;
using DigimonApp.Domain.Services;
using DigimonApp.Resources;
using DigimonApp.Services;
using Moq;
using Xunit;

namespace DigimonAppTests.Services
{
    public class DigimonsServiceTest
    {
        public Mock<IDigimonsRepository> digimonsRepositoryMock = new Mock<IDigimonsRepository>();
        public Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        public Mock<IRabbitMQService> rabbitMqService = new Mock<IRabbitMQService>();

        public DigimonsService digimonsService;

        public DigimonsServiceTest()
        {
            digimonsService = new DigimonsService(
                digimonsRepositoryMock.Object,
                unitOfWork.Object, 
                rabbitMqService.Object);
        }

        [Fact]
        public async void ListDigimonsWithSuccess()
        {
            var digimons = new List<Digimon> {
                new Digimon { Id = 1, Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" },
                new Digimon { Id = 2, Name = "Name2", Level = DigimonLevelEnum.ROOKIE, Image = "Image2" }
            };

            var listDigimonsResource = new ListDigimonResource();
            digimonsRepositoryMock.Setup(s => s.ListAsync(listDigimonsResource)).ReturnsAsync(digimons);

            var result = await digimonsService.ListAsync(listDigimonsResource);
            Assert.Equal(digimons, result);

            digimonsRepositoryMock.Verify(s => s.ListAsync(listDigimonsResource));
            digimonsRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void SaveDigimonWithSuccess()
        {
            var digimonToInsert = new Digimon { Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };

            var result = await digimonsService.SaveAsync(digimonToInsert);
            Assert.Equal(digimonToInsert, result.Digimon);
            Assert.True(result.Success);

            digimonsRepositoryMock.Verify(s => s.FindByNameAsync(digimonToInsert.Name));
            digimonsRepositoryMock.Verify(s => s.AddAsync(digimonToInsert));
            digimonsRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void SaveDigimonWithExistingNameWithoutSuccess()
        {
            var existingDigimon = new Digimon { Id = 1, Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            var digimonToInsert = new Digimon { Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            digimonsRepositoryMock.Setup(s => s.FindByNameAsync(existingDigimon.Name)).ReturnsAsync(existingDigimon);

            var result = await digimonsService.SaveAsync(digimonToInsert);
            Assert.False(result.Success);
        }

        [Fact]
        public async void UpdateDigimonWithSuccess()
        {
            var existingDigimon = new Digimon { Id = 12, Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            var digimonToUpdate = new Digimon { Id = 12, Name = "Name12", Level = DigimonLevelEnum.ROOKIE, Image = "Image12" };
            digimonsRepositoryMock.Setup(s => s.FindByIdAsync(12)).ReturnsAsync(existingDigimon);

            var result = await digimonsService.UpdateAsync(12, digimonToUpdate);
            Assert.Equal(digimonToUpdate.Name, result.Digimon.Name);
            Assert.Equal(digimonToUpdate.Level, result.Digimon.Level);
            Assert.Equal(digimonToUpdate.Image, result.Digimon.Image);
            Assert.True(result.Success);

            digimonsRepositoryMock.Verify(s => s.FindByIdAsync(digimonToUpdate.Id));
            digimonsRepositoryMock.Verify(s => s.FindByNameAsync(digimonToUpdate.Name));
            digimonsRepositoryMock.Verify(s => s.Update(It.IsAny<Digimon>()));
            digimonsRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void UpdateDigimonWithInexistentIdWithoutSuccess()
        {
            var digimonToUpdate = new Digimon { Name = "Name12", Level = DigimonLevelEnum.ROOKIE, Image = "Image12" };

            var result = await digimonsService.UpdateAsync(0, digimonToUpdate);
            Assert.Null(result.Digimon);
            Assert.False(result.Success);

            digimonsRepositoryMock.Verify(s => s.FindByIdAsync(digimonToUpdate.Id));
            digimonsRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void UpdateDigimonWithExistentNameWithoutSuccess()
        {
            var existingDigimon = new Digimon { Id = 2, Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            var digimonToUpdate = new Digimon { Id = 1, Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            digimonsRepositoryMock.Setup(s => s.FindByIdAsync(digimonToUpdate.Id)).ReturnsAsync(digimonToUpdate);
            digimonsRepositoryMock.Setup(s => s.FindByNameAsync(existingDigimon.Name)).ReturnsAsync(existingDigimon);

            var result = await digimonsService.UpdateAsync(digimonToUpdate.Id, digimonToUpdate);
            Assert.False(result.Success);

            digimonsRepositoryMock.Verify(s => s.FindByIdAsync(digimonToUpdate.Id));
            digimonsRepositoryMock.Verify(s => s.FindByNameAsync(digimonToUpdate.Name));
            digimonsRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void DeleteDigimonWithSuccess()
        {
            var digimonToDelete = new Digimon { Id = 1, Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            digimonsRepositoryMock.Setup(s => s.FindByIdAsync(1)).ReturnsAsync(digimonToDelete);

            var result = await digimonsService.DeleteAsync(1);
            Assert.Equal(digimonToDelete, result.Digimon);
            Assert.True(result.Success);

            digimonsRepositoryMock.Verify(s => s.FindByIdAsync(digimonToDelete.Id));
            digimonsRepositoryMock.Verify(s => s.Remove(digimonToDelete));
            digimonsRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void DeleteDigimonWithInexistentIdWithoutSuccess()
        {
            var result = await digimonsService.DeleteAsync(0);
            Assert.Null(result.Digimon);
            Assert.False(result.Success);

            digimonsRepositoryMock.Verify(s => s.FindByIdAsync(0));
            digimonsRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
