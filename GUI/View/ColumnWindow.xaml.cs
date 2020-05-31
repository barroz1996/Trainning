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
using GUI.ViewModel;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for ColumnWindow.xaml
    /// </summary>
    public partial class ColumnWindow : Window
    {
        private ColumnViewModel vm;
        public ColumnWindow(Service service,string email,int columnOrdinal)
        { 
            InitializeComponent();
            vm = new ColumnViewModel(service,email,columnOrdinal);
            this.DataContext = vm; 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.DeleteTask((Model.Task)Tasks.SelectedItem);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.AssignTask((Model.Task)Tasks.SelectedItem, vm.EmailAssignee);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            vm.AdvanceTask((Model.Task)Tasks.SelectedItem);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
           // this = vm.NextColumn;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            vm.GetTask(vm.Email, Tasks.SelectedIndex);
            vm.Reload();
        }
    }
}
