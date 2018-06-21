using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET.API_Models
{
    public class LolLoginLoginQueue
    {
        public int approximateWaitTimeSeconds { get; set; }
        public int estimatedPositionInQueue { get; set; }
        public bool isPositionCapped { get; set; }
    }
}
