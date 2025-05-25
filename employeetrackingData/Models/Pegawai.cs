using System;
using System.Collections.Generic;

namespace employeetrackingData.Models;

public partial class Pegawai
{
    public int Idpegawai { get; set; }

    public string? KodePegawai { get; set; }

    public string? Namapegawai { get; set; }

    public DateOnly? Tanggalmasuk { get; set; }

    public DateOnly? Tanggalberakhir { get; set; }

    public int? Idcabang { get; set; }

    public int? Idjabatan { get; set; }

    public virtual Cabang? IdcabangNavigation { get; set; }

    public virtual Jabatan? IdjabatanNavigation { get; set; }
}
