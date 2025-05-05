// ------------------------------------------------------------
// File: Adapter.cs
// Description: Implementation of the IAdapter interface.
// Author: James Walsh
// Version: 1.0.1
// ------------------------------------------------------------

using System;
using System.Diagnostics;

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
        /// Internal Class used to contain Logging Calls for Parent Class.
        /// Enables logging only in DevMode.
        /// </summary>
        private static class InternalLogging
        {
            /// <summary>
            /// Logs the Adapter State Info.
            /// </summary>
            [Conditional("DEV_MODE")]
            public static void LogAdapterInfo(string id_, bool isActive_)
                => AdapterHost.Adapter.Log($"AdapterId: {id_} | IsActive: {isActive_}");

            /// <summary>
            /// Logs Successful PowerOn.
            /// </summary>
            [Conditional("DEV_MODE")]
            public static void LogPowerOnSuccess(string id_)
                => AdapterHost.Adapter.Log($"[AdapterHost] Successfully booted the {id_}.");

            /// <summary>
            /// Logs Successful PowerOff.
            /// </summary>
            [Conditional("DEV_MODE")]
            public static void LogPowerOffSuccess(string id_)
                => AdapterHost.Adapter.Log($"[AdapterHost] Successfully shutdown the {id_}.");
        }

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
        /// Logs the Adapter State info.
        /// </summary>
        public void LogAdapterInfo()
            => InternalLogging.LogAdapterInfo(AdapterId, IsActive);

        /// <summary>
        /// Starts the adapter.
        /// </summary>
        public void PowerOn()
        {
            try
            {
                Instance = this;
                InternalLogging.LogPowerOnSuccess(AdapterId);
            }
            catch (Exception ex)
            {
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
                Instance = null;
                InternalLogging.LogPowerOffSuccess(AdapterId);
            }
            catch (Exception ex)
            {
                ThrowException(ex, $"[AdapterHost] Failed to shutdown the {AdapterId}.");
            }
        }

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        public void Log(string message_)
        {
            if (AdapterHost.IsDevMode) UnityEngine.Debug.Log($"{message_}");
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        public void LogWarning(string message_)
        {
            if (AdapterHost.IsDevMode) UnityEngine.Debug.LogWarning($"{message_}");
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        public void LogError(string message_)
        {
            if (AdapterHost.IsDevMode) UnityEngine.Debug.LogError($"{message_}");
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