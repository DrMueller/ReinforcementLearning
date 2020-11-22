using System.Collections.Generic;
using System.Linq;
using ConsoleApp4.Models;

namespace ConsoleApp4.Services
{
    internal static class QTableFactory
    {
        internal static QTable Create(int amountOfFields)
        {
            var cells = new List<QCell>();

            for (var i = 0; i < amountOfFields; i++)
            {
                cells.Add(new QCell(i, Action.Down, -1));
                cells.Add(new QCell(i, Action.Left, -1));
                cells.Add(new QCell(i, Action.Right, -1));
                cells.Add(new QCell(i, Action.Up, -1));
            }

            return new QTable(cells);
        }
    }
}