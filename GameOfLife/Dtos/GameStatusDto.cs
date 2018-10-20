namespace GameOfLife.Dtos
{
    public class GameStatusDto
    {
        public GameStatusDto()
        {

        }
        public GameStatusDto(int columns, int rows)
        {
            Rows = rows;
            Columns = columns;
            Board = new bool[columns, rows];
        }

        public void Evolve()
        {

        }
        public int Generation { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public bool[,] Board { get; set; }
    }
}
