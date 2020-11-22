using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.Models.Environments
{
    public class Maze
    {
        public IReadOnlyCollection<MazeField> Fields { get; }

        public Maze(IReadOnlyCollection<MazeField> fields)
        {
            Fields = fields;
        }

        public static Maze Create(int mazeSize)
        {
            var fields = new List<MazeField>();

            for (var row = 0; row < mazeSize; row++)
            {
                for (var col = 0; col < mazeSize; col++)
                {
                    fields.Add(new MazeField(row, col, col == 5 && row == 5));
                }
            }

            return new Maze(fields);
        }
    }
}