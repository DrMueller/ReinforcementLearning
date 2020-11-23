using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Mmu.Rl.WpfUi.Models;
using Mmu.Rl.WpfUi.Models.Environments;
using Mmu.Rl.WpfUi.Models.QValues;

namespace Mmu.Rl.WpfUi.Services
{
    public static class EnvironmentRenderer
    {
        private static readonly IDictionary<Models.Action, Dock> _dockMap = new Dictionary<Models.Action, Dock>
        {
            { Models.Action.Down, Dock.Bottom },
            { Models.Action.Left, Dock.Left },
            { Models.Action.Right, Dock.Right },
            { Models.Action.Up, Dock.Top },
        };

        public static void Render(
            Maze maze,
            QTable qtable,
            State state,
            Canvas canvas)
        {
            canvas.Dispatcher.Invoke(
                () =>
                {
                    canvas.Children.Clear();

                    foreach (var field in maze.Fields)
                    {
                        RenderField(field, qtable, state, canvas);
                    }

                    var mw = (MainWindow)Application.Current.MainWindow;
                    mw.UpdateLayout();
                });
        }

        private static void AppendTextBlock(Panel dp, Models.Action action, IReadOnlyCollection<QCell> stateCells)
        {
            var qValue = Math.Round(stateCells.Single(f => f.Action == action).QValue, 2).ToString(CultureInfo.InvariantCulture);
            var txb = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                Text = qValue
            };

            if (action == Models.Action.Left || action == Models.Action.Right)
            {
                txb.VerticalAlignment = VerticalAlignment.Center;
            }
            else
            {
                txb.HorizontalAlignment = HorizontalAlignment.Center;
            }

            var dock = _dockMap[action];
            DockPanel.SetDock(txb, dock);
            dp.Children.Add(txb);
        }

        private static Brush MapBrush(MazeField field, State state)
        {
            if (field.IsWinningPoint)
            {
                return new SolidColorBrush
                {
                    Color = Colors.Red
                };
            }

            if (field.Column == state.Column && field.Row == state.Row)
            {
                return new SolidColorBrush
                {
                    Color = Colors.Yellow
                };
            }

            return new SolidColorBrush
            {
                Color = Colors.LightBlue
            };
        }

        private static void RenderField(MazeField field, QTable qTable, State state, Canvas canvas)
        {
            const int FieldSize = 60;

            var rectangle = new Rectangle
            {
                Height = FieldSize,
                Width = FieldSize,
            };

            var blackBrush = new SolidColorBrush
            {
                Color = Colors.Black
            };

            var dp = new DockPanel
            {
                LastChildFill = false,
                Width = FieldSize,
                Height = FieldSize,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            var qCells = qTable.Cells.Where(f => f.State.Row == field.Row && f.State.Column == field.Column).ToList();

            AppendTextBlock(dp, Models.Action.Down, qCells);
            AppendTextBlock(dp, Models.Action.Left, qCells);
            AppendTextBlock(dp, Models.Action.Right, qCells);
            AppendTextBlock(dp, Models.Action.Up, qCells);

            Canvas.SetLeft(dp, field.Column * FieldSize);
            Canvas.SetTop(dp, field.Row * FieldSize);
            Panel.SetZIndex(dp, 3);

            Canvas.SetLeft(rectangle, field.Column * FieldSize);
            Canvas.SetTop(rectangle, field.Row * FieldSize);

            rectangle.StrokeThickness = 1;
            rectangle.Fill = MapBrush(field, state);
            rectangle.Stroke = blackBrush;

            canvas.Children.Add(dp);
            canvas.Children.Add(rectangle);
        }
    }
}