using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Windows.Media.Animation;
using System.Threading;
using System.Text.RegularExpressions;
using KrnlAPI;

namespace WYNNUTILITIES
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KrnlApi krnlAPI = new KrnlApi();
        public MainWindow()
        {
            InitializeComponent();
            krnlAPI.Initialize();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ExitApplication_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MultiInstances_Clicked(object sender, RoutedEventArgs e)
        {
            new Mutex(true, "ROBLOX_singletonMutex");
            MessageBox.Show("On!");
        }

        private void TOPMOST_CheckChange(object sender, RoutedEventArgs e)
        {
            if (TOPMOST.IsChecked == true)
            {
                this.Topmost = true;
            }
            if (TOPMOST.IsChecked != true)
            {
                this.Topmost = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            krnlAPI.Execute(Editor.Text); //execute
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            krnlAPI.Inject(); //inject
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void setfpscap_Click(object sender, RoutedEventArgs e)
        {
            int fps = int.Parse(fpscap.Text);
            krnlAPI.SetFrameRate(fps);
        }
    }
}
