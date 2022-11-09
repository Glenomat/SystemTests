using System.Collections.Generic;
using FlagHandlers;
using Movement.BasicMovement.Settings;
using Movement.Flags;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement.BasicMovement
{
    /// <summary>
    /// Implements Walking, Running, Jumping and Crouching
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class BasicMovement : MonoBehaviour
    {
        private Rigidbody playerRb;
        [SerializeField] private List<MovementSetting> movementSettings = new List<MovementSetting>();
        private Vector3 finalSpeed;
        
        // Walking
        private float walkingSpeed;
        private Vector2 walkingDir;

        // Running
        private float runningMultiplier;
        
        // Jumping
        private float jumpingHeight;
        private float fallMultiplier;
        private float lowJumpMultiplier;
        
        // Ground Check
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask groundLayer;
        
        // Crouching
        private float slowDownMultiplier = 1.5f;

        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            
            // Deconstruct settings and sets flags
            foreach (var setting in movementSettings)
            {
                switch (setting)
                {
                    case WalkingSettings x:
                        MovementFlagHandler.SetFlag(MovementFlags.CanWalk);
                        x.Deconstruct(out walkingSpeed);
                        break;
                    case RunningSettings x:
                        MovementFlagHandler.SetFlag(MovementFlags.CanRun);
                        x.Deconstruct(out runningMultiplier);
                        break;
                    case JumpingSetting x:
                        MovementFlagHandler.SetFlag(MovementFlags.CanJump);
                        x.Deconstruct(out jumpingHeight, out fallMultiplier, out lowJumpMultiplier);
                        break;
                    case CrouchingSetting x:
                        MovementFlagHandler.SetFlag(MovementFlags.CanCrouch);
                        x.Deconstruct(out slowDownMultiplier);
                        break;
                    default:
                        break;
                }
            }
        }

        private void FixedUpdate()
        {
            // Walking
            finalSpeed = new Vector3(walkingDir.x, playerRb.velocity.y, walkingDir.y);

            // Only applies the Crouch multiplier when the flag is set
            if (MovementFlagHandler.CheckFlag(MovementStateFlags.Crouching))
                finalSpeed = new Vector3(finalSpeed.x * slowDownMultiplier, playerRb.velocity.y, 
                    finalSpeed.z * slowDownMultiplier);

            // Only applies the Running Multiplier when the Flag is set
            if (MovementFlagHandler.CheckFlag(MovementStateFlags.Running))
                    finalSpeed = new Vector3(finalSpeed.x * runningMultiplier, playerRb.velocity.y,
                    finalSpeed.z * runningMultiplier);
            
            // Applies a multiplier when the player falls so the jumps are not so linear
            if (playerRb.velocity.y < 0)
                finalSpeed += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
            // Applies more force when the jump button is held down
            else if (playerRb.velocity.y > 0 && !MovementFlagHandler.CheckFlag(MovementStateFlags.Jumping))
                finalSpeed += Vector3.up * (Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);

            playerRb.velocity = finalSpeed;
            
            if(OnGround())
                PlayerFlagHandler.SetFlag(PlayerFlags.OnGround);
            else
                PlayerFlagHandler.RemoveFlag(PlayerFlags.OnGround);
        }

        public void Walk(InputAction.CallbackContext ctx)
        {
            if (!MovementFlagHandler.CheckFlag(MovementFlags.CanWalk))
                return;

            if (ctx.valueType != typeof(Vector2))
                return;

            if (ctx.started)
                MovementFlagHandler.SetFlag(MovementStateFlags.Walking);
            else if (ctx.canceled)
                MovementFlagHandler.RemoveFlag(MovementStateFlags.Walking);

            var rawOutput = ctx.ReadValue<Vector2>();
            
            var forward = transform.forward * rawOutput.y * walkingSpeed;
            var side = transform.right * rawOutput.x * walkingSpeed;
            walkingDir = new Vector2(forward.x, forward.z) + new Vector2(side.x, side.z);
        }

        public void Run(InputAction.CallbackContext ctx)
        {
            if (!MovementFlagHandler.CheckFlag(MovementFlags.CanRun))
                return;
            
            if (ctx.started)
                MovementFlagHandler.SetFlag(MovementStateFlags.Running);
            else if (ctx.canceled)
                MovementFlagHandler.RemoveFlag(MovementStateFlags.Running);
        }

        public void Jump(InputAction.CallbackContext ctx)
        {
            if (!MovementFlagHandler.CheckFlag(MovementFlags.CanJump))
                return;

            if (ctx.started && PlayerFlagHandler.CheckFlag(PlayerFlags.OnGround))
            {
                MovementFlagHandler.SetFlag(MovementStateFlags.Jumping);
                playerRb.velocity += Vector3.up * Mathf.Sqrt(-2f * Physics.gravity.y * jumpingHeight);
            }
            else if(ctx.canceled)
                MovementFlagHandler.RemoveFlag(MovementStateFlags.Jumping);
        }

        public void Crouch(InputAction.CallbackContext ctx)
        {
            if (!MovementFlagHandler.CheckFlag(MovementFlags.CanCrouch))
                return;

            if(ctx.started)
                MovementFlagHandler.SetFlag(MovementStateFlags.Crouching);
            if(ctx.canceled)
                MovementFlagHandler.RemoveFlag(MovementStateFlags.Crouching);
        }
        
        private bool OnGround() => Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);
    }
}
