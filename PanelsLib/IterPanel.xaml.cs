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
    /// Логика взаимодействия для IterPanel.xaml
    /// </summary>
    public partial class IterPanel : UserControl
    {
        public IterPanel(Iteration iteration, bool type)
        {
            InitializeComponent();

            CounterTextBlock.Text = iteration.number.ToString();
            IntervalTextBlock.Text = $"{iteration.interval.start}...{iteration.interval.end}";
            KsiTextBlock.Text = $"{(type ? 'ξ' : 'x')}{iteration.number} = {iteration.average}";
            ValueTextBlock.Text = $"{(type ? "f(ξ" : "φ(x")}{iteration.number}) = {iteration.value}";
            ConclusiontextBlock.Text = (iteration.value > 0) ? " > 0" : (iteration.value == 0) ? " = 0" : " < 0";
        }
    }
}
