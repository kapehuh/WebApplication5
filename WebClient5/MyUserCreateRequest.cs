using System.Xml.Linq;

namespace WebClient5
{
    public class MyUserCreateRequest
    {
        public MyUserCreateRequest()
        {
        }

        public MyUserCreateRequest(string _MUname, int _MUage, string _MUemail)
        {
            MUname = _MUname;
            MUage = _MUage;
            MUemail = _MUemail;
        }

        public string? MUname { get; init; }

        public int? MUage { get; init; }

        public string? MUemail { get; init; }
    }
}