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

namespace font_maker
{
    /// <summary>
    /// DrawWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DrawWindow : Window
    {
        const int BoxSize = 20;
        int Rows;
        int Columns;
        bool[,] Pixels;

        public DrawWindow(int row, int col)
        {
            InitializeComponent();

            this.Rows = row;
            this.Columns = col;
            this.Pixels = new bool[row, col];

            SetWindowSize(row, col, BoxSize, CanvasGrid);
            CreateBoxes(row, col, BoxSize, CanvasGrid);

        }

        private void SetWindowSize(int row, int col, int boxSize, Grid container)
        {
            container.Width = col * BoxSize;
            container.Height = row * BoxSize;

            this.Width = container.Width + 30;
            this.Height = container.Height + 100;
        }

        private void CreateBoxes(int row, int col, int boxSize, Grid container)
        {
            container.Children.Clear();
            container.Background = new SolidColorBrush(Colors.Gray);

            StackPanel rowsPanel = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            for (int r = 0; r < row; ++r)
            {
                StackPanel oneRowPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                };

                for (int c = 0; c < col; ++c)
                {
                    Label box = new Label()
                    {
                        Tag = new Point(c, r),
                        Width= boxSize - 2,
                        Height = boxSize - 2,
                        Background = new SolidColorBrush(Colors.White),
                        Margin = new Thickness(1)
                    };

                    box.MouseEnter += Box_MouseEnter;
                    box.MouseMove += Box_MouseMove;
                    box.MouseLeftButtonDown += Box_MouseLeftButtonDown;

                    oneRowPanel.Children.Add(box);
                }

                rowsPanel.Children.Add(oneRowPanel);
            }

            container.Children.Add(rowsPanel);
        }

        private void Box_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mark(sender as Label);
        }

        private void Box_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Mark(sender as Label);
            }
        }

        private void Box_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Mark(sender as Label);
            }
        }

        private void Mark(Label label)
        {
            label.Background = new SolidColorBrush(Colors.Black);
            Point point = (Point)label.Tag;
            Pixels[(int)point.Y, (int)point.X] = true;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "Draw something :)";
            CreateBoxes(Rows, Columns, BoxSize, CanvasGrid);

            Pixels = new bool[Rows, Columns];
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < Rows; ++row)
            {
                int num = 0;
                for (int col = 0; col < Columns; ++col)
                {
                    bool pixel = Pixels[row, col];
                    num += (1 << (Columns - col - 1)) * (pixel ? 1 : 0);
                }

                sb.Append("0x");
                string format = "X" + (Columns / 4 + ((Columns % 4 != 0) ? 1 : 0));

                sb.Append(num.ToString(format));
                if (row < Rows - 1)
                {
                    sb.Append(", ");
                }
            }

            ResultTextBox.Text = sb.ToString();
        }
    }
}
