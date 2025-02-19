﻿namespace Humanizer
{
    class BulgarianNumberToWordsConverter : GenderedNumberToWordsConverter
    {
        static readonly string[] UnitsMap =
        [
            "нула", "едно", "две", "три", "четири", "пет", "шест", "седем", "осем", "девет", "десет", "единадесет",
            "дванадесет", "тринадесет", "четиринадесет", "петнадесет", "шестнадесет", "седемнадесет", "осемнадесет",
            "деветнадесет"
        ];

        static readonly string[] TensMap =
        [
            "нула", "десет", "двадесет", "тридесет", "четиридесет", "петдесет", "шестдесет", "седемдесет",
            "осемдесет", "деветдесет"
        ];

        static readonly string[] HundredsMap =
        [
            "нула", "сто", "двеста", "триста", "четиристотин", "петстотин", "шестстотин", "седемстотин",
            "осемстотин", "деветстотин"
        ];

        static readonly string[] HundredsOrdinalMap =
        [
            string.Empty, "стот", "двест", "трист", "четиристот", "петстот", "шестстот", "седемстот", "осемстот",
            "деветстот"
        ];

        static readonly string[] UnitsOrdinal =
        [
            string.Empty, "първ", "втор", "трет", "четвърт", "пет", "шест", "седм", "осм", "девeт", "десeт",
            "единадесет", "дванадесет", "тринадесет", "четиринадесет", "петнадесет", "шестнадесет", "седемнадесет",
            "осемнадесет", "деветнадесет"
        ];

        public override string Convert(long input, GrammaticalGender gender, bool addAnd = true) =>
            Convert(input, gender, false);

        static string Convert(long input, GrammaticalGender gender, bool isOrdinal, bool addAnd = true)
        {
            if (input is > int.MaxValue or < int.MinValue)
            {
                throw new NotImplementedException();
            }

            if (input == 0)
            {
                return isOrdinal ? "нулев" + GetEndingForGender(gender, input) : "нула";
            }

            var parts = new List<string>();

            if (input < 0)
            {
                parts.Add("минус");
                input = -input;
            }

            var lastOrdinalSubstitution = "";

            if (input / 1000000000 > 0)
            {
                parts.Add(input < 2000000000 ? "един милиард" : Convert(input / 1000000000, gender, false) + " милиарда");

                if (isOrdinal)
                    lastOrdinalSubstitution = Convert(input / 1000000000, gender, false) + " милиард" +
                                              GetEndingForGender(gender, input);
                input %= 1000000000;
            }

            if (input / 1000000 > 0)
            {
                parts.Add(input < 2000000 ? "един милион" : Convert(input / 1000000, gender, false) + " милиона");

                if (isOrdinal)
                    lastOrdinalSubstitution = Convert(input / 1000000, gender, false) + " милион" +
                                              GetEndingForGender(gender, input);

                input %= 1000000;
            }

            if (input / 1000 > 0)
            {
                if (input < 2000)
                    parts.Add("хиляда");
                else
                {
                    parts.Add(Convert(input / 1000, gender, false) + " хиляди");
                }

                if (isOrdinal)
                    lastOrdinalSubstitution = Convert(input / 1000, gender, false) + " хиляд" +
                                              GetEndingForGender(gender, input);

                input %= 1000;
            }

            if (input / 100 > 0)
            {
                parts.Add(HundredsMap[(int)input / 100]);

                if (isOrdinal)
                    lastOrdinalSubstitution = HundredsOrdinalMap[(int)input / 100] + GetEndingForGender(gender, input);

                input %= 100;
            }

            if (input > 19)
            {
                parts.Add(TensMap[input / 10]);

                if (isOrdinal)
                    lastOrdinalSubstitution = TensMap[(int)input / 10] + GetEndingForGender(gender, input);

                input %= 10;
            }

            if (input > 0)
            {
                parts.Add(UnitsMap[input]);

                if (isOrdinal)
                    lastOrdinalSubstitution = UnitsOrdinal[input] + GetEndingForGender(gender, input);
            }

            if (parts.Count > 1)
            {
                parts.Insert(parts.Count - 1, "и");
            }

            if (isOrdinal && !string.IsNullOrWhiteSpace(lastOrdinalSubstitution))
                parts[parts.Count - 1] = lastOrdinalSubstitution;

            return string.Join(" ", parts);
        }

        public override string ConvertToOrdinal(int input, GrammaticalGender gender) =>
            Convert(input, gender, true);

        static string GetEndingForGender(GrammaticalGender gender, long input)
        {
            if (input == 0)
            {
                return gender switch
                {
                    GrammaticalGender.Masculine => "",
                    GrammaticalGender.Feminine => "а",
                    GrammaticalGender.Neuter => "о",
                    _ => throw new ArgumentOutOfRangeException(nameof(gender))
                };
            }

            if (input < 99)
            {
                return gender switch
                {
                    GrammaticalGender.Masculine => "и",
                    GrammaticalGender.Feminine => "а",
                    GrammaticalGender.Neuter => "о",
                    _ => throw new ArgumentOutOfRangeException(nameof(gender))
                };
            }
            else
            {
                return gender switch
                {
                    GrammaticalGender.Masculine => "ен",
                    GrammaticalGender.Feminine => "на",
                    GrammaticalGender.Neuter => "но",
                    _ => throw new ArgumentOutOfRangeException(nameof(gender))
                };
            }
        }
    }
}
