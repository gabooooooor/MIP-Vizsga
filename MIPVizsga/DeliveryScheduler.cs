using System;
using System.Collections.Generic;
using System.Linq;

namespace MIPVizsga
{
    public class DeliveryScheduler
    {
        private int consecutivePriority3Count = 0;

        // Gets the next delivery to process based on priority rules
        public Delivery GetNextDelivery(List<Delivery> allDeliveries)
        {
            // Get only pending deliveries
            var pendingDeliveries = allDeliveries.Where(d => d.Status == "Pending").ToList();

            if (pendingDeliveries.Count == 0)
                return null;

            // If we've done 2 consecutive Priority 3, process a lower priority
            if (consecutivePriority3Count >= 2)
            {
                // Get lowest ID among Priority 1 or 2
                var lowerPriority = pendingDeliveries
                    .Where(d => d.Priority == 1 || d.Priority == 2)
                    .OrderBy(d => d.Id)
                    .FirstOrDefault();

                if (lowerPriority != null)
                {
                    // Reset counter and process this lower priority
                    consecutivePriority3Count = 0;
                    return lowerPriority;
                }
            }

            // Try to get Priority 3
            var priority3 = pendingDeliveries
                .Where(d => d.Priority == 3)
                .OrderBy(d => d.Id)
                .FirstOrDefault();

            if (priority3 != null)
            {
                consecutivePriority3Count++;
                return priority3;
            }

            // No Priority 3, get lowest Priority 2
            var priority2 = pendingDeliveries
                .Where(d => d.Priority == 2)
                .OrderBy(d => d.Id)
                .FirstOrDefault();

            if (priority2 != null)
            {
                consecutivePriority3Count = 0;
                return priority2;
            }

            // Get lowest Priority 1
            var priority1 = pendingDeliveries
                .Where(d => d.Priority == 1)
                .OrderBy(d => d.Id)
                .FirstOrDefault();

            if (priority1 != null)
            {
                consecutivePriority3Count = 0;
                return priority1;
            }

            return null;
        }
    }
}