using System.Threading;
using System.Windows.Controls;
using Mmu.Rl.WpfUi.Models;
using Mmu.Rl.WpfUi.Models.Environments;

namespace Mmu.Rl.WpfUi.Services
{
    public static class Runner
    {
        public static void Run(Canvas canvas)
        {
            const double LearningRate = 0.4;
            const double DiscountFactor = 0.999;
            const int AmountOfEpisodes = 10000;
            const int MazeSize = 10;

            var qTable = QTableFactory.Create(MazeSize);
            var environment = new Environment(MazeSize);

            // Start new episode
            for (var i = 0; i < AmountOfEpisodes; i++)
            {
                var observation = environment.Reset();
                var nextAction = qTable.GetNextAction(observation.State);

                State prevState = null;
                var prevAction = Action.Down;

                // Start timeline
                for (var timeStamp = 0; timeStamp <= 2500; timeStamp++)
                {
                    Thread.Sleep(500);
                    environment.Render(canvas);
                    var actionResult = environment.Step(nextAction);
                    nextAction = qTable.GetNextAction(actionResult.Observation.State);

                    if (prevState != null)
                    {
                        var oldQValue = qTable.GetQValue(prevState, prevAction);
                        var newQValue = oldQValue;

                        if (actionResult.IsDone)
                        {
                            newQValue += LearningRate * (actionResult.Reward.Value - oldQValue);
                        }
                        else
                        {
                            // SARSA 
                            var newQTableValue = qTable.GetQValue(
                                actionResult.Observation.State,
                                nextAction);

                            newQValue += LearningRate * (actionResult.Reward.Value * DiscountFactor * newQTableValue - oldQValue);
                        }

                        qTable.SetQValue(prevState,
                            nextAction,
                            newQValue);
                    }

                    prevState = actionResult.Observation.State;
                    prevAction = nextAction;
                }
            }
        }
    }
}