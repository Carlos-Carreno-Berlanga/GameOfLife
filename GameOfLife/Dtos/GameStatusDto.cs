namespace GameOfLife.Dtos
{
    public class GameStatusDto
    {
        public GameStatusDto(int columns, int rows, bool[,] board, int generation)
        {
            Rows = rows;
            Columns = columns;
            Board = board;
            Generation = generation;

        }

        //public GameStatusDto(int columns, int rows)
        //{
        //    Rows = rows;
        //    Columns = columns;
        //    Board = new bool[columns, rows];
        //    Generation = 0;
        //}

        public void Evolve()
        {

        }
        public int Generation { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public bool[,] Board { get; set; }
    }
}
