using SeverGrpc_NHibernate.Model;
using SeverGrpc_NHibernate.RepositoryNHibernate;
using Shared;
using Shared.DTOs.ResponseModel;

namespace SeverGrpc_NHibernate.Service
{
    public class ClassService : IClassService
    {
        private readonly INHibernateRepository<Class> _classRepository;
        public ClassService(INHibernateRepository<Class> classRepository)
        {
            _classRepository = classRepository;
        }
        public async Task<List<ClassResponse>> GetClassesAsync()
        {
            var classes =  _classRepository.All();
            return await Task.FromResult(classes.Select(c => new ClassResponse
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                TeacherName = c.Teacher!.Name
            }).ToList());
        }
    }
}
