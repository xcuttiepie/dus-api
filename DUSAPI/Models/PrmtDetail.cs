using System;
using System.Collections.Generic;

namespace DUSAPI.Models
{
    public partial class PrmtDetail
    {
        public int Id { get; set; }
        public int? TransactionId { get; set; }
        public string? Document { get; set; }
        public DateTime? DateUploaded { get; set; }
        public string? FullDetails { get; set; }
        public string? Status { get; set; }
        public byte IsDelete { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? Branch { get; set; }
    }
}
