using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace UWP_Lectura_Texto_Asincrono
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Debug.WriteLine(ApplicationData.Current.LocalFolder);
        }

        private async void Lectura_Fichero (object sender, RoutedEventArgs e)
        {
            String textoFinal = "";
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await folder.GetFileAsync("Educación_Fundamental.txt");
            var readFile = await Windows.Storage.FileIO.ReadLinesAsync(file);
            
            Debug.WriteLine("Total lineas: " + readFile.Count);
            for (int i = 1; i <= readFile.Count; i++)
            {
                textoFinal += "\n" + readFile[i - 1];

                float percent = ((float)i / readFile.Count ) * 100;
                Debug.WriteLine (percent);
                Debug.WriteLine(readFile[i - 1]);
                Windows.UI.Core.DispatchedHandler porcentajeTexto = () =>
                {
                    texto_porcentaje.Text = percent.ToString();
                };
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, porcentajeTexto);
            }

            texto_Fichero.Text = textoFinal;
        }
    }
}
