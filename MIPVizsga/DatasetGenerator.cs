using System;
using System.Collections.Generic;

namespace MIPVizsga
{
    public static class DatasetGenerator
    {
        public static List<Delivery> GenerateDataset()
        {
            List<Delivery> deliveries = new List<Delivery>();

            // Generate 30 Priority 1 deliveries (RegularDelivery)
            for (int i = 0; i < 30; i++)
            {
                RegularDelivery delivery = new RegularDelivery();
                delivery.Recipient = "RecipientP1_" + (i + 1);
                delivery.Priority = 1;
                delivery.Status = "Pending";
                deliveries.Add(delivery);
            }

            // Generate 30 Priority 2 deliveries (RegularDelivery)
            for (int i = 0; i < 30; i++)
            {
                RegularDelivery delivery = new RegularDelivery();
                delivery.Recipient = "RecipientP2_" + (i + 1);
                delivery.Priority = 2;
                delivery.Status = "Pending";
                deliveries.Add(delivery);
            }

            // Generate 30 Priority 3 deliveries (ExpressDelivery)
            for (int i = 0; i < 30; i++)
            {
                ExpressDelivery delivery = new ExpressDelivery();
                delivery.Recipient = "RecipientP3_" + (i + 1);
                delivery.Priority = 3;
                delivery.Status = "Pending";
                deliveries.Add(delivery);
            }

            // Generate 20 more mixed priority deliveries
            for (int i = 0; i < 20; i++)
            {
                Delivery delivery;
                if (i % 3 == 0)
                {
                    delivery = new ExpressDelivery();
                    delivery.Priority = 3;
                }
                else if (i % 3 == 1)
                {
                    delivery = new RegularDelivery();
                    delivery.Priority = 1;
                }
                else
                {
                    delivery = new RegularDelivery();
                    delivery.Priority = 2;
                }

                delivery.Recipient = "RecipientExtra_" + (i + 1);
                delivery.Status = "Pending";
                deliveries.Add(delivery);
            }

            return deliveries;
        }
    }
}