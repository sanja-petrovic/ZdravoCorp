using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IDoctorRepository : IRepositoryBase<Model.Doctor, String>
    {
        public Model.Doctor GetByEmail(String email);
        public List<Model.Doctor> GetBySpecialty(string specialty);
        public List<String> GetAllSpecialties();
    }
}
