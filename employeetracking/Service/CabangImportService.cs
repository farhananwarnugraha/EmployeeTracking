using ClosedXML.Excel;
using employeetrackingBusiness.Interfaces;
using employeetrackingData.Models;

namespace employeetracking.Service
{
    public class CabangImportService
    {
        private readonly ICabang _cabangRepository;

        public CabangImportService(ICabang cabangRepository)
        {
            _cabangRepository = cabangRepository;
        }

        public async Task<int> ImportFileExcel(IFormFile file)
        {
            var listToInsert = new List<Cabang>();

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using var workBook = new XLWorkbook(stream);
            var workSheet = workBook.Worksheet(1);
            var rows = workSheet.RangeUsed().RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                var kode = row.Cell(1).GetString();
                var name = row.Cell(2).GetString();
                var address = row.Cell(3).GetString();

                if (!string.IsNullOrWhiteSpace(kode) && !string.IsNullOrEmpty(name))
                {
                    if (!_cabangRepository.Get().Any(cab => cab.Kodecabang == kode))
                    {
                        listToInsert.Add(new Cabang
                        {
                            Kodecabang = kode,
                            Namacabang = name,
                            Alamatcabang = address
                        });
                    }
                }
            }

            foreach (var cabang in listToInsert)
            {
                _cabangRepository.AddCabang(cabang);
            }

            return listToInsert.Count();
        }
    }
}
