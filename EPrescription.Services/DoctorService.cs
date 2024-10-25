using EPrescription.Entities;
using EPrescription.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Services
{
    public class DoctorService
    {
        private EPrescriptionDbContext _context;
        private DoctorUnitOfWork doctorUnitOfWork;

        public DoctorService()
        {
            _context = new EPrescriptionDbContext();
            doctorUnitOfWork = new DoctorUnitOfWork(_context);
        }
        public void AddOrEditDoctor(Doctor doctor)
        {
            var doctorEntry = doctorUnitOfWork.DoctorRepository.GetFirstDefault();
            if (doctorEntry != null)
            {
                doctorEntry.Name = doctor.Name;
                doctorEntry.Degree = doctor.Degree;
                doctorEntry.Specialty = doctor.Specialty;
                doctorEntry.Institute = doctor.Institute;
                doctorEntry.Phone = doctor.Phone;
                doctorEntry.Email = doctor.Email;
                doctorEntry.Website = doctor.Website;
                doctorUnitOfWork.DoctorRepository.Update(doctorEntry);
                doctorUnitOfWork.Save();
            }
            else
            {
                var newDoctor = new Doctor()
                {
                    Name = doctor.Name,
                    Degree = doctor.Degree,
                    Specialty = doctor.Specialty,
                    Institute = doctor.Institute,
                    RegNo = doctor.RegNo,
                    Phone = doctor.Phone,
                    Email = doctor.Email,
                    Website = doctor.Website
                };
                doctorUnitOfWork.DoctorRepository.Add(newDoctor);
                doctorUnitOfWork.Save();
            }
        }
        public Doctor GetFirstDefault()
        {
            return doctorUnitOfWork.DoctorRepository.GetFirstDefault();
        }
    }
}
