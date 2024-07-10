using System;
using System.Collections.Generic;

namespace DUSAPI.Models
{
    public partial class VMenu
    {
        public int Id { get; set; }
        public string? SubMenu { get; set; }
        public string? Type { get; set; }
        public string? AssignParent { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Status { get; set; } = null!;
        public byte IsDelete { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsSelected { get; set; }
    }
}
