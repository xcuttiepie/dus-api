using System;
using System.Collections.Generic;

namespace DUSAPI.Models
{
    public partial class Branching
    {
        public int Id { get; set; }
        public int? BranchCode { get; set; }
        public int? BranchId { get; set; }
        public int? UserId { get; set; }
        public int? EmailId { get; set; }
        public string Status { get; set; } = null!;
        public byte IsDelete { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
