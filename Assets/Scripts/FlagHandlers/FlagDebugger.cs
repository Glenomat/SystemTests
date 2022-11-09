using Movement.Flags;
using UnityEngine;

namespace FlagHandlers
{
    public class FlagDebugger : MonoBehaviour
    {
        public MovementFlags debugMovementFlags;
        public MovementStateFlags debugMovementStateFlags;
        public PlayerFlags debugPlayerFlags;

        private void Update()
        {
            debugMovementFlags = MovementFlagHandler.movementFlags;
            debugMovementStateFlags = MovementFlagHandler.movementStateFlags;
            debugPlayerFlags = PlayerFlagHandler.playerFlags;
        }
    }
}