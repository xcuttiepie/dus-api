using System;
using System.Collections.Generic;

namespace DUSAPI.Models
{
    public partial class VDocument
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Status { get; set; } = null!;
        public byte IsDelete { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
