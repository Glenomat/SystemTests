using System;

namespace Movement.Flags
{
    [Flags]
    public enum MovementStateFlags
    {
        None = 0,
        Walking = 1,
        Running = 2,
        Jumping = 4,
        Crouching = 8,
        Dashing = 16,
    }
}