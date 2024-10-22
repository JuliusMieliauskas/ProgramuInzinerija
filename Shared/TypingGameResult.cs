using System.Collections;
using System.Collections.Generic;

namespace Shared;

public class TypingGameResult
{
    public int Id { get; set; }
    public int WordsPerMinute { get; set; }
    public int Errors { get; set; }

    public TypingGameResult() { }

    public TypingGameResult(int id, int wordsPerMinute, int errors)
    {
        Id = id;
        WordsPerMinute = wordsPerMinute;
        Errors = errors;
    }
}
