using DigimonApp.Domain.Models;
using DigimonApp.Domain.Repositories;
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

        [Fact]
        public async void ListDigimonsWithSuccess()
        {
            var digimons = new List<Digimon> {
                new Digimon { Id = 1, Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" },
                new Digimon { Id = 2, Name = "Name2", Level = DigimonLevelEnum.ROOKIE, Image = "Image2" }
            };

            var listDigimonsResource = new ListDigimonResource();
            digimonsRepositoryMock.Setup(s => s.ListAsync(listDigimonsResource)).ReturnsAsync(digimons);

            var digimonsService = new DigimonsService(digimonsRepositoryMock.Object, unitOfWork.Object);
            var result = await digimonsService.ListAsync(listDigimonsResource);

            Assert.Equal(digimons, result);
        }

        [Fact]
        public async void SaveDigimonWithSuccess()
        {
            var digimonToInsert = new Digimon { Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };

            var digimonsService = new DigimonsService(digimonsRepositoryMock.Object, unitOfWork.Object);
            var result = await digimonsService.SaveAsync(digimonToInsert);

            Assert.Equal(digimonToInsert, result.Digimon);
            Assert.True(result.Success);
        }

        [Fact]
        public async void UpdateDigimonWithSuccess()
        {
            var existingDigimon = new Digimon { Id = 12, Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            digimonsRepositoryMock.Setup(s => s.FindByIdAsync(12)).ReturnsAsync(existingDigimon);

            var digimonToUpdate = new Digimon { Id = 12, Name = "Name12", Level = DigimonLevelEnum.ROOKIE, Image = "Image12" };

            var digimonsService = new DigimonsService(digimonsRepositoryMock.Object, unitOfWork.Object);
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

            var digimonsService = new DigimonsService(digimonsRepositoryMock.Object, unitOfWork.Object);
            var result = await digimonsService.UpdateAsync(0, digimonToUpdate);

            Assert.Null(result.Digimon);
            Assert.False(result.Success);
        }

        [Fact]
        public async void DeleteDigimonWithSuccess()
        {
            var digimonToDelete = new Digimon { Id = 1, Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            digimonsRepositoryMock.Setup(s => s.FindByIdAsync(1)).ReturnsAsync(digimonToDelete);

            var digimonsService = new DigimonsService(digimonsRepositoryMock.Object, unitOfWork.Object);
            var result = await digimonsService.DeleteAsync(1);

            Assert.Equal(digimonToDelete, result.Digimon);
            Assert.True(result.Success);
        }

        [Fact]
        public async void DeleteDigimonWithInexistentId()
        {
            var digimonsService = new DigimonsService(digimonsRepositoryMock.Object, unitOfWork.Object);
            var result = await digimonsService.DeleteAsync(0);

            Assert.Null(result.Digimon);
            Assert.False(result.Success);
        }
    }
}
