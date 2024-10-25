using EPrescription.Entities;
using EPrescription.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPrescription.Web.Models
{
    public class ClinicModel:Clinic
    {
        private ClinicService _clinicService;

        public ClinicModel()
        {
            _clinicService = new ClinicService();
        }
        public IEnumerable<Clinic> GetAllClinics()
        {
            return _clinicService.GetAllClinics();
        }

        public void Add()
        {
            _clinicService.AddClinic(this);
        }
    }
}