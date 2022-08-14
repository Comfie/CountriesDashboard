using System;
using System.Collections.Generic;

namespace CountryDashboard.Models
{
    public partial class Countries
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
    }
}
