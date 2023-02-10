using System.Collections.Generic;

namespace ChoseNumber
{
    public class Gamer
    {
        List<LevelInfo> levels = new List<LevelInfo>();

        public Gamer()
        {
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
            return levels[levelId].GetNumbers();
        }

        public bool MakeStep(int levelId, int i , int j)
        {
            return levels[levelId].MakeStep(i, j);
        }
    }
}
