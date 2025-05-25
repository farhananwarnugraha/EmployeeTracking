using ClosedXML.Excel;
using employeetrackingBusiness.Interfaces;
using employeetrackingData.Models;

namespace employeetracking.Service
{
    public class PegawaiImportService
    {
        private readonly IPegawai _pegawaiRepository;

        public PegawaiImportService(IPegawai pegawaiRepository)
        {
            _pegawaiRepository = pegawaiRepository;
        }

        public async Task<int> ImportFileExcel(IFormFile file)
        {
            var listToInsert = new List<Pegawai>();

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using var workBook = new XLWorkbook(stream);
            var workSheet = workBook.Worksheet(1);
            var rows = workSheet.RangeUsed().RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                var kode = row.Cell(1).GetString();
                var name = row.Cell(2).GetString();
                var jabtan = row.Cell(3).GetValue<int>();
                var cabang = row.Cell(4).GetValue<int>();
                var tanggalMasuk = row.Cell(5).GetDateTime();
                var tanggalBerakhir = row.Cell(6).GetDateTime();

                if (!string.IsNullOrWhiteSpace(kode) && !string.IsNullOrEmpty(name))
                {
                    if (!_pegawaiRepository.Get().Any(p => p.KodePegawai == kode))
                    {
                        listToInsert.Add(new Pegawai
                        {
                            KodePegawai = kode,
                            Namapegawai = name,
                            Idjabatan  = jabtan,
                            Idcabang =cabang,
                            Tanggalmasuk = DateOnly.FromDateTime(tanggalMasuk),
                            Tanggalberakhir = DateOnly.FromDateTime(tanggalBerakhir)
                        });
                    }
                }
            }

            foreach (var pegawai in listToInsert)
            {
                _pegawaiRepository.Insert(pegawai);
            }

            return listToInsert.Count();
        }
    }
}
