using System;
using System.Collections.Generic;

namespace DUSAPI.Models
{
    public partial class VPrmtAttachment1
    {
        public int Id { get; set; }
        public int? TransactionId { get; set; }
        public string? Filename { get; set; }
        public string? NewFilename { get; set; }
        public byte IsDelete { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Branch { get; set; }
    }
}
