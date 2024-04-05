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
using static System.Net.Mime.MediaTypeNames;

namespace MathApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double[] coefs;
        Interval interval;
        double accuracy;
        Calculation answer;
        Solution solution;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            coefs = new double[4] { Convert.ToDouble(X0TextBox.Text), Convert.ToDouble(X1TextBox.Text), Convert.ToDouble(X2TextBox.Text), Convert.ToDouble(X3TextBox.Text)};
            interval = new Interval()
            {
                start = Convert.ToDouble(StartTextBox.Text.Replace('.', ',')),
                end = Convert.ToDouble(EndTextBox.Text.Replace('.', ','))
            };
            accuracy = Convert.ToDouble(AccTextBox.Text.Replace('.', ','));

            try 
            {
                answer = new Calculation(coefs, interval, accuracy);
                ErrorTextBlock.Text = "";
            }
            catch (Exception ex) 
            { 
                ErrorTextBlock.Text = ex.Message;
                return;
            }

            SetIsEnablesToMethodButtons(true);
            CalculateButton.IsEnabled = false;
        }

        private void ViewHDMethodButton_Click(object sender, RoutedEventArgs e)
        {
            AnswerStackPanel.Children.Clear();
            solution = answer.AnswerSolution(Methods.HALF_DIVISION, ref AnswerStackPanel);
            AddIterPanels(true);
        }

        private void ViewChMethodButton_Click(object sender, RoutedEventArgs e)
        {
            AnswerStackPanel.Children.Clear();
            solution = answer.AnswerSolution(Methods.CHORDS, ref AnswerStackPanel);
            AddIterPanels(true);
        }

        private void ViewTangentMethodButton_Click(object sender, RoutedEventArgs e)
        {
            AnswerStackPanel.Children.Clear();
            solution = answer.AnswerSolution(Methods.TANGENT, ref AnswerStackPanel);
            AddIterPanels(true);
        }

        private void ViewIterMethodButton_Click(object sender, RoutedEventArgs e)
        {
            AnswerStackPanel.Children.Clear();
            try
            {
                solution = answer.AnswerSolution(Methods.ITERATIONS, ref AnswerStackPanel);
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = ex.Message;
                return;
            }
            AddIterPanels(false);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            AnswerStackPanel.Children.Clear();

            X3TextBox.Text = null; X2TextBox.Text = null;
            X1TextBox.Text = null; X0TextBox.Text = null;

            StartTextBox.Text = null; EndTextBox.Text = null;

            AccTextBox.Text = null;

            SetIsEnablesToMethodButtons(false);
            CalculateButton.IsEnabled = true;
        }

        private void AddIterPanels(bool type)
        {
            foreach (var iteration in solution.iterations)
            {
                var iterPanel = new IterPanel(iteration, type);

                AnswerStackPanel.Children.Add(iterPanel);
            }

            AnswerStackPanel.Children.Add(new TextBlock
            {
                Text = $"{(type ? 'ξ' : 'x')} = {solution.root}",
                FontWeight = FontWeights.ExtraBold,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20),
                FontSize = 25
            });
        }

        private void SetIsEnablesToMethodButtons(bool enablesToMethodButtons)
        {
            ViewHDMethodButton.IsEnabled = enablesToMethodButtons;
            ViewChMethodButton.IsEnabled = enablesToMethodButtons;
            ViewTangentMethodButton.IsEnabled = enablesToMethodButtons;
            ViewIterMethodButton.IsEnabled = enablesToMethodButtons;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            SetIsEnablesToMethodButtons(false);
            CalculateButton.IsEnabled = true;
        }
    }
}
