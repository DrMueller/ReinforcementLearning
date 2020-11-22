namespace Mmu.Rl.WpfUi.Models
{
    public class Observation
    {
        public Observation(State state)
        {
            State = state;
        }

        public State State { get; }
    }
}