using ClosedXML.Excel;
using employeetrackingBusiness.Interfaces;
using employeetrackingData.Models;
using Microsoft.AspNetCore.Http;

namespace employeetracking.Service
{
    public class JabtanInportService
    {
        private readonly IJabatan _jabatanRepos;

        public JabtanInportService(IJabatan jabatanRepos)
        {
            _jabatanRepos = jabatanRepos;
        }

        public async Task<int> ImportFileExcel(IFormFile file)
        {
            var listToInsert = new List<Jabatan>();

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using var workBook = new XLWorkbook(stream);
            var workSheet = workBook.Worksheet(1);
            var rows = workSheet.RangeUsed().RowsUsed().Skip(1);

            foreach(var row in rows)
            {
                var name = row.Cell(1).GetString();
                var kode = row.Cell(2).GetString();

                if(!string.IsNullOrWhiteSpace(kode) && !string.IsNullOrEmpty(name))
                {
                    if(!_jabatanRepos.GetAllJabatan().Any(j => j.Kodejabatan == kode))
                    {
                        listToInsert.Add(new Jabatan
                        {
                            NamaJabatan = name,
                            Kodejabatan = kode
                        });
                    }
                }
            }

            foreach(var jab in listToInsert)
            {
                _jabatanRepos.Insert(jab);
            }

            return listToInsert.Count();
        }
    }
}
