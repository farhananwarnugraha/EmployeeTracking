using employeetrackingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employeetrackingBusiness.Interfaces
{
    public interface ICabang
    {
        List<Cabang> Get();
        int Count();
        List<Cabang> Get(int pageNumber, int pageSize, string namaCabang);
        int Count(string namaCabang);
        Cabang Get(int idCabang);
        void AddCabang(Cabang cabang);
        void UpdateCabang(Cabang cabang);
        void DeleteCabang(Cabang cabang);
    }
}
