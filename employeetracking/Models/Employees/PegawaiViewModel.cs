namespace employeetracking.Models.Employees
{
    public class PegawaiViewModel
    {
        public int IdPegawai { get; set; }
        public string KodePegawai { get; set; }
        public string NamaPegawai { get; set; } = null!;
        public string Jabatan { get; set; } = null!;
        public string Cabang { get; set; } = null!;
        public string TanggalMasuk { get; set; } = null!;
        public string TanggalBerakhir { get; set; } = null!;
    }
}
