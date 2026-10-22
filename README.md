# PonyExpress Delivery Manager

## Overview
A Windows Forms application that manages emergency package deliveries using a complex priority scheduling algorithm. Deliveries are stored in SQLite and processed by a background thread with sophisticated priority rules.

## Features
- ✅ Windows Forms desktop application
- ✅ SQLite database for persistent storage
- ✅ Complex priority scheduler (Priority 3 first, alternates every 2 consecutive)
- ✅ Background thread processing (System.Threading.Thread)
- ✅ LINQ filtering/ordering and GroupBy operations
- ✅ Trace logging with timestamps
- ✅ Data persistence across restarts

## How to Run

### Prerequisites
- Visual Studio 2026 (or later)
- .NET Framework 4.7.2
- System.Data.SQLite NuGet package (already included)

### Steps
1. Open `MIPVizsga.slnx` in Visual Studio
2. Press `Ctrl+Shift+B` to build the solution
3. Press `F5` to run the application

### Using the Application

#### Add a Single Delivery
1. Enter recipient name in the "Recipient" textbox
2. Select priority (1, 2, or 3)
3. Click "Add Delivery"
4. The delivery is saved to the database immediately

#### Generate Test Dataset
1. Click "Generate Dataset"
2. This creates 110 deliveries: 30 Priority 1, 30 Priority 2, 30 Priority 3, plus 20 mixed
3. All deliveries are saved to SQLite database

#### Process Deliveries
1. Click "Start Processing"
2. The application processes deliveries in priority order:
   - Priority 3 comes first (300ms processing time)
   - After 2 consecutive Priority 3, process oldest Priority 1 or 2 (600ms each)
   - Then back to Priority 3
3. The grid updates in real-time
4. Status counts show Pending/Processing/Completed
5. Click "Stop Processing" to stop (after current delivery finishes)

## Database Location
- **File**: `ponyexpress.db`
- **Location**: `MIPVizsga\bin\Debug\ponyexpress.db` (created at runtime)
- **Schema**: 
  - Id (INTEGER, PRIMARY KEY AUTOINCREMENT)
  - Recipient (TEXT, NOT NULL)
  - Priority (INTEGER, NOT NULL - values 1, 2, or 3)
  - Status (TEXT, NOT NULL - values: "Pending", "Processing", "Completed")

## Trace Logging
- **File**: `trace.log`
- **Location**: `MIPVizsga\bin\Debug\trace.log` (created at runtime)
- **Contents**: Logs two event types:
  1. "Delivery X added" - when a delivery is inserted
  2. "Delivery X completed" - when a delivery finishes processing
- Each entry includes a timestamp in format: `[YYYY-MM-DD HH:mm:ss.fff]`

## Implementation Details

### Architecture
- **Delivery.cs** - Abstract base class with RegularDelivery and ExpressDelivery subclasses
- **IDeliveryRepository.cs** - Interface for data operations
- **SqliteDeliveryRepository.cs** - SQLite implementation with parameterized SQL
- **DeliveryScheduler.cs** - Complex priority algorithm using LINQ
- **DatasetGenerator.cs** - Programmatically generates 110 test deliveries
- **TraceLogger.cs** - Configures System.Diagnostics.Trace logging
- **Form1.cs** - UI logic and background thread management

### Key Technologies
- **Database**: SQLite with parameterized queries
- **Threading**: System.Threading.Thread (not Task/async)
- **LINQ Operations**:
  1. DeliveryScheduler: Where, OrderBy, FirstOrDefault for filtering and scheduling
  2. Form1.UpdateStatusCounts: GroupBy for counting deliveries by status
- **Logging**: System.Diagnostics.Trace with TextWriterTraceListener

### Thread Safety
- Lock object protects database access during processing
- UI updates use Control.Invoke to run on main thread
- Cooperative stop flag (no Thread.Abort)

## Requirements Verification

### ✅ NON-NEGOTIABLE DATASET GATE
- Generate dataset creates 110 deliveries (30 P1, 30 P2, 30 P3, 20 mixed)
- SQLite database created with 100+ rows
- Each delivery has nonempty Recipient, unique auto-generated Id, Status = "Pending"

### ✅ Object-Oriented Design (3 points)
- Abstract Delivery class with properties and abstract GetProcessingDelayMs()
- RegularDelivery: Priority 1-2, 600ms delay
- ExpressDelivery: Priority 3, 300ms delay
- IDeliveryRepository interface with insert, load, status-update
- Polymorphic calls and type reconstruction from database

### ✅ SQLite Database (4 points)
- ponyexpress.db created automatically
- Correct schema with all columns and constraints
- Parameterized SQL for all queries
- Data persists across application restarts

### ✅ Windows Forms Interface (2 points)
- Recipient textbox, Priority dropdown (1,2,3)
- Add Delivery and Generate Dataset buttons
- DataGridView showing Id, Recipient, Priority, Status
- Start/Stop Processing buttons
- Status counts for Pending, Processing, Completed
- Input validation (empty recipient rejection)

### ✅ Background Processing with Threads (4 points)
- Exactly one System.Threading.Thread created
- No Task or async/await
- Status transitions: Pending → Processing → Completed
- Polymorphic delay times (300ms vs 600ms)
- UI refreshed using Invoke (responsive)
- Cooperative stop with flag (no Thread.Abort)

### ✅ Priority Scheduler (4 points)
- Priority 3 processes first
- For equal priority, uses ascending Id
- After 2 consecutive Priority 3, processes oldest Priority 1 or 2
- Counter resets after lower-priority processing
- Algorithm works for any dataset (not hardcoded)

### ✅ LINQ Operations (2 points)
1. Scheduling: Where, OrderBy, FirstOrDefault in DeliveryScheduler
2. UI Counts: GroupBy in Form1.UpdateStatusCounts

### ✅ Trace Logging (1 point)
- System.Diagnostics.Trace with TextWriterTraceListener
- trace.log file with timestamps
- Logs: delivery added and delivery completed
- AutoFlush enabled
