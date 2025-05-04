// ------------------------------------------------------------
// File: Adapter.cs
// Description: Implementation of the IAdapter interface.
// Author: James Walsh
// Version: 1.0.0
// ------------------------------------------------------------

using System;
using UnityEngine;

namespace JCore.AdapterHost
{
    /// <summary>
    /// Implementation of the Adapter for use by the AdapterHost.
    /// Example Implementation of a UnityEngine Adapter.
    /// </summary>
    public class Adapter : MonoBehaviour, IAdapter
    {
        /// <summary>
        /// Returns the current Adapter instance.
        /// </summary>
        public static Adapter Instance { get; private set; }

        /// <summary>
        /// Returns the Adapter's Id.
        /// </summary>
        public string AdapterId
            => "UnityAdapter";

        /// <summary>
        /// Returns true if active.
        /// </summary>
        public bool IsActive
            => Instance != null;

        /// <summary>
        /// Logs Adapter State info.
        /// </summary>
        public void LogAdapterInfo()
            => Log($"AdapterId: {AdapterId} | IsActive: {IsActive}");

        /// <summary>
        /// Starts the adapter.
        /// </summary>
        public void PowerOn()
        {
            try
            {
                // Set
                Instance = this;

                // Log Success
                Log($"[AdapterHost] Successfully booted the {AdapterId}.");
            }
            catch (Exception ex)
            {
                // Throw
                ThrowException(ex, $"[AdapterHost] Failed to boot the {AdapterId}.");
            }
        }

        /// <summary>
        /// Stops the adapter.
        /// </summary>
        public void PowerOff()
        {
            try
            {
                // Remove
                Instance = null;

                // Log Success
                Log($"[AdapterHost] Successfully shutdown the {AdapterId}.");
            }
            catch (Exception ex)
            {
                // Throw
                ThrowException(ex, $"[AdapterHost] Failed to shutdown the {AdapterId}.");
            }
        }

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        public void Log(string message_)
        {
            if (AdapterHost.IsDevMode) Debug.Log($"{message_}");
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        public void LogWarning(string message_)
        {
            if (AdapterHost.IsDevMode) Debug.LogWarning($"{message_}");
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        public void LogError(string message_)
        {
            if (AdapterHost.IsDevMode) Debug.LogError($"{message_}");
        }

        /// <summary>
        /// Unified exception handling bridge to engine-side crash reporting.
        /// </summary>
        public void ThrowException(Exception ex_, string context_ = null)
        {
            if (AdapterHost.IsDevMode)
            {
                // Parse Error
                var msg = context_ != null
                    ? $"[{context_}] Exception: {ex_.Message}\n{ex_.StackTrace}"
                    : $"Exception: {ex_.Message}\n{ex_.StackTrace}";

                // Log Error
                LogError(msg);

                // Throw Error
                throw ex_;
            }
        }
    }
}