using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FoxMoney.Server.Entities {
    public class ApplicationUser : IdentityUser<int> {
        public bool IsEnabled { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [StringLength(250)]
        public string FirstName { get; set; }
        [StringLength(250)]
        public string LastName { get; set; }
        [NotMapped]
        public string Name {
            get {
                return this.FirstName + " " + this.LastName;
            }
        }
    }
}