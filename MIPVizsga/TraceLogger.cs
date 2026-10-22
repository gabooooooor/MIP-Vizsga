using System;
using System.Diagnostics;
using System.IO;

namespace MIPVizsga
{
    public static class TraceLogger
    {
        // Static constructor runs once when class is first used
        static TraceLogger()
        {
            // Configure trace to write to trace.log file
            string logFile = "trace.log";
            TextWriterTraceListener listener = new TextWriterTraceListener(logFile);
            Trace.Listeners.Add(listener);
            Trace.AutoFlush = true;
        }

        public static void LogDeliveryAdded(int deliveryId)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Trace.WriteLine($"[{timestamp}] Delivery {deliveryId} added");
        }

        public static void LogDeliveryCompleted(int deliveryId)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Trace.WriteLine($"[{timestamp}] Delivery {deliveryId} completed");
        }

        public static void Flush()
        {
            Trace.Flush();
        }
    }
}