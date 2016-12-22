
using Core.Units.Components;
using Core.Units.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class BuildableItem : ConsumableItem, IBuildableItem
    {
        [SerializeField]
        private float timeToBuild;

        public float TimeToBuild { get { return timeToBuild; } }
    }
}
