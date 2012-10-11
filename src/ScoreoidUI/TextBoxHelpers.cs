using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ScoreoidUI
{
    public static class TextBoxHelpers
    {
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.RegisterAttached("Watermark", typeof (string), typeof (TextBoxHelpers),
                                                new PropertyMetadata(string.Empty, OnWatermarkChanged));

        public static Dictionary<int, TextBlock> watermarks = new Dictionary<int, TextBlock>();

        public static string GetWatermark(DependencyObject obj)
        {
            return (string) obj.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        public static void OnWatermarkChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as Control;
            bool isTextBox = control is TextBox;
            bool isPasswordBox = control is PasswordBox;

            if (isTextBox == false && isPasswordBox == false) return;

            if (!watermarks.ContainsKey(control.GetHashCode()))
            {
                control.Loaded += (s, ev) =>
                                      {
                                          var grid = VisualTreeHelper.GetChild(control, 0) as Grid;
                                          if (grid != null)
                                          {
                                              var tb = new TextBlock
                                                           {
                                                               Text = (string) e.NewValue,
                                                               VerticalAlignment = VerticalAlignment.Center,
                                                               Opacity = 0.5,
                                                               Margin = new Thickness(10, -4, 0, 0)
                                                           };
                                              watermarks[control.GetHashCode()] = tb;
                                              grid.Children.Add(tb);
                                          }
                                      };

                if (isTextBox)
                {
                    var textBox = control as TextBox;
                    textBox.GotFocus +=
                        (s, ev) => { watermarks[textBox.GetHashCode()].Visibility = Visibility.Collapsed; };

                    textBox.TextChanged += (s, ev) =>
                                               {
                                                   if (textBox.Text.Length == 0)
                                                   {
                                                       watermarks[textBox.GetHashCode()].Visibility = Visibility.Visible;
                                                   }
                                                   else
                                                   {
                                                       watermarks[textBox.GetHashCode()].Visibility =
                                                           Visibility.Collapsed;
                                                   }
                                               };

                    textBox.LostFocus += (s, ev) =>
                                             {
                                                 if (textBox.Text.Length > 0)
                                                 {
                                                     watermarks[textBox.GetHashCode()].Visibility = Visibility.Collapsed;
                                                 }
                                                 else
                                                 {
                                                     watermarks[textBox.GetHashCode()].Visibility = Visibility.Visible;
                                                 }
                                             };
                }

                if (isPasswordBox)
                {
                    var passwordBox = control as PasswordBox;

                    passwordBox.GotFocus +=
                        (s, ev) => { watermarks[passwordBox.GetHashCode()].Visibility = Visibility.Collapsed; };

                    passwordBox.PasswordChanged += (s, ev) =>
                                                       {
                                                           if (passwordBox.Password.Length == 0)
                                                           {
                                                               watermarks[passwordBox.GetHashCode()].Visibility =
                                                                   Visibility.Visible;
                                                           }
                                                           else
                                                           {
                                                               watermarks[passwordBox.GetHashCode()].Visibility =
                                                                   Visibility.Collapsed;
                                                           }
                                                       };

                    passwordBox.LostFocus += (s, ev) =>
                                                 {
                                                     if (passwordBox.Password.Length > 0)
                                                     {
                                                         watermarks[passwordBox.GetHashCode()].Visibility =
                                                             Visibility.Collapsed;
                                                     }
                                                     else
                                                     {
                                                         watermarks[passwordBox.GetHashCode()].Visibility =
                                                             Visibility.Visible;
                                                     }
                                                 };
                }
            }
            else
            {
                watermarks[control.GetHashCode()].Text = (string) e.NewValue;
            }
        }
    }
}