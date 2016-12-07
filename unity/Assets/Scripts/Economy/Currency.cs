using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Economy
{
    public enum Currency
    {
        Money,
        Oil,
        Power,
        Mineral
    }

    public class Cost
    {
        private List<CurrencyCost> CurrencyCosts;

        public static Cost operator +(Cost cost1, CurrencyCost cost2)
        {
            var total = new Cost();
            total.CurrencyCosts = cost1.CurrencyCosts;
            total.CurrencyCosts.Add(cost2);
            return total;
        }

        public static implicit operator Cost(CurrencyCost cost)
        {
            return new Cost(cost);
        }

        public Cost()
        {
            CurrencyCosts = new List<CurrencyCost>();
        }

        public Cost(CurrencyCost cost)
        {
            CurrencyCosts = new List<CurrencyCost>();
            CurrencyCosts.Add(cost);
        }

        public override string ToString()
        {
            return string.Format("Cost: {0}", string.Join(", ", CurrencyCosts.Select(c => c.ToString()).ToArray()));
        }
    }

    public struct CurrencyCost
    {
        public Currency Currency;
        public float Units;

        public static Cost operator +(CurrencyCost cost1, CurrencyCost cost2)
        {
            var total = new Cost(cost1);
            total += cost2;
            return total;
        }

        public CurrencyCost(Currency currency, float units)
        {
            Currency = currency;
            Units = units;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Units, Currency);
        }
    }
}
