using System;
using System.Collections.Generic;

namespace BFPR4B.EHiring.ApiService.Models.Data;

public partial class Tblapplication
{
    public int Applicationno { get; set; }

    public string Applicationcode { get; set; } = null!;

    public int Jobno { get; set; }

    public int Userno { get; set; }

    public int Statusno { get; set; }

    public string Remarks { get; set; } = null!;

    public DateTime Applicationdate { get; set; }
}
