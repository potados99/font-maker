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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace font_maker
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(RowTextBox.Text, out int row) && Int32.TryParse(ColTextBox.Text, out int col))
            {
                new DrawWindow(row, col).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong input! only number is allowed.");
            }
        }
    }
}
