namespace Mmu.Rl.WpfUi.Domain.Models
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