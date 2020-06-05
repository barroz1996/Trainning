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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        private BoardWindowView vm;
        public BoardWindow(Service service,string email)
        {
            InitializeComponent();
            vm = new BoardWindowView(service, email);
            this.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.LogOut(vm.Email);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.MoveRight(vm.Email, ColList.SelectedIndex);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            vm.MoveLeft(vm.Email, ColList.SelectedIndex);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            vm.AddColumn(vm.Email, vm.NewColName, vm.ColOrdinal);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            vm.RemoveColumn(vm.Email, ColList.SelectedIndex);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            vm.AddTask(vm.Email, vm.NewTaskTitle, vm.NewDescription, vm.DueData);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            vm.SetLimit(vm.Email, ColList.SelectedIndex, vm.NewLimit);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            vm.GetColumn(vm.Email, ColList.SelectedIndex);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            vm.ChangeColumnName(ColList.SelectedIndex);
        }
    }
}
