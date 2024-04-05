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

namespace MathApp
{
    /// <summary>
    /// Логика взаимодействия для DefinerFiPanel.xaml
    /// </summary>
    public partial class DefinerFiPanel : UserControl
    {
        public DefinerFiPanel(double coef, string fi, string derivative, double start, double valueStart, double end, double valueEnd)
        {
            InitializeComponent();

            FiTextBlock.Text = $"по x^{coef}: φ(x) = {fi}";
            DerivativeTextBlock.Text = $"φ'(x) = {derivative}";
            ValueStartTextBlock.Text = $"|φ'({start})| = {Math.Round(valueStart, 2)}";
            ValueEndTextBlock.Text = $"|φ'({end})| = {Math.Round(valueEnd, 2)}";
            ConclusiontextBlock.Text = (valueStart <= 1 && valueEnd <= 1) ? "подходит" : "не подходит";
        }
    }
}
