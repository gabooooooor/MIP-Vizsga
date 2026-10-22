using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIPVizsga
{
    public abstract class Delivery
    {
        public int Id { get; set; }
        public string Recipient { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }

        public abstract int GetProcessingDelayMs();
    }

    public class RegularDelivery : Delivery
    {
        public override int GetProcessingDelayMs()
        {
            return 600;
        }
    }

    public class ExpressDelivery : Delivery
    {
        public override int GetProcessingDelayMs()
        {
            return 300;
        }
    }
}
