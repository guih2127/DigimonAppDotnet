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
        }

        [Fact]
        public async void SaveDigimonWithSuccess()
        {
            var digimonToInsert = new Digimon { Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            var result = await digimonsService.SaveAsync(digimonToInsert);

            Assert.Equal(digimonToInsert, result.Digimon);
            Assert.True(result.Success);
        }

        [Fact]
        public async void SaveDigimonWithExistingName()
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
        }

        [Fact]
        public async void UpdateDigimonWithInexistentId()
        {
            var digimonToUpdate = new Digimon { Name = "Name12", Level = DigimonLevelEnum.ROOKIE, Image = "Image12" };

            var result = await digimonsService.UpdateAsync(0, digimonToUpdate);
            Assert.Null(result.Digimon);
            Assert.False(result.Success);
        }

        [Fact]
        public async void UpdateDigimonWithExistentName()
        {
            var existingDigimon = new Digimon { Id = 1, Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            var digimonToUpdate = new Digimon { Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            digimonsRepositoryMock.Setup(s => s.FindByNameAsync(existingDigimon.Name)).ReturnsAsync(existingDigimon);

            var result = await digimonsService.UpdateAsync(2, digimonToUpdate);
            Assert.False(result.Success);
        }

        [Fact]
        public async void DeleteDigimonWithSuccess()
        {
            var digimonToDelete = new Digimon { Id = 1, Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            digimonsRepositoryMock.Setup(s => s.FindByIdAsync(1)).ReturnsAsync(digimonToDelete);

            var result = await digimonsService.DeleteAsync(1);
            Assert.Equal(digimonToDelete, result.Digimon);
            Assert.True(result.Success);
        }

        [Fact]
        public async void DeleteDigimonWithInexistentId()
        {
            var result = await digimonsService.DeleteAsync(0);
            Assert.Null(result.Digimon);
            Assert.False(result.Success);
        }
    }
}
