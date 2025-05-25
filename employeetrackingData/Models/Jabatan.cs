using System;
using System.Collections.Generic;

namespace employeetrackingData.Models;

public partial class Jabatan
{
    public int Idjabatan { get; set; }

    public string? NamaJabatan { get; set; }

    public string? Kodejabatan { get; set; }

    public virtual ICollection<Pegawai> Pegawais { get; set; } = new List<Pegawai>();
}
