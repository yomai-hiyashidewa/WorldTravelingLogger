using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    public class FileErrorContext
    {
        public int X { get; set; }
        public int Y { get; set; }

        public FileErrorContext(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
