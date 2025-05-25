using employeetrackingBusiness.Interfaces;
using employeetrackingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employeetrackingBusiness.Repositories
{
    public class CabangRepository : ICabang
    {
        private readonly EmployeeTrackingContext _context;

        public CabangRepository(EmployeeTrackingContext context)
        {
            _context = context;
        }

        public void AddCabang(Cabang cabang)
        {
            _context.Cabangs.Add(cabang);
            _context.SaveChanges();
        }

        public int Count(string namaCabang)
        {
            return _context.Cabangs.Where(cabang => cabang.Namacabang.ToLower().Contains(namaCabang ?? "".ToLower())).Count();
        }

        public int Count()
        {
            return _context.Cabangs.Count();
        }

        public void DeleteCabang(Cabang cabang)
        {
            _context.Cabangs.Remove(cabang);
            _context.SaveChanges();
        }

        public List<Cabang> Get(int pageNumber, int pageSize, string namaCabang)
        {
            return _context.Cabangs
                .Where(
                    cabang => cabang.Namacabang.ToLower().Contains(namaCabang ?? "".ToLower())
                )
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Cabang Get(int idCabang)
        {
            return _context.Cabangs.Where(cab => cab.Idcabang == idCabang).FirstOrDefault() ?? throw new Exception("Cabang Tidak ditemukan");
        }

        public List<Cabang> Get()
        {
            return _context.Cabangs.ToList();
        }

        public void UpdateCabang(Cabang cabang)
        {
            _context.Cabangs.Update(cabang);
            _context.SaveChanges();
        }
    }
}
