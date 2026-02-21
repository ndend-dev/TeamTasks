using TeamTasks.BL.Interfaces;
using TeamTasks.DAL.Interfaces;
using TeamTasks.DAL.Models;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.BL.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepository _developerRepository;

        public DeveloperService(IDeveloperRepository developer)
        {
            _developerRepository = developer;
        }

        public async Task<List<Developer>> GetAll()
        {
            return await _developerRepository.GetAllAsync();
        }

        public async Task<List<Developer>> GetAllActive()
        {
            var developers = await _developerRepository.GetAllAsync();

            return developers.Where(x => x.IsActive == true).ToList();
        }

        public async Task<Developer> GetById(Guid id)
        {
            return await _developerRepository.GetById(id);
        }


        public async Task<(bool success, string message)> Create(DeveloperRequestDto developerreq)
        {
            var developer = new Developer
            {
                FirstName = developerreq.FirstName,
                LastName = developerreq.LastName,
                Email = developerreq.Email,
                IsActive = true,
                CreatedAt = DateTime.Now,
            };

            try
            {

                await _developerRepository.Create(developer);
                var resp  = await _developerRepository.SaveChangesAsync();

                if(resp)
                    return (success: resp, message: "Desarrollador creado exitosamente.");
                else
                    return (success: resp, message: "Error al crear desarollador.");

            }
            catch (Exception ex)
            {
                return (success: false, message: ex.Message);
            }
        }


        public async Task<bool> Update(Guid id, DeveloperRequestDto developerreq)
        {

            var developer = new Developer
            {
                DeveloperId = id,
                FirstName = developerreq.FirstName,
                LastName = developerreq.LastName,
                Email = developerreq.Email,
                IsActive = developerreq.IsActive
            };

            await _developerRepository.Update(developer);

            return await _developerRepository.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            await _developerRepository.Delete(id);
            return await _developerRepository.SaveChangesAsync();

        }
    }
}
