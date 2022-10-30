﻿using DigimonApp.Domain.Models;
using DigimonApp.Resources;

namespace DigimonApp.Domain.Repositories
{
    public interface IDigimonsRepository
    {
        Task<IEnumerable<Digimon>> ListAsync(ListDigimonResource resource);
        Task AddAsync(Digimon digimon);
        Task<Digimon> FindByIdAsync(int id);
        void Update(Digimon digimon);
        void Remove(Digimon digimon);
    }
}
