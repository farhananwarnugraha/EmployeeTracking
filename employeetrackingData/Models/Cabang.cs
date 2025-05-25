using System;
using System.Collections.Generic;

namespace employeetrackingData.Models;

public partial class Cabang
{
    public int Idcabang { get; set; }

    public string? Namacabang { get; set; }

    public string? Alamatcabang { get; set; }

    public string? Kodecabang { get; set; }

    public virtual ICollection<Pegawai> Pegawais { get; set; } = new List<Pegawai>();
}
