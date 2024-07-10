using System;
using System.Collections.Generic;

namespace DUSAPI
{
    public partial class CoreSystem
    {
        public int Id { get; set; }
        public string Debugpassword { get; set; } = null!;
        public int Pismutilizationmonth { get; set; }
        public int Pismutilizationyear { get; set; }
        public string Mobadminpincode { get; set; } = null!;
    }
}
