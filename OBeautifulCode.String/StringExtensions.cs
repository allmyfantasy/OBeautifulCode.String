﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.String source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.String.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Adds some convenient extension methods to strings.
    /// </summary>
#if !OBeautifulCodeStringRecipesProject
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.String", "See package version number")]
    internal
#else
    public
#endif
    static class StringExtensions
    {
        private static readonly Encoding AsciiEncoding = new ASCIIEncoding();

        private static readonly Encoding UnicodeEncoding = new UnicodeEncoding();

        private static readonly Encoding Utf8Encoding = new UTF8Encoding();

        private static readonly Regex CsvParsingRegex = new Regex("(?:,\"|^\")(\"\"|[\\w\\W]*?)(?=\",|\"$)|(?:,(?!\")|^(?!\"))([^,]*?)(?=$|,)|(\r\n|\n)", RegexOptions.Compiled);

        private static readonly Regex NotAlphaNumericRegex = new Regex("[^a-zA-Z0-9]", RegexOptions.Compiled);

        /// <summary>
        /// Appends one string to the another (base) if the base string
        /// doesn't already end with the string to append.
        /// </summary>
        /// <param name="value">The base string.</param>
        /// <param name="shouldEndWith">The string to append.</param>
        /// <remarks>
        /// If the string to append is the empty string, this method will always return the base string.
        /// </remarks>
        /// <returns>
        /// The inputted string where the last character is a backslash.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="shouldEndWith"/> is null.</exception>
        public static string AppendMissing(
            this string value,
            string shouldEndWith)
        {
            new { value }.Must().NotBeNull();
            new { shouldEndWith }.Must().NotBeNull();

            if (!value.EndsWith(shouldEndWith, StringComparison.CurrentCulture))
            {
                value = value + shouldEndWith;
            }

            return value;
        }

        /// <summary>
        /// Parses a CSV string and returns the values.
        /// </summary>
        /// <param name="value">The CSV to parse.</param>
        /// <param name="nullValueEncoding">Optional value indicating how nulls are encoded.  Defaulted to null, which results in a collection that never contains null.</param>
        /// <returns>
        /// Returns the values contained within a CSV.
        /// If <paramref name="value"/> is null, returns an empty collection.
        /// </returns>
        public static IReadOnlyCollection<string> FromCsv(
            this string value,
            string nullValueEncoding = null)
        {
            var result = new List<string>();

            // we return an empty collection because ToCsv returns null when the input is an empty collection.
            if (value == null)
            {
                return result;
            }

            // the regex doesn't solve for leading comma
            if (value.StartsWith(",", StringComparison.OrdinalIgnoreCase))
            {
                value = "," + value;
            }

            var matches = CsvParsingRegex.Matches(value);
            foreach (Match match in matches)
            {
                var parsedValue = match.Groups.Cast<Group>().Skip(1).Select(_ => _.Value).Aggregate((working, next) => working + next);
                parsedValue = parsedValue.Replace("\"\"", "\"");
                if (parsedValue == nullValueEncoding)
                {
                    parsedValue = null;
                }

                result.Add(parsedValue);
            }

            return result;
        }

        /// <summary>
        /// Determines if a string is alpha numeric.
        /// </summary>
        /// <param name="value">The string to evaluate.</param>
        /// <remarks>
        /// An empty string ("") is considered alpha-numeric.
        /// </remarks>
        /// <returns>
        /// Returns true if the string is alpha-numeric, false if not.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static bool IsAlphanumeric(
            this string value)
        {
            new { value }.Must().NotBeNull();

            var result = !NotAlphaNumericRegex.IsMatch(value);
            return result;
        }

        /// <summary>
        /// Performs a fast case-insensitive string replacement.
        /// </summary>
        /// <remarks>
        /// adapted from <a href="http://www.codeproject.com/KB/string/fastestcscaseinsstringrep.aspx"/>
        /// If newValue is null, all occurrences of oldValue are removed.
        /// </remarks>
        /// <param name="value">the string being searched.</param>
        /// <param name="oldValue">string to be replaced.</param>
        /// <param name="newValue">string to replace all occurrences of oldValue.</param>
        /// <returns>
        /// A string where the case-insensitive string replacement has been applied.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="oldValue"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="oldValue"/> is an empty string.</exception>
        public static string ReplaceCaseInsensitive(
            this string value,
            string oldValue,
            string newValue)
        {
            new { value }.Must().NotBeNull();
            new { oldValue }.Must().NotBeNull().And().NotBeEmptyString();

            if (newValue == null)
            {
                newValue = string.Empty;
            }

            var count = 0;
            var position0 = 0;
            int position1;
            var upperString = value.ToUpper(CultureInfo.CurrentCulture);
            var upperPattern = oldValue.ToUpper(CultureInfo.CurrentCulture);
            var inc = (value.Length / oldValue.Length) * (newValue.Length - oldValue.Length);
            var chars = new char[value.Length + Math.Max(0, inc)];
            while ((position1 = upperString.IndexOf(upperPattern, position0, StringComparison.CurrentCulture)) != -1)
            {
                for (var i = position0; i < position1; ++i)
                {
                    chars[count++] = value[i];
                }

                foreach (var t in newValue)
                {
                    chars[count++] = t;
                }

                position0 = position1 + oldValue.Length;
            }

            if (position0 == 0)
            {
                return value;
            }

            for (var i = position0; i < value.Length; ++i)
            {
                chars[count++] = value[i];
            }

            var result = new string(chars, 0, count);
            return result;
        }

        /// <summary>
        /// Splits a string into chunks of a specified length.
        /// </summary>
        /// <param name="value">The string to split.</param>
        /// <param name="lengthPerChunk">The length of each chunk when splitting the specified string.</param>
        /// <returns>
        /// <paramref name="value"/> split into an ordered list of chunks, where each chunk is of length <paramref name="lengthPerChunk"/>.
        /// If the length of <paramref name="value"/> cannot be evenly divided by <paramref name="lengthPerChunk"/>, then the last
        /// chunk will contain less characters.  No characters are truncated.  If <paramref name="value"/> is the empty string
        /// then an empty list is returned.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="lengthPerChunk"/> is &lt;= 0.</exception>
        public static IReadOnlyList<string> SplitIntoChunksOfLength(
            this string value,
            int lengthPerChunk)
        {
            new { value }.Must().NotBeNull();
            new { lengthPerChunk }.Must().BeGreaterThan(0);

            var result = new List<string>((value.Length / lengthPerChunk) + 1);

            for (int i = 0; i < value.Length; i += lengthPerChunk)
            {
                var chunk = value.Substring(i, Math.Min(lengthPerChunk, value.Length - i));
                result.Add(chunk);
            }

            return result;
        }

        /// <summary>
        /// Converts the specified string to an alpha-numeric string
        /// by removing all non-alpha-numeric characters.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <remarks>
        /// An empty string ("") is considered alpha-numeric.
        /// </remarks>
        /// <returns>
        /// The specified string with all non-alpha-numeric characters removed.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static string ToAlphanumeric(
            this string value)
        {
            new { value }.Must().NotBeNull();

            var result = NotAlphaNumericRegex.Replace(value, string.Empty);
            return result;
        }

        /// <summary>
        /// Encodes all characters in a given string to an array of bytes encoded in ASCII.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>byte array representing the string in ASCII.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static byte[] ToAsciiBytes(
            this string value)
        {
            var result = value.ToBytes(AsciiEncoding);
            return result;
        }

        /// <summary>
        /// Converts a string to a byte-array with a given encoding.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>byte array representing the string in a given encoding.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="encoding"/> is null.</exception>
        public static byte[] ToBytes(
            this string value,
            Encoding encoding)
        {
            new { value }.Must().NotBeNull();
            new { encoding }.Must().NotBeNull();

            var result = encoding.GetBytes(value);
            return result;
        }

        /// <summary>
        /// Makes a string safe to insert as a value into a
        /// comma separated values (CSV) object such as a file.
        /// </summary>
        /// <remarks>
        /// Here are the rules for making a string CSV safe:
        /// <a href="http://en.wikipedia.org/wiki/Comma-separated_values" />.
        /// </remarks>
        /// <param name="value">The string to make safe.</param>
        /// <returns>
        /// Returns a string that is safe to insert into a CSV object.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static string ToCsvSafe(
            this string value)
        {
            new { value }.Must().NotBeNull();

            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var containsCommas = value.Contains(",");
            var containsDoubleQuotes = value.Contains("\"");
            var containsLineBreak = value.Contains(Environment.NewLine);
            containsLineBreak = containsLineBreak || value.Contains("\n");
            var hasLeadingSpace = value.First() == ' ';
            var hasTrailingSpace = value.Last() == ' ';

            if (containsDoubleQuotes)
            {
                value = value.Replace("\"", "\"\"");
            }

            if (containsCommas || containsDoubleQuotes || containsLineBreak || hasLeadingSpace || hasTrailingSpace)
            {
                value = "\"" + value + "\"";
            }

            return value;
        }

        /// <summary>
        /// Performs both ToLower() and Trim() on a string.
        /// </summary>
        /// <param name="value">The string to operate on.</param>
        /// <returns>Lower-case, trimmed string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static string ToLowerTrimmed(
            this string value)
        {
            new { value }.Must().NotBeNull();

            var result = value.ToLower(CultureInfo.CurrentCulture).Trim();
            return result;
        }

        /// <summary>
        /// Encodes all characters in a given string to an array of bytes encoded in unicode.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>byte array representing the string in unicode.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static byte[] ToUnicodeBytes(
            this string value)
        {
            var result = value.ToBytes(UnicodeEncoding);
            return result;
        }

        /// <summary>
        /// Encodes all characters in a given string to an array of bytes encoded in UTF-8.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>byte array representing the string in UTF-8.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static byte[] ToUtf8Bytes(
            this string value)
        {
            var result = value.ToBytes(Utf8Encoding);
            return result;
        }
    }
}
