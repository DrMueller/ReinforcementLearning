namespace Mmu.Rl.WpfUi.Domain.Models.QValues
{
    public class QCell
    {
        public QCell(State state, Action action, double qValue)
        {
            State = state;
            Action = action;
            QValue = qValue;
        }

        public Action Action { get; }
        public double QValue { get; private set; }

        public State State { get; }

        public void UpdateQValue(double value)
        {
            QValue = value;
        }
    }
}