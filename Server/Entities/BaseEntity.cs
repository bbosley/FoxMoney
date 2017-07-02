using System;

namespace FoxMoney.Server.Entities {
    public class BaseEntity : IEntityBase {
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}