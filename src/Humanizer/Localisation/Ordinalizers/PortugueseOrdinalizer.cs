﻿namespace Humanizer
{
    class PortugueseOrdinalizer : DefaultOrdinalizer
    {
        public override string Convert(int number, string numberString) =>
            Convert(number, numberString, GrammaticalGender.Masculine);

        public override string Convert(int number, string numberString, GrammaticalGender gender)
        {
            // N/A in Portuguese
            if (number == 0)
            {
                return "0";
            }

            if (gender == GrammaticalGender.Feminine)
            {
                return numberString + "ª";
            }

            return numberString + "º";
        }
    }
}
