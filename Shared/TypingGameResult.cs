using System;
using System.Collections.Generic;

namespace Shared;

public class TypingGameResult : IComparable<TypingGameResult>
{
    public int Id { get; set; }
    public int WordsPerMinute { get; set; }
    public int Errors { get; set; }
    public TypingGameStatus Status { get; set; }

    public double WpmToErrorRatio => Errors == 0 ? WordsPerMinute : (double)WordsPerMinute / Errors;

    public TypingGameResult() { }

    public TypingGameResult(int id, int wordsPerMinute, int errors = 0, TypingGameStatus? status = null)
    {
        Id = id;
        WordsPerMinute = wordsPerMinute;
        Errors = errors;
        Status = status ?? (errors == 0 ? TypingGameStatus.PerfectRun : TypingGameStatus.NotPerfectRun);
    }

    public int CompareTo(TypingGameResult other)
    {
        if (other == null) return 1;

        return WpmToErrorRatio.CompareTo(other.WpmToErrorRatio);
    }
}