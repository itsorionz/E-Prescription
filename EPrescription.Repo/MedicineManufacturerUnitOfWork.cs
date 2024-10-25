using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class MedicineManufacturerUnitOfWork : IDisposable
    {
        private EPrescriptionDbContext _context;
        private MedicineManufacturerRepository manufacturerRepository;
        public MedicineManufacturerUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            manufacturerRepository = new MedicineManufacturerRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public MedicineManufacturerRepository MedicineManufacturerRepository
        {
            get
            {
                return manufacturerRepository;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
