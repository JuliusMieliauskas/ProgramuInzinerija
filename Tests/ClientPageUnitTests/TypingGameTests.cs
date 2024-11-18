using Client.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Shared;
using Data;
using System.Net.Http.Json;
namespace Tests;
public class TypingGameTests
{
    private readonly TypingBase _typingClass = new TypingBase();

    [Theory]
    [InlineData("hello world", "hello world", 20, 13, 0)]   
    [InlineData("hello world", "hella warld", 20, 13, 2)]   
    [InlineData("helloworld", "hello world", 15, 8, 5)]    
    [InlineData("quick brown fox", "quick brown dog", 10, 9, 2)] 
    [InlineData("", "hello world", 25, 0, 0)]               
    [InlineData("test", "testing", 0, 1, 0)]               
    public void CalculateWPM_CalculatesCorrectWPMAndErrorCount(string userInput, string sampleText, int timeRemaining, int expectedWPM, int expectedErrorCount)
    {
        _typingClass.UserInput = userInput;
        _typingClass.SampleText = sampleText;
        _typingClass.TimeRemaining = timeRemaining;
        _typingClass.WPM = 0; 
        _typingClass.ErrorCount = 0; 

        _typingClass.CalculateWPM();

        Assert.Equal(expectedWPM, _typingClass.WPM);
        Assert.Equal(expectedErrorCount, _typingClass.ErrorCount);
    }

}