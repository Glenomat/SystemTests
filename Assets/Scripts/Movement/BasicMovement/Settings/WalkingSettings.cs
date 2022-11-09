using UnityEngine;

namespace Movement.BasicMovement.Settings
{
    [CreateAssetMenu(fileName = "New Walking Setting", menuName = "Movement/Walking", order = 0)]
    public class WalkingSettings : MovementSetting
    {
        [SerializeField] private float movementSpeed = 10f;

        public void Deconstruct(out float movementSpeed)
        {
            movementSpeed = this.movementSpeed;
        }
    }
}