﻿namespace Humanizer
{
    abstract class GenderedNumberToWordsConverter : INumberToWordsConverter
    {
        readonly GrammaticalGender _defaultGender;

        protected GenderedNumberToWordsConverter(GrammaticalGender defaultGender = GrammaticalGender.Masculine) =>
            _defaultGender = defaultGender;

        /// <summary>
        /// Converts the number to string using the locale's default grammatical gender
        /// </summary>
        public string Convert(long number) =>
            Convert(number, _defaultGender);

        /// <summary>
        /// Converts the number to specific string form using the locale's default grammatical gender
        /// </summary>
        public string Convert(long number, WordForm wordForm) =>
            Convert(number, wordForm, _defaultGender);

        /// <summary>
        /// Converts the number to string using the locale's default gramatical gender and adds "and"
        /// </summary>
        /// <param name="addAnd">Whether "and" should be included.</param>
        public string Convert(long number, bool addAnd) =>
            Convert(number, _defaultGender);

        /// <summary>
        /// Converts the number to a specific string form using the locale's default grammatical gender and adds "and"
        /// </summary>
        public string Convert(long number, bool addAnd, WordForm wordForm) =>
            Convert(number, wordForm, _defaultGender, addAnd);

        /// <summary>
        /// Converts the number to string using the provided grammatical gender
        /// </summary>
        public abstract string Convert(long number, GrammaticalGender gender, bool addAnd = true);

        /// <summary>
        /// Converts the number to a specific string form using the provided grammatical gender
        /// </summary>
        public virtual string Convert(long number, WordForm wordForm, GrammaticalGender gender, bool addAnd = true) =>
            Convert(number, gender, addAnd);

        /// <summary>
        /// Converts the number to ordinal string using the locale's default grammatical gender
        /// </summary>
        public string ConvertToOrdinal(int number) =>
            ConvertToOrdinal(number, _defaultGender);

        /// <summary>
        /// Converts the number to ordinal string using the provided grammatical gender
        /// </summary>
        public abstract string ConvertToOrdinal(int number, GrammaticalGender gender);

        /// <summary>
        /// Converts the number to specific ordinal string form
        /// </summary>
        public string ConvertToOrdinal(int number, WordForm wordForm) =>
            ConvertToOrdinal(number, _defaultGender, wordForm);

        /// <summary>
        /// Converts the number to specific ordinal string form using the provided grammatical gender
        /// </summary>
        public virtual string ConvertToOrdinal(int number, GrammaticalGender gender, WordForm wordForm) =>
            ConvertToOrdinal(number, gender);

        /// <summary>
        /// Converts integer to named tuple (e.g. 'single', 'double' etc.).
        /// </summary>
        public virtual string ConvertToTuple(int number) =>
            Convert(number);
    }
}
