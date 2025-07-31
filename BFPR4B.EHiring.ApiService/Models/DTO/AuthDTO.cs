using System.ComponentModel.DataAnnotations;

namespace BFPR4B.EHiring.ApiService.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        public string Username { get; set; } = "";
        [Required]
        public string Userpass { get; set; } = "";
        public string Otpcode { get; set; } = "";
        public string TempToken { get; set; } = "";
        [Required]
        public string Client_id { get; set; } = "";
        [Required]
        public string Client_secret { get; set; } = "";
        [Required]
        public string Grant_type { get; set; } = "";
        [Required]
        public string Login_type { get; set; } = "";
    }

    public class RegistrationRequestDTO
    {
        [Required]
        public string Lastname { get; set; } = "";
        [Required]
        public string Firstname { get; set; } = "";
        public string Miname { get; set; } = "";
        public string Suffix { get; set; } = "";
        [Required]
        public string Mobileno { get; set; } = "";
        [Required]
        public string Emailadd { get; set; } = "";
        [Required]
        public DateTime Birthdate { get; set; } = Convert.ToDateTime("1/1/1900");
        [Required]
        public string Password { get; set; } = "";
    }
}
