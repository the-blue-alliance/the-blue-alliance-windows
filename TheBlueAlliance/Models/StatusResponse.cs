using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBA.Models
{
    public class StatusResponse
    {
        public bool IsSuccessful { get; set; }
        public StatusModel Data { get; set; }
        public Exception Exception { get; set; }
    }
}
