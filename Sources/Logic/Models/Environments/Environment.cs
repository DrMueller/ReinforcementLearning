using System;
using System.Linq;
using System.Text;

namespace ConsoleApp4.Models.Environments
{
    public class Environment
    {
        private readonly int _mazeSize;
        private int _currentCol;
        private int _currentRow;
        private Maze _maze;

        public Environment(int mazeSize)
        {
            _mazeSize = mazeSize;
            _maze = Maze.Create(mazeSize);
        }

        public void Render()
        {
            var displayMaze = new char[_mazeSize, _mazeSize];

            var sb = new StringBuilder();

            for (var row = 0; row < _mazeSize; row++)
            {
                var rowBuilder = new StringBuilder();
                for (var col = 0; col < _mazeSize; col++)
                {
                    if (row == _currentRow && col == _currentCol)
                    {
                        rowBuilder.Append('|');
                    }
                    else
                    {
                        rowBuilder.Append('|');
                    }
                }

                sb.AppendLine(rowBuilder.ToString());
            }

            Console.Write(sb.ToString());
        }

        public int Reset()
        {
            _currentCol = 0;
            _currentRow = 0;
            _maze = Maze.Create(_mazeSize);

            return _currentCol;
        }

        public ActionResult Step(Action action)
        {
            if (action == Action.Down && _currentRow == 0)
            {
                return new ActionResult(_currentRow, new Reward(-100), false);
            }

            if (action == Action.Up && _currentRow == _mazeSize)
            {
                return new ActionResult(_currentRow, new Reward(-100), false);
            }

            if (action == Action.Left && _currentRow == 0)
            {
                return new ActionResult(_currentCol, new Reward(-100), false);
            }

            if (action == Action.Right && _currentRow == _mazeSize)
            {
                return new ActionResult(_currentCol, new Reward(-100), false);
            }

            var winningPoint = _maze.Fields.Single(f => f.IsWinningPoint);
            if (winningPoint.Column == _currentCol && winningPoint.Row == _currentRow)
            {
                return new ActionResult(0, new Reward(0), true);
            }

            if (action == Action.Down)
            {
                _currentRow++;

                if (winningPoint.Row > _currentRow)
                {
                    return new ActionResult(_currentRow, new Reward(1), false);
                }

                if (winningPoint.Row < _currentRow)
                {
                    return new ActionResult(_currentRow, new Reward(-1), false);
                }

                if (winningPoint.Row == _currentRow)
                {
                    return new ActionResult(_currentRow, new Reward(0), false);
                }
            }

            if (action == Action.Up)
            {
                _currentRow--;

                if (winningPoint.Row > _currentRow)
                {
                    return new ActionResult(_currentRow, new Reward(-1), false);
                }

                if (winningPoint.Row < _currentRow)
                {
                    return new ActionResult(_currentRow, new Reward(1), false);
                }

                if (winningPoint.Row == _currentRow)
                {
                    return new ActionResult(_currentRow, new Reward(0), false);
                }
            }

            if (action == Action.Left)
            {
                _currentCol--;

                if (winningPoint.Column > _currentCol)
                {
                    return new ActionResult(_currentCol, new Reward(-1), false);
                }

                if (winningPoint.Column < _currentCol)
                {
                    return new ActionResult(_currentCol, new Reward(1), false);
                }

                if (winningPoint.Column == _currentCol)
                {
                    return new ActionResult(_currentCol, new Reward(0), false);
                }
            }

            if (action == Action.Right)
            {
                _currentCol++;

                if (winningPoint.Column > _currentCol)
                {
                    return new ActionResult(_currentCol, new Reward(1), false);
                }

                if (winningPoint.Column < _currentCol)
                {
                    return new ActionResult(_currentCol, new Reward(-1), false);
                }

                if (winningPoint.Column == _currentCol)
                {
                    return new ActionResult(_currentCol, new Reward(0), false);
                }
            }

            throw new Exception("no idea");
        }
    }
}