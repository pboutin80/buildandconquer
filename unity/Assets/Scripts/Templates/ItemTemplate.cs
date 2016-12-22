using Core.Economy;
using UnityEngine;

namespace Assets.Scripts.Templates
{
    public class ItemTemplate : ScriptableObject
    {
        [SerializeField]
        private string _description;
        [SerializeField]
        private Cost _cost;
        [SerializeField]
        private float _timeToBuild;

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public string Description
        {
            get { return _description; }
            private set { _description = value; }
        }

        public Cost Cost
        {
            get { return _cost; }
            private set { _cost = value; }
        }

        public float TimeToBuild
        {
            get { return _timeToBuild; }
            private set { _timeToBuild = value; }
        }
    }

    public class ProducerItemTemplate : ItemTemplate
    {

    }
}