using System.Collections.Generic;
using System.Linq;

namespace Mmu.Rl.WpfUi.Models.QValues
{
    public class QTable
    {
        public QTable(IReadOnlyCollection<QCell> cells)
        {
            Cells = cells;
        }

        public IReadOnlyCollection<QCell> Cells { get; }

        public Action GetNextAction(State state)
        {
            return Cells
                .Where(cell => cell.State == state)
                .OrderByDescending(f => f.QValue)
                .First()
                .Action;
        }

        public double GetQValue(State state, Action action)
        {
            return Cells.Single(f => f.Action == action && f.State == state).QValue;
        }

        public void SetQValue(State state, Action action, double value)
        {
            Cells.Single(f => f.Action == action && f.State == state).UpdateQValue(value);
        }
    }
}