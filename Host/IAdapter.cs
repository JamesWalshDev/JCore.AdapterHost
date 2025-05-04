// ------------------------------------------------------------
// File: IAdapter.cs
// Description: Interface for Adapter implementations.
// Author: James Walsh
// Version: 1.0.0
// ------------------------------------------------------------

using System;

namespace JCore.AdapterHost
{
    /// <summary>
    /// Defines an interface for environment-specific runtime adapters.
    /// </summary>
    public interface IAdapter
    {
        /// <summary>
        /// Returns the current Adapter's Id.
        /// </summary>
        string AdapterId { get; }

        /// <summary>
        /// Returns true if active.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Logs Adapter State info.
        /// </summary>
        void LogAdapterInfo();

        /// <summary>
        /// Starts the adapter.
        /// </summary>
        void PowerOn();

        /// <summary>
        /// Stops the adapter.
        /// </summary>
        void PowerOff();
        
        /// <summary>
        /// Logs an informational message.
        /// </summary>
        void Log(string message_);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        void LogWarning(string message_);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        void LogError(string message_);

        /// <summary>
        /// Unified exception handling bridge to engine-side crash reporting.
        /// </summary>
        void ThrowException(Exception ex_, string context = null);
    }
}