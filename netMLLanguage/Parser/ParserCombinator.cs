using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

// http://lorgonblog.wordpress.com/2007/12/03/monadic-parser-combinators-part-two/
// http://blogs.msdn.com/b/lukeh/archive/2007/08/19/monadic-parser-combinators-using-c-3-0.aspx
namespace netMLLanguage.Parser
{
    public delegate ParseResult<T> Parse<T>(string input);

    public static class Parser
    {
        public static Parse<T> Fail<T>()
        {
            return input =>
            {
                string rest = input;
                return new ParseResult<T>(default(T), null, false);
            };
        }

        public static ParseResult<T> End<T>(string input)
        {
            string rest = input;
            return new ParseResult<T>(default(T), input, false);
        }        

        public static Parse<T> Return<T>(T value)
        {
            return input =>
            {
                string rest = input;
                return new ParseResult<T>(value, rest, true);
            };
        }

        public static Parse<T2> Then<T1, T2>(this Parse<T1> parse1, Func<T1, Parse<T2>> parse2)
        {
            return parse1.Bind(parse2);
        }

        public static Parse<T2> Then_<T1, T2>(this Parse<T1> parse1, Func<T1, Parse<T2>> parse2)
        {
            return value =>
            {
                var parse1Result = parse1(value);
                return parse2(parse1Result.Result)(parse1Result.RemainingInput);
            };
        }

        public static Parse<T> Repeat<T>(this Parse<T> parse1, Func<T, Parse<T>> parse2)
        {
            return value =>
            {
                var parse1Result = parse1(value);
                var parse2Result = parse2(parse1Result.Result)(parse1Result.RemainingInput);
                if (parse2Result.Succeeded)
                {
                    return parse2(parse2Result.Result)(parse2Result.RemainingInput);
                }
                return parse2Result;
            };
        }

        public static Parse<T> Repeat<T>(this Parse<T> parse1, Parse<T> parse2)
        {
            return value =>
            {
                var parse1Result = parse1(value);
                ParseResult<T> parse2Result = parse2(parse1Result.RemainingInput);
                if (parse2Result.Succeeded)
                {
                    return RepeatHelper(parse2)(parse2Result.RemainingInput);
                }
                return parse2Result;
            };
        }

        private static Parse<T> RepeatHelper<T>(Parse<T> parse)
        {
            return value =>
            {
                ParseResult<T> parseResult = parse(value);
                if (parseResult.Succeeded)
                {
                    return RepeatHelper(parse)(parseResult.RemainingInput);
                }
                return parseResult;
            };
        }

        public static Parse<int> Operate(this Parse<int> parse1, Parse<Func<int, int, int>> operatorFunction)
        {
            return value =>
            {
                var parse1Result = parse1(value);
                if (string.IsNullOrEmpty(parse1Result.RemainingInput))
                {
                    return parse1Result;
                }
                Parse<int> operatorHelp = OperateHelper(parse1Result.Result, parse1, operatorFunction);
                return operatorHelp(parse1Result.RemainingInput);
            };
        }

        public static Parse<int> OperateHelper(int value, Parse<int> parse1, Parse<Func<int, int, int>> operatorFunc)
        {
            return localvalue =>
            {
                var operatorResult = operatorFunc(localvalue);
                var resultParse1 = parse1(operatorResult.RemainingInput);
                if (operatorResult.Result != null && resultParse1.Succeeded)
                {
                    var operationResult = operatorResult.Result(value, resultParse1.Result);
                    if (!string.IsNullOrEmpty(resultParse1.RemainingInput))
                    {
                        var nextOperatorHelperResult =
                            OperateHelper(operationResult, parse1, operatorFunc)(resultParse1.RemainingInput);
                        if (nextOperatorHelperResult.Succeeded)
                        {
                            return nextOperatorHelperResult;
                        }
                    }
                    return new ParseResult<int>(operationResult, resultParse1.RemainingInput, true);
                }
                else
                {
                    return new ParseResult<int>(value, resultParse1.RemainingInput, true);
                }
            };
        }

        public static Parse<char> Letter()
        {
            return value => Match(char.IsLetter)(value);
        }

        public static Parse<char> LetterOrDigit()
        {
            return value => Match(char.IsLetterOrDigit)(value);
        }

        public static Parse<char> LetterOrDigitOrPoint()
        {
            return value => Match(chr => char.IsLetterOrDigit(chr) || chr == '.')(value);
        }

        public static Parse<char> IsPoint()
        {
            return value => Match(c => c == '.')(value);
        }

        public static Parse<char> IsWhitespace()
        {
            return value => Match(c => c == ' ')(value);
        }

        public static Parse<char> IsSingleQuotation()
        {
            return value => Match(c => c == '\'')(value);
        }

        public static Parse<char> IsDoubleQuotation()
        {
            return value => Match(c => c == '\"')(value);
        }

        public static Parse<char> Digit()
        {
            return value => Match(char.IsDigit)(value);
        }

        public static Parse<int> DigitValue()
        {
            return value =>
            {
                ParseResult<char> whiteSpaceResult = IsWhitespace()(value);
                if (!whiteSpaceResult.Succeeded)
                {
                    ParseResult<char> result = Match(char.IsDigit)(value);
                    int resultValue = 0;
                    if (result.Succeeded)
                    {
                        resultValue = Convert.ToInt32(result.Result.ToString());
                        return new ParseResult<int>(resultValue, result.RemainingInput, true);
                    }
                    else if(result.RemainingInput == "")
                    {
                        return new ParseResult<int>(resultValue, "", false);
                    }
                    else
                    {
                        return new ParseResult<int>(resultValue, value, false);
                    }
                }
                else if(whiteSpaceResult.RemainingInput == "")
                {
                    ParseResult<int> result = End<int>(whiteSpaceResult.RemainingInput);
                    return result;
                }
                else
                {
                    ParseResult<int> result = DigitValue()(whiteSpaceResult.RemainingInput);
                    return result;
                }
            };
        }

        public static Parse<string> StringValue()
        {
            return StringValue(String.Empty);
        }

        public static Parse<string> StringValue(string str)
        {
            return value =>
            {
                ParseResult<char> whiteSpaceResult = IsWhitespace()(value);
                if (!whiteSpaceResult.Succeeded)
                {
                    //ParseResult<char> singleQuotationResult = IsSingleQuotation()(value);
                    //if (!singleQuotationResult.Succeeded)
                    //{
                    ParseResult<char> parseResult = LetterOrDigitOrPoint()(value);
                    if (parseResult.Succeeded)
                    {
                        var stringVal = StringValue(str + parseResult.Result.ToString());
                        ParseResult<string> result = stringVal(parseResult.RemainingInput);
                        return result;
                    }
                    if (string.IsNullOrEmpty(str))
                        return new ParseResult<string>(str, value, false);
                    return new ParseResult<string>(str, value, true);
                    //}
                    //else
                    //{
                    //    var stringVal = StringValue(str);
                    //    ParseResult<string> result = stringVal(singleQuotationResult.RemainingInput);
                    //    return result;
                    //}
                }
                else
                {
                    var stringVal = StringValue(str);
                    ParseResult<string> result = stringVal(whiteSpaceResult.RemainingInput);
                    return result;
                }
            };
        }

        public static Parse<bool> Bool()
        {
            return BoolHelper(String.Empty);
        }

        public static Parse<bool> BoolHelper(string boolValue)
        {
            return value =>
            {
                ParseResult<char> whiteSpaceResult = IsWhitespace()(value);
                if (!whiteSpaceResult.Succeeded)
                {
                    ParseResult<char> parseResult = LetterOrDigit()(value);
                    if (parseResult.Succeeded)
                    {
                        var stringVal = StringValue(boolValue + parseResult.Result.ToString());
                        ParseResult<string> result = stringVal(parseResult.RemainingInput);
                        if (result.Result.ToLower() == "true")
                            return new ParseResult<bool>(true, parseResult.RemainingInput, true);
                        else
                        {
                            return new ParseResult<bool>(false, parseResult.RemainingInput, true);
                        }
                    }
                    if (string.IsNullOrEmpty(boolValue))
                        return new ParseResult<bool>(false, value, false);
                    return new ParseResult<bool>(false, value, true);
                }
                else
                {
                    var stringVal = StringValue(boolValue);
                    ParseResult<string> result = stringVal(whiteSpaceResult.RemainingInput);
                    if (result.Result.ToLower() == "true")
                        return new ParseResult<bool>(true, result.RemainingInput, true);
                    else
                    {
                        return new ParseResult<bool>(false, result.RemainingInput, true);
                    }
                }
            };
        }

        public static Parse<string> BoolString()
        {
            return BoolStringHelper(String.Empty);
        }

        public static Parse<string> BoolStringHelper(string boolValue)
        {
            return value =>
            {
                ParseResult<char> whiteSpaceResult = IsWhitespace()(value);
                if (!whiteSpaceResult.Succeeded)
                {
                    ParseResult<char> parseResult = LetterOrDigit()(value);
                    if (parseResult.Succeeded)
                    {
                        var stringVal = StringValue(boolValue + parseResult.Result.ToString());
                        ParseResult<string> result = stringVal(parseResult.RemainingInput);
                        if (result.Result.ToLower() == "true")
                            return new ParseResult<string>("true", parseResult.RemainingInput, true);
                        else
                        {
                            return new ParseResult<string>("false", parseResult.RemainingInput, true);
                        }
                    }
                    if (string.IsNullOrEmpty(boolValue))
                        return new ParseResult<string>("false", value, false);
                    return new ParseResult<string>("false", value, true);
                }
                else
                {
                    var stringVal = StringValue(boolValue);
                    ParseResult<string> result = stringVal(whiteSpaceResult.RemainingInput);
                    if (result.Result.ToLower() == "true")
                        return new ParseResult<string>("true", result.RemainingInput, true);
                    else
                    {
                        return new ParseResult<string>("false", result.RemainingInput, true);
                    }
                }
            };
        }

        public static Parse<int> Integer()
        {
            return IntegerHelper(0);
        }

        public static Parse<string> IntegerString()
        {
            return IntegerHelperString(0.ToString());
        }

        private static Parse<int> IntegerHelper(int x)
        {
            return value =>
            {
                ParseResult<int> parseResult = DigitValue()(value);
                if (parseResult.Succeeded)
                {
                    var intVal = IntegerHelper(10 * x + parseResult.Result);
                    ParseResult<int> result = intVal(parseResult.RemainingInput);
                    return result;
                }
                if (x == 0)
                    return new ParseResult<int>(x, parseResult.RemainingInput, false);
                return new ParseResult<int>(x, parseResult.RemainingInput, true);
            };
        }

        private static Parse<string> IntegerHelperString(string x)
        {
            return value =>
            {
                ParseResult<int> parseResult = DigitValue()(value);
                if (parseResult.Succeeded)
                {
                    var innerValue = 10 * Convert.ToInt16(x) + parseResult.Result;
                    var intVal = IntegerHelperString(innerValue.ToString());
                    ParseResult<string> result = intVal(parseResult.RemainingInput);
                    return result;
                }
                if (x.ToString() == "0")
                    return new ParseResult<string>(x, parseResult.RemainingInput, false);
                return new ParseResult<string>(x, parseResult.RemainingInput, true);
            };
        }

        public static Parse<decimal> Decimal()
        {
            return DecimalHelper(0, true, 1);
        }

        private static Parse<decimal> DecimalHelper(decimal x, bool front, int stelle)
        {
            return value =>
            {
                if (front)
                {
                    ParseResult<int> parseResult = DigitValue()(value);
                    if (parseResult.Succeeded)
                    {
                        var doubleVal = DecimalHelper(10 * x + parseResult.Result, true, 1);
                        ParseResult<decimal> result = doubleVal(parseResult.RemainingInput);
                        return result;
                    }
                    else
                    {
                        ParseResult<char> pointResult = IsPoint()(value);
                        if (pointResult.Succeeded)
                        {
                            var doubleVal = DecimalHelper(x, false, 1);
                            ParseResult<decimal> result = doubleVal(pointResult.RemainingInput);
                            return result;
                        }
                        return new ParseResult<decimal>(x, parseResult.RemainingInput, false);
                    }
                }
                else
                {
                    ParseResult<int> parseResult = DigitValue()(value);
                    if (parseResult.Succeeded)
                    {
                        decimal dec = (decimal)parseResult.Result;
                        for (int i = 0; i < stelle; i++)
                        {
                            dec = dec / 10;
                        }
                        var doubleVal = DecimalHelper(x + dec, false, ++stelle);
                        ParseResult<decimal> result = doubleVal(parseResult.RemainingInput);
                        return result;
                    }
                    return new ParseResult<decimal>(x, parseResult.RemainingInput, false);
                }
            };
        }

        public static Parse<string> DecimalString()
        {
            return DecimalStringHelper(0.ToString(), true, 1);
        }

        private static Parse<string> DecimalStringHelper(string x, bool front, int stelle)
        {
            return value =>
            {
                var culture = new CultureInfo("en-gb");
                if (front)
                {
                    ParseResult<int> parseResult = DigitValue()(value);
                    if (parseResult.Succeeded)
                    {
                        string str = Convert.ToString(10 * Convert.ToInt32(x) + Convert.ToInt32(parseResult.Result), culture);
                        var doubleVal = DecimalStringHelper(str, true, 1);
                        ParseResult<string> result = doubleVal(parseResult.RemainingInput);
                        return result;
                    }
                    else
                    {
                        ParseResult<char> pointResult = IsPoint()(value);
                        if (pointResult.Succeeded)
                        {
                            var doubleVal = DecimalStringHelper(x, false, 1);
                            ParseResult<string> result = doubleVal(pointResult.RemainingInput);
                            return result;
                        }
                        if (string.IsNullOrEmpty(x) || (!parseResult.Succeeded && !pointResult.Succeeded))
                            return new ParseResult<string>(x, parseResult.RemainingInput, false);
                        return new ParseResult<string>(x, parseResult.RemainingInput, true);
                    }
                }
                else
                {
                    ParseResult<int> parseResult = DigitValue()(value);
                    if (parseResult.Succeeded)
                    {
                        decimal dec = (decimal)parseResult.Result;
                        for (int i = 0; i < stelle; i++)
                        {
                            dec = dec / 10;
                        }
                        string str = Convert.ToString(Convert.ToDecimal(x) + Convert.ToDecimal(dec), culture);
                        var doubleVal = DecimalStringHelper(str, false, ++stelle);
                        ParseResult<string> result = doubleVal(parseResult.RemainingInput);
                        return result;
                    }
                    if (string.IsNullOrEmpty(x))
                        return new ParseResult<string>(x, parseResult.RemainingInput, false);
                    return new ParseResult<string>(x, parseResult.RemainingInput, true);
                }
            };
        }

        public static Parse<string> Literal(string literalToParse)
        {
            string localLiteralToParse = literalToParse;
            if (string.IsNullOrEmpty(localLiteralToParse))
            {
                return value => new ParseResult<string>("", value, false);
            }
            else
            {
                return value =>
                {
                    var newLength = value.TrimStart(' ').Length;
                    if (value.TrimStart(' ').Length < literalToParse.Length)
                    {
                        return new ParseResult<string>("", value, false);
                    }
                    else if (value != String.Empty && value.TrimStart(' ').Substring(0, literalToParse.Length).ToLower() == localLiteralToParse.ToLower())
                    {
                        return new ParseResult<string>(localLiteralToParse.ToLower(), value.TrimStart(' ').Substring(localLiteralToParse.Length, newLength - localLiteralToParse.Length), true);
                    }
                    else
                    {
                        return new ParseResult<string>("", value, false);
                    }
                };
            }
        }

        public static Parse<string> StringLiteral(string str)
        {
            return value =>
            {
                var newLength = value.TrimStart(' ').Length;
                ParseResult<string> result = new ParseResult<string>(
                    str, value.TrimStart(' ').Substring(str.Length, newLength - str.Length), true);
                return result;
            };
        }

        public static Parse<T> Or<T>(this Parse<T> parse1, Parse<T> parse2)
        {
            return value =>
            {
                var parse1Result = parse1(value);
                if (!parse1Result.Succeeded)
                {
                    return parse2(value);
                }
                else
                {
                    return parse1Result;
                }
            };
        }

        public static Parse<T> Or_<T>(this Parse<T> parse1, Func<T, Parse<T>> parse2)
        {
            return value =>
            {
                var parse1Result = parse1(value);
                if (!parse1Result.Succeeded)
                {
                    return parse2(parse1Result.Result)(parse1Result.RemainingInput);
                }
                else
                {
                    return parse1Result;
                }
            };
        }

        public static Parse<T2> And<T1, T2>(this Parse<T1> parse1, Parse<T2> parse2)
        {
            return input =>
            {
                if (!string.IsNullOrEmpty(input))
                {
                    var parse1Result = parse1(input);
                    if (parse1Result.Succeeded)
                    {
                        return parse2(parse1Result.RemainingInput);
                    }
                    else
                    {
                        return new ParseResult<T2>(default(T2), input, false);
                    }
                }
                else
                {
                    return new ParseResult<T2>(default(T2), String.Empty, false);
                }
            };
        }

        public static Parse<T> And_<T>(this Parse<T> parse1, Func<T, Parse<T>> parse2)
        {
            return value =>
            {
                var parse1Result = parse1(value);
                if (parse1Result.Succeeded)
                {
                    return parse2(parse1Result.Result)(parse1Result.RemainingInput);
                }
                else
                {
                    return parse1Result;
                }
            };
        }

        public static Parse<char> Item()
        {
            return new Parse<char>(input =>
            {
                if (input != null && input.ToCharArray().Count() > 0)
                {
                    char chr = input.ToCharArray()[0];
                    string rest = input.Remove(0, 1);
                    return new ParseResult<char>(chr, rest, true);
                }
                return new ParseResult<char>(' ', String.Empty, false);
            });
        }

        public static Parse<char> Match(Func<char, bool> func)
        {
            return Then(Item(), value =>
            {
                if (func(value))
                    return Return(value);
                return Fail<char>();
            });
        }
    }
}
