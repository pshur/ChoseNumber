using System;

namespace ChoseNumber
{
    public class LevelInfo
    {
        public long Time { get; set; } = 0;
        public int Score { get; set; } = 0;
        public int Level { get; set; }
        private int[,] numbers = new int[5,5];
        private int currentStep = 0;

        public LevelInfo(int _level)
        {
            Level = _level;
            InitLevel();
        }

        private void InitLevel()
        {
            Random rd = new Random();
            int rand_num;
            int[] local_num = new int[25];
            for (int i=0; i<25; i++)
            {
                do
                {
                    rand_num = rd.Next(1, 26);
                } while (Array.IndexOf(local_num, rand_num) != -1);
                local_num[i] = rand_num;
            }
            int l = 0;
            for (int i = 0; i < 5; i++) 
            {
                for (int j = 0; j < 5; j++)
                {
                    numbers[i,j] = local_num[l];
                    l++;
                }
            }
        }

        public bool MakeStep(int value)
        {
            if (value == currentStep + 1)
            {
                currentStep++;
                return true;
            }
            Score++;
            return false;
        }

        public int[,] GetNumbers()
        {
            return numbers;
        }

        public bool IsFinish()
        {
            return currentStep == 25;
        }
    }
}
