using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIGABYTE.Utility.Dto
{
    public class SmtpMailInputDto
    {
        public string Subject { set; get; }

        public string BodyContent { set; get; }

        public List<string> ToMails { set; get; }

        public List<string> CCToMails { set; get; }

        public List<string> BCCToMails { set; get; }
    }
}
