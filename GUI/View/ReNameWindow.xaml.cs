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
    /// Interaction logic for ReNameWindow.xaml
    /// </summary>
    public partial class ReNameWindow : Window
    {
        private SetBoardWindowView vm;
        public ReNameWindow(BackendController controller, string email)
        {
            InitializeComponent();
            this.vm = new SetBoardWindowView(controller, email);
            this.DataContext = vm;
        }

        private void ChangeName_Click(object sender, RoutedEventArgs e)
        {
            if (vm.ChangeName())
                this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
