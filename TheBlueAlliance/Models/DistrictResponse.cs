using System;
using System.Collections.Generic;

namespace TBA.Models
{
    public class DistrictResponse
    {
        public bool IsSuccessful { get; set; }
        public DistrictListModel Data { get; set; }
        public Exception Exception { get; set; }
    }

    public class DistrictListResponse
    {
        public bool IsSuccessful { get; set; }
        public List<DistrictListModel> Data { get; set; }
        public Exception Exception { get; set; }
        public string Year { get; set; }
    }
}
