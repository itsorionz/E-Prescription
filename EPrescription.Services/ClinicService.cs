using EPrescription.Entities;
using EPrescription.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Services
{
    public class ClinicService
    {
        private EPrescriptionDbContext _context;
        private ClinicUnitOfWork _clinicUnitOfWork;

        public ClinicService()
        {
            _context = new EPrescriptionDbContext();
            _clinicUnitOfWork = new ClinicUnitOfWork(_context);
        }

        public IEnumerable<Clinic> GetAllClinics()
        {
            return _clinicUnitOfWork.ClinicRepository.GetAll();
        }
        public void AddClinic(Clinic clinic)
        {
            var newClinic = new Clinic()
            {
                Name = clinic.Name,
                Address = clinic.Address,
                Phone = clinic.Phone,
                Mobile = clinic.Mobile,
                Timings = clinic.Timings,
                Email = clinic.Email,
                OffDays = clinic.OffDays
            };
            _clinicUnitOfWork.ClinicRepository.Add(newClinic);
            _clinicUnitOfWork.Save();
        }
    }
}
