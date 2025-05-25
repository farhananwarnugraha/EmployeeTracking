using employeetracking.Models;
using employeetracking.Models.Positions;
using employeetrackingBusiness.Interfaces;
using employeetrackingData.Models;
using System.Security.Cryptography.Xml;

namespace employeetracking.Service
{
    public class JabatanService
    {
        private readonly IJabatan _repository;

        public JabatanService(IJabatan repository)
        {
            _repository = repository;
        }

        public JabatansViewModel Get(int pageNumber, int pageSize, string namaJabatan)
        {
            var jabatanModel = _repository.GetJabatan(pageNumber, pageSize, namaJabatan)
                .Select(
                    jab => new JabatanViewModel()
                    {
                        IdJabatan = jab.Idjabatan,
                        KodeJabatan = jab.Kodejabatan ?? throw new Exception("Kode Jabata tidak ada"),
                        NamaJabatan = jab.NamaJabatan ?? throw new Exception("Nama Jabata tidak ada")
                    }
                );
            return new JabatansViewModel()
            {
                ListJabatan = jabatanModel.ToList(),
                Paginations = new PaginatiosViewModel()
                {
                    PageIndex = pageNumber,
                    PageSize = pageSize,
                    TotalRows = _repository.Count(namaJabatan)
                }
            };
        }

        public JabatanRequestViewModel Get(int idJabatan)
        {
            var jabtanById = _repository.GetJabatanById(idJabatan);
            return new JabatanRequestViewModel()
            {
                KodeeJabatan = jabtanById.Kodejabatan ?? throw new Exception(),
                NamaJabatan = jabtanById.NamaJabatan ?? throw new Exception()
            };
        }

        public JabatanRequestViewModel Get()
        {
            return new JabatanRequestViewModel();
        }

        public void AddJabatan(JabatanRequestViewModel jabataRequest)
        {
            var jabatanRequestInsert = new Jabatan()
            {
                Kodejabatan = jabataRequest.KodeeJabatan,
                NamaJabatan = jabataRequest.NamaJabatan
            };
            _repository.Insert(jabatanRequestInsert);
        }

        public void UpdateJabatan(int jabatanId, JabatanRequestViewModel jabatanRequest)
        {
            var modelJabatan = _repository.GetJabatanById(jabatanId);
            modelJabatan.Kodejabatan = jabatanRequest.KodeeJabatan;
            modelJabatan.NamaJabatan = jabatanRequest.NamaJabatan;
            _repository.Update(modelJabatan);
        }

        public void Delete(int jabatanId)
        {
            var jabatan = _repository.GetJabatanById(jabatanId);
            _repository.Delete(jabatan);
        }
    }
}
