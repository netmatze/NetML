using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netMLLanguage.Parser
{
    public class ParseResult<T>
    {
        public bool Succeeded { get; set; }
        public readonly T Result;
        public readonly string RemainingInput;
        public ParserState ParserState { get; set; }
        public Error Error_ { get; set; }

        public ParseResult(T result, string remainingInput, bool succeded)
        {
            this.Result = result;
            this.RemainingInput = remainingInput;
            this.Succeeded = succeded;
        }
    }

    public class ParserState
    {
        public int Position { get; set; }
        public string Input { get; set; }
    }

    public class Error
    {
        public int Position { get; set; }
        public string Message { get; set; }
    }
}
