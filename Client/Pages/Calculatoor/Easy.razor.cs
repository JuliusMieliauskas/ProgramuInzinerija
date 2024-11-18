using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public class EasyBase : ComponentBase
    {
        protected int number1;
        protected int number2;
        protected string operation;
        protected int correctAnswer;
        protected string userAnswer = string.Empty;
        protected bool showResult = false;
        protected bool isCorrect = false;

        private Random random = new Random();

        protected override void OnInitialized()
        {
            GenerateNewProblem();
        }

        protected void GenerateNewProblem()
        {
            showResult = false;
            number1 = random.Next(1, 100);

            if (random.Next(0, 2) == 0)
            {
                operation = "+";
                number2 = random.Next(1, 100);
                correctAnswer = number1 + number2;
            }
            else
            {
                operation = "-";
                number2 = random.Next(1, number1);
                correctAnswer = number1 - number2;
            }

            userAnswer = string.Empty;
        }

        protected void CheckAnswer()
        {
            if (int.TryParse(userAnswer, out int parsedAnswer))
            {
                isCorrect = parsedAnswer == correctAnswer;
            }
            else
            {
                isCorrect = false;
            }

            showResult = true;
        }
    }
}