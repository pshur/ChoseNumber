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
        // объект игры
        private Gamer gamer;
        // уровень текущий
        private int levelID = 1;
        private Stopwatch timer;
        public LevelGame(UserScore _score)
        {
            InitializeComponent();
            this.Title = baseTitle;
            // создание игры, инициализация карт
            gamer = new Gamer(_score);
            buttonStart.Content = "Начать уровень " + levelID;
            this.Title = baseTitle + "(уровень " + levelID + ")";
        }
        // кнопка начать уровень
        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            // очистка игрового поля
            gridGame.Children.Clear();
            // заполение игрового поля цифрами из уровня
            BuildGameField();
            // диактивация кнопки начать уровень
            buttonStart.Visibility = Visibility.Hidden;
            // активация игрового поля
            gridGame.Visibility = Visibility.Visible;
            // засекаем время игры
            timer = new Stopwatch();
            timer.Start();
        }
        // построение игрового поля
        private void BuildGameField()
        {
            Button button;
            // получение карты игры
            int[,] numbers = gamer.GetLevelInfo(levelID);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    // создание кнопок с цифрами по карте полей
                    button = new Button();
                    button.Content = numbers[i, j].ToString();
                    // прикрепляем событие для обработки кнопки
                    button.Click += CommonBtn_Click;
                    // красим кнопку
                    button.Background = Brushes.DimGray;
                    // добавляем кнопку в поле визуальное
                    gridGame.Children.Add(button);
                    Grid.SetColumn(button, i);
                    Grid.SetRow(button, j);
                }
            }     
        }
        // обработчик кнопки в поле
        private void CommonBtn_Click(object sender, EventArgs e)
        {
            // идентифицируем нажатую кнопку
            Button button = (Button)sender;
            // получаем ее занчение (цифру)
            int value = int.Parse(button.Content.ToString());
            // проверяем верную ли кнопку нажали
            bool step = gamer.MakeStep(levelID, value);
            // делаем мигание кнопки 
            Highlight(button, step);
            // проверяем все ли кнопки прошли
            if (gamer.IsFinish(levelID))
            {
                // если да, стоп время
                timer.Stop();
                gamer.SetTime(timer.ElapsedMilliseconds / 1000, levelID);
                // если все уровни пройдены
                if (levelID == 5)
                {
                    // вывод статистики и переход на гравный экран
                    ShowStatisticByGame();
                    return;
                }
                // переход на след уровень
                levelID++;
                buttonStart.Content = "Начать уровень " + levelID;
                buttonStart.Visibility = Visibility.Visible;
                gridGame.Visibility = Visibility.Hidden;
                this.Title = baseTitle + "(уровень " + levelID + ")";
            }
        }
        // вывод статистики, переход на главный экран
        private void ShowStatisticByGame()
        {   
            MessageBox.Show("Игра окончена\n" + gamer.Statistic());
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
        // мигание кнопки
        private void Highlight(Button button, bool step)
        {
            if (button.Background.IsFrozen)
                button.Background = button.Background.CloneCurrentValue();
            // анимация перехода от текущего цвета к красному или зеленому и возврат назад
            ColorAnimation animHighlight = new ColorAnimation(step ? Colors.Green : Colors.Red, new Duration(TimeSpan.FromSeconds(0.2)));
            ColorAnimation animDeHighlight = new ColorAnimation(Colors.DimGray, new Duration(TimeSpan.FromSeconds(0.2)));
            // включение последовательности анимации
            animHighlight.Completed += (s, e) =>
            {
                button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animDeHighlight);
            };
            button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animHighlight);
        }
    }
}
