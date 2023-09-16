using System.Text;

namespace App
{
    
    public record RomanNumber
    {
        private const char ZERO_DIGIT = 'N';
        private const char MINUS_SIGN = '-';
        private const char DIGIT_QOUTE = '\'';
        private const string DIGITS_SEPARATOR = ", ";
        private const string INVALID_DIGIT_MESSAGE = "Invalid Roman didgit(s):";
        private const string EMPTY_INPUT_MESSAGE = "NULL or empty input";
        private const string ADD_NULL_MESSAGE = "Cannot Add null object";
        private const string NULL_MESSAGE_PATTERN = "{0}: '{1}'";
        private const string SUM_NULL_MESSAGE = "Invalid Sum() invocation with NULL argument";
        private const string INVALID_DATA_SUM_MESSAGE = "Invalid Sum() invocation with NULL argument: ";
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
    }
}
