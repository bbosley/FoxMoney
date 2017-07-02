namespace FoxMoney.Server.Entities {
    public class Security : BaseEntity {
        public string Name { get; set; }
        public string YahooCode { get; set; }
        public bool Active { get; set; }
    }
}