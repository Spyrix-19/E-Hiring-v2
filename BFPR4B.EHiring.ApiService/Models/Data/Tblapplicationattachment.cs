using System;
using System.Collections.Generic;

namespace BFPR4B.EHiring.ApiService.Models.Data;

public partial class Tblapplicationattachment
{
    public int Attachmentno { get; set; }

    public int Userno { get; set; }

    public int Applicationno { get; set; }

    public int Jobno { get; set; }

    public string Attachmentname { get; set; } = null!;

    public string Attachmenttype { get; set; } = null!;

    public int Statusno { get; set; }

    public string Remarks { get; set; } = null!;

    /// <summary>
    /// PNG, JPEG, PDF, DOCX
    /// </summary>
    public string Filetype { get; set; } = null!;

    public DateTime Datemodified { get; set; }

    public DateTime Dateencoded { get; set; }
}
