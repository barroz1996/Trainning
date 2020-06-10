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
    /// Interaction logic for SetLimitWindow.xaml
    /// </summary>
    public partial class SetLimitWindow : Window
    {
        private SetBoardWindowView vm;
        public SetLimitWindow(BackendController controller, string email, int ordinal, string limit)
        {
            InitializeComponent();
            this.vm = new SetBoardWindowView(controller, email, limit, ordinal);
            this.DataContext = vm;
        }

        private void SetLimit_Click(object sender, RoutedEventArgs e)
        {
            vm.setLimit(this);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
