using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCalc
{

    public partial class MainWindow : Window
    {
        private const String AllSymbols = "0123456789+-*/,^()";

        public MainWindow()
        {
            InitializeComponent();
            // Функция конструирует кнопку
            CalcButton getButton(Char symbol, Action act)
            {
                var result = new CalcButton() { Content = symbol };
                result.Click += (s, e) => act();
                return result;
            }
            foreach (var i in AllSymbols)
            {
                ButtonsPanel.Children.Add(getButton(i, () => EditBox.Text += i));
            }
            ButtonsPanel.Children.Add(getButton('C', () => EditBox.Text = String.Empty));
        }

        private void CheckTextInput(Object sender, TextCompositionEventArgs e)
        {
            foreach (var i in e.Text)
            {
                if (!AllSymbols.Contains(i))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void CheckTextChanged(object sender, TextChangedEventArgs e)
        {
            if (EditBox.Text.Length == 0)
            {
                ResultBox.Text = String.Empty;
                return;
            }
            var builder = new StringBuilder(EditBox.Text);
            while (builder.Length > 0 && "^+*/".Contains(builder[0]))
            {
                builder.Remove(0, 1);
            }
            EditBox.Text = builder.ToString();
            if(EditBox.Text.Length == 0 || "√+-*/(^,".Contains(EditBox.Text.Last()))
            {
                return;
            }
            try
            {
                ResultBox.Text = EditBox.Text.Calculate().ToString();
            }
            catch (Exception error)
            {
                ResultBox.Text = error.Message;
            }
        }
    }
}
