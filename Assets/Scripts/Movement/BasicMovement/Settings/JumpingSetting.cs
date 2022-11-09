using UnityEngine;

namespace Movement.BasicMovement.Settings
{
    [CreateAssetMenu(fileName = "New Jumping Setting", menuName = "Movement/Jumping", order = 2)]
    public class JumpingSetting : MovementSetting
    {
        [SerializeField] private float jumpingHeight = 5f;
        [SerializeField] private float fallMult = 2.5f;
        [SerializeField] private float lowJumpMult = 2f;

        public void Deconstruct(out float jumpingHeight, out float fallMult, out float lowJumpMult)
        {
            jumpingHeight = this.jumpingHeight;
            fallMult = this.fallMult;
            lowJumpMult = this.lowJumpMult;
        }
    }
}