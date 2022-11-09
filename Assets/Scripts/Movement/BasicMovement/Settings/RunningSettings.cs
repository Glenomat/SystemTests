using UnityEngine;

namespace Movement.BasicMovement.Settings
{
    [CreateAssetMenu(fileName = "New Running Setting", menuName = "Movement/Running", order = 1)]
    public class RunningSettings : MovementSetting
    {
        [SerializeField] private float runningMultiplier = 1.5f;

        public void Deconstruct(out float runningMultiplier)
        {
            runningMultiplier = this.runningMultiplier;
        }
    }
}