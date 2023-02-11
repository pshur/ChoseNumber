using System.Collections.Generic;

namespace ChoseNumber
{
    public class Gamer
    {
        private UserScore user;
        List<LevelInfo> levels = new List<LevelInfo>();

        public Gamer(UserScore _score)
        {
            user = _score;
            InitLevels();
        }

        private void InitLevels()
        {
            for (int i = 1; i <= 5; i++)
            {
                levels.Add(new LevelInfo(i));
            }
        }

        public int[,] GetLevelInfo(int levelId)
        {
            return levels[levelId - 1].GetNumbers();
        }

        public bool MakeStep(int levelId, int value)
        {
            return levels[levelId - 1].MakeStep(value);
        }

        public bool IsFinish(int levelId)
        {
            return levels[levelId-1].IsFinish();
        }

        public void SetTime(long time, int levelId)
        {
            levels[levelId - 1].Time = time;
        }

        public string Statistic()
        {
            string stat = "";
            long result = 0;
            int allscore = 0;
            for (int i = 0; i < 5; i++)
            {
                result += levels[i].Time;
                allscore += levels[i].Score;
                stat += "Уровень " + (i + 1) + ": " + levels[i].Time.ToString() + " c. (ошибок: " + levels[i].Score + ")\n";
                user.Score[i] = levels[i].Score;
                user.Time[i] = levels[i].Time;
            }
            stat += "Общее время: " + result.ToString() + " c. (ошибок: " + allscore + ")\n";
            SaveStatistic();
            return stat;
        }

        private void SaveStatistic()
        {
            XmlJober.AddDataToFile(user);
        }
    }
}
