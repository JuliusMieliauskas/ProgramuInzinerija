using Client.Pages;
using Microsoft.AspNetCore.Components;
using Bunit;
using Moq;
using System.Net.Http;
using Xunit;
using Shared;
using System.Net.Http.Json;
using System.Threading;

namespace Tests
{
    public class CalculatorGameTests
    {
        private class FakeNavigationManager : NavigationManager
        {
            public string? UriVisited { get; private set; }

            public FakeNavigationManager() : base() => Initialize("http://localhost/", "http://localhost/");

            protected override void NavigateToCore(string uri, bool forceLoad)
            {
                UriVisited = uri;
            }
        }

        [Fact]
        public void Calc_NavigateToEasy_NavigatesToEasyPage()
        {
            var fakeNavigationManager = new FakeNavigationManager();
            var calcBase = new CalcBase { NavigationManager = fakeNavigationManager };

            calcBase.NavigateToEasy();

            Assert.Equal("/easy", fakeNavigationManager.UriVisited);
        }

        [Fact]
        public void Calc_NavigateToMedium_NavigatesToMediumPage()
        {
            var fakeNavigationManager = new FakeNavigationManager();
            var calcBase = new CalcBase { NavigationManager = fakeNavigationManager };

            calcBase.NavigateToMedium();

            Assert.Equal("/medium", fakeNavigationManager.UriVisited);
        }

        [Fact]
        public void Easy_GenerateNewProblem_CreatesValidProblem()
        {
            var easyBase = new EasyBase();
            easyBase.GenerateNewProblem();

            Assert.NotEqual(0, easyBase.number1);
            Assert.NotEqual(0, easyBase.number2);
            Assert.True(easyBase.operation == "+" || easyBase.operation == "-");
        }

        [Fact]
        public void Easy_CheckAnswer_ValidAnswer_IncrementsCorrectAnswers()
        {
            var easyBase = new EasyBase
            {
                number1 = 5,
                number2 = 3,
                operation = "+",
                correctAnswer = 8,
                userAnswer = "8"
            };

            easyBase.CheckAnswer();

            Assert.True(easyBase.isCorrect);
            Assert.Equal(1, easyBase.correctAnswers);
        }

        [Fact]
        public void Easy_CheckAnswer_InvalidAnswer_SetsIsCorrectFalse()
        {
            var easyBase = new EasyBase
            {
                number1 = 5,
                number2 = 3,
                operation = "+",
                correctAnswer = 8,
                userAnswer = "7"
            };

            easyBase.CheckAnswer();

            Assert.False(easyBase.isCorrect);
        }

        [Fact]
        public void Medium_GenerateNewProblem_CreatesValidProblem()
        {
            var mediumBase = new MediumBase();
            mediumBase.GenerateNewProblem();

            Assert.NotEqual(0, mediumBase.number1);
            Assert.NotEqual(0, mediumBase.number2);
            Assert.Contains(mediumBase.operation, new[] { "+", "-", "*", "/" });
        }

        [Fact]
        public void Medium_CheckAnswer_DivisionCorrectAnswer_SetsIsCorrectTrue()
        {
            var mediumBase = new MediumBase
            {
                number1 = 20,
                number2 = 4,
                operation = "/",
                correctAnswer = 5,
                userAnswer = "5"
            };

            mediumBase.CheckAnswer();

            Assert.True(mediumBase.isCorrect);
        }

        [Fact]
        public void Easy_RestartGame_ResetsGameState()
        {
            var easyBase = new EasyBase
            {
                IsTestMode = true,
                correctAnswers = 5,
                totalRounds = 10,
                timeIsUp = true,
                remainingTime = 0
            };

            easyBase.RestartGame();

            Assert.False(easyBase.timeIsUp);
            Assert.Equal(30, easyBase.remainingTime);
            Assert.Equal(0, easyBase.totalRounds);
            Assert.Equal(0, easyBase.correctAnswers);
        }

        [Fact]
        public void Easy_RestartGame_ResetsGameState_WithBUnit()
        {
            using var ctx = new TestContext();
            var easyBase = ctx.RenderComponent<EasyBase>().Instance;

            easyBase.RestartGame();

            Assert.False(easyBase.timeIsUp);
            Assert.Equal(30, easyBase.remainingTime);
            Assert.Equal(0, easyBase.totalRounds);
            Assert.Equal(0, easyBase.correctAnswers);
        }

        [Fact]
        public void Medium_RestartGame_ResetsGameState()
        {
            var mediumBase = new MediumBase
            {
                IsTestMode = true,
                correctAnswers = 3,
                totalRounds = 8,
                timeIsUp = true,
                remainingTime = 0
            };

            mediumBase.RestartGame();

            Assert.False(mediumBase.timeIsUp);
            Assert.Equal(30, mediumBase.remainingTime);
            Assert.Equal(0, mediumBase.totalRounds);
            Assert.Equal(0, mediumBase.correctAnswers);
        }

        [Fact]
        public void Easy_CheckAnswer_InvalidInput_SetsIsCorrectFalse()
        {
            var easyBase = new EasyBase
            {
                userAnswer = "not_a_number"
            };

            easyBase.CheckAnswer();

            Assert.False(easyBase.isCorrect);
        }

        [Fact]
        public void Easy_StartCountdown_TimerExpires_SetsTimeIsUp()
        {
            var easyBase = new EasyBase
            {
                IsTestMode = true,
                remainingTime = 3
            };

            easyBase.ForceCountdownToExpire();

            Assert.True(easyBase.timeIsUp);
            Assert.Equal(0, easyBase.remainingTime);
        }

        [Fact]
        public void Medium_CheckAnswer_InvalidInput_SetsErrorMessage()
        {
            var mediumBase = new MediumBase
            {
                userAnswer = "invalid",
                IsTestMode = true
            };

            mediumBase.CheckAnswer();

            Assert.Equal("Invalid input: 'invalid' is not a number.", mediumBase.errorMessage);
        }

        [Fact]
        public void Medium_StartCountdown_TimerExpires_SetsTimeIsUp()
        {
            var mediumBase = new MediumBase
            {
                IsTestMode = true,
                remainingTime = 3
            };

            mediumBase.ForceCountdownToExpire();

            Assert.True(mediumBase.timeIsUp);
            Assert.Equal(0, mediumBase.remainingTime);
        }

        [Fact]
        public void Medium_GenerateNewProblem_ValidatesAllOperations()
        {
            var mediumBase = new MediumBase();
            for (int i = 0; i < 10; i++)
            {
                mediumBase.GenerateNewProblem();

                Assert.InRange(mediumBase.number1, 1, 100);
                Assert.InRange(mediumBase.number2, 1, 100);
                Assert.Contains(mediumBase.operation, new[] { "+", "-", "*", "/" });

                switch (mediumBase.operation)
                {
                    case "+":
                        Assert.Equal(mediumBase.correctAnswer, mediumBase.number1 + mediumBase.number2);
                        break;
                    case "-":
                        Assert.Equal(mediumBase.correctAnswer, mediumBase.number1 - mediumBase.number2);
                        break;
                    case "*":
                        Assert.Equal(mediumBase.correctAnswer, mediumBase.number1 * mediumBase.number2);
                        break;
                    case "/":
                        Assert.Equal(mediumBase.correctAnswer, mediumBase.number1 / mediumBase.number2);
                        break;
                }
            }
        }

        [Fact]
        public void Medium_GenerateNewProblem_SetsValidState()
        {
            var mediumBase = new MediumBase();

            mediumBase.GenerateNewProblem();

            Assert.NotEqual(0, mediumBase.number1);
            Assert.NotEqual(0, mediumBase.number2);
            Assert.Contains(mediumBase.operation, new[] { "+", "-", "*", "/" });
            Assert.Equal(string.Empty, mediumBase.userAnswer);
            Assert.Equal(string.Empty, mediumBase.errorMessage);
        }

        
        [Fact]
        public void Medium_CheckAnswer_TimeIsUp_DoesNothing()
        {
            var mediumBase = new MediumBase
            {
                timeIsUp = true,
                userAnswer = "5",
                correctAnswers = 2,
                totalRounds = 3,
            };

            mediumBase.CheckAnswer();

            Assert.Equal(2, mediumBase.correctAnswers);
            Assert.Equal(3, mediumBase.totalRounds);
            Assert.False(mediumBase.isCorrect);
        }

    }
}