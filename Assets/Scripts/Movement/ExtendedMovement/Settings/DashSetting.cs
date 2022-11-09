using UnityEngine;

namespace Movement.ExtendedMovement.Settings
{
    [CreateAssetMenu(fileName = "New Dash Setting", menuName = "Movement/Dash", order = 6)]
    public class DashSetting : MovementSetting
    {
        [SerializeField] private int dashAmount = 1;
        [SerializeField] private float dashSpeed = 20f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float dashCooldown = 0.5f;

        public void Deconstruct(out int dashAmount, out float dashSpeed, out float dashDuration, out float dashCooldown)
        {
            dashAmount = this.dashAmount;
            dashSpeed = this.dashSpeed;
            dashDuration = this.dashDuration;
            dashCooldown = this.dashCooldown;
        }
    }
}