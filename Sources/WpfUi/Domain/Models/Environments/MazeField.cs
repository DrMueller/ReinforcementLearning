namespace Mmu.Rl.WpfUi.Domain.Models.Environments
{
    public class MazeField
    {
        public MazeField(int row, int column, bool isWinningPoint)
        {
            Row = row;
            Column = column;
            IsWinningPoint = isWinningPoint;
        }

        public int Column { get; }
        public bool IsWinningPoint { get; }
        public int Row { get; }
    }
}