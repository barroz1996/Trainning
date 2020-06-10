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
        }

        private void RemoveColumn_Click(object sender, RoutedEventArgs e)
        {
            vm.RemoveColumn(ColumnList.SelectedIndex);
        }

        private void MoveRight_Click(object sender, RoutedEventArgs e)
        {
            vm.Move(ColumnList.SelectedIndex, 1);
        }

        private void MoveLeft_Click(object sender, RoutedEventArgs e)
        {
            vm.Move(ColumnList.SelectedIndex, -1);
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            vm.AddTask();
        }

        private void AdvanceTask_Click(object sender, RoutedEventArgs e)
        {
            vm.AdvanceTask();
        }

        private void RenameColumn_Click(object sender, RoutedEventArgs e)
        {
            vm.ChangeName(ColumnList.SelectedIndex,((Model.Column)ColumnList.SelectedItem).Name);
        }

        private void SetLimit_Click(object sender, RoutedEventArgs e)
        {
            vm.SetLimit(ColumnList.SelectedIndex, ((Model.Column)ColumnList.SelectedItem).Limit);
        }
        private void TasksList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            vm.OpenTask();
        }
    }
}
