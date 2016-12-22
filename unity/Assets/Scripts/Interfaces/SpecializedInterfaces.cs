using Core.Attributes;
using System;
using System.Collections.Generic;

namespace Core.Units.Interfaces
{
    [ConfigurationInterface]
    public interface IProducerItem
    {
        event Action<IBuildableItem> ProductionCompleted;

        float ProductionSpeed { get; }
        Queue<ProductionItem> ProductionQueue { get; }

        ProductionItem Produce<T>() where T : IBuildableItem;
        bool CancelProduction();
        bool CancelProduction(ProductionItem item);
        bool SuspendProduction();
        bool SuspendProduction(ProductionItem item);
    }

    public struct ProductionItem
    {
        public float TimeToBuild { get; private set; }
        public float Progression { get; private set; }
    }

    [ConfigurationInterface]
    public interface IRankableItem
    {
        event Action<int> LevelIncreased;

        int Level { get; }
        float Experience { get; }
        float ExperienceToLevelUp { get; }

        bool EarnExperience(float experience);
    }
}
