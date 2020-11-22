using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.Models
{
    public class State
    {
        public int Index { get; }
        public Action Action { get; }

        public State(int index, Action action)
        {
            Index = index;
            Action = action;
        }
    }
}