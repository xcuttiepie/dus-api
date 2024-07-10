using System;
using System.Collections.Generic;

namespace DUSAPI.Models
{
    public partial class Branch
    {
        public int Id { get; set; }
        public string? Branchcode { get; set; }
        public string? Branchname { get; set; }
        public string? BranchAddress { get; set; }
        public string Status { get; set; } = null!;
        public byte IsDelete { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
