using System;
using System.Collections.Generic;

namespace BFPR4B.EHiring.ApiService.Models.Data;

public partial class Tblotp
{
    public int Detno { get; set; }

    public int Userno { get; set; }

    public string Otpcode { get; set; } = null!;

    /// <summary>
    /// &apos;login&apos;, &apos;reset_password&apos;
    /// </summary>
    public string Otptype { get; set; } = null!;

    public DateTime Expiration { get; set; }

    public bool IsUsed { get; set; }

    public string Ipaddress { get; set; } = null!;

    /// <summary>
    /// SMS, Email,
    /// </summary>
    public string Channel { get; set; } = null!;

    public string System { get; set; } = null!;

    public DateTime Dateencoded { get; set; }
}
