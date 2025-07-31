using System.Net;

namespace BFPR4B.EHiring.ApiService.Helpers.Utility
{
    public class Utils
    {
        public static string DesignMemberNameLF(string honorific, string firstname, string miname, string lastname, string suffix)
        {
            string _membername = "";
            string _honorific = string.IsNullOrEmpty(honorific) ? "" : honorific;
            string _firstname = string.IsNullOrEmpty(firstname) ? "" : firstname;
            string _miname = string.IsNullOrEmpty(miname) ? "" : miname;
            string _lastname = string.IsNullOrEmpty(lastname) ? "" : lastname;
            string _suffix = string.IsNullOrEmpty(suffix) ? "" : suffix;

            if (!string.IsNullOrEmpty(_miname) && !string.IsNullOrEmpty(_suffix))
            {
                _membername = (_lastname.Trim().ToUpper() == "N/A" ? "" : _lastname.Trim()) + " " + (_suffix.Trim().ToUpper() == "N/A" ? "" : _suffix.Trim()) + ", " + (_firstname.Trim().ToUpper() == "N/A" ? "" : _firstname.Trim()) + " " + (_miname.Trim().ToUpper() == "N/A" ? "" : _miname.Trim() + (_miname.Length.ToString() == "1" ? " " : " "));
            }
            else if (!string.IsNullOrEmpty(_miname) && string.IsNullOrEmpty(_suffix))
            {
                _membername = (_lastname.Trim().ToUpper() == "N/A" ? "" : _lastname.Trim()) + ", " + (_firstname.Trim().ToUpper() == "N/A" ? "" : _firstname.Trim()) + " " + (_miname.Trim().ToUpper() == "N/A" ? "" : _miname.Trim() + (_miname.Length.ToString() == "1" ? " " : " "));
            }
            else if (string.IsNullOrEmpty(_miname) && !string.IsNullOrEmpty(_suffix))
            {
                _membername = (_lastname.Trim().ToUpper() == "N/A" ? "" : _lastname.Trim()) + " " + (_suffix.Trim().ToUpper() == "N/A" ? "" : _suffix.Trim()) + ", " + (_firstname.Trim().ToUpper() == "N/A" ? "" : _firstname.Trim());
            }
            else
            {
                _membername = (_lastname.Trim().ToUpper() == "N/A" ? "" : _lastname.Trim()) + ", " + (_firstname.Trim().ToUpper() == "N/A" ? "" : _firstname.Trim());
            }

            if (!string.IsNullOrEmpty(_honorific))
            {
                _membername = _honorific + _membername;
            }

            return _membername;
        }

        public static string DesignMemberNamePersonnel(int rankno, string honorific, string firstname, string miname, string lastname, string suffix)
        {
            string _membername = "";
            string _honorific = string.IsNullOrEmpty(honorific) ? "" : honorific.Trim();
            string _firstname = string.IsNullOrEmpty(firstname) ? "" : firstname.Trim();
            string _miname = string.IsNullOrEmpty(miname) ? "" : miname.Trim();
            string _lastname = string.IsNullOrEmpty(lastname) ? "" : lastname.Trim();
            string _suffix = string.IsNullOrEmpty(suffix) ? "" : suffix.Trim();

            if (!string.IsNullOrEmpty(_miname) && !string.IsNullOrEmpty(_suffix))
            {
                _membername = $"{(_firstname.Trim().ToUpper() == "N/A" ? "" : _firstname.Trim())} {(_miname.Trim().ToUpper() == "N/A" ? "" : _miname.Trim())} {(_lastname.Trim().ToUpper() == "N/A" ? "" : _lastname.Trim())} {(_suffix.Trim().ToUpper() == "N/A" ? "" : _suffix.Trim())}";
            }
            else if (!string.IsNullOrEmpty(_miname) && string.IsNullOrEmpty(_suffix))
            {
                _membername = $"{(_firstname.Trim().ToUpper() == "N/A" ? "" : _firstname.Trim())} {(_miname.Trim().ToUpper() == "N/A" ? "" : _miname.Trim())} {(_lastname.Trim().ToUpper() == "N/A" ? "" : _lastname.Trim())}";
            }
            else if (string.IsNullOrEmpty(_miname) && !string.IsNullOrEmpty(_suffix))
            {
                _membername = $"{(_firstname.Trim().ToUpper() == "N/A" ? "" : _firstname.Trim())} {(_lastname.Trim().ToUpper() == "N/A" ? "" : _lastname.Trim())} {(_suffix.Trim().ToUpper() == "N/A" ? "" : _suffix.Trim())}";
            }
            else
            {
                _membername = $"{(_firstname.Trim().ToUpper() == "N/A" ? "" : _firstname.Trim())} {(_lastname.Trim().ToUpper() == "N/A" ? "" : _lastname.Trim())}";
            }

            if (!string.IsNullOrEmpty(_honorific))
            {
                _membername = $"{_honorific} {_membername}";
            }

            // if officer.. upper case
            if (rankno >= 1 && rankno <= 7)
            {
                return _membername.ToUpper();
            }
            else
            {
                return CapitalizeWords(_membername); // else Title Case pag NOR
            }
        }

        public static string DesignMemberNameFL(int rankno, string honorific, string firstname, string miname, string lastname, string suffix)
        {
            string _membername = "";
            string _honorific = string.IsNullOrEmpty(honorific) ? "" : honorific.Trim();
            string _firstname = string.IsNullOrEmpty(firstname) ? "" : firstname.Trim();
            string _miname = string.IsNullOrEmpty(miname) ? "" : miname.Trim().ToUpper().Substring(0, 1);
            string _lastname = string.IsNullOrEmpty(lastname) ? "" : lastname.Trim();
            string _suffix = string.IsNullOrEmpty(suffix) ? "" : suffix.Trim();

            if (!string.IsNullOrEmpty(_miname) && !string.IsNullOrEmpty(_suffix))
            {
                _membername = $"{(_firstname.Trim().ToUpper() == "N/A" ? "" : _firstname.Trim())} {(_miname.Trim().ToUpper() == "N/A" ? "" : _miname.Trim())} {(_lastname.Trim().ToUpper() == "N/A" ? "" : _lastname.Trim())} {(_suffix.Trim().ToUpper() == "N/A" ? "" : _suffix.Trim())}";
            }
            else if (!string.IsNullOrEmpty(_miname) && string.IsNullOrEmpty(_suffix))
            {
                _membername = $"{(_firstname.Trim().ToUpper() == "N/A" ? "" : _firstname.Trim())} {(_miname.Trim().ToUpper() == "N/A" ? "" : _miname.Trim())} {(_lastname.Trim().ToUpper() == "N/A" ? "" : _lastname.Trim())}";
            }
            else if (string.IsNullOrEmpty(_miname) && !string.IsNullOrEmpty(_suffix))
            {
                _membername = $"{(_firstname.Trim().ToUpper() == "N/A" ? "" : _firstname.Trim())} {(_lastname.Trim().ToUpper() == "N/A" ? "" : _lastname.Trim())} {(_suffix.Trim().ToUpper() == "N/A" ? "" : _suffix.Trim())}";
            }
            else
            {
                _membername = $"{(_firstname.Trim().ToUpper() == "N/A" ? "" : _firstname.Trim())} {(_lastname.Trim().ToUpper() == "N/A" ? "" : _lastname.Trim())}";
            }

            if (!string.IsNullOrEmpty(_honorific))
            {
                _membername = $"{_honorific} {_membername}";
            }

            // if officer.. upper case
            if (rankno >= 1 && rankno <= 7)
            {
                return _membername.ToUpper();
            }
            else
            {
                return CapitalizeWords(_membername); // else Title Case pag NOR
            }
        }

        // Title Case ang word
        public static string CapitalizeWords(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }
            return string.Join(' ', words);
        }

        public static string CheckFileURL(string publicurl, string localurl)
        {

            bool validurl;
            string url;

            try
            {
                HttpWebRequest request = WebRequest.Create(localurl) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                validurl = true;
            }
            catch
            {
                validurl = false;
            }

            if (validurl == true)
            {
                url = publicurl;
            }
            else
            {
                url = publicurl;
            }

            return url;
        }

        //compate and ate as year
        public static int ComputeAgeYear(DateTime birthday)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;


            if (birthday > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        // year month days ang computation
        public static (int Years, int Months, int Days) ComputeAgeDetailed(DateTime birthday)
        {
            DateTime today = DateTime.Today;

            int years = today.Year - birthday.Year;
            int months = today.Month - birthday.Month;
            int days = today.Day - birthday.Day;

            if (months < 0)
            {
                years--;
                months += 12;
            }

            if (days < 0)
            {
                months--;
                DateTime previousMonth = today.AddMonths(-1);
                days += DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month);
            }

            return (Years: years, Months: months, Days: days);
        }

        public static string CheckFileURL2(string publicurl, string localurl)
        {
            string _l = CleanPath(localurl);

            if (File.Exists(_l))
            {
                return publicurl;
            }
            else
            {

            }

            return "";
        }

        public static string CleanPath(string path)
        {
            // Replace any instance of two backslashes with a single backslash
            return path.Replace("\\\\", "\\");
        }

        public static string CovertBase64toFile(string base64, string filename, string filetype, string filepath)
        {

            if (string.IsNullOrEmpty(base64) || string.IsNullOrEmpty(filename) || string.IsNullOrEmpty(filetype) || string.IsNullOrEmpty(filepath))
            {
                return string.Empty;
            }

            string newFileName = string.Empty;

            try
            {
                byte[] fileBytes = Convert.FromBase64String(base64);
                string extension = filetype.Trim().ToLower();
                newFileName = $"{filename.Trim()}.{extension}";
                string fullPath = Path.Combine(filepath.Trim(), newFileName);

                File.WriteAllBytesAsync(fullPath, fileBytes);
            }
            catch (FormatException ex)
            {
                // Handle the exception (e.g., log it)
                throw new InvalidOperationException("Invalid base64 string.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions (e.g., log it)
                throw new InvalidOperationException("An error occurred while saving the file.", ex);
            }

            return newFileName;
        }

        public static dynamic calculateImageSize(string base64)
        {
            var padding = 0;
            var base64StringLength = 0;

            if (base64.EndsWith("==")) { padding = 2; }
            else if (base64.EndsWith("=")) { padding = 1; }
            else { padding = 0; }

            base64StringLength = base64.Length;
            var inBytes = base64StringLength / 4 * 3 - padding;
            return inBytes / 1000;
        }

        public static string GenerateUniqueString()
        {
            // Get the current timestamp in the format YYYYMMDDHHmmSS
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Generate 5 random alphanumeric characters
            string randomAlphanumeric1 = GenerateRandomAlphanumeric(5);

            // Generate another 5 random alphanumeric characters
            string randomAlphanumeric2 = GenerateRandomAlphanumeric(5);

            // Concatenate the timestamp and the random characters
            return $"{timestamp}-{randomAlphanumeric1}-{randomAlphanumeric2}";
        }

        private static string GenerateRandomAlphanumeric(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
