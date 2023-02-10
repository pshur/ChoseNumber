namespace ChoseNumber
{
    public class UserScore
    {
        public string UserName { get; } = "";
        public char Sex { get; }
        public int Age { get; }
        public int[] Score  { get; } = new int[5];
        public double[] Time { get; } = new double[5];

        public UserScore(string _name, Gender gender, int _age)
        {
            UserName = _name;
            Sex = gender == Gender.Male ? 'М' : 'Ж';
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
