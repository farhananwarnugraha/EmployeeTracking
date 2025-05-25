using DocumentFormat.OpenXml.Wordprocessing;
using employeetracking.Models;
using employeetracking.Models.Branches;
using employeetracking.Models.Employees;
using employeetracking.Models.Positions;
using employeetrackingBusiness.Interfaces;
using employeetrackingData.Models;

namespace employeetracking.Service
{
    public class PegawaiService
    {
        private readonly IPegawai _repository;
        private readonly IJabatan _jabatanRepo;
        private readonly ICabang _cabangRepo;

        public PegawaiService(IPegawai repository, IJabatan jabatanRepo, ICabang cabangRepo)
        {
            _repository = repository;
            _jabatanRepo = jabatanRepo;
            _cabangRepo = cabangRepo;
        }

        public PegawaisViewModel Get(int pageNumber, int pageSize, string namaPegawai)
        {
            var pegawai = _repository.Get(pageNumber, pageSize, namaPegawai)
                .Select(
                    p => new PegawaiViewModel()
                    {
                        IdPegawai = p.Idpegawai,
                        KodePegawai = p.KodePegawai ?? throw new Exception(""),
                        NamaPegawai = p.Namapegawai ?? throw new Exception(""),
                        Jabatan = p.IdjabatanNavigation?.NamaJabatan ?? throw new Exception("N/A"),
                        Cabang = p.IdcabangNavigation?.Namacabang ?? throw new Exception("N/A"),
                        TanggalMasuk = p.Tanggalmasuk.ToString(),
                        TanggalBerakhir = p.Tanggalberakhir.ToString()
                    }
                );
            return new PegawaisViewModel()
            {
                ListPegawai = pegawai.ToList(),
                Paginations = new PaginatiosViewModel()
                {
                    PageIndex = pageNumber,
                    PageSize = pageSize,
                    TotalRows = _repository.Count(namaPegawai)
                },
                NamaPegawai = namaPegawai
            };
        }

        public PegawaiRequestViewModel Get()
        {
            return new PegawaiRequestViewModel()
            {
                ListJabatan = _jabatanRepo.GetAllJabatan()
                .Select(jab => new JabatanViewModel()
                {
                    IdJabatan = jab.Idjabatan,
                    KodeJabatan = jab.Kodejabatan??"N/A",
                    NamaJabatan = jab.NamaJabatan ?? "N/A"
                }).ToList(),
                ListCabang = _cabangRepo.Get()
                .Select(cab => new CabangViewModel()
                {
                    IdCabang = cab.Idcabang,
                    KodeCabang = cab.Kodecabang ?? "N/A",
                    NamaCabang = cab.Namacabang ?? "N/A"
                }).ToList()
            };
        }

        public void Insert(PegawaiRequestViewModel insertRequest)
        {
            var viewModel = new Pegawai()
            {
                KodePegawai = insertRequest.KodePegawai,
                Namapegawai = insertRequest.NamaPegawai,
                Idjabatan = insertRequest.JabatanPegawai,
                Idcabang = insertRequest.CabangPegawai,
                Tanggalmasuk = insertRequest.TanggalMasuk,
                Tanggalberakhir = insertRequest.TanggalBerakhir
            };

            _repository.Insert(viewModel);
        }

        public PegawaiRequestViewModel Get(int id)
        {
            var model = _repository.Get(id);
            var jabatan = _jabatanRepo.GetAllJabatan().Select(jab => new JabatanViewModel()
            {
                IdJabatan = jab.Idjabatan,
                KodeJabatan = jab.Kodejabatan ?? "N/A",
                NamaJabatan = jab.NamaJabatan ?? "N/A"
            }).ToList();
            var cabang = _cabangRepo.Get().Select(cab => new CabangViewModel()
            {
                IdCabang = cab.Idcabang,
                KodeCabang = cab.Kodecabang ?? "N/A",
                NamaCabang = cab.Namacabang ?? "N/A"
            }).ToList();
            return new PegawaiRequestViewModel()
            {
                KodePegawai = model.KodePegawai ?? "N/A",
                NamaPegawai = model.Namapegawai ?? "N/A",
                JabatanPegawai = model.Idjabatan,
                CabangPegawai = model.Idcabang,
                TanggalMasuk = model.Tanggalmasuk,
                TanggalBerakhir = model.Tanggalberakhir,
                ListJabatan = jabatan,
                ListCabang = cabang
            };
        }

        public void Update(int id, PegawaiRequestViewModel updateRequest)
        {
            var model = _repository.Get(id);
            model.KodePegawai = updateRequest.KodePegawai;
            model.Namapegawai = updateRequest.NamaPegawai;
            model.Idjabatan = updateRequest.JabatanPegawai;
            model.Idcabang = updateRequest.CabangPegawai;
            model.Tanggalmasuk = updateRequest.TanggalMasuk;
            model.Tanggalberakhir = updateRequest.TanggalBerakhir;
            _repository.Update(model);
        }

        public void Delete(int id)
        {
            var model = _repository.Get(id);
            if(model != null)
            {
                _repository.Delete(model);
            }
        }
    }
}
