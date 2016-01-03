using System;
using System.Collections.Generic;

namespace TBA.Models
{
    public class TeamResponse
    {
        public bool IsSuccessful { get; set; }
        public TeamModel Data { get; set; }
        public Exception Exception { get; set; }
    }

    public class TeamListResponse
    {
        public bool IsSuccessful { get; set; }
        public List<TeamModel> Data { get; set; }
        public Exception Exception { get; set; }
    }
}
