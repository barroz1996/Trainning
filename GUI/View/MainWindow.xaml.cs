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
using IntroSE.Kanban.Backend.ServiceLayer;


namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewLogin vm;
        public MainWindow()
        {
            InitializeComponent();
            this.vm = new MainWindowViewLogin();
            this.DataContext = vm;           
        }

    

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Model.User u = vm.Login();
            if (u != null)
            {
                BoardWindow boardView = new BoardWindow(u);
                boardView.Show();              
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            vm.Register();
        }
    }
}
