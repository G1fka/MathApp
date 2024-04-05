using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MathApp
{
    internal class Calculation
    {
        public double[] coefs;
        public Interval interval;
        public double accuracy;

        public Calculation(double[] coefs, Interval interval, double accuracy)
        {
            this.coefs = coefs;
            this.interval = interval;
            this.accuracy = accuracy;
            SetDirection();
        }

        void SetDirection()
        {
            if (this.Calculate(this.interval.start) * this.Calculate(this.interval.end) < 0) {
                this.interval.increasing = (
                        this.Calculate(this.interval.start) < 0 &&
                        this.Calculate(this.interval.end) > 0
                );
                return;
            }

            throw new Exception("Несколько либо нет корней на промежутке!");
        }

        private double Calculate(double x)
        {
            return coefs[3] * Math.Pow(x, 3) + coefs[2] * x * x + coefs[1] * x + coefs[0]; 
        }

        private double CalculateDerivative(double x)
        {
            return coefs[3] * 3 * Math.Pow(x, 2) + coefs[2] * 2 * x + coefs[1];
        }

        private double CalculateSecondDerivative(double x)
        {
            return coefs[3] * 3 * 2 * x + coefs[2] * 2;
        }

        private int DefineFi(ref StackPanel stackPanel)
        {
            if (coefs[1] != 0)
            {
                double valueStart = Math.Abs(CalculateDerivativeFi(interval.start, 1));
                double valueEnd = Math.Abs(CalculateDerivativeFi(interval.end, 1));
                var dFiP = new DefinerFiPanel(1,
                    $"({((coefs[3] == 0) ? "" : ((coefs[3] > 0) ? ((coefs[3] == 1) ? "-x^3" : $"{-1 * coefs[3]}x^3") : ((coefs[3] == -1) ? "x^3" : $"{-1 * coefs[3]}x^3")))}" +
                    $"{((coefs[2] == 0) ? "" : ((coefs[2] > 0) ? ((coefs[2] == 1) ? "-x^2" : $"-{coefs[2]}x^2") : ((coefs[2] == -1) ? "+x^2" : $" + {-1 * coefs[2]}x^2")))}" +
                    $"{((coefs[0] == 0) ? "" : ((coefs[0] > 0) ? ((coefs[0] == 1) ? "-1" : $"-{coefs[0]}") : ((coefs[0] == -1) ? "+1" : $"+{-1 * coefs[0]}")))}){((coefs[1] != 1) ? $" / {coefs[1]}" : "")}",

                    $"({((coefs[3] == 0) ? "" : ((coefs[3] > 0) ? $"-{coefs[3]*3}x^2" : $"{-coefs[3]*3}x^2"))}" + 
                    $"{((coefs[2] == 0) ? "" : ((coefs[2] > 0) ? $"-{coefs[2] * 2}x" : $"+{-coefs[2] * 2}x"))}){((coefs[1] != 1) ? $" / {coefs[1]}" : "")}",
                    
                    interval.start, valueStart, interval.end, valueEnd);

                stackPanel.Children.Add(dFiP);
                if (valueStart <= 1 && valueEnd <= 1)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Height = 1,
                        Width = 800,
                        Fill = Brushes.Black,
                        Margin = new System.Windows.Thickness(0, 0, 0, 30)
                    };
                    stackPanel.Children.Add(rectangle);
                    return 1;
                }
            }
            if (coefs[2] != 0)
            {
                double valueStart = Math.Abs(CalculateDerivativeFi(interval.start, 2));
                double valueEnd = Math.Abs(CalculateDerivativeFi(interval.end, 2));
                var dFiP = new DefinerFiPanel(2,
                    $"(({((coefs[3] == 0) ? "" : ((coefs[3] > 0) ? ((coefs[3] == 1) ? "-x^3" : $"{-1 * coefs[3]}x^3") : ((coefs[3] == -1) ? "x^3" : $"{-1 * coefs[3]}x^3")))}" +
                    $"{((coefs[1] == 0) ? "" : ((coefs[1] > 0) ? ((coefs[1] == 1) ? "-x" : $"-{coefs[1]}x") : ((coefs[1] == -1) ? "+x" : $"+{-1 * coefs[1]}x")))}" +
                    $"{((coefs[0] == 0) ? "" : ((coefs[0] > 0) ? ((coefs[0] == 1) ? "-1" : $"-{coefs[0]}") : ((coefs[0] == -1) ? "+1" : $"+{-1 * coefs[0]}")))}){((coefs[2] != 1) ? $" / {coefs[2]}" : "")})^(1/2)",

                    $"(1/2) * " +
                    $"(({((coefs[3] == 0) ? "" : ((coefs[3] > 0) ? ((coefs[3] == 1) ? "-x^3" : $"{-1 * coefs[3]}x^3") : ((coefs[3] == -1) ? "x^3" : $"{-1 * coefs[3]}x^3")))}" +
                    $"{((coefs[1] == 0) ? "" : ((coefs[1] > 0) ? ((coefs[1] == 1) ? "-x" : $"-{coefs[1]}x") : ((coefs[1] == -1) ? "+x" : $"+{-1 * coefs[1]}x")))}" +
                    $"{((coefs[0] == 0) ? "" : ((coefs[0] > 0) ? ((coefs[0] == 1) ? "-1" : $"-{coefs[0]}") : ((coefs[0] == -1) ? "+1" : $"+{-1 * coefs[0]}")))}){((coefs[2] != 1) ? $" / {coefs[2]}" : "")})^(-1/2) * " +
                    $"({((coefs[3] == 0) ? "" : $"{-coefs[3]*3}x^2")}" +
                    $"{((coefs[1] == 0) ? "" : ((coefs[1] > 0) ? ((coefs[1] == 1) ? "-1" : $"-{coefs[1]}") : ((coefs[1] == -1) ? "+1" : $"+{-1 * coefs[1]}")))}){((coefs[2] != 1) ? $" / {coefs[2]}" : "")}",

                    interval.start, valueStart, interval.end, valueEnd);

                stackPanel.Children.Add(dFiP);
                if (valueStart <= 1 && valueEnd <= 1)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Height = 1,
                        Width = 800,
                        Fill = Brushes.Black,
                        Margin = new System.Windows.Thickness(0, 0, 0, 30)
                    };
                    stackPanel.Children.Add(rectangle);
                    return 2;
                }
            }
            if (coefs[3] != 0)
            {
                double valueStart = Math.Abs(CalculateDerivativeFi(interval.start, 3));
                double valueEnd = Math.Abs(CalculateDerivativeFi(interval.end, 3));
                var dFiP = new DefinerFiPanel(3,
                    $"(({((coefs[2] == 0) ? "" : ((coefs[2] > 0) ? ((coefs[2] == 1) ? "-x^2" : $"{-1 * coefs[2]}x^2") : ((coefs[2] == -1) ? "x^2" : $"{-1 * coefs[2]}x^2")))}" +
                    $"{((coefs[1] == 0) ? "" : ((coefs[1] > 0) ? ((coefs[1] == 1) ? "-x" : $"-{coefs[1]}x") : ((coefs[1] == -1) ? "+x" : $"+{-1 * coefs[1]}x")))}" +
                    $"{((coefs[0] == 0) ? "" : ((coefs[0] > 0) ? ((coefs[0] == 1) ? "-1" : $"-{coefs[0]}") : ((coefs[0] == -1) ? "+1" : $"+{-1 * coefs[0]}")))}){((coefs[3] != 1) ? $" / {coefs[3]}" : "")})^(1/3)",

                    $"(1/3) * " +
                    $"(({((coefs[2] == 0) ? "" : ((coefs[2] > 0) ? ((coefs[3] == 1) ? "-x^2" : $"{-1 * coefs[2]}x^2") : ((coefs[2] == -1) ? "x^2" : $"{-1 * coefs[2]}x^2")))}" +
                    $"{((coefs[1] == 0) ? "" : ((coefs[1] > 0) ? ((coefs[1] == 1) ? "-x" : $"-{coefs[1]}x") : ((coefs[1] == -1) ? "+x" : $"+{-1 * coefs[1]}x")))}" +
                    $"{((coefs[0] == 0) ? "" : ((coefs[0] > 0) ? ((coefs[0] == 1) ? "-1" : $"-{coefs[0]}") : ((coefs[0] == -1) ? "+1" : $"+{-1 * coefs[0]}")))}){((coefs[3] != 1) ? $" / {coefs[3]}" : "")})^(-2/3) * " +
                    $"({((coefs[2] == 0) ? "" : $"{-coefs[2] * 2}x")}" +
                    $"{((coefs[1] == 0) ? "" : ((coefs[1] > 0) ? ((coefs[1] == 1) ? "-1" : $"-{coefs[1]}") : ((coefs[1] == -1) ? "+1" : $"+{-1 * coefs[1]}")))}){((coefs[3] != 1) ? $" / {coefs[3]}" : "")}",

                    interval.start, valueStart, interval.end, valueEnd);

                stackPanel.Children.Add(dFiP);
                if (valueStart <= 1 && valueEnd <= 1)
                {
                    Rectangle rectangle = new Rectangle { 
                        Height = 1, 
                        Width = 800, 
                        Fill = Brushes.Black, 
                        Margin = new System.Windows.Thickness(0, 0, 0, 30) 
                    };
                    stackPanel.Children.Add(rectangle);
                    return 3;
                }
            }
            throw new Exception("Не удалось вычислить φ");
        }

        private double CalculateFi(double x, int definer)
        {
            switch (definer)
            {
                case 1:
                    {
                        return (-1 * coefs[3] * Math.Pow(x, 3) - coefs[2] * x * x - coefs[0]) / coefs[1];
                    }
                case 2:
                    {
                        return Math.Pow((-1 * coefs[3] * Math.Pow(x, 3) - coefs[1] * x - coefs[0]) / coefs[2], 1 / 2.0) * ((x < 0) ? (-1) : 1);
                    }
                case 3:
                    {
                        var tmp = (-1 * coefs[2] * x * x - coefs[1] * x - coefs[0]) / coefs[3];

                        if (tmp < 0)
                            return -Math.Pow(-tmp, 1 / 3.0);
                        return Math.Pow(tmp, 1 / 3.0);
                    }
            }
            throw new Exception("Не удалось вычислить φ");
        }

        private double CalculateDerivativeFi(double x, int definer)
        {
            switch (definer)
            {
                case 1:
                    {
                        return (-1 * coefs[3] * 3 * Math.Pow(x, 2) - coefs[2] * 2 * x) / coefs[1];
                    }
                case 2:
                    {
                        return 1 / 2.0 * Math.Pow((-1 * coefs[3] * Math.Pow(x, 3) - coefs[1] * x - coefs[0]) / coefs[2], -1 / 2.0) * (-1 * coefs[3] * 3 * Math.Pow(x, 2) - coefs[1]) / coefs[2];
                    }
                case 3:
                    {
                        return 1 / 3.0 * Math.Pow(Math.Pow((-1 * coefs[2] * x * x - coefs[1] * x - coefs[0]) / coefs[3], 2), -1 / 3.0) * (-1 * coefs[2] * 2 * x - coefs[1]) / coefs[3];
                    }
            }
            throw new Exception("Не удалось вычислить φ");
        }

        public Solution AnswerSolution(Methods method, ref StackPanel stackPanel)
        {
            int counter = 1;
            bool end = false;

            var inter = interval;
            double average = 0.0, val = 0.0;
            List<Iteration> iterations = new List<Iteration>();

            int definer = 0;

            if (method == Methods.ITERATIONS)
            {
                definer = DefineFi(ref stackPanel);
            }

            while (true)
            {
                switch (method)
                {
                    case Methods.HALF_DIVISION:
                        {
                            average = (inter.start + inter.end) / 2;
                            val = Calculate(average);
                            break;
                        }
                    case Methods.CHORDS:
                        {
                            average = inter.start - (Calculate(inter.start) / (Calculate(inter.end) - Calculate(inter.start)) * (inter.end - inter.start));
                            val = Calculate(average);
                            break;
                        }
                    case Methods.TANGENT:
                        {
                            var intent = (CalculateDerivative(average) * CalculateSecondDerivative(average) > 0) ? inter.end : inter.start;
                            average = intent - Calculate(intent) / CalculateDerivative(intent);
                            val = Calculate(average);
                            break;
                        }
                    case Methods.ITERATIONS:
                        {
                            average = inter.start;
                            val = CalculateFi(average, definer);
                            break;
                        }
                }
                

                var iteration = new Iteration 
                { 
                    number = counter, 
                    interval = inter, 
                    average = Math.Round(average, 5), 
                    value = Math.Round(val, 5)
                };

                switch (method)
                {
                    case Methods.HALF_DIVISION:
                        {
                            inter.start = ((bool)inter.increasing && val < 0 || !(bool)inter.increasing && val > 0) ? average : inter.start;
                            inter.end = ((bool)inter.increasing && val > 0 || !(bool)inter.increasing && val < 0) ? average : inter.end;
                            break;
                        }
                    case Methods.CHORDS:
                        {
                            inter.start = (CalculateDerivative(average) * CalculateSecondDerivative(average) > 0) ? average : inter.start;
                            inter.end = (CalculateDerivative(average) * CalculateSecondDerivative(average) < 0) ? average : inter.end;
                            break;
                        }
                    case Methods.TANGENT:
                        {
                            inter.start = (CalculateDerivative(average) * CalculateSecondDerivative(average) < 0) ? average : inter.start;
                            inter.end = (CalculateDerivative(average) * CalculateSecondDerivative(average) > 0) ? average : inter.end;
                            break;
                        }
                    case Methods.ITERATIONS:
                        {
                            inter.start = val;
                            break;
                        }
                }

                inter = new Interval 
                {
                    start = Math.Round(inter.start, 5),
                    end = Math.Round(inter.end, 5),
                    increasing = inter.increasing
                };

                iterations.Add(iteration);

                counter++;

                switch (method)
                {
                    case Methods.HALF_DIVISION:
                        {
                            if (Math.Abs(inter.end - inter.start) <= this.accuracy || val == 0)
                                end = true;
                            break;
                        }
                    case Methods.CHORDS: case Methods.TANGENT: 
                        {
                            if (counter != 2 && Math.Abs(average - iterations[counter - 3].average) <= this.accuracy || val == 0)
                                end = true;
                            break;
                        }
                    case Methods.ITERATIONS:
                        {
                            if (counter != 2 && Math.Abs(val - iterations[counter - 3].value) <= this.accuracy)
                                end = true;
                            break;
                        }
                }

                if (end) break;
            }

            return new Solution 
            {
                iterations = iterations.ToArray(),
                root = Math.Round(average, 5)
            };
        }
    }
}
