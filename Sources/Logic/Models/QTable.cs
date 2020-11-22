using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp4.Models
{
    public class QTable
    {
        public IReadOnlyCollection<QCell> Cells { get; }

        public QTable(IReadOnlyCollection<QCell> cells)
        {
            Cells = cells;
        }

        public Action GetNextAction(int state)
        {
            return Cells
                .Where(f => f.FieldIndex == state)
                .OrderByDescending(f => f.QValue)
                .First()
                .Action;
        }

        public double GetQValue(State state)
        {
            return Cells.Single(f => f.Action == state.Action && f.FieldIndex == state.Index).QValue;
        }

        public void SetQValue(State state, double value)
        {
            Cells.Single(f => f.Action == state.Action && f.FieldIndex == state.Index).UpdateQValue(value);
        }
    }
}