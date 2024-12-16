namespace Shared
{
    public class InvalidInputException : Exception
    {
        public string? UserInput { get; }

        public InvalidInputException() 
            : base("Invalid input! Please enter a valid number.") { }

        public InvalidInputException(string message)
            : base(message) { }

        public InvalidInputException(string message, string userInput)
            : base(message)
        {
            UserInput = userInput;
        }

        public InvalidInputException(string message, Exception inner)
            : base(message, inner) { }
    }
}
