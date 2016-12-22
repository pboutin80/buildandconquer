
namespace Core.Enums
{
    public enum MoveCompletion
    {
        Reached,
        Interrupted,
        TargetNoMoreReachable,
        Aborted,
    }

    public enum MovingMode
    {
        DontAttack,
        AttackInRange,
        StopMoveAndAttack,
    }

    public enum OffensiveMode
    {
        DontAttack,
        ReturnAttack,
        MoveTowardAttacker,
        MoveTowardTargetUntilNoneInRange,
    }
}
