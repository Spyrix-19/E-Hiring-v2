using System;
using System.Collections.Generic;

namespace BFPR4B.EHiring.ApiService.Models.Data;

public partial class Tblapplicationuser
{
    public int Userno { get; set; }

    public string Userpass { get; set; } = null!;

    public byte[] Passwordsalt { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Miname { get; set; } = null!;

    public string Suffix { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public DateTime Birthdate { get; set; }

    public string Height { get; set; } = null!;

    public string Weight { get; set; } = null!;

    public string Bloodtype { get; set; } = null!;

    public int Religion { get; set; }

    public int Civilstatus { get; set; }

    public int Age { get; set; }

    public int Gender { get; set; }

    public string Emailaddress { get; set; } = null!;

    public bool Isverifiedemail { get; set; }

    public string Contactno { get; set; } = null!;

    public bool Isverifiedcontact { get; set; }

    public string Tin { get; set; } = null!;

    public string Pagibig { get; set; } = null!;

    public string Gsis { get; set; } = null!;

    public string Sss { get; set; } = null!;

    public string Philhealth { get; set; } = null!;

    public string Nbi { get; set; } = null!;

    public DateTime Nbiissueddate { get; set; }

    public string Streetaddress { get; set; } = null!;

    public int Barangayno { get; set; }

    public int Cityno { get; set; }

    public int Provinceno { get; set; }

    public int Regionno { get; set; }

    public string Presentaddress { get; set; } = null!;

    public int Presentbarangayno { get; set; }

    public int Presentcityno { get; set; }

    public int Presentprovinceno { get; set; }

    public int Presentregionno { get; set; }

    public int Course { get; set; }

    public string Profilepictype { get; set; } = null!;

    public string Profileurl { get; set; } = null!;

    public DateTime Dateupdated { get; set; }

    public DateTime Dateencoded { get; set; }
}
