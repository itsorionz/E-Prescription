using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class RoleUnitOfWork : IDisposable
    {
         private EPrescriptionDbContext _context { get; set; }
        private RoleRepository _roleRepository { get; set; }

        public RoleUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            _roleRepository = new RoleRepository(_context);
        }

        public RoleRepository RoleRepository
        {
            get
            {
                return _roleRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Save(string loggedInUserId)
        {
            _context.SaveChanges(loggedInUserId);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
    public class RoleTaskUnitOfWork : IDisposable
    {
        private EPrescriptionDbContext _context { get; set; }
        private RoleTaskRepository _roleTaskRepository { get; set; }

        public RoleTaskUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            _roleTaskRepository = new RoleTaskRepository(_context);
        }

        public RoleTaskRepository RoleTaskRepository
        {
            get
            {
                return _roleTaskRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

         public void Save(string loggedInUserId)
        {
            _context.SaveChanges(loggedInUserId);
        }

         public void Dispose()
         {
             _context.Dispose();
         }
    }
}
