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


namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Service service;
        private MainWindowViewLogin vm;
        public MainWindow()
        {
            InitializeComponent();
            this.service = new Service();
            this.vm = new MainWindowViewLogin();
            this.DataContext = vm;
            service.LoadData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.Login(service, vm.Email, vm.Password);
                      
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.Register(service, vm.RegEmail, vm.RegPassword, vm.NickName, vm.Host);
        }
    }
}
