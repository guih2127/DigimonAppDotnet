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
using static DigimonAppTests.Utils.Utils;

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

            ValidateResourceForTests(listDigimonsResource, digimonsController);
            var result = await digimonsController.GetAllAsync(listDigimonsResource);
            var digimonsResult = result.ToList();

            Assert.True(digimonsController.ModelState.IsValid);

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
            var digimonToSave = new SaveDigimonResource { Name = "Name1", Level = 1, Image = "Image1" };
            var digimon = new Digimon { Name = "Name1", Level = DigimonLevelEnum.ROOKIE, Image = "Image1" };
            var digimonResource = new DigimonResource { Name = "Name1", Level = "ROOKIE", Image = "Image1" };
            var digimonResponse = new DigimonResponse(digimon);

            // TODO - Verificar se é possível testar sem usar o It.IsAny, e sim com os parâmetros corretos
            digimonsService.Setup(s => s.SaveAsync(It.IsAny<Digimon>())).ReturnsAsync(digimonResponse);

            ValidateResourceForTests(digimonToSave, digimonsController);
            var result = await digimonsController.PostAsync(digimonToSave) as OkObjectResult;
            var digimonReturned = result?.Value as DigimonResource;

            Assert.True(digimonsController.ModelState.IsValid);

            Assert.Equal(digimonResource.Id, digimonReturned?.Id);
            Assert.Equal(digimonResource.Name, digimonReturned?.Name);
            Assert.Equal(digimonResource.Level, digimonReturned?.Level);
            Assert.Equal(digimonResource.Image, digimonReturned?.Image);
            Assert.Equal((int)HttpStatusCode.OK, result?.StatusCode);

            digimonsService.Verify(s => s.SaveAsync(It.IsAny<Digimon>()));
            digimonsService.VerifyNoOtherCalls();
        }

        [Fact]
        public async void PostDigimonWithoutNameWithoutSuccess()
        {
            var digimonToSave = new SaveDigimonResource { Level = 1, Image = "Image1" };

            ValidateResourceForTests(digimonToSave, digimonsController);
            var result = await digimonsController.PostAsync(digimonToSave) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.False(digimonsController.ModelState.IsValid);
            Assert.Equal(1, digimonsController.ModelState?.Count);
            Assert.Equal(
                "Name is required", 
                digimonsController.ModelState?.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).FirstOrDefault()
            );
        }

        [Fact]
        public async void PostDigimonWithoutImageWithoutSuccess()
        {
            var digimonToSave = new SaveDigimonResource { Name = "Name1", Level = 1 };

            ValidateResourceForTests(digimonToSave, digimonsController);
            var result = await digimonsController.PostAsync(digimonToSave) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.False(digimonsController.ModelState.IsValid);
            Assert.Equal(1, digimonsController.ModelState?.Count);
            Assert.Equal(
                "Image is required",
                digimonsController.ModelState?.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).FirstOrDefault()
            );
        }

        [Fact]
        public async void PostDigimonWithoutLevelWithoutSuccess()
        {
            var digimonToSave = new SaveDigimonResource { Name = "Name1", Image = "Image1" };

            ValidateResourceForTests(digimonToSave, digimonsController);
            var result = await digimonsController.PostAsync(digimonToSave) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.False(digimonsController.ModelState.IsValid);
            Assert.Equal(1, digimonsController.ModelState?.Count);
            Assert.Equal(
                "Level is required",
                digimonsController.ModelState?.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).FirstOrDefault()
            );
        }

        [Fact]
        public async void PostDigimonWithLevelSmallerThanOneWithoutSuccess()
        {
            var digimonToSave = new SaveDigimonResource { Name = "Name1", Image = "Image1", Level = 0 };

            ValidateResourceForTests(digimonToSave, digimonsController);
            var result = await digimonsController.PostAsync(digimonToSave) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.False(digimonsController.ModelState.IsValid);
            Assert.Equal(1, digimonsController.ModelState?.Count);
            Assert.Equal(
                "Please enter a value between 1 and 6",
                digimonsController.ModelState?.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).FirstOrDefault()
            );
        }

        [Fact]
        public async void PostDigimonWithLevelHigherThenSixWithoutSuccess()
        {
            var digimonToSave = new SaveDigimonResource { Name = "Name1", Image = "Image1", Level = 7 };

            ValidateResourceForTests(digimonToSave, digimonsController);
            var result = await digimonsController.PostAsync(digimonToSave) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.False(digimonsController.ModelState.IsValid);
            Assert.Equal(1, digimonsController.ModelState?.Count);
            Assert.Equal(
                "Please enter a value between 1 and 6",
                digimonsController.ModelState?.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).FirstOrDefault()
            );
        }

        [Fact]
        public async void UpdateDigimonWithSuccess()
        {
            var existingDigimon = new DigimonResource { Id = 1, Image = "Image1", Level = "ROOKIE", Name = "Name1" };
            var newDigimondata = new SaveDigimonResource { Image = "Image2", Level = (int)DigimonLevelEnum.MEGA, Name = "Name2" };
            var digimon = new Digimon { Id = 1, Image = newDigimondata.Image, Level = DigimonLevelEnum.MEGA, Name = newDigimondata.Name };
            var digimonResponse = new DigimonResponse(digimon);

            ValidateResourceForTests(newDigimondata, digimonsController);
            digimonsService.Setup(s => s.UpdateAsync(existingDigimon.Id, It.IsAny<Digimon>())).ReturnsAsync(digimonResponse);
            var result = await digimonsController.PutAsync(existingDigimon.Id, newDigimondata) as OkObjectResult;
            var digimonReturned = result?.Value as DigimonResource;

            Assert.Equal(existingDigimon.Id, digimonReturned?.Id);
            Assert.Equal(newDigimondata.Name, digimonReturned?.Name);
            Assert.Equal(((DigimonLevelEnum)newDigimondata.Level).ToString(), digimonReturned?.Level);
            Assert.Equal(newDigimondata.Image, digimonReturned?.Image);

            Assert.Equal((int)HttpStatusCode.OK, result?.StatusCode);
            Assert.True(digimonsController.ModelState.IsValid);
        }

        [Fact]
        public async void UpdateDigimonWithoutNameWithoutSuccess()
        {
            var id = 1;
            var digimonToSave = new SaveDigimonResource { Level = 4, Image = "Image1" };

            ValidateResourceForTests(digimonToSave, digimonsController);
            var result = await digimonsController.PutAsync(id, digimonToSave) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.False(digimonsController.ModelState.IsValid);
            Assert.Equal(1, digimonsController.ModelState?.Count);
            Assert.Equal(
                "Name is required",
                digimonsController.ModelState?.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).FirstOrDefault()
            );
        }

        [Fact]
        public async void UpdateDigimonWithoutImageWithoutSuccess()
        {
            var id = 1;
            var digimonToSave = new SaveDigimonResource { Name = "Name1", Level = 1 };

            ValidateResourceForTests(digimonToSave, digimonsController);
            var result = await digimonsController.PutAsync(id, digimonToSave) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.False(digimonsController.ModelState.IsValid);
            Assert.Equal(1, digimonsController.ModelState?.Count);
            Assert.Equal(
                "Image is required",
                digimonsController.ModelState?.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).FirstOrDefault()
            );
        }

        [Fact]
        public async void UpdateDigimonWithoutLevelWithoutSuccess()
        {
            var id = 1;
            var digimonToSave = new SaveDigimonResource { Name = "Name1", Image = "Image1" };

            ValidateResourceForTests(digimonToSave, digimonsController);
            var result = await digimonsController.PutAsync(id, digimonToSave) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.False(digimonsController.ModelState.IsValid);
            Assert.Equal(1, digimonsController.ModelState?.Count);
            Assert.Equal(
                "Level is required",
                digimonsController.ModelState?.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).FirstOrDefault()
            );
        }

        [Fact]
        public async void UpdateDigimonWithLevelSmallerThanOneWithoutSuccess()
        {
            var id = 1;
            var digimonToSave = new SaveDigimonResource { Name = "Name1", Image = "Image1", Level = 0 };

            ValidateResourceForTests(digimonToSave, digimonsController);
            var result = await digimonsController.PutAsync(id, digimonToSave) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.False(digimonsController.ModelState.IsValid);
            Assert.Equal(1, digimonsController.ModelState?.Count);
            Assert.Equal(
                "Please enter a value between 1 and 6",
                digimonsController.ModelState?.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).FirstOrDefault()
            );
        }

        [Fact]
        public async void UpdateDigimonWithLevelHigherThenSixWithoutSuccess()
        {
            var id = 1;
            var digimonToSave = new SaveDigimonResource { Name = "Name1", Image = "Image1", Level = 7 };

            ValidateResourceForTests(digimonToSave, digimonsController);
            var result = await digimonsController.PutAsync(id, digimonToSave) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.False(digimonsController.ModelState.IsValid);
            Assert.Equal(1, digimonsController.ModelState?.Count);
            Assert.Equal(
                "Please enter a value between 1 and 6",
                digimonsController.ModelState?.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).FirstOrDefault()
            );
        }
    }
}