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

        public PageMain()
        {
            InitializeComponent();

            _model = new Model();
            _controller = new Controller(_model, this);
            StopnieCelciusza.IsChecked = true;
            Interwal1s.IsChecked = true;
            KolumnaWartosc.IsChecked = true;
            KolumnaMin.IsChecked = true;
            KolumnaMax.IsChecked = true;
        }

        private void MenuItem_Click_OProgramie(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nazwa programu: CPInfo_GUI\nWersja: 1.0.2.1\nAutor: Paweł Choszczyk", "O programie", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Click_StopnieCelciusza(object sender, RoutedEventArgs e)
        {
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerJednostkaTemperatury("Stopnie Celciusza");
        }

        private void MenuItem_Click_StopnieFarenheita(object sender, RoutedEventArgs e)
        {
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerJednostkaTemperatury("Stopnie Farenheita");
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
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerAktualizacjaInterwalow("250 ms");

        }

        private void Interwal500ms_Click(object sender, RoutedEventArgs e)
        {
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerAktualizacjaInterwalow("500 ms");
        }

        private void Interwal1s_Click(object sender, RoutedEventArgs e)
        {
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerAktualizacjaInterwalow("1 s");
        }

        private void Interwal2s_Click(object sender, RoutedEventArgs e)
        {
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerAktualizacjaInterwalow("2 s");
        }

        private void Interwal5s_Click(object sender, RoutedEventArgs e)
        {
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerAktualizacjaInterwalow("5 s");
        }

        private void Interwal10s_Click(object sender, RoutedEventArgs e)
        {
            WybierzTylkoJedenMenuItem(sender);
            _controller.ControlerAktualizacjaInterwalow("10 s");
        }

        private void Kolumna_Click(object sender, RoutedEventArgs e)
        {
            
            _controller.ControlerWyswietlanieKolumny(sender);
        }

        private void ZapiszJako_Click(object sender, RoutedEventArgs e)
        {
            _controller.ControlerPobranePobierz();
        }

        private void PrzyciskWybierz_Click(object sender, RoutedEventArgs e)
        {
            var wybranyPodzespol = (PodzespolComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (!string.IsNullOrEmpty(wybranyPodzespol))
            {
                DataGridCzujniki.ItemsSource = null;

                DataGridCzujniki.ItemsSource = _controller.ControlerInformacjeOPodzespolach(wybranyPodzespol);
            }
            else
            {
                MessageBox.Show("Nie wybrano podzesołu");
            }

        }

        private void Zakoncz_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
