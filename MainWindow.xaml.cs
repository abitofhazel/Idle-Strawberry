using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
using System.Xml.Linq;

namespace Idle_Game
{
    public sealed partial class MainWindow : Window
    {
        private DispatcherTimer gameTimer;
        private DispatcherTimer autoClickTimer1;
        private DispatcherTimer autoClickTimer2;
        private DispatcherTimer autoClickTimer3;
        private DispatcherTimer autoSaveTimer;
        private DispatcherTimer saveMessageTimer;

        private int Strawberries = 0;
        private int produceAmount = 1;

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

            autoSaveTimer = new DispatcherTimer();
            autoSaveTimer.Interval = TimeSpan.FromSeconds(10);
            autoSaveTimer.Tick += AutoSaveTimer_Tick;
            autoSaveTimer.Start();

            saveMessageTimer = new DispatcherTimer();
            saveMessageTimer.Interval = TimeSpan.FromSeconds(2);
            saveMessageTimer.Tick += SaveMessageTimer_Tick;

            LoadGame();
        }

        private void GameTimer_Tick(object sender, object e)
        {
            StrawberryText.Text = $"🍓: {Strawberries}";
        }

        private void AutoClickTimer_Tick(object sender, object e)
        {
            Strawberries += 1;
        }

        private void AutoSaveTimer_Tick(object sender, object e)
        {
            SaveGame();
        }

        private void Produce()
        {
            Strawberries += produceAmount;
        }

        private void Save_Click(object sender, object e)
        {
            SaveGame();

            SaveButton.Content = "Opgeslagen!";

            saveMessageTimer.Start();
        }

        private void SaveMessageTimer_Tick(object sender, object e)
        {
            SaveButton.Content = "Opslaan";

            saveMessageTimer.Stop();
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
                    produceAmount = 10;
                    upgradeAmount1++;
                }
                else if (upgradeAmount1 == 1)
                {
                    produceAmount = 20;
                    upgradeAmount1++;
                }
                else if (upgradeAmount1 == 2)
                {
                    produceAmount = 40;
                    upgradeAmount1++;
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
                    produceAmount = 50;
                    upgradeAmount2++;
                }
                else if (upgradeAmount2 == 1)
                {
                    produceAmount = 100;
                    upgradeAmount2++;
                }
                else if (upgradeAmount2 == 2)
                {
                    produceAmount = 200;
                    upgradeAmount2++;
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
                    produceAmount = 100;
                    upgradeAmount3++;
                }
                else if (upgradeAmount3 == 1)
                {
                    produceAmount = 200;
                    upgradeAmount3++;
                }
                else if (upgradeAmount3 == 2)
                {
                    produceAmount = 400;
                    upgradeAmount3++;
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

                private XDocument GameState => new XDocument(
            new XElement("Game",
                new XElement("Strawberries", Strawberries),
                new XElement("ProduceAmount", produceAmount),

                new XElement("Upgrades",
                    new XElement("Upgrade1",
                        new XElement("Amount", upgradeAmount1),
                        new XElement("Price", upgradePrice1)
                    ),
                    new XElement("Upgrade2",
                        new XElement("Amount", upgradeAmount2),
                        new XElement("Price", upgradePrice2)
                    ),
                    new XElement("Upgrade3",
                        new XElement("Amount", upgradeAmount3),
                        new XElement("Price", upgradePrice3)
                    )
                ),

                new XElement("AutoProducers",
                    new XElement("AutoProducer1",
                        new XElement("Enabled", autoClickTimer1.IsEnabled),
                        new XElement("Interval", autoClickTimer1.Interval.TotalMilliseconds)
                    ),
                    new XElement("AutoProducer2",
                        new XElement("Enabled", autoClickTimer2.IsEnabled),
                        new XElement("Interval", autoClickTimer2.Interval.TotalMilliseconds)
                    ),
                    new XElement("AutoProducer3",
                        new XElement("Enabled", autoClickTimer3.IsEnabled),
                        new XElement("Interval", autoClickTimer3.Interval.TotalMilliseconds)
                    )
                ),

                new XElement("Timestamp", DateTime.Now)
            )
        );

        private void SaveGame()
        {
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "savegame.xml"
            );

            GameState.Save(path);
        }

        private async void LoadGame()
        {
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "savegame.xml"
            );

            if (!File.Exists(path))
                return;

            try
            {
                XDocument doc = XDocument.Load(path);

                Strawberries = int.Parse(
                    doc.Root.Element("Strawberries").Value
                );

                produceAmount = int.Parse(
                    doc.Root.Element("ProduceAmount").Value
                );

                XElement upgrades = doc.Root.Element("Upgrades");

                upgradeAmount1 = int.Parse(
                    upgrades.Element("Upgrade1").Element("Amount").Value
                );

                upgradePrice1 = int.Parse(
                    upgrades.Element("Upgrade1").Element("Price").Value
                );

                upgradeAmount2 = int.Parse(
                    upgrades.Element("Upgrade2").Element("Amount").Value
                );

                upgradePrice2 = int.Parse(
                    upgrades.Element("Upgrade2").Element("Price").Value
                );

                upgradeAmount3 = int.Parse(
                    upgrades.Element("Upgrade3").Element("Amount").Value
                );

                upgradePrice3 = int.Parse(
                    upgrades.Element("Upgrade3").Element("Price").Value
                );

                XElement autos = doc.Root.Element("AutoProducers");

                double interval1 = double.Parse(
                    autos.Element("AutoProducer1").Element("Interval").Value
                );

                double interval2 = double.Parse(
                    autos.Element("AutoProducer2").Element("Interval").Value
                );

                double interval3 = double.Parse(
                    autos.Element("AutoProducer3").Element("Interval").Value
                );

                autoClickTimer1.Interval =
                    TimeSpan.FromMilliseconds(interval1);

                autoClickTimer2.Interval =
                    TimeSpan.FromMilliseconds(interval2);

                autoClickTimer3.Interval =
                    TimeSpan.FromMilliseconds(interval3);

                if (bool.Parse(
                    autos.Element("AutoProducer1").Element("Enabled").Value))
                {
                    autoClickTimer1.Start();
                    AutoButton1.Visibility = Visibility.Collapsed;
                }

                if (bool.Parse(
                    autos.Element("AutoProducer2").Element("Enabled").Value))
                {
                    autoClickTimer2.Start();
                    AutoButton2.Visibility = Visibility.Collapsed;
                }

                if (bool.Parse(
                    autos.Element("AutoProducer3").Element("Enabled").Value))
                {
                    autoClickTimer3.Start();
                    AutoButton3.Visibility = Visibility.Collapsed;
                }

                StrawberryText.Text = $"🍓: {Strawberries}";

                UpgradeButton1.Content =
                    $"Upgrade Growth Speed ({upgradePrice1})";

                UpgradeButton2.Content =
                    $"Upgrade Watering System ({upgradePrice2})";

                UpgradeButton3.Content =
                    $"Upgrade Fertilizer ({upgradePrice3})";

                if (upgradeAmount1 >= 3)
                    UpgradeButton1.Visibility = Visibility.Collapsed;

                if (upgradeAmount2 >= 3)
                    UpgradeButton2.Visibility = Visibility.Collapsed;

                if (upgradeAmount3 >= 3)
                    UpgradeButton3.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Savebestand kon niet worden geladen."
                );


                Strawberries = 0;
                produceAmount = 1;

                upgradeAmount1 = 0;
                upgradeAmount2 = 0;
                upgradeAmount3 = 0;

                upgradePrice1 = 100;
                upgradePrice2 = 1000;
                upgradePrice3 = 10000;

                autoClickTimer1.Stop();
                autoClickTimer2.Stop();
                autoClickTimer3.Stop();

                AutoButton1.Visibility = Visibility.Visible;
                AutoButton2.Visibility = Visibility.Visible;
                AutoButton3.Visibility = Visibility.Visible;

                UpgradeButton1.Visibility = Visibility.Visible;
                UpgradeButton2.Visibility = Visibility.Visible;
                UpgradeButton3.Visibility = Visibility.Visible;

                UpgradeButton1.Content = "Upgrade Growth Speed (100)";
                UpgradeButton2.Content = "Upgrade Watering System (1000)";
                UpgradeButton3.Content = "Upgrade Fertilizer (10000)";

                StrawberryText.Text = "🍓: 0";
            }
        }
    }
}
