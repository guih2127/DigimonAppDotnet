using DigimonApp.Domain.Models;
using DigimonApp.Domain.Repositories;
using DigimonApp.Domain.Services;
using DigimonApp.Domain.Services.Communication;
using DigimonApp.Resources;
using DigimonApp.Resources.RabbitMQ;
using DigimonApp.Utils;

namespace DigimonApp.Services
{
    public class DigimonsService : IDigimonsService
    {
        private readonly IDigimonsRepository _digimonRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRabbitMQService _rabbitMQService;

        public DigimonsService(IDigimonsRepository digimonRepository, IUnitOfWork unitOfWork, IRabbitMQService rabbitMQService)
        {
            _digimonRepository = digimonRepository;
            _unitOfWork = unitOfWork;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<IEnumerable<Digimon>> ListAsync(ListDigimonResource resource)
        {
            return await _digimonRepository.ListAsync(resource);
        }

        public async Task<DigimonResponse> UpdateAsync(int id, Digimon digimon)
        {
            var existingDigimon = await _digimonRepository.FindByIdAsync(id);
            if (existingDigimon == null)
                return new DigimonResponse("Digimon not found.");

            var digimonWithSameName = await _digimonRepository.FindByNameAsync(digimon.Name);
            if (digimonWithSameName != null)
                return new DigimonResponse($"A Digimon with this name already exists");

            try
            {
                existingDigimon.Name = digimon.Name;
                existingDigimon.Level = digimon.Level;
                existingDigimon.Image = digimon.Image;

                _digimonRepository.Update(existingDigimon);
                await _unitOfWork.CompleteAsync();

                _rabbitMQService.SendLogMessage(LogUtils.CreateLogObject(digimon, true, RabbitMqLogOperationType.UPDATE), "DigimonQueue");

                return new DigimonResponse(existingDigimon);
            }
            catch (Exception ex)
            {
                _rabbitMQService.SendLogMessage(LogUtils.CreateLogObject(digimon, false, RabbitMqLogOperationType.UPDATE), "DigimonQueue");
                return new DigimonResponse($"An error occurred when updating the digimon: {ex.Message}");
            }
        }

        public async Task<DigimonResponse> SaveAsync(Digimon digimon)
        {
            var digimonWithSameName = await _digimonRepository.FindByNameAsync(digimon.Name);
            if (digimonWithSameName != null)
                return new DigimonResponse($"A Digimon with this name already exists");

            try
            {
                await _digimonRepository.AddAsync(digimon);
                await _unitOfWork.CompleteAsync();
                _rabbitMQService.SendLogMessage(LogUtils.CreateLogObject(digimon, true, RabbitMqLogOperationType.SAVE), "DigimonQueue");

                return new DigimonResponse(digimon);
            }
            catch (Exception ex)
            {
                _rabbitMQService.SendLogMessage(LogUtils.CreateLogObject(digimon, true, RabbitMqLogOperationType.SAVE, ex.Message), "DigimonQueue");
                return new DigimonResponse($"An error occurred when saving the digimon: {ex.Message}");
            }
        }

        public async Task<DigimonResponse> DeleteAsync(int id)
        {
            var existingDigimon = await _digimonRepository.FindByIdAsync(id);

            if (existingDigimon == null)
                return new DigimonResponse("Digimon not found.");

            try
            {
                _digimonRepository.Remove(existingDigimon);
                await _unitOfWork.CompleteAsync();

                _rabbitMQService.SendLogMessage(LogUtils.CreateLogObject(existingDigimon, true, RabbitMqLogOperationType.SAVE), "DigimonQueue");

                return new DigimonResponse(existingDigimon);
            }
            catch (Exception ex)
            {
                _rabbitMQService.SendLogMessage(LogUtils.CreateLogObject(existingDigimon, false, RabbitMqLogOperationType.SAVE), "DigimonQueue");
                return new DigimonResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }
    }
}
