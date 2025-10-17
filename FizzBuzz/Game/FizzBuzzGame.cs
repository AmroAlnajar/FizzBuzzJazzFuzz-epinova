namespace FizzBuzz.Game
{
    public class FizzBuzzGame
    {
        private const int FizzDivisor = 3;
        private const int BuzzDivisor = 5;

        public void Run(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
              Console.WriteLine(GetFizzBuzzValue(i));
            }
        }

        private string GetFizzBuzzValue(int number)
        {
            bool divisbleByFizz = number % FizzDivisor == 0;
            bool divisbleByBuzz = number % BuzzDivisor == 0;

            if (divisbleByFizz && divisbleByBuzz) return "FizzBuzz";
            if (divisbleByFizz) return "Fizz";
            if (divisbleByBuzz) return "Buzz";

            return number.ToString();
        }
    }
}