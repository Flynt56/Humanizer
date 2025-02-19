﻿namespace Humanizer
{
    class UkrainianFormatter() : DefaultFormatter("uk")
    {
        protected override string GetResourceKey(string resourceKey, int number)
        {
            var grammaticalNumber = RussianGrammaticalNumberDetector.Detect(number);
            var suffix = GetSuffix(grammaticalNumber);
            return resourceKey + suffix;
        }

        static string GetSuffix(RussianGrammaticalNumber grammaticalNumber)
        {
            if (grammaticalNumber == RussianGrammaticalNumber.Singular)
            {
                return "_Singular";
            }

            if (grammaticalNumber == RussianGrammaticalNumber.Paucal)
            {
                return "_Paucal";
            }

            return "";
        }
    }
}
