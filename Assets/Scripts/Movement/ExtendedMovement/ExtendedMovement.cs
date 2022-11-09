using System;
using System.Collections.Generic;
using FlagHandlers;
using Movement.ExtendedMovement.Settings;
using Movement.Flags;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement.ExtendedMovement
{
    [RequireComponent(typeof(BasicMovement.BasicMovement))]
    public class ExtendedMovement : MonoBehaviour
    {
        private Rigidbody playerRb;
        [SerializeField] private List<MovementSetting> movementSettings = new List<MovementSetting>();

        // Dashing
        private int dashAmount;
        private float dashSpeed;
        private float dashDuration;
        private float dashCooldown;
        private int currDashAmount;
        private float currDashTime;
        private float currDashCooldown;

        // Multi Jumping
        private int extraJumps;
        private float jumpForce;
        private int currExtraJumps;

        private void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            
            foreach (var setting in movementSettings)
            {
                switch (setting)
                {
                    case DashSetting x:
                        MovementFlagHandler.SetFlag(MovementFlags.CanDash);
                        x.Deconstruct(out dashAmount, out dashSpeed, out dashDuration, out dashCooldown);
                        break;
                    case MultiJumpSetting x:
                        MovementFlagHandler.SetFlag(MovementFlags.CanMultiJump);
                        x.Deconstruct(out extraJumps, out jumpForce);
                        break;
                    default:
                        break;
                }
            }
        }

        private void FixedUpdate()
        {
            // Dashing (Currently only in looking direction and not Key direction)
            if (MovementFlagHandler.CheckFlag(MovementStateFlags.Dashing) && currDashTime < dashDuration)
            {
                currDashTime += Time.deltaTime;
                playerRb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
            }
            else if (MovementFlagHandler.CheckFlag(MovementStateFlags.Dashing) && currDashCooldown < dashCooldown)
            {
                currDashCooldown += Time.deltaTime;
                if (currDashCooldown >= dashCooldown)
                {
                    currDashAmount = dashAmount;
                    MovementFlagHandler.RemoveFlag(MovementStateFlags.Dashing);
                }
            }
        }

        // Todo: Based on Key Press
        public void Dash(InputAction.CallbackContext ctx)
        {
            if (!MovementFlagHandler.CheckFlag(MovementFlags.CanDash))
                return;

            if ((ctx.started && !MovementFlagHandler.CheckFlag(MovementStateFlags.Dashing)) || (currDashAmount > 0 && ctx.started))
            {
                currDashTime = 0f;
                currDashCooldown = 0f;
                currDashAmount -= 1;
                MovementFlagHandler.SetFlag(MovementStateFlags.Dashing);
            }
        }
        public void MultiJump(InputAction.CallbackContext ctx)
        {
            if (!MovementFlagHandler.CheckFlag(MovementFlags.CanJump) || 
                !MovementFlagHandler.CheckFlag(MovementFlags.CanMultiJump))
                return;

            if (ctx.started && !PlayerFlagHandler.CheckFlag(PlayerFlags.OnGround) && currExtraJumps > 0)
            {
                currExtraJumps -= 1;
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                MovementFlagHandler.SetFlag(MovementStateFlags.Jumping);
            }
            else if (ctx.canceled)
                MovementFlagHandler.RemoveFlag(MovementStateFlags.Jumping);
            
            if(PlayerFlagHandler.CheckFlag(PlayerFlags.OnGround))
                currExtraJumps = extraJumps;
        }
    }
}