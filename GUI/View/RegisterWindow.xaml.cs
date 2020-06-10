using Presentation.ViewModel;
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

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private RegisterWindowView vm;
        public RegisterWindow(BackendController controller)
        {
            InitializeComponent();
            this.vm = new RegisterWindowView(controller);
            this.DataContext = vm;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Register())
            {
                this.Close();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
