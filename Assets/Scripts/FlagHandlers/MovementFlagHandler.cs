using System;
using System.Collections.Generic;
using Movement.Flags;

namespace FlagHandlers
{
    public static class MovementFlagHandler
    {
        public static MovementStateFlags movementStateFlags { get; private set; }
        public static MovementFlags movementFlags { get; private set; }

        /// <summary>
        /// Sets a movement flag
        /// </summary>
        /// <param name="flag">Flag that will be set</param>
        /// <typeparam name="T">Flag type</typeparam>
        public static void SetFlag<T>(T flag) where T: Enum
        {
            switch (flag)
            {
                case MovementStateFlags val:
                    movementStateFlags |= val;
                    break;
                case MovementFlags val:
                    movementFlags |= val;
                    break;
            }
        }

        /// <summary>
        /// Sets multiple movement flags
        /// </summary>
        /// <param name="flags">Array of the flags that will be set</param>
        /// <typeparam name="T">Flag type</typeparam>
        public static void SetFlags<T>(IEnumerable<T> flags) where T: Enum 
        {
            foreach (var flag in flags)
            {
                switch (flag)
                {
                    case MovementStateFlags val:
                        movementStateFlags |= val;
                        break;
                    case MovementFlags val:
                        movementFlags |= val;
                        break;
                }
            }
        }

        /// <summary>
        /// Removes a flag
        /// </summary>
        /// <param name="flag">Flag that will be removed</param>
        /// <typeparam name="T">Flag type</typeparam>
        public static void RemoveFlag<T>(T flag) where T : Enum
        {
            switch (flag)
            {
                case MovementStateFlags val:
                    movementStateFlags &= ~val;
                    break;
                case MovementFlags val:
                    movementFlags &= ~val;
                    break;
            }
        }

        /// <summary>
        /// Removes multiple flags
        /// </summary>
        /// <param name="flags">Array of flags that will be removed</param>
        /// <typeparam name="T">Flag types</typeparam>
        public static void RemoveFlags<T>(IEnumerable<T> flags) where T : Enum
        {
            foreach (var flag in flags)
            {
                switch (flag)
                {
                    case MovementStateFlags val:
                        movementStateFlags &= ~val;
                        break;
                    case MovementFlags val:
                        movementFlags &= ~val;
                        break;
                }
            }
        }

        /// <summary>
        /// Checks if a specific flag is set. It needs to be a movement flag
        /// </summary>
        /// <param name="flag">Flag that will be checked</param>
        /// <typeparam name="T">Flag type</typeparam>
        /// <returns>If the flag is set</returns>
        public static bool CheckFlag<T>(T flag) where T : Enum
        {
            switch (flag)
            {
                case MovementStateFlags val:
                    return movementStateFlags.HasFlag(val);
                case MovementFlags val:
                    return movementFlags.HasFlag(val);
                default:
                    return false;
            }
        }
    }
}