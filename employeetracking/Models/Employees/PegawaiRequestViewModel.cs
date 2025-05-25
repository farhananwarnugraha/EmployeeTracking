using employeetracking.Models.Branches;
using employeetracking.Models.Positions;
using employeetrackingData.Models;

namespace employeetracking.Models.Employees
{
    public class PegawaiRequestViewModel
    {
        public string KodePegawai { get; set; } = null!;
        public string NamaPegawai { get; set; } = null!;
        public int? JabatanPegawai { get; set; }
        public int? CabangPegawai { get; set; }
        public DateOnly? TanggalMasuk { get; set; }
        public DateOnly? TanggalBerakhir { get; set; }

        public List<JabatanViewModel>? ListJabatan { get; set; }
        public List<CabangViewModel>? ListCabang { get; set; }
    }
}
