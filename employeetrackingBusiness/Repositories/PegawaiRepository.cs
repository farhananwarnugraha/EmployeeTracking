using employeetrackingBusiness.Interfaces;
using employeetrackingData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employeetrackingBusiness.Repositories
{
    public class PegawaiRepository : IPegawai
    {
        private readonly EmployeeTrackingContext _context;

        public PegawaiRepository(EmployeeTrackingContext context)
        {
            _context = context;
        }

        public int Count()
        {
            return _context.Pegawais.Count();
        }

        public int Count(string namaPegawai)
        {
            return _context.Pegawais.Where(pegawai => pegawai.Namapegawai.ToLower().Contains(namaPegawai ?? "".ToLower())).Count();
        }

        public void Delete(Pegawai pegawai)
        {
            _context.Pegawais.Remove(pegawai);
            _context.SaveChanges();
        }

        public List<Pegawai> Get()
        {
            return _context.Pegawais.ToList();
        }

        public List<Pegawai> Get(int pageNumber, int pageSize, string namaPegawai)
        {
            return _context.Pegawais
                .Where(pegawai => pegawai.Namapegawai.ToLower().Contains(namaPegawai ?? "".ToLower()))
                .Include(pegawai => pegawai.IdcabangNavigation)
                .Include(pegawai => pegawai.IdjabatanNavigation)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Pegawai Get(int idPegawai)
        {
            return _context.Pegawais.FirstOrDefault(pegawai => pegawai.Idpegawai == idPegawai) ?? throw new Exception("Pegawai Tidak ada");
        }

        public void Insert(Pegawai pegawai)
        {
            _context.Pegawais.Add(pegawai);
            _context.SaveChanges();
        }

        public void Update(Pegawai pegawai)
        {
            _context.Pegawais.Update(pegawai);
            _context.SaveChanges();
        }
    }
}
