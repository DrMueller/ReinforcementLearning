using System;
using System.Collections.Generic;

namespace Mmu.Rl.WpfUi.Domain.Models.Environments
{
    public class Maze
    {
        private Maze(IReadOnlyCollection<MazeField> fields)
        {
            Fields = fields;
        }

        public IReadOnlyCollection<MazeField> Fields { get; }

        public static Maze Create(int mazeSize)
        {
            var rnd = new Random();
            var winnerCol = rnd.Next(mazeSize - 1);
            var winnerRow = rnd.Next(mazeSize - 1);
            var fields = new List<MazeField>();

            for (var row = 0; row < mazeSize; row++)
            {
                for (var col = 0; col < mazeSize; col++)
                {
                    fields.Add(new MazeField(row, col, col == winnerCol && row == winnerRow));
                }
            }

            return new Maze(fields);
        }
    }
}