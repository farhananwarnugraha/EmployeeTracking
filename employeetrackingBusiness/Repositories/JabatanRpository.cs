using System;
using employeetrackingBusiness.Interfaces;
using employeetrackingData.Models;

namespace employeetrackingBusiness.Repositories;

public class JabatanRpository : IJabatan
{
    private readonly EmployeeTrackingContext _context;

    public JabatanRpository(EmployeeTrackingContext context)
    {
        _context = context;
    }

    public int Count(string namajabatan)
    {
        return _context.Jabatans.Where(jabatan => jabatan.NamaJabatan.ToLower().Contains(namajabatan ?? "".ToLower())).Count();
    }

    public int Count()
    {
        return _context.Jabatans.Count();
    }

    public void Delete(Jabatan jabatan)
    {
        _context.Remove(jabatan);
        _context.SaveChanges();
    }

    public List<Jabatan> GetAllJabatan()
    {
        return _context.Jabatans.ToList();
    }

    public List<Jabatan> GetJabatan(int pageNumber, int pageSize, string namaJabatan)
    {
        return _context.Jabatans
            .Where(
                jabatan => jabatan.NamaJabatan.ToLower().Contains(namaJabatan ?? "".ToLower())
            )
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public Jabatan GetJabatanById(int jabatanId)
    {
        return _context.Jabatans.Where(jabatan => jabatan.Idjabatan == jabatanId).FirstOrDefault() ?? throw new Exception("Tidak ada jabatan");
    }

    public void Insert(Jabatan jabatan)
    {
        _context.Jabatans.Add(jabatan);
        _context.SaveChanges();
    }

    public void Update(Jabatan jabatan)
    {
        _context.Update(jabatan);
        _context.SaveChanges();
    }
}
