using DigimonApp.Domain.Models;
using DigimonApp.Domain.Repositories;
using DigimonApp.Domain.Services;
using DigimonApp.Domain.Services.Communication;
using DigimonApp.Resources;

namespace DigimonApp.Services
{
    public class DigimonsService : IDigimonsService
    {
        private readonly IDigimonsRepository _digimonRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DigimonsService(IDigimonsRepository digimonRepository, IUnitOfWork unitOfWork)
        {
            _digimonRepository = digimonRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Digimon>> ListAsync()
        {
            return await _digimonRepository.ListAsync();
        }

        public async Task<DigimonResponse> UpdateAsync(int id, Digimon digimon)
        {
            var existingDigimon = await _digimonRepository.FindByIdAsync(id);

            if (existingDigimon == null)
                return new DigimonResponse("Digimon not found.");

            existingDigimon.Name = digimon.Name;
            existingDigimon.Level = digimon.Level;
            existingDigimon.Image = digimon.Image;

            try
            {
                _digimonRepository.Update(existingDigimon);
                await _unitOfWork.CompleteAsync();

                return new DigimonResponse(existingDigimon);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new DigimonResponse($"An error occurred when updating the digimon: {ex.Message}");
            }
        }

        public async Task<DigimonResponse> SaveAsync(Digimon digimon)
        {
            try
            {
                await _digimonRepository.AddAsync(digimon);
                await _unitOfWork.CompleteAsync();

                return new DigimonResponse(digimon);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
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

                return new DigimonResponse(existingDigimon);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new DigimonResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }
    }
}
