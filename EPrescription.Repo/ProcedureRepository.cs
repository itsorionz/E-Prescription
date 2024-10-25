using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class ProcedureRepository:Repository<Procedure>
    {
        private EPrescriptionDbContext _context;
        public ProcedureRepository(EPrescriptionDbContext context):base(context)
        {
            _context = context;
        }

        public bool IsProcedureNameExist(string procedureName, string initialProcedureName)
        {
            bool isNotExist = true;
            if (procedureName != string.Empty && initialProcedureName == "undefined")
            {
                var isExist = _context.Procedures.Any(x => x.ProcedureName.ToLower().Equals(procedureName.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (procedureName != string.Empty && initialProcedureName != "undefined")
            {
                var isExist = _context.Procedures.Any(x => x.ProcedureName.ToLower() == procedureName.ToLower() && x.ProcedureName.ToLower() != initialProcedureName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }
    }
}
