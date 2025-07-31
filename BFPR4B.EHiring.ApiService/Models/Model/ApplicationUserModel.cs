using System.ComponentModel.DataAnnotations;

namespace BFPR4B.EHiring.ApiService.Models.Model
{
    public class ApplicationUserModel
    {
        [Key]
        public int Userno { get; set; } = 0;
        public string Lastname { get; set; } = "";
        public string Firstname { get; set; } = "";
        public string Miname { get; set; } = "";
        public string Suffix { get; set; } = "";
        public string Fullname { get; set; } = "";
        public DateTime Birthdate { get; set; } = Convert.ToDateTime("1/1/1900");
        public string Height { get; set; } = "";
        public string Weight { get; set; } = "";
        public string Bloodtype { get; set; } = "";
        public int Religion { get; set; } = 0;
        public string Religionname { get; set; } = "";
        public int Civilstatus { get; set; } = 0;
        public string Civilstatusname { get; set; } = "";
        public int Age { get; set; } = 0;
        public int Gender { get; set; } = 0;
        public string Gendername { get; set; } = "";
        public string Emailaddress { get; set; } = "";
        public bool Isverifiedemail { get; set; } = false;
        public string Contactno { get; set; } = "";
        public bool Isverifiedcontact { get; set; } = false;
        public string Tin { get; set; } = "";
        public string Pagibig { get; set; } = "";
        public string Gsis { get; set; } = "";
        public string Sss { get; set; } = "";
        public string Philhealth { get; set; } = "";
        public string Nbi { get; set; } = "";
        public DateTime Nbiissueddate { get; set; } = Convert.ToDateTime("1/1/1900");
        public string Streetaddress { get; set; } = "";
        public int Barangayno { get; set; } = 0;
        public string Barangayname { get; set; } = "";
        public int Cityno { get; set; } = 0;
        public string Cityname { get; set; } = "";
        public int Provinceno { get; set; } = 0;
        public string Provincename { get; set; } = "";
        public int Regionno { get; set; } = 0;
        public string Regionname { get; set; } = "";
        public string Presentaddress { get; set; } = "";
        public int Presentbarangayno { get; set; } = 0;
        public string Presentbarangayname { get; set; } = "";
        public int Presentcityno { get; set; } = 0;
        public string Presentcityname { get; set; } = "";
        public int Presentprovinceno { get; set; } = 0;
        public string Presentprovincename { get; set; } = "";
        public int Presentregionno { get; set; } = 0;
        public string Presentregionname { get; set; } = "";
        public int Course { get; set; } = 0;
        public string Coursename { get; set; } = "";
        public string Profilepictype { get; set; } = "";
        public string Profileurl { get; set; } = "";
        public DateTime Dateupdated { get; set; } = Convert.ToDateTime("1/1/1900");
        public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
    }
}
