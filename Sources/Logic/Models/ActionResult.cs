using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.Models
{
    public class ActionResult
    {
        public int Observation { get; }
        public Reward Reward { get; }
        public bool IsDone { get; }

        public ActionResult(int observation, Reward reward, bool isDone)
        {
            Observation = observation;
            Reward = reward;
            IsDone = isDone;
        }
    }
}