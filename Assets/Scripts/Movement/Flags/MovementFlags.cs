using System;

namespace Movement.Flags
{
    [Flags]
    public enum MovementFlags
    {
        None = 0,
        CanWalk = 1,
        CanRun = 2,
        CanJump = 4,
        CanCrouch = 8,
        CanDash = 16,
        CanMultiJump = 32,
    }
}