using CPInfo_GUI.model;
using CPInfo_GUI.Models;
using CPInfo_GUI.view;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPInfo_GUI.Controllers
{
    internal class Controller
    {
        private Model _model;
        private PageMain _view;

        public string JednostkaTemperatury { get; set; }
        public string AktualizacjaInterwalow { get; set; }
        public List<string> KolumnyWyswietlane { get; set; }


        public Controller(Model model, PageMain view)
        {
            this._model = model;
            this._view = view;
            this.JednostkaTemperatury = "°C";
            this.AktualizacjaInterwalow = "1 s";
            this.KolumnyWyswietlane = new List<string>() { "Wartość" };

        }

        public void ControlerWyswietlanieKolumny()
        {
            KolumnyWyswietlane.Clear();
            KolumnyWyswietlane.AddRange(_view.WybraneKolumny);
        }

        public void ControlerJednostkaTemperatury()
        {
            string wyborTemperatury = _view.WidokTemperatur;
            string jednostka = string.Empty;
            if (wyborTemperatury.Equals("Stopnie Celciusza"))
            {
                JednostkaTemperatury = "°C";
            }
            else if (wyborTemperatury.Equals("Stopnie Farenheita"))
            {
                JednostkaTemperatury = "°F";
            }

        }

        public void ControlerAktualizacjaInterwalow()
        {
            string wyborAktualizajiInterwalow = _view.WidokAktualizacjaInterwalow;
            AktualizacjaInterwalow = wyborAktualizajiInterwalow;
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
        

        
    }
}
