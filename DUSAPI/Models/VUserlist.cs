using System;
using System.Collections.Generic;

namespace DUSAPI.Models
{
    public partial class VUserlist
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public string? Middlename { get; set; }
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
        public string Status { get; set; } = null!;
        public byte IsDelete { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? UserType { get; set; }
        public string? AccountType { get; set; }
    }
}
