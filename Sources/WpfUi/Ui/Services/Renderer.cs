using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Mmu.Rl.WpfUi.Domain.Models;
using Mmu.Rl.WpfUi.Domain.Models.Environments;
using Mmu.Rl.WpfUi.Domain.Models.QValues;
using Mmu.Rl.WpfUi.Ui.Services.Servants;

namespace Mmu.Rl.WpfUi.Ui.Services
{
    public static class Renderer
    {
        private static Canvas _canvas;
        private static Maze _maze;

        public static void InitializeRenderer(Maze maze, Canvas canvas)
        {
            _maze = maze;
            _canvas = canvas;

            RendererInitializer.Initialize(maze, canvas);
        }

        public static void RenderEnvironment(
            QTable qtable,
            State state)
        {
            _canvas.Dispatcher.Invoke(
                () =>
                {
                    foreach (var field in _maze.Fields)
                    {
                        UpdateField(field, qtable, state);
                    }

                    var mw = (MainWindow)Application.Current.MainWindow;
                    mw.UpdateLayout();
                });
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

        private static void UpdateField(MazeField field, QTable qtable, State state)
        {
            var qCells = qtable.Cells.Where(f => f.State.Column == field.Column && f.State.Row == field.Row);

            var rect = _canvas.Children.OfType<Rectangle>().Single(f => f.Tag == field);
            rect.Fill = MapBrush(field, state);

            var dp = _canvas.Children.OfType<DockPanel>().Single(f => f.Tag == field);
            var textBlocks = dp.Children.OfType<TextBlock>();

            foreach (var txb in textBlocks)
            {
                var action = (Domain.Models.Action)txb.Tag;
                var qValue = Math.Round(qCells.Single(f => f.Action == action).QValue, 2).ToString(CultureInfo.InvariantCulture);
                txb.Text = qValue;
            }
        }
    }
}