using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("PatientInvestigations")]
    public class PatientInvestigation : Entity
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
        public int InvestigationId { get; set; }
        public string Investigation { get; set; }
    }
}
