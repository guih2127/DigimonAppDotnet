using AutoMapper;
using DigimonApp.Controllers;
using DigimonApp.Domain.Models;
using DigimonApp.Domain.Services;
using DigimonApp.Domain.Services.Communication;
using DigimonApp.Mapping;
using DigimonApp.Resources;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Xunit;

namespace DigimonAppTests.Controllers
{
    public class DigimonsControllerTest
    {
        public Mock<IDigimonsService> digimonsService = new Mock<IDigimonsService>();
        public IMapper? mapper;
        public DigimonsController digimonsController;

        public DigimonsControllerTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ModelToResourceProfile>();
                cfg.AddProfile<ResourceToModelProfile>();
            });
            var mapper = config.CreateMapper();

            digimonsController = new DigimonsController(digimonsService.Object, mapper);
        }

        [Fact]
        public async void ListDigimonsWithSuccess()
        {
            var digimons = new List<Digimon> {
                new Digimon { Id = 1, Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" },
                new Digimon { Id = 2, Name = "Name2", Level = DigimonLevelEnum.ROOKIE, Image = "Image2" }
            };
            var listDigimonsResource = new ListDigimonResource();
            digimonsService.Setup(s => s.ListAsync(listDigimonsResource)).ReturnsAsync(digimons);

            var digimonResources = new List<DigimonResource> {
                new DigimonResource { Id = 1, Name = "Name1", Level = "ROOKIE", Image = "Image1" },
                new DigimonResource { Id = 2, Name = "Name2", Level = "ROOKIE", Image = "Image2" }
            };

            var result = await digimonsController.GetAllAsync(listDigimonsResource);
            var digimonsResult = result.ToList();

            Assert.Equal(digimonResources[0].Id, digimonsResult[0].Id);
            Assert.Equal(digimonResources[0].Name, digimonsResult[0].Name);
            Assert.Equal(digimonResources[0].Level, digimonsResult[0].Level);
            Assert.Equal(digimonResources[0].Image, digimonsResult[0].Image);

            Assert.Equal(digimonResources[1].Id, digimonsResult[1].Id);
            Assert.Equal(digimonResources[1].Name, digimonsResult[1].Name);
            Assert.Equal(digimonResources[1].Level, digimonsResult[1].Level);
            Assert.Equal(digimonResources[1].Image, digimonsResult[1].Image);

            digimonsService.Verify(s => s.ListAsync(listDigimonsResource));
            digimonsService.VerifyNoOtherCalls();
        }

        [Fact]
        public async void PostDigimonWithSuccess()
        {
            var digimonToSave = new SaveDigimonResource { Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            var digimon = new Digimon { Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            var digimonResource = new DigimonResource { Name = "Name1", Level = "ROOKIE", Image = "Image1" };
            var digimonResponse = new DigimonResponse(digimon);

            // TODO - Verificar se é possível testar sem usar o It.IsAny, e sim com os parâmetros corretos
            digimonsService.Setup(s => s.SaveAsync(It.IsAny<Digimon>())).ReturnsAsync(digimonResponse);
            var result = await digimonsController.PostAsync(digimonToSave) as OkObjectResult;
            var digimonReturned = result?.Value as DigimonResource;

            Assert.Equal(digimonResource.Id, digimonReturned?.Id);
            Assert.Equal(digimonResource.Name, digimonReturned?.Name);
            Assert.Equal(digimonResource.Level, digimonReturned?.Level);
            Assert.Equal(digimonResource.Image, digimonReturned?.Image);
            Assert.Equal((int)HttpStatusCode.OK, result?.StatusCode);

            digimonsService.Verify(s => s.SaveAsync(It.IsAny<Digimon>()));
            digimonsService.VerifyNoOtherCalls();
        }

    }
}