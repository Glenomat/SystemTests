using UnityEngine;

namespace Movement.ExtendedMovement.Settings
{
    [CreateAssetMenu(fileName = "New Multi-jump Setting", menuName = "Movement/Multi Jump", order = 0)]
    public class MultiJumpSetting : MovementSetting
    {
        [SerializeField] private int extraJumps = 1;
        [SerializeField] private float jumpForce = 10f;

        public void Deconstruct(out int extraJumps, out float jumpForce)
        {
            extraJumps = this.extraJumps;
            jumpForce = this.jumpForce;
        }
    }
}