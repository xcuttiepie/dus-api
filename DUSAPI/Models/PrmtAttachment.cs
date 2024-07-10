using System;
using System.Collections.Generic;

namespace DUSAPI.Models
{
    public partial class PrmtAttachment
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public string? Filename { get; set; }
        public string? NewFilename { get; set; }
        public string? Filepath { get; set; }
        public byte IsDelete { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? Branch { get; set; }
    }
}
