using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.ResourceObjects
{
    public class CreateLifeformResourceObject
    {
        public string Name { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }
        public string Color { get; set; }
    }
}
