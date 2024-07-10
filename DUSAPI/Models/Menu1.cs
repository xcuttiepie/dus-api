using System;
using System.Collections.Generic;

namespace DUSAPI.Models
{
    public partial class Menu1
    {
        public int Id { get; set; }
        public int? Menus { get; set; }
        public int? Type { get; set; }
        public int? AssignSubmenu { get; set; }
        public int? UserId { get; set; }
        public int? EmailId { get; set; }
        public string Status { get; set; } = null!;
        public byte IsDelete { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsSelected { get; set; }
    }
}
