namespace BFPR4B.EHiring.ApiService.Models.DTO
{
    public class LoginRequestDTO
    {
        public string Username { get; set; } = "";
        public string Userpass { get; set; } = "";
        public string Otpcode { get; set; } = "";
        public string Client_id { get; set; } = "";
        public string Client_secret { get; set; } = "";
        public string Grant_type { get; set; } = "";
        public string Login_type { get; set; } = "";
    }

    public class RegistrationRequestDTO
    {
        public string Lastname { get; set; } = "";
        public string Firstname { get; set; } = "";
        public string Miname { get; set; } = "";
        public string Suffix { get; set; } = "";
        public string Mobileno { get; set; } = "";
        public string Emailadd { get; set; } = "";
        public DateTime Birthdate { get; set; } = Convert.ToDateTime("1/1/1900");
        public string Password { get; set; } = "";
    }
}
