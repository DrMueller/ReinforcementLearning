using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Mmu.Rl.WpfUi.Models;
using Mmu.Rl.WpfUi.Models.Environments;

namespace Mmu.Rl.WpfUi.Services
{
    public static class EnvironmentRenderer
    {
        public static void Render(
            Maze maze,
            State state,
            Canvas canvas)
        {
            canvas.Dispatcher.Invoke(
                () =>
                {
                    foreach(var field in maze.Fields)
                    {
                        RenderField(field, state, canvas);
                    }

                    var mw = (MainWindow)Application.Current.MainWindow;
                    mw.UpdateLayout();
                });
        }

        private static void RenderField(MazeField field, State state, Canvas canvas)
        {
            var rectangle = new Rectangle
            {
                Height = 30,
                Width = 30,
            };

            var blackBrush = new SolidColorBrush
            {
                Color = Colors.Black
            };

            Canvas.SetLeft(rectangle, field.Column * 30);
            Canvas.SetTop(rectangle, field.Row * 30);

            rectangle.StrokeThickness = 1;
            rectangle.Fill = MapBrush(field, state);
            rectangle.Stroke = blackBrush;

            canvas.Children.Add(rectangle);
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
    }
}

