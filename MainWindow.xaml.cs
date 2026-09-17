using Microsoft.UI.Xaml;
using System;

namespace Idle_Game
{
    public sealed partial class MainWindow : Window
    {
        private DispatcherTimer gameTimer;
        private DispatcherTimer autoClickTimer1;
        private DispatcherTimer autoClickTimer2;
        private DispatcherTimer autoClickTimer3;

        private int Strawberries = 0;
        private int produceAmount = 10000;

        private int upgradeAmount1 = 0;
        private int upgradeAmount2 = 0;
        private int upgradeAmount3 = 0;

        private int upgradePrice1 = 100;
        private int upgradePrice2 = 1000;
        private int upgradePrice3 = 10000;


        public MainWindow()
        {
            this.InitializeComponent();

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(100);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            autoClickTimer1 = new DispatcherTimer();
            autoClickTimer1.Interval = TimeSpan.FromMilliseconds(1000);
            autoClickTimer1.Tick += AutoClickTimer_Tick;

            autoClickTimer2 = new DispatcherTimer();
            autoClickTimer2.Interval = TimeSpan.FromMilliseconds(750);
            autoClickTimer2.Tick += AutoClickTimer_Tick;

            autoClickTimer3 = new DispatcherTimer();
            autoClickTimer3.Interval = TimeSpan.FromMilliseconds(500);
            autoClickTimer3.Tick += AutoClickTimer_Tick;
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
            if (Strawberries >= upgradePrice1)
            {
                Strawberries -= upgradePrice1;

                upgradePrice1 *= 2;
                UpgradeButton1.Content = $"Upgrade Growth Speed ({upgradePrice1})";

                if (upgradeAmount1 == 0)
                {
                    produceAmount += 10;
                    upgradeAmount1++;
                }
                else if (upgradeAmount1 == 1)
                {
                    produceAmount += 20;
                    upgradeAmount1++;
                }
                else if (upgradeAmount1 == 2)
                {
                    produceAmount += 40;
                    UpgradeButton1.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Upgrade_Click2(object sender, object e)
        {
            if (Strawberries >= upgradePrice2)
            {
                Strawberries -= upgradePrice2;

                upgradePrice2 *= 2;
                UpgradeButton2.Content = $"Upgrade Watering System ({upgradePrice2})";

                if (upgradeAmount2 == 0)
                {
                    produceAmount += 50;
                    upgradeAmount2++;
                }
                else if (upgradeAmount2 == 1)
                {
                    produceAmount += 100;
                    upgradeAmount2++;
                }
                else if (upgradeAmount2 == 2)
                {
                    produceAmount += 200;
                    UpgradeButton2.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Upgrade_Click3(object sender, object e)
        {
            if (Strawberries >= upgradePrice3)
            {
                Strawberries -= upgradePrice3;

                upgradePrice3 *= 2;
                UpgradeButton3.Content = $"Upgrade Fertilizer ({upgradePrice3})";

                if (upgradeAmount3 == 0)
                {
                    produceAmount += 100;
                    upgradeAmount3++;
                }
                else if (upgradeAmount3 == 1)
                {
                    produceAmount += 200;
                    upgradeAmount3++;
                }
                else if (upgradeAmount3 == 2)
                {
                    produceAmount += 400;
                    UpgradeButton3.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Auto_Click1(object sender, object e)
        {
            if (Strawberries >= 100)
            {
                Strawberries -= 100;

                AutoButton1.Visibility = Visibility.Collapsed;

                autoClickTimer1.Start();

                System.Diagnostics.Debug.WriteLine("AutoProducer Producing Every 1 Second.");
            }
        }

        private void Auto_Click2(object sender, object e)
        {
            if (Strawberries >= 1000)
            {
                Strawberries -= 1000;

                AutoButton2.Visibility = Visibility.Collapsed;

                autoClickTimer2.Start();

                System.Diagnostics.Debug.WriteLine("AutoProducer Producing Every 0,75 Second.");
            }
        }

        private void Auto_Click3(object sender, object e)
        {
            if (Strawberries >= 10000)
            {
                Strawberries -= 10000;

                AutoButton3.Visibility = Visibility.Collapsed;

                autoClickTimer3.Start();

                System.Diagnostics.Debug.WriteLine("AutoProducer Producing Every 0,50 Second.");
            }
        }
    }
}
