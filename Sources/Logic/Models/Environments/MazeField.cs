namespace ConsoleApp4.Models.Environments
{
    public class MazeField
    {
        public int Column { get; }
        public bool IsWinningPoint { get; }
        public int Row { get; }

        public MazeField(int row, int column, bool isWinningPoint)
        {
            Row = row;
            Column = column;
            IsWinningPoint = isWinningPoint;
        }
    }
}