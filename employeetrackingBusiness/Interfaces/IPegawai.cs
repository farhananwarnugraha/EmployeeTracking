using employeetrackingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employeetrackingBusiness.Interfaces
{
    public interface IPegawai
    {
        List<Pegawai> Get();
        List<Pegawai> Get(int pageNumber, int pageSize, string namaPegawai);
        int Count();
        int Count(string namaPegawai);
        Pegawai Get(int idPegawai);
        void Insert(Pegawai pegawai);
        void Update(Pegawai pegawai);
        void Delete(Pegawai pegawai);
    }
}
