using Xunit.Sdk;
using System;

namespace Tests;
public class TestNullException : Exception
{
    public string TestName { get; }

    public TestNullException() { }

    public TestNullException(string message)
        : base(message) { }
    public TestNullException(string message, string testName)
        : this(message)
    {
        TestName = testName;
    }

    public TestNullException(string message, Exception inner)
        : base(message, inner) { }
}