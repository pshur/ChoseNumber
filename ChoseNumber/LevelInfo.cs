using System;

namespace ChoseNumber
{
    public class LevelInfo
    {
        // поле хранит время уровня
        public long Time { get; set; } = 0;
        // поле хранит счет ошибок
        public int Score { get; set; } = 0;
        // текущай уровень
        public int Level { get; set; }
        // карта поля
        private int[,] numbers = new int[5,5];
        // пред нажатое число
        private int currentStep = 0;

        public LevelInfo(int _level)
        {
            Level = _level;
            InitLevel();
        }
        // инициализация уровня рандомными числами
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
        // делаем шаг, путем сравнивания нажатой кнопки и значения которое должно быть
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
        // сетер для получения карты поля
        public int[,] GetNumbers()
        {
            return numbers;
        }
        // проверка окончен ли уровень
        public bool IsFinish()
        {
            return currentStep == 25;
        }
    }
}
