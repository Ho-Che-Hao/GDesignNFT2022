using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIGABYTE.Utility.Dto
{
    public class SmtpMailOutputDto
    {
        public bool Success { set; get; }

        public Exception Exception { set; get; }

        public string Message { set; get; }
    }
}
