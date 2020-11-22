namespace ConsoleApp4.Models
{
    public class QCell
    {
        public Action Action { get; }
        public int FieldIndex { get; }
        public double QValue { get; private set; }

        public QCell(int fieldIndex, Action action, double qValue)
        {
            FieldIndex = fieldIndex;
            Action = action;
            QValue = qValue;
        }

        public void UpdateQValue(double value)
        {
            QValue = value;
        }
    }
}