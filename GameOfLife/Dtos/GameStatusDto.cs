namespace GameOfLife.Dtos
{
    public class GameStatusDto
    {
        public GameStatusDto(int columns, int rows, string[,] board, int generation)
        {
            Rows = rows;
            Columns = columns;
            Board = board;
            Generation = generation;

        }

        public int Generation { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public string[,] Board { get; set; }
    }
}
