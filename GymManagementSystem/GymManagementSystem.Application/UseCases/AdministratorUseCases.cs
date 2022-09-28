using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystem.Domain;
using GymManagementSystem.Infrastructure;

namespace GymManagementSystem.Application
{
    public class AdministratorUseCases : IAdministratorUseCases
    {
        private readonly IAdministratorsRepository administratorsRepository;

        public AdministratorUseCases(IAdministratorsRepository administratorsRepository)
        {
            this.administratorsRepository = administratorsRepository;
        }

        public async Task AddAdministratorAsync(Administrator administrator)
        {
            if (administrator == null) return;

            var Administrators = await administratorsRepository.GetAdministratorsAsync();
            administrator.AdministratorId = Administrators.Max(x => x.AdministratorId) + 1;

            await administratorsRepository.AddAdministratorAsync(administrator);
        }

        public async Task EditAdministratorAsync(Administrator administrator)
        {
            if (administrator == null) return;

            await administratorsRepository.UpdateAdministratorAsync(administrator);
        }

        public async Task DeleteAdministratorAsync(int administratorId)
        {
            await administratorsRepository.DeleteAdministratorAsync(administratorId);
        }

        public async Task<List<Administrator>> GetAdministratorsAsync()
        {
            return await administratorsRepository.GetAdministratorsAsync();
        }

        public async Task<Administrator> GetAdministratorByIdAsync(int administratorId)
        {
            return await administratorsRepository.GetAdministratorByIdAsync(administratorId);
        }
    }
}
