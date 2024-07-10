using System;
using System.Collections.Generic;

namespace DUSAPI.Models
{
    public partial class Button
    {
        public int Id { get; set; }
        public int? Menu { get; set; }
        public int UserId { get; set; }
        public int? EmailId { get; set; }
        public string? Status { get; set; }
        public byte? IsAdd { get; set; }
        public byte? IsEdit { get; set; }
        public byte? IsDelete { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
