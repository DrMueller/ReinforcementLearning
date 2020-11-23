using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Mmu.Rl.WpfUi.Domain.Models;
using Mmu.Rl.WpfUi.Domain.Models.Environments;

namespace Mmu.Rl.WpfUi.Ui.Services.Servants
{
    public static class RendererInitializer
    {
        private static readonly IDictionary<Action, Dock> _dockMap = new Dictionary<Action, Dock>
        {
            { Action.Down, Dock.Bottom },
            { Action.Left, Dock.Left },
            { Action.Right, Dock.Right },
            { Action.Up, Dock.Top },
        };

        internal static void Initialize(Maze maze, Canvas canvas)
        {
            canvas.Dispatcher.Invoke(
                () =>
                {
                    canvas.Children.Clear();

                    foreach (var field in maze.Fields)
                    {
                        CreateField(field, canvas);
                    }
                });
        }

        private static void AppendTextBlock(Panel dp, Action action)
        {
            var txb = new TextBlock
            {
                Tag = action
            };

            if (action == Action.Left || action == Action.Right)
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

        private static void CreateField(MazeField field, Canvas canvas)
        {
            const int FieldSize = 60;

            var rectangle = new Rectangle
            {
                Height = FieldSize,
                Width = FieldSize,
                Tag = field
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
                Tag = field,
            };

            AppendTextBlock(dp, Action.Down);
            AppendTextBlock(dp, Action.Up);
            AppendTextBlock(dp, Action.Left);
            AppendTextBlock(dp, Action.Right);

            Canvas.SetLeft(dp, field.Column * FieldSize);
            Canvas.SetTop(dp, field.Row * FieldSize);
            Panel.SetZIndex(dp, 3);

            Canvas.SetLeft(rectangle, field.Column * FieldSize);
            Canvas.SetTop(rectangle, field.Row * FieldSize);

            rectangle.StrokeThickness = 1;
            rectangle.Stroke = blackBrush;

            canvas.Children.Add(dp);
            canvas.Children.Add(rectangle);
        }
    }
}