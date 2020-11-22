﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Mmu.Rl.WpfUi.Services;

namespace Mmu.Rl.WpfUi.Models.Environments
{
    public class Environment
    {
        private readonly int _mazeSize;
        private State _currentState;
        private Maze _maze;

        private readonly IDictionary<Action, Func<MazeField, ActionResult>> _moves;

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

        public void Render(Canvas canvas)
        {
            EnvironmentRenderer.Render(
                _maze,
                _currentState,
                canvas);
        }

        public Observation Reset()
        {
            _currentState = new State(0, 0);
            _maze = Maze.Create(_mazeSize);

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
                return new ActionResult(new Observation(_currentState), new Reward(0), true);
            }

            return _moves[action](winningField);
        }

        private(bool, ActionResult) CheckForBorders(Action action)
        {
            var caughtBorder = false;

            switch (action)
            {
                case Action.Down when _currentState.Row == 0:
                    caughtBorder = true;

                    break;
                case Action.Up when _currentState.Row == _mazeSize:
                    caughtBorder = true;

                    break;
                case Action.Left when _currentState.Column == 0:
                    caughtBorder = true;

                    break;
                case Action.Right when _currentState.Column == _mazeSize:
                    caughtBorder = true;

                    break;
            }

            if (caughtBorder)
            {
                return (true, new ActionResult(new Observation(_currentState), new Reward(-100), false));
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