using System;
using System.Collections.Generic;

namespace DUSAPI.Models
{
    public partial class VBranching
    {
        public int Id { get; set; }
        public string? BranchCode { get; set; }
        public string? BranchName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string Status { get; set; } = null!;
        public byte IsDelete { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
