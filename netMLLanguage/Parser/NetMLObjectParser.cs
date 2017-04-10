using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netMLLanguage.Parser
{
    public static class NetMLObjectParser
    {
        public static Parse<T> CreateNetMLObject<T>(this Parse<T> parse, 
            NetMLObjectBuilder netMLObjectBuilder)
        {
            return value =>
            {
                ParseResult<T> result = parse(value);
                if (result.Succeeded)
                {
                    netMLObjectBuilder.Create(new NetMLObject());
                }
                return result;
            };
        }

        public static Parse<string> CreateClassificationAlgorithmusObject(this Parse<string> parse, NetMLObjectBuilder netMLObjectBuilder)
        {
            return value =>
            {
                ParseResult<string> result = parse(value);
                if (result.Succeeded)
                {
                    var culture = new CultureInfo("en-gb");
                    var str =
                            result.Result.ToString();
                    netMLObjectBuilder.CreateAlgorithmusClassification(str);
                }
                return result;
            };
        }

        public static Parse<string> CreateAlgorithmus(this Parse<string> parse, NetMLObjectBuilder netMLObjectBuilder)
        {
            return value =>
            {
                ParseResult<string> result = parse(value);
                if (result.Succeeded)
                {
                    var culture = new CultureInfo("en-gb");
                    var str =
                            result.Result.ToString();
                    netMLObjectBuilder.CreateAlgorithmus(str);
                }
                return result;
            };
        }

        public static Parse<string> CreateOption(this Parse<string> parse, NetMLObjectBuilder netMLObjectBuilder)
        {
            return value =>
            {
                ParseResult<string> result = parse(value);
                if (result.Succeeded)
                {
                    var culture = new CultureInfo("en-gb");
                    var str =
                            result.Result.ToString();
                    netMLObjectBuilder.AddOption(str);
                }
                return result;
            };
        }

        public static Parse<T> CreateVariable<T>(this Parse<T> parse, NetMLObjectBuilder netMLObjectBuilder)
        {
            return value =>
            {
                ParseResult<T> result = parse(value);
                if (result.Succeeded)
                {
                    var culture = new CultureInfo("en-gb");
                    var str =
                            result.Result.ToString();
                    netMLObjectBuilder.AddVariable(str);
                }
                return result;
            };
        }

        public static Parse<T> CreateValue<T>(this Parse<T> parse, NetMLObjectBuilder netMLObjectBuilder)
        {
            return value =>
            {
                ParseResult<T> result = parse(value);
                if (result.Succeeded)
                {
                    double decValue;
                    var culture = new CultureInfo("en-gb");
                    bool boolValue;
                    if (double.TryParse(result.Result.ToString(), NumberStyles.Number, culture, out decValue))
                    {
                        netMLObjectBuilder.AddValue(decValue);
                    }
                    else if (bool.TryParse(result.Result.ToString(), out boolValue))
                    {
                        netMLObjectBuilder.AddValue(boolValue);
                    }
                    else
                    {
                        var str =
                            result.Result.ToString().Replace("'", "").ToString();
                        netMLObjectBuilder.AddValue(str);
                    }
                }
                return result;
            };
        }
    }
}
