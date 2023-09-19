using System.Text;
using System.Text.RegularExpressions;

namespace App
{
    
    public record RomanNumber
    {
        private const char ZERO_DIGIT = 'N';
        private const char MINUS_SIGN = '-';
        private const char PLUS_SIGN = '+';
        private const char DIGIT_QOUTE = '\'';
        private const string SPACES_REGEX = @"\s+";
        private const string DIGITS_SEPARATOR = ", ";
        private const string INVALID_DIGIT_MESSAGE = "Invalid Roman didgit(s):";
        private const string EMPTY_INPUT_MESSAGE = "NULL or empty input";
        private const string ADD_NULL_MESSAGE = "Cannot Add null object";
        private const string NULL_MESSAGE_PATTERN = "{0}: '{1}'";
        private const string SUM_NULL_MESSAGE = "Invalid Sum() invocation with NULL argument";
        private const string EVAL_INVALID_EXPRESSION_MESSAGE = "Invalid expression";
        private const string EVAL_TOO_MANY_ARGUMENTS_MESSAGE = "Too many arguments";
        public int Value { get; set; }
        public RomanNumber(int value = 0)
        {
            Value = value;
        }

        public override string ToString()
        {
            // відобразити значення Value у формі римського числа (в оптимальній формі)
            // головна ідея - послідовно зменшення початкового числа і формування результату
            Dictionary<int, string> parts = new()
            {
                { 1000 , "M"  },
                { 900  , "CM" },
                { 500  , "D"  },
                { 400  , "CD" },
                { 100  , "C"  },
                { 90   , "XC" },
                { 50   , "L"  },
                { 40   , "XL" },
                { 10   , "X"  },
                { 9    , "IX" },
                { 5    , "V"  },
                { 4    , "IV" },
                { 1    , "I"  }
            }; // 1982 - M CM L XXX II

            if (Value == 0) return ZERO_DIGIT.ToString();

            bool isNegative = Value < 0;
            var number = isNegative ? -Value : Value;

            StringBuilder sb = new();

            if (isNegative) sb.Append(MINUS_SIGN);

            foreach (var part in parts)
            {
                while (number >= part.Key)
                {
                    sb.Append(part.Value);
                    number -= part.Key;
                }
            }

            return sb.ToString();
        }

        public static RomanNumber Parse(String input)
        {
            input = input?.Trim()!;

            if (input == ZERO_DIGIT.ToString()) return new();

            CheckValidityOrThrow(input);
            CheckLegalityOrThrow(input);

            int lastDigitIndex = input[0] == MINUS_SIGN ? 1 : 0;

            int prev = 0;
            int current;
            int result = 0;

            for (int i = input.Length - 1; i >= lastDigitIndex; i--)
            {
                current = DigitValue(input[i]);
                result += prev > current ? -current : current;
                prev = current;
            }

            return new() { Value = result * (1 - 2 * lastDigitIndex) };

            /* Правила "читання" римських чисел:
             * Якщо цифра передує
             * більшій цифрі, то вона віднімається (IV, IX) - "I"
             * меньшій або рівні - додається (VI, II, XI)
             * Решту правил ігноруємо - робимо максимально "дружній" інтерфейс
             * 
             * Алгоритм - "заходимо" з правої цифри, її завжди додаємо, запам'ятовуємо та порівнюємо з наступною
             */
        }

        private static int DigitValue(char digit)
        {
            return digit switch
            {
                //ZERO_DIGIT => 0,
                'I' => 1,
                'V' => 5,
                'X' => 10,
                'L' => 50,
                'C' => 100,
                'D' => 500,
                'M' => 1000,
                _ => throw new ArgumentException($"{INVALID_DIGIT_MESSAGE} {DIGIT_QOUTE}{digit}{DIGIT_QOUTE}")
            };
        }

        private static void CheckValidityOrThrow(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentException(EMPTY_INPUT_MESSAGE);

            if (input.StartsWith(MINUS_SIGN)) input = input[1..];

            List<char> invalidChars = new();

            foreach(char c in input)
            {
                //if (c == '-') continue;
                try { DigitValue(c); }
                catch { invalidChars.Add(c); }
            }

            if(invalidChars.Count > 0)
            {
                string chars = string.Join(DIGITS_SEPARATOR, invalidChars.Select(c => $"{DIGIT_QOUTE}{c}{DIGIT_QOUTE}"));
                throw new ArgumentException($"{INVALID_DIGIT_MESSAGE} {chars}");
            }
        }

        private static void CheckLegalityOrThrow(string input)
        {
            if (input.Length == 0) return;

            int maxDigit = 0;
            int lessDigitsCount = 0;
            int lastDigitIndex = input.StartsWith(MINUS_SIGN) ? 1 : 0;
            for (int i = input.Length - 1; i >= lastDigitIndex; i--)
            {
                int digitValue = DigitValue(input[i]);
                if (digitValue < maxDigit)
                {
                    lessDigitsCount++;
                    if (lessDigitsCount > 1) throw new ArgumentException(input);
                }
                else
                {
                    maxDigit = digitValue;
                    lessDigitsCount = 0;
                }

            }
        }

        public RomanNumber Add(RomanNumber other)
        {
            if (other is null) throw new ArgumentNullException(string.Format(NULL_MESSAGE_PATTERN, ADD_NULL_MESSAGE, nameof(other)));
            return this with { Value = Value + other.Value };
        }

        public static RomanNumber Sum(params RomanNumber[] numbers)
        {
            if(numbers is null) throw new ArgumentNullException(string.Format(NULL_MESSAGE_PATTERN, SUM_NULL_MESSAGE, nameof(numbers)));
            //if (numbers.Length == 1) throw new ArgumentException(INVALID_DATA_SUM_MESSAGE + numbers.ToString());
            int sum = 0;
            foreach (RomanNumber number in numbers) sum += number.Value;
            return new RomanNumber(sum);
        }

        public static RomanNumber Eval(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentException(EMPTY_INPUT_MESSAGE);

            input = Regex.Replace(input, SPACES_REGEX, string.Empty);

            if (input.StartsWith(PLUS_SIGN) || input.EndsWith(PLUS_SIGN)) throw new ArgumentException(EVAL_INVALID_EXPRESSION_MESSAGE);

            if (!input.Contains(PLUS_SIGN) && !input.Contains(MINUS_SIGN)) 
            {
                RomanNumber result = new();
                try
                {
                    result = RomanNumber.Parse(input);
                    return result;
                }
                catch(Exception e) 
                {
                    throw new ArgumentException(e.Message + MINUS_SIGN);
                }
            }

            List<string> expression = new(3) { string.Empty, string.Empty, string.Empty };
            int index = 0;
            int operatorCount = 0;

            int i = 0;

            if (input.StartsWith(MINUS_SIGN))
            {
                expression[0] = MINUS_SIGN.ToString();
                i = 1;
            }

            for (; i < input.Length; i++)
            {
                if (input[i] == PLUS_SIGN)
                {
                    expression[1] = PLUS_SIGN.ToString();
                    index = 2;
                    operatorCount++;
                    if (operatorCount == 2) throw new ArgumentException(EVAL_TOO_MANY_ARGUMENTS_MESSAGE);
                    continue;
                }

                if (input[i] == MINUS_SIGN && index == 0)
                {
                    expression[1] = MINUS_SIGN.ToString();
                    index = 2;
                    operatorCount++;
                    if (operatorCount == 2) throw new ArgumentException(EVAL_TOO_MANY_ARGUMENTS_MESSAGE);
                    continue;
                }

                try
                {
                    if (input[i] != ZERO_DIGIT && input[i] != MINUS_SIGN)
                    {
                        DigitValue(input[i]);
                    }
                    expression[index] += input[i];
                }
                catch (Exception e)
                {
                    throw new ArgumentException(e.Message);
                }
            }

            RomanNumber left, right;
            try
            {
                left = RomanNumber.Parse(expression[0]);
                right = RomanNumber.Parse(expression[2]);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

            int res;

            if (expression[1] == PLUS_SIGN.ToString()) res = left.Value + right.Value;
            else res = left.Value - right.Value;

            return new(res);
        }

        //public static RomanNumber Eval(string input)
        //{
        //    if (string.IsNullOrEmpty(input)) throw new ArgumentException(EMPTY_INPUT_MESSAGE);

        //    if (!input.Contains(PLUS_SIGN)) throw new ArgumentException(EVAL_INPUT_PLUS_MESSAGE);
            
        //    input = input.Trim();

        //    if (input.StartsWith(PLUS_SIGN) || input.EndsWith(PLUS_SIGN)) throw new ArgumentException(EVAL_INPUT_PLUS_MESSAGE);

        //    string[] arr = input.Split(PLUS_SIGN);
        //    //string[] arr = input.Split(SPACE_SEPARATOR);

        //    if (arr.Length != 2) throw new ArgumentException(EVAL_INPUT_NUMBERS_COUNT_MESSAGE);

        //    //arr[0].Trim();
        //    //arr[1].Trim();

        //    //if (arr.Length != 3) throw new ArgumentException(EVAL_INPUT_NUMBERS_COUNT_MESSAGE);
        //    //if (arr[1] != PLUS_SIGN.ToString()) throw new ArgumentException(EVAL_INPUT_PLUS_MESSAGE);

        //    RomanNumber left, right;
        //    try
        //    {
        //        left = RomanNumber.Parse(arr[0]);
        //        right = RomanNumber.Parse(arr[1]);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new ArgumentException(ex.Message);
        //    }

        //    return new RomanNumber(left.Value + right.Value);
        //}
    }
}
