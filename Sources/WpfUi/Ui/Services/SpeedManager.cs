namespace Mmu.Rl.WpfUi.Ui.Services
{
    internal static class SpeedManager
    {
        public static int SpeedInMiliSeconds { get; private set; } = 10;

        internal static void MaxSpeed()
        {
            SpeedInMiliSeconds = 1;
        }

        internal static void MinSpeed()
        {
            SpeedInMiliSeconds = 1000;
        }

        internal static void SlowDown()
        {
            SpeedInMiliSeconds -= 10;
        }

        internal static void SpeedUp()
        {
            SpeedInMiliSeconds += 10;
        }
    }
}