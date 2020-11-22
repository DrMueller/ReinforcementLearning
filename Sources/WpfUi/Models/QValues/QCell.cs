namespace Mmu.Rl.WpfUi.Models.QValues
{
    public class QCell
    {
        public QCell(State state, Action action, double qValue)
        {
            State = state;
            Action = action;
            QValue = qValue;
        }

        public State State { get; }
        public Action Action { get; }
        public double QValue { get; private set; }

        public void UpdateQValue(double value)
        {
            QValue = value;
        }
    }
}