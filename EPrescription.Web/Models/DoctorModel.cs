using EPrescription.Entities;
using EPrescription.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPrescription.Web.Models
{
    public class DoctorModel:Doctor
    {
        private DoctorService _doctorService;

        public DoctorModel()
        {
            _doctorService = new DoctorService();
            var doctorEntry = _doctorService.GetFirstDefault();
            if (doctorEntry != null)
            {
                this.Name = doctorEntry.Name;
                this.Degree = doctorEntry.Degree;
                this.Specialty = doctorEntry.Specialty;
                this.Institute = doctorEntry.Institute;
                this.Phone = doctorEntry.Phone;
                this.RegNo = doctorEntry.RegNo;
                this.Email = doctorEntry.Email;
                this.Website = doctorEntry.Website;
            }
        }

        internal void AddOrEdit(Doctor doctor)
        {
            _doctorService.AddOrEditDoctor(doctor);
        }

        public Doctor GetFirstDefault()
        {
            return _doctorService.GetFirstDefault();
        }

    }
}