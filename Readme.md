# JCore AdapterHost | API Reference
---

## Introduction

JCore AdapterHost is a lightweight, engine-agnostic runtime bridge designed to abstract away engine-specific implementation details (e.g., Unity, MonoGame, Console). It provides a centralized, plug-and-play interface for bootstrapping platform-specific behavior such as logging, diagnostics, and crash reporting—without hard-coding any platform logic into your core systems.

Key Benefits:

* Decouples platform logic from core application logic
* Enables testable, engine-agnostic architecture
* Supports runtime swapping of adapter implementations
* Ideal for both runtime use and headless simulations/testing

---

## Quick Start

| Steps                 | Description                                                                            |
| --------------------- | -------------------------------------------------------------------------------------- |
| `1. Download Source`  | Clone or copy the JCore.AdapterHost Source namespace into your project.                |
| `2. Import Source`    | Copy the Source into the Service Scope root.                                           |
| `3. Extend IAdapter`  | Extend the IAdapter and Adapter classes to facilitate required functionality.          |
| `4. Boot AdapterHost` | Call 'AdapterHost.Boot(new Adapter())' after the owning Service has been Instantiated. |

---

## Architectural Overview

| Component     | Role                                                               |
| ------------- | ------------------------------------------------------------------ |
| `AdapterHost` | Static host that loads a runtime adapter into global context       |
| `IAdapter`    | Interface of the runtime adapter.                                  |
| `Adapter`     | Implementation of the runtime adapter.                             |

---

### JCore | AdapterHost

The AdapterHost is a static class responsible for managing the active runtime IAdapter instance used by a Service in the application. It allows dynamic assignment of engine adapters, toggles development mode behaviors, and serves as the main entry point for adapter lifecycle management.

Instructions: Simply call the AdapterHost.Boot() method after the owning Service has been Instantiated.

| Type       | Member                    | Returns    | Description                                                  |
| ---------- | ------------------------- | ---------- | ------------------------------------------------------------ |
| `Property` | `Adapter`                 | `IAdapter` | Gets the Active Adapter.                                     |
| `Property` | `IsDevMode`               | `Bool`     | Gets the current Development Mode.                           |
| `Method`   | `Boot(IAdapter adapter)`  | `Void`     | Boots the AdapterHost for use.                               |
| `Method`   | `Start(IAdapter adapter)` | `Void`     | Assigns and Loads the specified Adapter.                     |
| `Method`   | `Stop()`                  | `Void`     | Unassigns the Active Adapter.                                |
| `Method`   | `Swap(IAdapter adapter)`  | `Void`     | Stops the Active Adapter and Starts a new one.               |

---

### JCore | IAdapter

The IAdapter is the interface for defining the implementation of an Adapter.

Instructions: Extend the interface to facilitate the Engine-Specific features coverd by the Adapter.

| Type       | Member                    | Returns    | Description                                                  |
| ---------- | ------------------------- | ---------- | ------------------------------------------------------------ |
| `Property` | `AdapterId`               | `String`   | Gets the Adapter's Id.                                       |
| `Property` | `IsActive`                | `Bool`     | Gets the Active Status of the Adapter.                       |
| `Method`   | `LogAdapterInfo()`        | `Void`     | Logs the Adapter State Info.                                 |
| `Method`   | `PowerOn()`               | `Void`     | Handles power on logic.                                      |
| `Method`   | `PowerOff()`              | `Void`     | Handles power off logic.                                     |
| `Method`   | `Log(string msg)`         | `Void`     | Logs an informational message to the Console.                |
| `Method`   | `LogWarning(string msg)`  | `Void`     | Logs a warning message to the Console.                       |
| `Method`   | `LogError(string msg)`    | `Void`     | Logs an error message to the Console.                        |
| `Method`   | `ThrowException()`        | `Void`     | Handles App Exceptions.                                      |

---

### JCore | Adapter : IAdapter

The Adapter is the implementation of IAdapter to be used by AdapterHost.

Instructions: Extend the implementation to facilitate the Engine-Specific features covered by the Adapter.

| Type       | Member                    | Returns    | Description                                                  |
| ---------- | ------------------------- | ---------- | ------------------------------------------------------------ |
| `Property` | `Instance`                | `Adapter`  | Gets the Instance of the Adapter.                            |
| `Property` | `AdapterId`               | `String`   | Implements IAdapter.                                         |
| `Property` | `IsActive`                | `Bool`     | Implements IAdapter.                                         |
| `Method`   | `LogAdapterInfo()`        | `Void`     | Implements IAdapter.                                         |
| `Method`   | `PowerOn()`               | `Void`     | Implements IAdapter.                                         |
| `Method`   | `PowerOff()`              | `Void`     | Implements IAdapter.                                         |
| `Method`   | `Log(string msg)`         | `Void`     | Implements IAdapter.                                         |
| `Method`   | `LogWarning(string msg)`  | `Void`     | Implements IAdapter.                                         |
| `Method`   | `LogError(string msg)`    | `Void`     | Implements IAdapter.                                         |
| `Method`   | `ThrowException()`        | `Void`     | Implements IAdapter.                                         |

---

## License

MIT License

Copyright (c) 2025 James Walsh

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the “Software”), to deal
in the Software without restriction, including without limitation the rights  
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell  
copies of the Software, and to permit persons to whom the Software is  
furnished to do so, subject to the following conditions:  

The above copyright notice and this permission notice shall be included in all  
copies or substantial portions of the Software.  

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR  
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,  
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE  
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER  
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,  
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN  
THE SOFTWARE.

---

## Author Notes

JCore AdapterHost was built to solve a recurring problem: how to cleanly separate core logic from engine specifics without introducing brittle, hard-wired dependencies. This utility acts as a runtime handshake between your platform-agnostic services and their environment-specific implementations.

Use JCore AdapterHost to:

* Keep your architecture testable, modular, and simulation-ready
* Inject diagnostics, crash reporting, and logging into any runtime—seamlessly
* Build core services once and plug them into any engine (Unity, MonoGame, Console, etc.)

This isn’t just a helper class. It’s a contract for clean separation of concerns. When every system has a stable adapter boundary, refactoring becomes fearless, ports become trivial, and core systems stay future-proof.

Design smart. Decouple hard. Build once, ship everywhere.

---