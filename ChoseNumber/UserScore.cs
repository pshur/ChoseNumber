namespace ChoseNumber
{
    public class UserScore
    {
        // поле фио
        public string UserName { get; } = "";
        // поле пол
        public string Sex { get; }
        // поле возраст
        public int Age { get; }
        // поле ошибок, массив
        public int[] Score  { get; set; } = new int[5];
        // поле времени, массив
        public long[] Time { get; set; } = new long[5];

        // создание игрока
        public UserScore(string _name, Gender gender, int _age)
        {
            UserName = _name;
            Sex = gender == Gender.Male ? "М" : "Ж";
            Age = _age;
        }
        // получение суммы ошибок по уровням
        public int AllScore()
        {
            int sum = 0;
            for (int i = 0; i < Score.Length; i++) 
            {
                sum += Score[i];
            }
            return sum;
        }
        // получение общего времени прохождения игры
        public double AllTime()
        {
            double sum = 0;
            for (int i = 0; i < Time.Length; i++)
            {
                sum += Time[i];
            }
            return sum;
        }

    }
    // перечисление пола для списка
    public enum Gender
    {
        Male,
        Female
    }
}
