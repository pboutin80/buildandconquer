
using Core.Attributes;
using Core.Economy;
using Core.Enums;
using System;
using UnityEngine;

namespace Core.Units.Interfaces
{
    [ConfigurationInterface]
    public interface IConsumableItem
    {
        Cost Cost { get; }
    }

    [ConfigurationInterface]
    public interface IBuildableItem : IConsumableItem
    {
        float TimeToBuild { get; }
    }

    [ConfigurationInterface]
    public interface IDamageableItem
    {
        float Health { get; }
        bool IsAlive { get; }

        bool Damage(float damage);
    }

    [ConfigurationInterface]
    public interface IOffensiveItem : IInterruptable
    {
        float AttackPower { get; }
        float Accuracy { get; }
        OffensiveMode OffensiveMode { get; set; }

        bool Attack(IDamageableItem target);
        bool ImprovePower(float bonus);
    }

    public interface IInterruptable
    {
        bool Stop();
    }

    [ConfigurationInterface]
    public interface IMoveableItem : IInterruptable
    {
        event Action<MoveCompletion> MoveCompleted;

        float Speed { get; }
        bool CanRotate { get; }
        MovingMode MovingMode { get; set; }

        bool MoveTo(Vector3 target);
        bool Follow(IMoveableItem target);
    }
}

