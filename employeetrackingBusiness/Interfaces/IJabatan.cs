using System;
using employeetrackingData.Models;

namespace employeetrackingBusiness.Interfaces;

public interface IJabatan
{
    List<Jabatan> GetAllJabatan();
    List<Jabatan> GetJabatan(int pageNumber, int pageSize, string namaJabatan);
    Jabatan GetJabatanById(int jabatanId);
    int Count(string namajabatan);
    int Count();
    void Insert(Jabatan jabatan);
    void Update(Jabatan jabatan);
    void Delete(Jabatan jabatan);
}
