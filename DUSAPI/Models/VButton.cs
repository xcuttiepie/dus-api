using System;
using System.Collections.Generic;

namespace DUSAPI.Models
{
    public partial class VButton
    {
        public int Id { get; set; }
        public string? Menu { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public byte? IsAdd { get; set; }
        public byte? IsEdit { get; set; }
        public byte? IsDelete { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
