using GameOfLife.Services.Interfaces;
using System.Collections.Generic;

namespace GameOfLife.Services.Implementation
{
    public class LifeformFactory : ILifeformFactory
    {
        private readonly bool[,] pixel = new bool[,]
 {
            {true}
 };

        private readonly bool[,] block = new bool[,]
{
            {true, true},
            {true, true}
};

        private readonly bool[,] blinker = new bool[,]
{
            {true, true, true}
};

        private readonly bool[,] lwss = new bool[,]
{
            {false, true, true, true, true},
            {true, false, false, false, true},
            {false, false, false, false, true},
            {true, false, false, true, false},
};

        private readonly Dictionary<string, bool[,]> lifeFormDictionary = new Dictionary<string, bool[,]>()
{
  {"PIXEL", new bool[,]
 {
            {true}
 }},
    {"BLOCK", new bool[,]
 {
            {true,true},
            {true,true}
 }},
  {"BLINKER",  new bool[,]
{
            {true, true, true}
}},
    {"GLIDER",  new bool[,]
{
            {false, true, false},
                 {false, false, true},
                   {true, true, true}
}},
  {"LWSS",new bool[,]
{
            {false, true, true, true, true},
            {true, false, false, false, true},
            {false, false, false, false, true},
            {true, false, false, true, false},
}
    }
};

        public string[,] createLifeform(string name, string color, int posCol, int posRow, string[,] grid)
        {
            bool[,] lifeForm;
            if (lifeFormDictionary.ContainsKey(name))
            {
                lifeForm = lifeFormDictionary[name];
            }
            else
            {
                lifeForm = lifeFormDictionary["PIXEL"];
            }

            int maxCol = grid.GetLength(0);
            int maxRow = grid.GetLength(1);
            for (int i = 0; i < lifeForm.GetLength(0); i++)
            {
                for (int j = 0; j < lifeForm.GetLength(1); j++)
                {
                    if (i + posCol <= maxCol && j + posRow <= maxRow && lifeForm[i, j])
                    {
                        grid[i + posCol, j + posRow] = color;
                    }
                }
            }
            return grid;
        }

    }
}
