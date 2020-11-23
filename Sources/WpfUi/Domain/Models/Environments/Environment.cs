using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Mmu.Rl.WpfUi.Domain.Models.QValues;
using Mmu.Rl.WpfUi.Ui.Services;

namespace Mmu.Rl.WpfUi.Domain.Models.Environments
{
    public class Environment
    {
        private readonly Maze _maze;
        private readonly int _mazeSize;

        private readonly IDictionary<Action, Func<MazeField, ActionResult>> _moves;
        private State _currentState;

        public Environment(int mazeSize)
        {
            _mazeSize = mazeSize;
            _maze = Maze.Create(mazeSize);
            _moves = new Dictionary<Action, Func<MazeField, ActionResult>>
            {
                { Action.Down, MoveDown },
                { Action.Left, MoveLeft },
                { Action.Right, MoveRight },
                { Action.Up, MoveUp }
            };
        }

        public void InitializeRenderer(Canvas canvas)
        {
            Renderer.InitializeRenderer(_maze, canvas);
        }

        public void Render(QTable qTable)
        {
            Renderer.RenderEnvironment(qTable, _currentState);
        }

        public Observation Reset()
        {
            var rnd = new Random();
            var startingRow = rnd.Next(_mazeSize);
            var startingCol = rnd.Next(_mazeSize);
            _currentState = new State(startingRow, startingCol);

            return new Observation(_currentState);
        }

        public ActionResult Step(Action action)
        {
            var (caughtBorder, actionResult) = CheckForBorders(action);

            if (caughtBorder)
            {
                return actionResult;
            }

            var winningField = _maze.Fields.Single(f => f.IsWinningPoint);

            if (winningField.Column == _currentState.Column && winningField.Row == _currentState.Row)
            {
                return new ActionResult(new Observation(_currentState), new Reward(1000), true);
            }

            return _moves[action](winningField);
        }

        private(bool, ActionResult) CheckForBorders(Action action)
        {
            var caughtBorder = false;

            switch (action)
            {
                case Action.Down when _currentState.Row == _mazeSize - 1:
                    caughtBorder = true;

                    break;
                case Action.Up when _currentState.Row == 0:
                    caughtBorder = true;

                    break;
                case Action.Left when _currentState.Column == 0:
                    caughtBorder = true;

                    break;
                case Action.Right when _currentState.Column == _mazeSize - 1:
                    caughtBorder = true;

                    break;
            }

            if (caughtBorder)
            {
                return (true, new ActionResult(new Observation(_currentState), new Reward(-1000), true));
            }

            return (false, null);
        }

        private ActionResult MoveDown(MazeField winningField)
        {
            _currentState = new State(_currentState.Row + 1, _currentState.Column);

            if (winningField.Row > _currentState.Row)
            {
                return ActionResult.CreateMovesTowards(_currentState);
            }

            if (winningField.Row < _currentState.Row)
            {
                return ActionResult.CreateMovesAway(_currentState);
            }

            if (winningField.Row == _currentState.Row)
            {
                return ActionResult.CreateNeutralMove(_currentState);
            }

            throw new Exception("no idea");
        }

        private ActionResult MoveLeft(MazeField winningField)
        {
            _currentState = new State(_currentState.Row, _currentState.Column - 1);

            if (winningField.Column < _currentState.Column)
            {
                return ActionResult.CreateMovesTowards(_currentState);
            }

            if (winningField.Column > _currentState.Column)
            {
                return ActionResult.CreateMovesAway(_currentState);
            }

            if (winningField.Column == _currentState.Column)
            {
                return ActionResult.CreateNeutralMove(_currentState);
            }

            throw new Exception("no idea");
        }

        private ActionResult MoveRight(MazeField winningField)
        {
            _currentState = new State(_currentState.Row, _currentState.Column + 1);

            if (winningField.Column > _currentState.Column)
            {
                return ActionResult.CreateMovesTowards(_currentState);
            }

            if (winningField.Column < _currentState.Column)
            {
                return ActionResult.CreateMovesAway(_currentState);
            }

            if (winningField.Column == _currentState.Column)
            {
                return ActionResult.CreateNeutralMove(_currentState);
            }

            throw new Exception("no idea");
        }

        private ActionResult MoveUp(MazeField winningField)
        {
            _currentState = new State(_currentState.Row - 1, _currentState.Column);

            if (winningField.Row < _currentState.Row)
            {
                return ActionResult.CreateMovesTowards(_currentState);
            }

            if (winningField.Row > _currentState.Row)
            {
                return ActionResult.CreateMovesAway(_currentState);
            }

            if (winningField.Row == _currentState.Row)
            {
                return ActionResult.CreateNeutralMove(_currentState);
            }

            throw new Exception("no idea");
        }
    }
}