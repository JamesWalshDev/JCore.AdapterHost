// ------------------------------------------------------------
// File: AdapterHost.cs
// Description: Core static host for Adapter Management.
// Author: James Walsh
// Version: 1.0.0
// ------------------------------------------------------------

using System;

namespace JCore.AdapterHost
{
    /// <summary>
    /// Static entry point for binding the runtime adapter. 
    /// This host locks in the core adapter for the application/system and powers it on once.
    /// </summary>
    public static class AdapterHost
    {
        /// <summary>
        /// Accidental multi-boot protection.
        /// </summary>
        private static bool _hasBooted;

        /// <summary>
        /// Accidental hot-swap protection.
        /// </summary>
        private static bool _isLocked;

        /// <summary>
        /// The Active Adapter.
        /// </summary>
        private static IAdapter _adapter;

        /// <summary>
        /// The Development Mode.
        /// </summary>
        public static bool IsDevMode
            => 
                #if DEV_MODE
                    true;
                #else
                    false;
                #endif

        /// <summary>
        /// /// Returns the Active Adapter.
        /// </summary>
        /// <returns>The Active Adapter.</returns>
        public static IAdapter Adapter
            => _adapter ?? throw new InvalidOperationException("[AdapterHost] Adapter not found.");

        /// <summary>
        /// Powers on the AdapterHost with Multi-Boot protection.
        /// Can only run once.
        /// </summary>
        /// <param name="adapter_">The specified Adapter.</param>
        public static void Boot(IAdapter adapter_)
        {   
            // Prevent accidental multi-boot
            if (_hasBooted) { return; }

            // Start
            Start(adapter_);

            // Boot
            _hasBooted = true;
        }

        /// <summary>
        /// Powers on the specified Adapter.
        /// </summary>
        /// <param name="adapter_">The specified Adapter.</param>
        public static void Start(IAdapter adapter_)
        {
            // Prevent accidental hot-swap
            if (_isLocked) { return; }

            // Add Adapter
            _adapter = adapter_ ?? throw new ArgumentNullException(nameof(adapter_), "[AdapterHost] Adapter cannot be null.");

            // Start
            _adapter.PowerOn();

            // Lock
            _isLocked = true;
        }

        /// <summary>
        /// Powers off the active Adapter.
        /// </summary>
        public static void Stop()
        {
            // Active check
            if (!_adapter.IsActive) { return; }

            // Unlock
            _isLocked = false;

            // Stop
            _adapter.PowerOff();

            // Remove Adapter
            _adapter = null;
        }

        /// <summary>
        /// Powers off the active Adapter and Powers on the specified Adapter.
        /// </summary>
        /// <param name="adapter_">The specified Adapter.</param>
        public static void Swap(IAdapter adapter_)
        {
            Stop();
            Start(adapter_);
        }
    }
}