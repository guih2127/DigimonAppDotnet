using DigimonApp.Domain.Models;
using DigimonApp.Domain.Repositories;
using DigimonApp.Services;
using Moq;
using Xunit;

namespace DigimonAppTests
{
    public class DigimonsServiceTest
    {
        public Mock<IDigimonsRepository> digimonsRepositoryMock = new Mock<IDigimonsRepository>();
        public Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();

        [Fact]
        public async void ListDigimons()
        {
            var digimons = new List<Digimon> {
                new Digimon { Id = 1, Name = "Name1", Level = "Level1", Image = "Image1" },
                new Digimon { Id = 2, Name = "Name2", Level = "Level2", Image = "Image2" }
            };

            digimonsRepositoryMock.Setup(s => s.ListAsync()).ReturnsAsync(digimons);

            var digimonsService = new DigimonsService(digimonsRepositoryMock.Object, unitOfWork.Object);
            var result = await digimonsService.ListAsync();

            Assert.Equal(digimons, result);
        }

        [Fact]
        public async void SaveDigimon()
        {
            var digimonToInsert = new Digimon { Name = "Name1", Level = "Level1", Image = "Image1" };

            var digimonsService = new DigimonsService(digimonsRepositoryMock.Object, unitOfWork.Object);
            var result = await digimonsService.SaveAsync(digimonToInsert);

            Assert.Equal(digimonToInsert, result.Digimon);
            Assert.True(result.Success);
        }

        [Fact]
        public async void UpdateDigimon()
        {
            var existingDigimon = new Digimon { Id = 12, Name = "Name1", Level = "Level1", Image = "Image1" };
            digimonsRepositoryMock.Setup(s => s.FindByIdAsync(12)).ReturnsAsync(existingDigimon);

            var digimonUpdated = new Digimon { Id = 12, Name = "Name12", Level = "Level12", Image = "Image12" };

            var digimonsService = new DigimonsService(digimonsRepositoryMock.Object, unitOfWork.Object);
            var result = await digimonsService.UpdateAsync(12, digimonUpdated);

            Assert.Equal(digimonUpdated.Name, result.Digimon.Name);
            Assert.Equal(digimonUpdated.Level, result.Digimon.Level);
            Assert.Equal(digimonUpdated.Image, result.Digimon.Image);

            Assert.True(result.Success);
        }

        [Fact]
        public async void DeleteDigimon()
        {
            var digimonToDelete = new Digimon { Id = 1, Name = "Name1", Level = "Level1", Image = "Image1" };
            digimonsRepositoryMock.Setup(s => s.FindByIdAsync(1)).ReturnsAsync(digimonToDelete);

            var digimonsService = new DigimonsService(digimonsRepositoryMock.Object, unitOfWork.Object);
            var result = await digimonsService.DeleteAsync(1);

            Assert.Equal(digimonToDelete, result.Digimon);
            Assert.True(result.Success);
        }
    }
}
