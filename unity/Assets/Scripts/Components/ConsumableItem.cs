
using Assets.Scripts.Economy;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class ConsumableItem : IConsumableItem
    {
        [SerializeField]
        private Cost cost;

        public Cost Cost { get { return cost; } }
    }

}
