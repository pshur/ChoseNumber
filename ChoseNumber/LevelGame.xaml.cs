using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Diagnostics;

namespace ChoseNumber
{
    /// <summary>
    /// Логика взаимодействия для LevelGame.xaml
    /// </summary>
    public partial class LevelGame : Window
    {
        private string baseTitle = "Игровое поле ";
        private Gamer gamer;
        private int levelID = 1;
        private Stopwatch timer;
        public LevelGame(UserScore _score)
        {
            InitializeComponent();
            this.Title = baseTitle;
            gamer = new Gamer(_score);
            buttonStart.Content = "Начать уровень " + levelID;
            this.Title = baseTitle + "(уровень " + levelID + ")";
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {   
            gridGame.Children.Clear();
            BuildGameField();
            buttonStart.Visibility = Visibility.Hidden;
            gridGame.Visibility = Visibility.Visible;
            timer = new Stopwatch();
            timer.Start();
        }

        private void BuildGameField()
        {
            Button button;
            int[,] numbers = gamer.GetLevelInfo(levelID);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    button = new Button();
                    button.Content = numbers[i, j].ToString();
                    button.Click += CommonBtn_Click;
                    button.Background = Brushes.DimGray;
                    gridGame.Children.Add(button);
                    Grid.SetColumn(button, i);
                    Grid.SetRow(button, j);
                }
            }     
        }
        private void CommonBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int value = int.Parse(button.Content.ToString());
            bool step = gamer.MakeStep(levelID, value);
            Highlight(button, step);
            if (gamer.IsFinish(levelID))
            {
                timer.Stop();
                gamer.SetTime(timer.ElapsedMilliseconds / 1000, levelID);
                if (levelID == 5)
                {                    
                    ShowStatisticByGame();
                    return;
                }               
                levelID++;
                buttonStart.Content = "Начать уровень " + levelID;
                buttonStart.Visibility = Visibility.Visible;
                gridGame.Visibility = Visibility.Hidden;
                this.Title = baseTitle + "(уровень " + levelID + ")";
            }
        }

        private void ShowStatisticByGame()
        {   
            MessageBox.Show("Игра окончена\n" + gamer.Statistic());
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void Highlight(Button button, bool step)
        {
            if (button.Background.IsFrozen)
                button.Background = button.Background.CloneCurrentValue();

            ColorAnimation animHighlight = new ColorAnimation(step ? Colors.Green : Colors.Red, new Duration(TimeSpan.FromSeconds(0.2)));
            ColorAnimation animDeHighlight = new ColorAnimation(Colors.DimGray, new Duration(TimeSpan.FromSeconds(0.2)));

            animHighlight.Completed += (s, e) =>
            {
                button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animDeHighlight);
            };
            button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animHighlight);
        }
    }
}
