﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("Clinics")]
    public class Clinic : Entity
    {    
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Timings { get; set; }
        [Display(Name="Off Days")]
        public string OffDays { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
