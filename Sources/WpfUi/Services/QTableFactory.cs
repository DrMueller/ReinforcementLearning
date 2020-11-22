using System.Collections.Generic;
using Mmu.Rl.WpfUi.Models;
using Mmu.Rl.WpfUi.Models.QValues;

namespace Mmu.Rl.WpfUi.Services
{
    internal static class QTableFactory
    {
        internal static QTable Create(int amountOfFields)
        {
            var cells = new List<QCell>();

            for (var row = 0; row < amountOfFields; row++)
            {
                for (var col = 0; col < amountOfFields; col++)
                {
                    cells.Add(new QCell(new State(row, col), Action.Down, -1));
                    cells.Add(new QCell(new State(row, col), Action.Left, -1));
                    cells.Add(new QCell(new State(row, col), Action.Right, -1));
                    cells.Add(new QCell(new State(row, col), Action.Up, -1));
                }
            }

            return new QTable(cells);
        }
    }
}