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
using Presentation.ViewModel;

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        private SetBoardWindowView vm;
        public AddTaskWindow(BackendController controller, string email)
        {
            InitializeComponent();
            this.vm = new SetBoardWindowView(controller, email);
            this.DataContext = vm;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (vm.AddTask())
            {
                this.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
