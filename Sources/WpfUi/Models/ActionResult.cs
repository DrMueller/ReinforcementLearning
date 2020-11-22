namespace Mmu.Rl.WpfUi.Models
{
    public class ActionResult
    {
        public ActionResult(Observation observation, Reward reward, bool isDone)
        {
            Observation = observation;
            Reward = reward;
            IsDone = isDone;
        }

        public bool IsDone { get; }
        public Observation Observation { get; }
        public Reward Reward { get; }

        public static ActionResult CreateMovesAway(State state)
        {
            return new ActionResult(new Observation(state), new Reward(-1), false);
        }

        public static ActionResult CreateMovesTowards(State state)
        {
            return new ActionResult(new Observation(state), new Reward(1), false);
        }

        public static ActionResult CreateNeutralMove(State state)
        {
            return new ActionResult(new Observation(state), new Reward(0), false);
        }
    }
}