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
using CPInfo_GUI.view;

namespace CPInfo_GUI
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ShowProgressBar();
            /*Tekst_powitalny.Visibility = Visibility.Collapsed;
            Main.Content = new PageMain();*/
        }

        private void Tekst_powitalny_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ShowProgressBar();
            /*Tekst_powitalny.Visibility = Visibility.Collapsed;
            Main.Content = new PageMain();*/
        }
        private async void ShowProgressBar()
        {
            // Ukrywa tekst powitalny i pokazuje ProgressBar
            Tekst_powitalny.Visibility = Visibility.Collapsed;
            Informacja_przejscia_dalej.Visibility = Visibility.Collapsed;
            progressBar.Visibility = Visibility.Visible;

            // Symulacja jakiejś operacji, np. 3 sekundowego oczekiwania
            await Task.Delay(3500); // Użyj await, aby opóźnić zadanie asynchronicznie

            // Po upływie 3 sekund, ukryj ProgressBar
            progressBar.Visibility = Visibility.Collapsed;
            Main.Content = new PageMain();
        }
        


    }
}
