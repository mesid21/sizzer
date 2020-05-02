using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Sizzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int risk;
        public bool IsEditRisk = false;
        public MainWindow()
        {
            InitializeComponent();
            risk = Properties.Settings.Default.risk;
            TxtRisk.Text = "₹" + risk;

        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            TxtSL.Focus();
        }

        private void TxtSL_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Escape)
            {
                if (IsEditRisk == true)
                {
                    Clear();
                    IsEditRisk = false;
                    TxtRisk.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#636D7A"));
                    TxtRisk.Text = "₹" + risk;
                }
                else { Clear(); }
            }

            if (e.Key == Key.Enter)
                try 
                {
                    if (IsEditRisk == true)
                    {                        
                        Properties.Settings.Default.risk = int.Parse(TxtSL.Text);
                        Properties.Settings.Default.Save();
                        risk = Properties.Settings.Default.risk;
                        TxtRisk.Text = "₹" + risk;

                        IsEditRisk = false;
                        TxtRisk.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#636D7A"));
                        Clear();
                    }
                    else
                    {
                        TxtSize.Text = Decimal.Truncate((risk / decimal.Parse(TxtSL.Text))).ToString();
                    }
                }                 
                catch (Exception)
                {
                    Clear();
                }                
            
        }

        private void TxtRisk_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsEditRisk = true;
            TxtRisk.Foreground = System.Windows.Media.Brushes.White;
            Clear();
        }

        private void Clear()
        {
            TxtSL.Clear();
            TxtSL.Focus();
            TxtSize.Text = "";
        }
    }
}
