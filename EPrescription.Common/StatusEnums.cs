using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Common
{
    public enum EnumActiveDeative : byte
    {
        //=======Form Aprroval Status flag values======
        Inactive = 0,      // Inactive or deleted,
        Active = 1,        // Active or draft, 
    }
    public enum EnumPatientStatus : byte
    {
        History_Pending =0,
        History_Collected =1,
        Prescript =2
    }
 
}
