using System;
using System.Collections.Generic;
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
                LevelGame game = new LevelGame(score);
                game.Show();
                this.Close();
            } else
            {
                MessageBox.Show("Поля заполнены некорректно!");
            }
        }

        private void showResults_Click(object sender, RoutedEventArgs e)
        {
            List<UserScore> users = XmlJober.ReadAllData();
            string stat = "";            
            foreach (UserScore user in users)
            {
                stat += user.UserName + "\t" + user.Sex + "\t" + user.Age + "\n\n";
                long result = 0;
                int allscore = 0;
                for (int i = 0; i < 5; i++)
                {
                    result += user.Time[i];
                    allscore += user.Score[i];
                    stat += "Уровень " + (i + 1) + ": " + user.Time[i].ToString() + " c. (ошибок: " + user.Score[i] + ")\n";

                }
                stat += "Общее время: " + result.ToString() + " c. (ошибок: " + allscore + ")\n\n";
            }
            MessageBox.Show(stat);
        }
    }
}
