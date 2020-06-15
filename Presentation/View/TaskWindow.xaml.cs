using Presentation.ViewModel;
using System.Windows;

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private TaskViewModel vm;
        public TaskWindow(BackendController controller, string email, Model.Task task)
        {
            InitializeComponent();
            vm = new TaskViewModel(controller, email, task);
            DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.UpdateTitle();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.UpdateDescription();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            vm.UpdateDueDate();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            vm.UpdateEmailAssignee();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
