using employeetracking.Models;
using employeetracking.Models.Branches;
using employeetracking.Models.Positions;
using employeetrackingBusiness.Interfaces;
using employeetrackingData.Models;

namespace employeetracking.Service
{
    public class CabangService
    {
        private readonly ICabang _cabangRepository;

        public CabangService(ICabang cabangRepository)
        {
            _cabangRepository = cabangRepository;
        }

        public CabangsViewModel Get(int pageNumber, int pageSize, string namaCabang)
        {
            var cabangs = _cabangRepository.Get(pageNumber, pageSize, namaCabang)
                .Select(
                    cab => new CabangViewModel()
                    {
                       IdCabang = cab.Idcabang,
                       KodeCabang = cab.Kodecabang ?? throw new Exception("N/A"),
                       NamaCabang = cab.Namacabang ?? throw new Exception("N/A"),
                       AlamatCabang = cab.Alamatcabang ?? throw new Exception("N/A")
                    }
                );
            return new CabangsViewModel()
            {
                ListCabang = cabangs.ToList(),
                Paginatios = new PaginatiosViewModel()
                {
                    PageIndex = pageNumber,
                    PageSize = pageSize,
                    TotalRows = _cabangRepository.Count(namaCabang)
                }
            };
        }

        public CabangRequestViewModel Get(int idCabang)
        {
            var cabangById = _cabangRepository.Get(idCabang);
            return new CabangRequestViewModel()
            {
                KodeCabang = cabangById.Kodecabang ?? throw new Exception(""),
                NamaCabang = cabangById.Namacabang ?? throw new Exception(""),
                AlamatCabang = cabangById.Alamatcabang ?? throw new Exception("")
            };
        }

        public CabangRequestViewModel Get()
        {
            return new CabangRequestViewModel();
        }

        public void AddCabang(CabangRequestViewModel cabangModelInsert)
        {
            var newCabang = new Cabang()
            {
                Kodecabang = cabangModelInsert.KodeCabang,
                Namacabang = cabangModelInsert.NamaCabang,
                Alamatcabang = cabangModelInsert.AlamatCabang,
            };

            _cabangRepository.AddCabang(newCabang);
        }

        public void UpdateCabang(int idCabang, CabangRequestViewModel cabangViewModelUpdate)
        {
            var modelCabang = _cabangRepository.Get(idCabang);
            modelCabang.Kodecabang = cabangViewModelUpdate.KodeCabang;
            modelCabang.Namacabang = cabangViewModelUpdate.NamaCabang;
            _cabangRepository.UpdateCabang(modelCabang);
        }

        public void DeleteCabang(int idCabang)
        {
            var cabang = _cabangRepository.Get(idCabang);
            if(cabang != null)
            {
                _cabangRepository.DeleteCabang(cabang);
            }
        }
    }
}
