using CPInfo_GUI.model;
using CPInfo_GUI.Models;
using CPInfo_GUI.view;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CPInfo_GUI.Controllers
{
    internal class Controller
    {
        private Model _model;
        private PageMain _view;
        private DispatcherTimer _timer;

        public string JednostkaTemperatury { get; set; }
        public string AktualizacjaInterwalow { get; set; }
        public List<string> KolumnyWyswietlane { get; set; }


        public Controller(Model model, PageMain view)
        {
            this._model = model;
            this._view = view;
            this.JednostkaTemperatury = "°C";
            this.AktualizacjaInterwalow = "1 s";
            this.KolumnyWyswietlane = new List<string>() { "Wartość", "Min", "Max" };


            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
            _model.DaneCzujnikow("CPU", "°C");
            _view.DataGridCzujniki.ItemsSource = _model.ListaCzujnikowInfo;
        }

        public void ControlerWyswietlanieKolumny(object sender)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string kolumna)
            {
                if (menuItem.IsChecked)
                {
                    if (!KolumnyWyswietlane.Contains(kolumna))
                    {
                        KolumnyWyswietlane.Add(kolumna);
                    }
                }
                else
                {
                    KolumnyWyswietlane.Remove(kolumna);
                }
            }
            _view.DataGridCzujniki.Columns[2].Visibility = KolumnyWyswietlane.Contains("Wartość") ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            _view.DataGridCzujniki.Columns[3].Visibility = KolumnyWyswietlane.Contains("Min") ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            _view.DataGridCzujniki.Columns[4].Visibility = KolumnyWyswietlane.Contains("Max") ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public void ControlerJednostkaTemperatury(string wyborTemperatury)
        {
            if (wyborTemperatury.Equals("Stopnie Celciusza"))
            {
                JednostkaTemperatury = "°C";
            }
            else if (wyborTemperatury.Equals("Stopnie Farenheita"))
            {
                JednostkaTemperatury = "°F";
            }

        }

        public void ControlerAktualizacjaInterwalow(string aktualizacjaInterwalow)
        {
            AktualizacjaInterwalow = aktualizacjaInterwalow;
            _timer.Interval = TimeSpan.FromMilliseconds(HelperKonwerter.KonverterMilisekundy(aktualizacjaInterwalow));
        }

        public void ControlerPobranePobierz()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "Zapisz plik jako",
                Filter = "Pliki tekstowe (*.txt)|*txt|Wszytkie pliki (*.*)|*.*",
                DefaultExt = ".txt",
                FileName = "NowyPlik",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                SpecyfikacjaKomputera specyfikacjaKomputera = new SpecyfikacjaKomputera();
                specyfikacjaKomputera.ZapisDoPliku(saveFileDialog.FileName);
            }
            
        }


        public List<CzujnikiInfo> ControlerInformacjeOPodzespolach(string wyborPodzespolu)
        {
            _model.WylaczenieWszystkichPodzespolow();
            _model.DaneCzujnikow(wyborPodzespolu, JednostkaTemperatury);
            if (!_model.ListaCzujnikowInfo.Any())
            {
                MessageBox.Show($"Nie udało się pobrać informacji o podzespole: {wyborPodzespolu}", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return _model.ListaCzujnikowInfo;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            _model.AktualizacjaCzujnikow(JednostkaTemperatury);

            _view.DataGridCzujniki.ItemsSource = null;
            _view.DataGridCzujniki.ItemsSource = _model.ListaCzujnikowInfo;
        }
    }
}
