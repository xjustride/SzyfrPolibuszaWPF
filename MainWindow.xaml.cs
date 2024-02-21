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

namespace SzyfrPolibuszaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
        }

        public string Encrypt(string input)
        {
            char[,] macierz = new char[6, 6]
                {
                    { 'a', 'ą', 'b', 'c', 'ć', 'd' },
                    { 'e', 'ę', 'f', 'g', 'h', 'i' },
                    { 'j', 'k', 'l', 'ł', 'm', 'n' },
                    { 'ń', 'o', 'ó', 'p', 'r', 's' },
                    { 'ś', 't', 'u', 'w', 'y', 'z' },
                    { 'ź', 'ż', ' ', '.', ',', '?' }
                };
            StringBuilder sb = new StringBuilder();
            foreach (char c in input.ToLower())
            {
                bool found = false;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (macierz[i, j] == c)
                        {
                            // Dodajemy 1 do indeksu, aby uniknąć 00 jako wartości szyfru
                            sb.Append($"{i + 1}{j + 1} ");
                            found = true;
                            break;
                        }
                    }
                    if (found) break;
                }
                if (!found) sb.Append(c); // Jeśli znak nie został znaleziony, dodajemy go bez zmian
            }
            return sb.ToString().Trim();
        }

        public string Decrypt(string input)
        {
            char[,] macierz = new char[6, 6]
                {
                    { 'a', 'ą', 'b', 'c', 'ć', 'd' },
                    { 'e', 'ę', 'f', 'g', 'h', 'i' },
                    { 'j', 'k', 'l', 'ł', 'm', 'n' },
                    { 'ń', 'o', 'ó', 'p', 'r', 's' },
                    { 'ś', 't', 'u', 'w', 'y', 'z' },
                    { 'ź', 'ż', ' ', '.', ',', '?' }
                };
            StringBuilder sb = new StringBuilder();
            string[] parts = input.Split(' ');
            foreach (string part in parts)
            {
                if (part.Length == 2 && char.IsDigit(part[0]) && char.IsDigit(part[1]))
                {
                    int i = int.Parse(part[0].ToString()) - 1;
                    int j = int.Parse(part[1].ToString()) - 1;
                    if (i >= 0 && i < 6 && j >= 0 && j < 6)
                    {
                        sb.Append(macierz[i, j]);
                    }
                }
                else
                {
                    sb.Append(part); // Jeśli część nie jest zakodowaną wartością, dodajemy ją bez zmian
                }
            }
            return sb.ToString();
        }


        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            string input = inputTextBox.Text;
            string encrypted = Encrypt(input);
            outputTextBox.Text = encrypted;
        }

        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            string input = inputTextBox.Text;
            string decrypted = Decrypt(input);
            outputTextBox.Text = decrypted;
        }

        private void inputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void outputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
    }
}
