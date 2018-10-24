using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.Services.Interfaces
{
    public interface ILifeformFactory
    {
        string[,] createLifeform(string name, string color, int posCol, int posRow, string[,] grid);
    }
}
