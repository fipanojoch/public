namespace CCard {
    public abstract class SimpleCard  {
        public decimal Balance { get; set; }
        protected abstract decimal SimpleInterest { get; }
        public decimal GetSimpleInterest() => SimpleInterest * Balance;
    }
    public class Visa : SimpleCard {
        protected override decimal SimpleInterest => 0.1M;
    }
    public class MasterCard : SimpleCard {
        protected override decimal SimpleInterest => 0.05M;
    }
    public class Discover : SimpleCard {
        protected override decimal SimpleInterest => 0.01M;
    }
}