using System.Threading;
using System.Windows.Controls;
using Mmu.Rl.WpfUi.Domain.Models;
using Mmu.Rl.WpfUi.Domain.Models.Environments;

namespace Mmu.Rl.WpfUi.Domain.Services
{
    public static class Runner
    {
        public static void Run(Canvas canvas)
        {
            const double LearningRate = 0.4;
            const double DiscountFactor = 0.999;
            const int AmountOfEpisodes = 10000;
            const int MazeSize = 20;

            var qTable = QTableFactory.Create(MazeSize);
            var environment = new Environment(MazeSize);
            environment.InitializeRenderer(canvas);

            // Start new episode
            while (true)

                //for (var i = 0; i < AmountOfEpisodes; i++)
            {
                var observation = environment.Reset();
                var nextAction = qTable.GetNextAction(observation.State);

                State prevState = null;
                var prevAction = Action.Down;

                // Start timeline
                for (var timeStamp = 0; timeStamp <= 2500; timeStamp++)
                {
                    var actionResult = environment.Step(nextAction);
                    Thread.Sleep(100);
                    environment.Render(qTable);

                    nextAction = qTable.GetNextAction(actionResult.Observation.State);

                    if (prevState != null)
                    {
                        var oldQValue = qTable.GetQValue(prevState, prevAction);
                        var newQValue = oldQValue;

                        if (actionResult.IsDone)
                        {
                            // As we are in an endstate, no future rewards need to be calculated
                            newQValue += LearningRate * (actionResult.Reward.Value - oldQValue);
                        }
                        else
                        {
                            var newQTableValue = qTable.GetQValue(
                                actionResult.Observation.State,
                                nextAction);

                            // SARSA 
                            newQValue += LearningRate * (actionResult.Reward.Value * DiscountFactor * newQTableValue - oldQValue);
                        }

                        // SARSA calculates the previous value, therefore update this one
                        qTable.SetQValue(prevState, prevAction, newQValue);
                    }

                    prevState = actionResult.Observation.State;
                    prevAction = nextAction;

                    if (actionResult.IsDone)
                    {
                        break;
                    }
                }
            }
        }
    }
}