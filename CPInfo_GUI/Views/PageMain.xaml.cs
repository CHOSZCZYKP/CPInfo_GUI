using CPInfo_GUI.Controllers;
using CPInfo_GUI.model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CPInfo_GUI.view
{
    /// <summary>
    /// Logika interakcji dla klasy PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        private Controller _controller;
        private Model _model;
        private DispatcherTimer _timer;
        public string WidokTemperatur { get; private set; }
        public string WidokAktualizacjaInterwalow { get; private set; }
        public List<string> WybraneKolumny { get; private set; }

        //te właściwości lepiej jednak będzie jeśli zamienię na na argumenty w funkcjach

        public PageMain()
        {
            InitializeComponent();

            _model = new Model();
            _controller = new Controller(_model, this);
            StopnieCelciusza.IsChecked = true;
            Interwal1s.IsChecked = true;
            KolumnaWartosc.IsChecked = true;
            WybraneKolumny = new List<string>() { "Wartość" };
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            _model.DaneCzujnikow("CPU", "°C");
            DataGridCzujniki.ItemsSource = _model.ListaCzujnikowInfo;
        }

        private void MenuItem_Click_OProgramie(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nazwa programu: CPInfo_GUI\nWersja: 1.0.2.1\nAutor: Paweł Choszczyk", "O programie", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Click_StopnieCelciusza(object sender, RoutedEventArgs e)
        {
            WidokTemperatur = "Stopnie Celciusza";
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerJednostkaTemperatury();
        }

        private void MenuItem_Click_StopnieFarenheita(object sender, RoutedEventArgs e)
        {
            WidokTemperatur = "Stopnie Farenheita";
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerJednostkaTemperatury();
        }

        private void WybierzTylkoJedenMenuItem(object sender)
        {
            if (sender is MenuItem wybranyMenuItem)
            {
                var rodzicManuItem = ItemsControl.ItemsControlFromItemContainer(wybranyMenuItem);

                if (rodzicManuItem != null)
                {
                    foreach (var item in rodzicManuItem.Items)
                    {
                        if (item is MenuItem menuItem && menuItem.Tag?.ToString() == wybranyMenuItem.Tag?.ToString())
                        {
                            menuItem.IsChecked = false;
                        }
                    }
                }
                wybranyMenuItem.IsChecked = true;
            }
        }

        private void Interwal250ms_Click(object sender, RoutedEventArgs e)
        {
            WidokAktualizacjaInterwalow = "250 ms";
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerAktualizacjaInterwalow();

        }

        private void Interwal500ms_Click(object sender, RoutedEventArgs e)
        {
            WidokAktualizacjaInterwalow = "500 ms";
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerAktualizacjaInterwalow();
        }

        private void Interwal1s_Click(object sender, RoutedEventArgs e)
        {
            WidokAktualizacjaInterwalow = "1 s";
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerAktualizacjaInterwalow();
        }

        private void Interwal2s_Click(object sender, RoutedEventArgs e)
        {
            WidokAktualizacjaInterwalow = "2 s";
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerAktualizacjaInterwalow();
        }

        private void Interwal5s_Click(object sender, RoutedEventArgs e)
        {
            WidokAktualizacjaInterwalow = "5 s";
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerAktualizacjaInterwalow();
        }

        private void Interwal10s_Click(object sender, RoutedEventArgs e)
        {
            WidokAktualizacjaInterwalow = "10 s";
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerAktualizacjaInterwalow();
        }

        private void Kolumna_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string kolumna)
            {
                if (menuItem.IsChecked)
                {
                    if (!WybraneKolumny.Contains(kolumna))
                    {
                        WybraneKolumny.Add(kolumna);
                    }
                }
                else
                {
                    WybraneKolumny.Remove(kolumna);
                }
            }
            _controller.ControlerWyswietlanieKolumny();
        }

        /*private void KolumnaMin_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem.IsChecked)
            {
                WybraneKolumny.Add("Min");
            }
            else
            {
                WybraneKolumny.Remove("Min");
            }
            _controller.ControlerWyswietlanieKolumny();
        }

        private void KolumnaMax_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem.IsChecked)
            {
                WybraneKolumny.Add("Max");
            }
            else
            {
                WybraneKolumny.Remove("Max");
            }
            _controller.ControlerWyswietlanieKolumny();
        }*/

        private void ZapiszJako_Click(object sender, RoutedEventArgs e)
        {
            _controller.ControlerPobranePobierz();
        }

        private void PrzyciskWybierz_Click(object sender, RoutedEventArgs e)
        {
            var wybranyPodzespol = (PodzespolComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (!string.IsNullOrEmpty(wybranyPodzespol))
            {
                _model.WylaczenieWszystkichPodzespolow();
                MessageBox.Show($"Wybrano podzespóół: {wybranyPodzespol}");
                _model.DaneCzujnikow(wybranyPodzespol, "°C");
                DataGridCzujniki.ItemsSource = null;

                DataGridCzujniki.ItemsSource = _model.ListaCzujnikowInfo;
                
                
            }
            else
            {
                MessageBox.Show("Nie wybrano podzesołu");
            }

        }
        // Zdarzenie, które wywołuje się co 1 sekundę
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Aktualizacja danych czujników
            _model.AktualizacjaCzujnikow("°C");

            // Odświeżenie DataGrid
            DataGridCzujniki.ItemsSource = null;
            DataGridCzujniki.ItemsSource = _model.ListaCzujnikowInfo;
        }
    }
}
