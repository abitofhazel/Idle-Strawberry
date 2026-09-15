using Microsoft.UI.Xaml;
using System;

namespace Idle_Game
{
    public sealed partial class MainWindow : Window
    {
        private DispatcherTimer gameTimer;
        private DispatcherTimer autoClickTimer;

        private int Strawberries = 0;
        private int produceAmount = 1;

        public MainWindow()
        {
            this.InitializeComponent();

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(100);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            autoClickTimer = new DispatcherTimer();
            autoClickTimer.Interval = TimeSpan.FromMilliseconds(1000);
            autoClickTimer.Tick += AutoClickTimer_Tick;
        }

        private void GameTimer_Tick(object sender, object e)
        {
            StrawberryText.Text = $"🍓: {Strawberries}";
        }

        private void AutoClickTimer_Tick(object sender, object e)
        {
            Strawberries += 1;
        }

        private void Produce()
        {
            Strawberries += produceAmount;
        }

        private void Produce_Click(object sender, object e)
        {
            Produce();
        }

        private void Upgrade_Click1(object sender, object e)
        {
            if (Strawberries >= 100)
            {
                Strawberries -= 100;

                UpgradeButton1.Visibility = Visibility.Collapsed;

                produceAmount = 10;
            }
        }

        private void Upgrade_Click2(object sender, object e)
        {
            if (Strawberries >= 1000)
            {
                Strawberries -= 1000;

                UpgradeButton2.Visibility = Visibility.Collapsed;

                produceAmount = 50;
            }
        }

        private void Upgrade_Click3(object sender, object e)
        {
            if (Strawberries >= 10000)
            {
                Strawberries -= 10000;

                UpgradeButton3.Visibility = Visibility.Collapsed;

                produceAmount = 100;
            }
        }

        private void Auto_Click1(object sender, object e)
        {
            if (Strawberries >= 100)
            {
                Strawberries -= 100;

                AutoButton1.Visibility = Visibility.Collapsed;

                autoClickTimer.Interval = TimeSpan.FromMilliseconds(1000);

                autoClickTimer.Start();
            }
        }

        private void Auto_Click2(object sender, object e)
        {
            if (Strawberries >= 1000)
            {
                Strawberries -= 1000;

                AutoButton1.Visibility = Visibility.Collapsed;

                autoClickTimer.Interval = TimeSpan.FromMilliseconds(67);

                autoClickTimer.Start(); 
            }
        }
    }
}
