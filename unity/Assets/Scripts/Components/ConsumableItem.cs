
using Core.Economy;
using Core.Units.Interfaces;
using UnityEngine;

namespace Core.Units.Components
{
    public abstract class ConsumableItem : IConsumableItem
    {
        [SerializeField]
        private Cost cost;

        public Cost Cost { get { return cost; } }
    }

}
