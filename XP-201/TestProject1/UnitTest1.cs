using App;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace TestProject1
{
    [TestClass]
    public class UnitTestRomanNumber
    {
        private static readonly Random r = new Random();
        [TestMethod]
        public void TestToString()
        {
            Dictionary<int, String> testCases = new()
            {
                { 0, "N" },
                { 1, "I" },
                { 2, "II" },
                { 3, "III" },
                { 4, "IV" },
                { 5, "V" },
                { 6, "VI" },
                { 7, "VII" },
                { 8, "VIII" },
                { 9, "IX" },
                { 10, "X" },
                { 11, "XI" },
                { 12, "XII" },
                { 13, "XIII" },
                { 14, "XIV" },
                { 15, "XV" },
                { 16, "XVI" },
                { 17, "XVII" },
                { 18, "XVIII" },
                { 19, "XIX" },
                { 20, "XX" },
                { 21, "XXI" },
                { 22, "XXII" },
                { 23, "XXIII" },
                { 29, "XXIX" },
                { 30, "XXX" },
                { 33, "XXXIII" },
                { 39, "XXXIX" },
                { 40, "XL" },
                { 44, "XLIV" },
                { 48, "XLVIII" },
                { 50, "L" },
                { 51, "LI" },
                { 52, "LII" },
                { 55, "LV" },
                { 59, "LIX" },
                { 62, "LXII" },
                { 64, "LXIV" },
                { 66, "LXVI" },
                { 75, "LXXV" },
                { 77, "LXXVII" },
                { 81, "LXXXI" },
                { 88, "LXXXVIII" },
                { 90, "XC" },
                { 92, "XCII" },
                { 99, "XCIX" },
                { 100, "C" },
                { 105, "CV" },
                { 107, "CVII" },
                { 111, "CXI" },
                { 115, "CXV" },
                { 123, "CXXIII" },
                { 222, "CCXXII" },
                { 234, "CCXXXIV" },
                { 246, "CCXLVI" },
                { 321, "CCCXXI" },
                { 333, "CCCXXXIII" },
                { 345, "CCCXLV" },
                { 378, "CCCLXXVIII" },
                { 404, "CDIV" },
                { 444, "CDXLIV" },
                { 456, "CDLVI" },
                { 500, "D" },
                { 555, "DLV" },
                { 567, "DLXVII" },
                { 609, "DCIX" },
                { 666, "DCLXVI" },
                { 678, "DCLXXVIII" },
                { 777, "DCCLXXVII" },
                { 789, "DCCLXXXIX" },
                { 888, "DCCCLXXXVIII" },
                { 890, "DCCCXC" },
                { 901, "CMI" },
                { 999, "CMXCIX" },
                { 1000, "M" },
                { 1007, "MVII" },
                { 1111, "MCXI" },
                { 1199, "MCXCIX" },
                { 1234, "MCCXXXIV" },
                { 1317, "MCCCXVII" },
                { 1350, "MCCCL" },
                { 1432, "MCDXXXII" },
                { 1500, "MD" },
                { 1575, "MDLXXV" },
                { 1632, "MDCXXXII" },
                { 1667, "MDCLXVII" },
                { 1734, "MDCCXXXIV" },
                { 1872, "MDCCCLXXII" },
                { 1969, "MCMLXIX" },
                { 1985, "MCMLXXXV" },
                { 2023, "MMXXIII" },
                { 2048, "MMXLVIII" },
                { 2107, "MMCVII" },
                { 2184, "MMCLXXXIV" },
                { 2222, "MMCCXXII" },
                { 2247, "MMCCXLVII" },
                { 2288, "MMCCLXXXVIII" },
                { 2345, "MMCCCXLV" },
                { 2392, "MMCCCXCII" },
                { 2496, "MMCDXCVI" },
                { 2499, "MMCDXCIX" },
                { 2500, "MMD" },
                { 2678, "MMDCLXXVIII" },
                { 2700, "MMDCC" },
                { 2781, "MMDCCLXXXI" },
                { 2884, "MMDCCCLXXXIV" },
                { 2958, "MMCMLVIII" },
                { 2999, "MMCMXCIX" },
                { 3000, "MMM" },
                {-23, "-XXIII" },
                {-169, "-CLXIX" },
                {-313, "-CCCXIII" },
                {-996, "-CMXCVI" },
                {-1272, "-MCCLXXII" },
                {-1456, "-MCDLVI" },
                {-1603, "-MDCIII" },
                {-1674, "-MDCLXXIV" },
                {-1718, "-MDCCXVIII" },
                {-1742, "-MDCCXLII" },
                {-1747, "-MDCCXLVII" },
                {-1784, "-MDCCLXXXIV" },
                {-1785, "-MDCCLXXXV" },
                {-1796, "-MDCCXCVI" },
                {-1884, "-MDCCCLXXXIV" },
                {-1945, "-MCMXLV" },
                {-1951, "-MCMLI" },
                {-1968, "-MCMLXVIII" },
                {-1972, "-MCMLXXII" },
                {-1980, "-MCMLXXX" },
                {-1982, "-MCMLXXXII" },
                {-2142, "-MMCXLII" },
                {-2266, "-MMCCLXVI" },
                {-2510, "-MMDX" },
                {-2727, "-MMDCCXXVII" },
                {-2730, "-MMDCCXXX" },
                {-2756, "-MMDCCLVI" },
                {-2767, "-MMDCCLXVII" },
                {-2777, "-MMDCCLXXVII" },
                {-2799, "-MMDCCXCIX" },
                {-2814, "-MMDCCCXIV" },
                {-2947, "-MMCMXLVII" },
                {-2970, "-MMCMLXX" },
                {-2987, "-MMCMLXXXVII" },
                {-2998, "-MMCMXCVIII"},
            };
            foreach (var pair in testCases)
            {
                Assert.AreEqual(pair.Value, new RomanNumber(pair.Key).ToString(), $"{pair.Key}.ToString() == {pair.Value}");
            }
            Assert.AreEqual("N", new RomanNumber().ToString(), "new RomanNumber() == N");

            // крос-тест
            for (int i = 0; i < 256; i++)
            {
                int number = r.Next(3001);
                string romeNumber = "";

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

                if (number == 0) romeNumber = "N";
                else
                {
                    bool isNegative = Convert.ToBoolean(r.Next(0, 2));
                    var tempNumber = number;
                    number = isNegative ? -tempNumber : tempNumber;

                    StringBuilder sb = new();

                    if (isNegative) sb.Append('-');

                    foreach (var part in parts)
                    {
                        while (tempNumber >= part.Key)
                        {
                            sb.Append(part.Value);
                            tempNumber -= part.Key;
                        }
                    }

                    romeNumber = sb.ToString();
                }

                Assert.AreEqual(romeNumber, new RomanNumber(number).ToString(), $"{number} == {romeNumber}");
            }
        }

        private static Dictionary<String, int> parseTests = new()
        {
            { "I"            , 1  },
            { "II"           , 2  },
            { "III"          , 3  },
            { "IIII"         , 4  }, // Особливі твердження - з них ми визначаємо про 
            { "IV"           , 4  }, // підтримку неоптимальних записів чисел
            { "V"            , 5  },
            { "VI"           , 6  },
            { "VII"          , 7  },
            { "VIII"         , 8  },
            { "IX"           , 9  },
            { "X"            , 10 },
            { "VV"           , 10 },          // ще одне наголошення неоптимальності
            { "IIIIIIIIII"   , 10 },    // ще одне наголошення неоптимальності
            { "VX"           , 5 },             // ще одне наголошення неоптимальності
            { "N"            , 0 },
            { "-L"           , -50 },
            { "-XL"          ,        -40 },
            { "-D"          , -500 },
            { "-C"          , -100 },
            { "-M"          , -1000 },
            { "D"           , 500 },
            { "C"           , 100 },
            { "M"           , 1000 },
            { "IM"          , 999 },
            { "-IM"         , -999 },
            { "IC"          , 99 },
            { "-IC"         , -99 },
            { "ID"          , 499 },
            { "-ID"         , -499 },
            { "VM"          , 995 },
            { "-VM"         , -995 },
            { "VC"          , 95 },
            { "-VC"         , -95 },
            { "VD"          , 495 },
            { "-VD"         , -495 },
            { "XM"          , 990 },
            { "-XM"         , -990 },
            { "XC"          , 90 },
            { "-XC"         , -90 },
            { "XD"          , 490 },
            { "-XD"         , -490 },
            { "MI"          , 1001 },
            { "-MI"         , -1001 },
            { "CI"          , 101 },
            { "-CI"         , -101 },
            { "DI"          , 501 },
            { "-DI"         , -501 },
            { "MV"          , 1005 },
            { "-MV"         , -1005 },
            { "CV"          , 105 },
            { "-CV"         , -105 },
            { "DV"          , 505 },
            { "-DV"         , -505 },
            { "MX"          , 1010 },
            { "-MX"         , -1010 },
            { "CX"          , 110 },
            { "-CX"         , -110 },
            { "DX"          , 510 },
            { "-DX"         , -510 },/////////////  
            { "LX"           , 60 },
            { "LXII"         , 62 },
            { "CCL"          , 250 },
            { "-CCIII"       , -203 },
            { "-LIV"         ,  -54},
            { "MDII"         , 1502 },
            { "DD"           ,1000 },
            { "CCCCC"        ,500 },
            { "IVIVIVIVIV"   ,20 },
            { "IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII", 32 },
            { "MMMMMDCXXIIIIIII"         ,  5627   },
            { "MMMMDCCXXXXXXXII"         ,  4772   },
            { "-MMMCCCCCCCCXXXXIIIIII"   , -3846   },
            { "MMMMMMMMXXXXXXXIX"        ,  8079   },
            { "CXIX"                     ,   119   },
            { "MCCCLXXVI"                ,  1376   },
            { "MDCLXXIX"                 ,  1679   },
            { "MMCCCLXXXVI"              ,  2386   },
            { "MMDLXXXII"                ,  2582   },
            { "MMDLVI"                   ,  2556   },
            { "CDXL"                     ,   440   },
            { "DLI"                      ,   551   },
            { "MMDCXCIV"                 ,  2694   },
            { "MLXVIII"                  ,  1068   },
            { "MCDLXV"                   ,  1465   },
            { "-DCCCXLIV"                ,  -844   },
            { "MMCCLXXI"                 ,  2271   },
            { "MMCXXX"                   ,  2130   },
            { "-CDLXIII"                 ,  -463   },
            { "CDXII"                    ,   412   },
            { "DCCXCIV"                  ,   794   },
            { "CCXX"                     ,   220   },
            { "MDCCCLXXV"                ,  1875   },
            { "XXXVII"                   ,    37   },
            { "MCMXCVI"                  ,  1996   },
            { "-CCCLXXXI"                ,  -381   },
            { "CXLII"                    ,   142   },
            { "-DCCLXXXIII"              ,  -783   },
            { "DCXXXVII"                 ,   637   },
            { "CMLXXXVIII"               ,   988   },
            { "-CCXXII"                  ,  -222   },
            { "\nCDIV\t"                 ,   404   },
            { " CDIV"                    ,   404   },
            { "XLVI"       , 46 },
            { "LXXXVII"    , 87 },
            { "CCXXXVII"   , 237 },
            { "DCCCLV"     , 855 },
            { "MMDCCCLXVI" , 2866 },
            { "MDCCXXIII"  , 1723 },
            { "MMCCCXLV"   , 2345 }
        };
        [TestMethod]
        public void TestParseValid()
        {
            /*
             Assert.AreEqual(        // RomanNumber.Parse("I").Value == 1
               1,                  // Значення, що очікується (що має бути, правильний варіант)
                RomanNumber         // Актуальне значення (те, що вирахуване)
                    .Parse("I")
                    .Value,
                "1 == I"            // Повідомлення, що з'явиться при провалі тесту
                );
            */

            foreach (var pair in parseTests)
            {
                Assert.AreEqual(pair.Value, RomanNumber.Parse(pair.Key).Value, $"{pair.Value} == {pair.Key}");
            }
        }

        [TestMethod]
        public void TestParseNonValid()
        {
            // Тестування з неправильними формами чисел
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse(null!), "null -> Exception");
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse(""), "'' -> Exception");
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse(" "), "' ' -> Exception");
            // саме виключення що виникло у лямбді повертається як результат
            var ex = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("ABC"), "'ABC' -> Exception");
            // вимагаємо щоб відомості про неправильну цифру було включено у повідомлення виключення
            //Assert.AreEqual('B', ex.Message[ex.Message.Length - 1]);
            Assert.IsTrue(ex.Message.Contains("'B'"), "ex.Message.Contains 'B'");

            //KeyValuePair<string, char> pair = new("Xx", 'x');
            Dictionary<string, char> testCases = new()
            {
                {"Xx", 'x' },
                {"Xy", 'y' },
                {"AX", 'A' },
                {"FD", 'F' },
                {"MMMMMNP", 'P' },
                {"(LL", '(' },
                {"X C", ' ' },
                {"X\tC", '\t' },
                {"X\nC", '\n' }
            };

            foreach (var pair in testCases)
            {
                // позбуваємось змінної ex - робимо вкладені вирази
                Assert.IsTrue(
                    Assert.ThrowsException<ArgumentException>(
                        () => RomanNumber.Parse(pair.Key),
                        $"'{pair.Key}' -> Exception")
                            .Message.Contains($"'{pair.Value}'"), $"ex.Message should Contain '{pair.Value}'");
            }


            var ex2 = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("ABC"), $"'ABC' -> Exception");
            //Assert.IsTrue(ex2.Message.Contains($"'B'") || ex2.Message.Contains($"'C'"), $"ex.Message should Contain 'B' or 'C'");

            Assert.IsTrue(ex.Message.Contains('A') && ex.Message.Contains('B'),
                    "'ABC' ex.Message should contain either 'A' and 'B'");

            Assert.IsFalse(ex2.Message.Length < 15, "ex.Message.Length min 15");
        }

        [TestMethod]
        public void TestParseIllegal()
        {
            //Assert.AreNotEqual(8, RomanNumber.Parse("IIX"), "Wrong number");
            string[] illegals = { "IIV", "IIX", "VVX", "IVX", "IIX", "VIX", "DDDM", "IID", "LID", "IIL", "LLM", "IIIIIDDM", "I-", "IN", "NV" };
            foreach (string illegal in illegals)
            {
                Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.Parse(illegal),
                    $"'{illegal}' -> Exception");
            }
        }

        [TestMethod]
        public void TestAdd()
        {
            RomanNumber r1 = new(10);
            RomanNumber r2 = new(20);

            int[] numbers1 = new int[] { 10, 12, 34, 32, 42, 11, 1177, 1672, -45, -32 };
            int[] numbers2 = new int[] { 87, 543, 58, 90, 43, 1111, 390, 1248, -1, 674 };
            string[] right_answers = new string[] { "XCVII", "DLV", "XCII", "CXXII", "LXXXV", "MCXXII", "MDLXVII", "MMCMXX", "-XLVI", "DCXLII" };

            Assert.IsInstanceOfType(r1.Add(r2), typeof(RomanNumber));
            Assert.AreEqual("XXX", r1.Add(r2).ToString());
            Assert.AreEqual(30, r1.Add(r2).Value);
            Assert.AreEqual("XXX", r2.Add(r1).ToString());
            Assert.AreEqual(30, r2.Add(r1).Value);

            for (int i = 0; i < 10; i++)
            {
                r1 = new(numbers1[i]);
                r2 = new(numbers2[i]);
                Assert.AreEqual(right_answers[i], r1.Add(r2).ToString(), $"{r1.Value} + {r2.Value} should equals {right_answers[i]}");
            }

            for (int i = 0; i < 100; i++)
            {
                int random_1 = r.Next(1500);
                int random_2 = r.Next(1500);
                string answer = new RomanNumber(random_1 + random_2).ToString();
                r1 = new(random_1);
                r2 = new(random_2);
                Assert.AreEqual(answer, r1.Add(r2).ToString(), $"{r1.Value} + {r2.Value} should equals {answer}");
            }


            var ex = Assert.ThrowsException<ArgumentNullException>(() => r1.Add(null!), "r1.Add(null!) --> ArgumentNullException");
            Assert.IsTrue(ex.Message.Contains($"Cannot Add null object", StringComparison.OrdinalIgnoreCase), $"ex.Message({ex.Message}) contains 'Cannot Add null object'");

            // переконуємось в тому що r2.Add(r1) це новий об'єкт, а не змінений r2
            Assert.AreNotSame(r2, r2.Add(r1), "Add() should return new item");
        }

        [TestMethod]
        public void TestSum()
        {
            //var numbers1 = new[] { 34, -239, 2000, 1, -1, 530, -2387 };
            RomanNumber r1 = new(10);
            RomanNumber r2 = new(20);
            var r3 = RomanNumber.Sum(r1, r2);
            Assert.IsInstanceOfType(r3, typeof(RomanNumber));
            Assert.AreNotSame(r3, r1);
            Assert.AreNotSame(r3, r2);
            Assert.AreEqual(60, RomanNumber.Sum(r1, r2, r3).Value);

            Assert.AreEqual("-LXII", RomanNumber.Sum(new(34), new(-239), new(2000), new(1), new(-1), new(530), new(-2387)).ToString(), "msg");

            var ex = Assert.ThrowsException<ArgumentNullException>(() => RomanNumber.Sum(null!), "Sum(null!) ThrowsException: ArgumentNullException");
            string expectedFagment = "Invalid Sum() invocation with NULL argument";

            Assert.IsTrue(ex.Message.Contains(expectedFagment, StringComparison.InvariantCultureIgnoreCase), $"ex.Message({ex.Message}) should contains '{expectedFagment}'");

            var emptyArr = Array.Empty<RomanNumber>();
            Assert.AreEqual(0, RomanNumber.Sum(emptyArr).Value, "Sum(empty) == 0");
            Assert.AreEqual(0, RomanNumber.Sum().Value, "Sum(empty) == 0"); // порожній перелік аргументів

            Assert.AreEqual(10, RomanNumber.Sum(r1).Value, "Sum(r1) == 10");

            Assert.AreEqual(0, RomanNumber.Sum(new(10), new(-10)).Value, "Sum() == 10");
            Assert.AreEqual(-1, RomanNumber.Sum(new(10), new(-11)).Value, "Sum() == -1");
            Assert.AreEqual(1, RomanNumber.Sum(new(10), new(-9)).Value, "Sum() == 1");
            for (int i = 0; i < 200; i++)
            {
                int x = r.Next(-3000, 3000);
                int y = r.Next(-3000, 3000);
                Assert.AreEqual(x + y, RomanNumber.Sum(new(x), new(y)).Value, $"{x} + {y} = {x + y}");
            }

            for (int i = 0; i < 200; i++)
            {
                RomanNumber x = new(r.Next(-3000, 3000));
                RomanNumber y = new(r.Next(-3000, 3000));
                Assert.AreEqual(x.Add(y).Value, RomanNumber.Sum(x, y).Value, $"{x} + {y} Add == Sum");
            }

        }

        [TestMethod]
        public void TestEval()
        {
            // pos
            Assert.IsNotNull(RomanNumber.Eval("IV + XL"));
            Assert.IsInstanceOfType(RomanNumber.Eval("IV + XL"), typeof(RomanNumber));

            Dictionary<string, int> testCases = new()
            {
                { "IV + XL"             , 44   },
                { "XXIII + LIV"         , 77   },
                { "MCCXXXIV + CDXLI"    , 1675 },
                { "CCCXXXII + CCXVII"   , 549  },
                { "MD + MD"             , 3000 },
                { "XXXVIII + CCXXVI"    , 264  },
                { "N + I"               , 1    },
                { "XXXIII + LXIV"       , 97   },
                { "CDXCVIII + II"       , 500  },
                { "XL--II"              , 42   },
                { "-X - II  "           , -12  },
                { "-X + II  "           , -8   },
                { "-X - -II "           , -8   },
                { "-X + -II "           , -12  },
                { "-X+-II   "           , -12  },
                { "-X +-II  "           , -12  },
                { "  IV  +  XL  "       , 44   },
                { "IV+XL"               , 44   }
            };

            foreach (var pair in testCases)
                Assert.AreEqual(pair.Value, RomanNumber.Eval(pair.Key).Value, $"Test case {pair.Key}");

            for (int i = 0; i < 200; i++)
            {
                RomanNumber left = new(r.Next(3000));
                RomanNumber right = new(r.Next(3000));

                Assert.AreEqual(left.Value + right.Value, 
                    RomanNumber.Eval($"{left} + {right}").Value,
                    $"{left} + {right} == {left.Value + right.Value}");

                Assert.AreEqual(left.Add(right).Value,
                    RomanNumber.Eval($"{left} + {right}").Value,
                    $"{left} + {right} == {left.Value + right.Value}");

                Assert.AreEqual(RomanNumber.Sum(left, right).Value,
                    RomanNumber.Eval($"{left} + {right}").Value,
                    $"{left} + {right} == {left.Value + right.Value}");
            }

            Assert.AreEqual(new RomanNumber(4).Value, RomanNumber.Eval("IV").Value);

            // neg
            var ex = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Eval("IV = XL"); }, "IV = XL -> exception");
            Assert.IsTrue(ex.Message.Contains("'='", StringComparison.InvariantCultureIgnoreCase), "Test case: IV = XL");
            ex = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Eval("IK + XL"); }, "IK + XL -> exception");
            Assert.IsTrue(ex.Message.Contains("'K'"), "Test case: IK + XL");
            ex = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Eval("IV XL +"); }, "IV XL + -> exception");
            Assert.IsTrue(ex.Message.Contains("Invalid expression", StringComparison.InvariantCultureIgnoreCase), "Test case: IV XL +");
            ex = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Eval("="); }, "= -> exception");
            Assert.IsTrue(ex.Message.Contains("'='", StringComparison.InvariantCultureIgnoreCase), $"Test case: = {ex.Message}");
            ex = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Eval(null!); }, "null! -> exception");
            Assert.IsTrue(ex.Message.Contains("NULL or empty", StringComparison.InvariantCultureIgnoreCase), "Test case: null!");

            ex = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Eval("IIC + II"); }, "IIC -> exception");
            Assert.IsTrue(ex.Message.Contains("IIC"), $"ex.Message {ex.Message}");
            ex = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Eval("AX + II"); }, "AX -> exception");
            Assert.IsTrue(ex.Message.Contains("'A'"), $"ex.Message {ex.Message}");
            ex = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Eval("X + X + X"); }, "three arguments -> exception");
            Assert.IsTrue(ex.Message.Contains("Too many arguments"), "Test case: X + X + X");
            ex = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Eval("+X + X"); }, "invalid -> exception");
            Assert.IsTrue(ex.Message.Contains("Invalid expression"), "Test case: +X + X");

        }
    }
}
/* Тестування виключень (exceptions)
 * У системі тестування виключення - провали тесту.
 * Тому вирази, що мають завершуватись виключеннями, оточують функціональними виразами (лямбдами).
 * Це дозволяє перенести появу виключення у середину тестового методу, де воно буде оброблене належним чином.
 * Особливості:
 * - перевіряєтсья суворий збіг типів виключень, більш загальний тип не зараховується як проходження тесту
 * - тест повертає викинуте виключення, це дозволяє накласти умови на повідомлення, причину тощо.
 * 
 * Слухання:
 * які з комбінацій вважати правильними, а які ні
 * 'X C', ' XC', 'XC ', 'XC\t', '\nXC', 'XC\n', 'X\nC'
 *   x      v      v      v        v      v        x
 */

/* Завдання. Eval
 * 1. Додавання. Очікувана семантика:
 *      RomanNumber res = RomanNumber.Eval("IV + XL")
 * 2. Віднімання
 * ** Positive
 * - Складаємо тести
 *  = NotNull
 *  = Type
 *  = Algo (1. З відомими рез-тами, 2. Циклом з випадковими)
 *  = Cross (1. З Add, з Sum)
 * ** Negative - p неправильними виразами
 *  = вимога Exception
 *  = вимога до повідомлення
 * - Реалізація з проходження тестів
 * - Рефакторинг
 */