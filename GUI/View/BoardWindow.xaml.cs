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

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        private BoardWindowView vm;
        public BoardWindow(Model.User user)
        {
            InitializeComponent();
            this.vm = new BoardWindowView(user);
            this.DataContext = vm;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            vm.Logout();
            this.Close();
        }

        private void AddColumn_Click(object sender, RoutedEventArgs e)
        {
            vm.AddColumn();
            vm.ReLoad();
        }

        private void RemoveColumn_Click(object sender, RoutedEventArgs e)
        {
            if (vm.RemoveColumn(ColumnList.SelectedIndex))
            {
                vm.ReLoad();
            }
        }

        private void MoveRight_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Move(ColumnList.SelectedIndex,1))
            {
                vm.ReLoad();
            }
        }

        private void MoveLeft_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Move(ColumnList.SelectedIndex, -1))
            {
                vm.ReLoad();
            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            vm.AddTask();
            vm.ReLoad();
        }

        private void AdvanceTask_Click(object sender, RoutedEventArgs e)
        {
            if (vm.AdvanceTask(Task))
            {

            }
        }

        private void RenameColumn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
