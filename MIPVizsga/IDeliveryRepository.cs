using System;
using System.Collections.Generic;

namespace MIPVizsga
{
    public interface IDeliveryRepository
    {
        void Initialize();
        void Insert(Delivery delivery);
        List<Delivery> LoadAll();
        void UpdateStatus(int deliveryId, string newStatus);
    }
}
