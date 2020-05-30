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
using System.Windows.Shapes;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace GUI
{
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        private Service service;
        private BoardWindowView vm;
        private string email;
        public BoardWindow(Service service,string email)
        {
            InitializeComponent();
            this.service = service;
            this.email = email;
            vm = new BoardWindowView(service, email);
            this.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.LogOut(this.service, this.email);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.MoveRight(service, email, ColList.SelectedIndex);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            vm.MoveLeft(service, email, ColList.SelectedIndex);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            vm.AddColumn(service, email, vm.NewColName, vm.ColOrdinal);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            vm.RemoveColumn(service, email, ColList.SelectedIndex);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            vm.AddTask(service, email, vm.NewTaskTitle, vm.NewDescription, vm.DueData);
        }
    }
}
