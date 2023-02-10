using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChoseNumber
{
    public partial class MainWindow : Window
    {
        private bool _validName = false;
        private bool _validAge = false;

        public MainWindow()
        {
            InitializeComponent();
            InitFields();
        }

        private void InitFields()
        {
            comboBoxSex.Items.Clear();
            comboBoxSex.Items.Add(Gender.Male);
            comboBoxSex.Items.Add(Gender.Female);
            comboBoxSex.SelectedItem = Gender.Male;
            textBoxAge.Text = "";
            textBoxAge.BorderBrush = Brushes.Black;
            textBoxName.Text = "";
            textBoxName.BorderBrush = Brushes.Black;
            _validName = false;
            _validAge = false;
        }

        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool valid = string.IsNullOrEmpty(textBoxName.Text as string) || textBoxName.Text.Length < 10;
            textBoxName.BorderBrush = !valid ? Brushes.Green : Brushes.Red;
            _validName = !valid;
        }

        private void textBoxAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            int invalid;
            bool valid = int.TryParse(textBoxAge.Text, out invalid);
            int value = valid ? int.Parse(textBoxAge.Text) : 0;
            textBoxAge.BorderBrush = value > 0 ? Brushes.Green : Brushes.Red;
            _validAge = value > 0;
        }

        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            if (_validAge && _validName)
            {
                UserScore score = new UserScore(textBoxName.Text, (Gender)comboBoxSex.SelectedItem, int.Parse(textBoxAge.Text));
                LevelInfo level = new LevelInfo(1);
            } else
            {
                MessageBox.Show("Поля заполнены некорректно!");
            }
        }

        private void showResults_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
