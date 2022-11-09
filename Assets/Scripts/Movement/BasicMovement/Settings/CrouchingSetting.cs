using UnityEngine;

namespace Movement.BasicMovement.Settings
{
    [CreateAssetMenu(fileName = "New Crouch Setting", menuName = "Movement/Crouching", order = 3)]
    public class CrouchingSetting : MovementSetting
    {
        [SerializeField] private float slowDownMult = 0.5f;

        public void Deconstruct(out float slowDownMult)
        {
            slowDownMult = this.slowDownMult;
        }
    }
}