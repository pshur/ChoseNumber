namespace ChoseNumber
{
    public class UserScore
    {
        public string UserName { get; } = "";
        public string Sex { get; }
        public int Age { get; }
        public int[] Score  { get; set; } = new int[5];
        public long[] Time { get; set; } = new long[5];

        public UserScore(string _name, Gender gender, int _age)
        {
            UserName = _name;
            Sex = gender == Gender.Male ? "М" : "Ж";
            Age = _age;
        }

        public int AllScore()
        {
            int sum = 0;
            for (int i = 0; i < Score.Length; i++) 
            {
                sum += Score[i];
            }
            return sum;
        }

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
    public enum Gender
    {
        Male,
        Female
    }
}
