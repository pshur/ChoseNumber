using System.Collections.Generic;

namespace ChoseNumber
{
    public class Gamer
    {
        // данные о пользователе
        private UserScore user;
        // данные о уровнях
        List<LevelInfo> levels = new List<LevelInfo>();

        // конструктор
        public Gamer(UserScore _score)
        {
            user = _score;
            InitLevels();
        }
        // инициализация всех уровней
        private void InitLevels()
        {
            for (int i = 1; i <= 5; i++)
            {
                levels.Add(new LevelInfo(i));
            }
        }
        // получение карты игры по номеру уровня
        public int[,] GetLevelInfo(int levelId)
        {
            return levels[levelId - 1].GetNumbers();
        }
        // сделать шаг по номеру уровня и выбраному значению, если шаг правильный true, иначе false
        public bool MakeStep(int levelId, int value)
        {
            return levels[levelId - 1].MakeStep(value);
        }
        // проверка конец ли уровня по номеру
        public bool IsFinish(int levelId)
        {
            return levels[levelId-1].IsFinish();
        }
        // запись времени прохождения уровня
        public void SetTime(long time, int levelId)
        {
            levels[levelId - 1].Time = time;
        }
        // формирование строки статистики по прохождениб уровня
        public string Statistic()
        {
            string stat = "";
            long result = 0;
            int allscore = 0;
            for (int i = 0; i < 5; i++)
            {
                result += levels[i].Time;
                allscore += levels[i].Score;
                stat += "Таблица " + (i + 1) + ": " + levels[i].Time.ToString() + " c. (ошибок: " + levels[i].Score + ")\n";
                user.Score[i] = levels[i].Score;
                user.Time[i] = levels[i].Time;
            }
            stat += "Общее время: " + result.ToString() + " c. (ошибок: " + allscore + ")\n";
            SaveStatistic();
            return stat;
        }
        // запись статистики в файл
        private void SaveStatistic()
        {
            XmlJober.AddDataToFile(user);
        }
    }
}
