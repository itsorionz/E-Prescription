using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class UserUnitOfWork : IDisposable
    {
        private EPrescriptionDbContext _context { get; set; }
        private UserRepository _userRepository { get; set; }

        public UserUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
        }

        public UserRepository UserRepository
        {
            get
            {
                return _userRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
