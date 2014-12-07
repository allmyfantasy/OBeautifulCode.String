﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BalanceTest.cs" company="OBeautifulCode">
//   Copyright 2014 OBeautifulCode
// </copyright>
// <summary>
//   Tests the <see cref="Balance"/> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Libs.String.Test
{
    using System;
    using System.Collections.Generic;

    using Xunit;
    using Xunit.Extensions;

    /// <summary>
    /// Tests the <see cref="Balance"/> class.
    /// </summary>
    public class BalanceTest
    {
        #region Fields (Private)

        #endregion

        #region Constructors

        #endregion

        #region Properties

        #endregion

        #region Public Methods
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_SingleCharacterOpenAndCloseMarkersAndSourceIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            int unbalancedPosition;
            const char Open = '(';
            const char Close = ')';

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => Balance.IsBalanced(null, Open, Close));
            Assert.Throws<ArgumentNullException>(() => Balance.IsBalanced(null, Open, Close, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_SingleCharacterOpenAndCloseMarkersAndSourceIsWhitespace_ThrowsArgumentException()
        {
            // Arrange
            int unbalancedPosition;
            const char Open = '(';
            const char Close = ')';
            string source1 = string.Empty;
            const string Source2 = "  ";
            const string Source3 = "  \r\n  ";

            // Act, Assert
            Assert.Throws<ArgumentException>(() => source1.IsBalanced(Open, Close));
            Assert.Throws<ArgumentException>(() => source1.IsBalanced(Open, Close, out unbalancedPosition));

            Assert.Throws<ArgumentException>(() => Source2.IsBalanced(Open, Close));
            Assert.Throws<ArgumentException>(() => Source2.IsBalanced(Open, Close, out unbalancedPosition));

            Assert.Throws<ArgumentException>(() => Source3.IsBalanced(Open, Close));
            Assert.Throws<ArgumentException>(() => Source3.IsBalanced(Open, Close, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_SingleCharacterOpenAndCloseMarkersAndOpenEqualsClose_ThrowsArgumentException()
        {
            // Arrange
            int unbalancedPosition;
            const string Source = "this is my source";
            const char Open = '(';
            const char Close = '(';
            
            // Act, Assert
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open, Close));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open, Close, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        /// <param name="toValidate">The string to validate.</param>
        /// <param name="opening">The opening marker(s).</param>
        /// <param name="closing">The closing marker(s).</param>
        /// <param name="expectedUnbalancedPosition">The expected unbalanced position.</param>
        [Theory]
        [InlineData("to) validate", '(', ')', 2)]
        [InlineData("to)(validate", '(', ')', 2)]
        [InlineData("tovalidate}", '{', '}', 10)]
        [InlineData("{}to{vali{dat}e", '{', '}', 4)]
        [InlineData("t(o va)lidat)e", '(', ')', 12)]
        public static void IsBalanced_SingleCharacterOpenAndCloseMarkersAndSourceIsNotBalanced_ReturnsFalseAndUnbalancedPosition(string toValidate, char opening, char closing, int expectedUnbalancedPosition)
        {
            // Arrange
            int unbalancedPosition;

            // Act, Assert
            Assert.False(toValidate.IsBalanced(opening, closing));
            Assert.False(toValidate.IsBalanced(opening, closing, out unbalancedPosition));
            Assert.Equal(expectedUnbalancedPosition, unbalancedPosition);
        }

        /// <summary>
        /// Test method.
        /// </summary>
        /// <param name="toValidate">The string to validate.</param>
        /// <param name="opening">The opening marker(s).</param>
        /// <param name="closing">The closing marker(s).</param>
        [Theory]
        [InlineData("to validate", '(', ')')]
        [InlineData("to()validate", '(', ')')]
        [InlineData("{tovalidate}", '{', '}')]
        [InlineData("tovalidate{}", '{', '}')]
        [InlineData("{}to{validat}e{}", '{', '}')]
        [InlineData("<a <ve<>ry> comp<lic<ate>d st>ri>ng", '<', '>')]
        [InlineData("t{o vali>date", '{', '>')]
        public static void IsBalanced_SingleCharacterOpenAndCloseMarkersAndSourceIsBalanced_ReturnsTrueAndNegativeOneForUnbalancedPosition(string toValidate, char opening, char closing)
        {
            // Arrange
            int unbalancedPosition;

            // Act, Assert
            Assert.True(toValidate.IsBalanced(opening, closing));
            Assert.True(toValidate.IsBalanced(opening, closing, out unbalancedPosition));
            Assert.Equal(-1, unbalancedPosition);
        }
        
        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_OpenAndCloseMarkersAreStringsAndSourceIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            int unbalancedPosition;
            const string Open = "(";
            const string Close = ")";

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => Balance.IsBalanced(null, Open, Close));
            Assert.Throws<ArgumentNullException>(() => Balance.IsBalanced(null, Open, Close, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_OpenAndCloseMarkersAreStringsAndSourceIsWhitespace_ThrowsArgumentException()
        {
            // Arrange
            int unbalancedPosition;
            const string Open = "(";
            const string Close = ")";
            string source1 = string.Empty;
            const string Source2 = "  ";
            const string Source3 = "  \r\n  ";

            // Act, Assert
            Assert.Throws<ArgumentException>(() => source1.IsBalanced(Open, Close));
            Assert.Throws<ArgumentException>(() => source1.IsBalanced(Open, Close, out unbalancedPosition));

            Assert.Throws<ArgumentException>(() => Source2.IsBalanced(Open, Close));
            Assert.Throws<ArgumentException>(() => Source2.IsBalanced(Open, Close, out unbalancedPosition));

            Assert.Throws<ArgumentException>(() => Source3.IsBalanced(Open, Close));
            Assert.Throws<ArgumentException>(() => Source3.IsBalanced(Open, Close, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_OpenAndCloseMarkersAreStringsAndOpenIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            int unbalancedPosition;
            const string Source = "this is the source";
            const string Close = ")";

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => Source.IsBalanced(null, Close));
            Assert.Throws<ArgumentNullException>(() => Source.IsBalanced(null, Close, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_OpenAndCloseMarkersAreStringsAndOpenIsWhitespace_ThrowsArgumentException()
        {
            // Arrange
            int unbalancedPosition;
            const string Source = "this is the source";
            const string Close = ")";
            string open1 = string.Empty;
            const string Open2 = "  ";
            const string Open3 = "  \r\n  ";

            // Act, Assert
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open1, Close));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open1, Close, out unbalancedPosition));

            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open2, Close));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open2, Close, out unbalancedPosition));

            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open3, Close));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open3, Close, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_OpenAndCloseMarkersAreStringsAndCloseIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            int unbalancedPosition;
            const string Source = "this is the source";
            const string Open = "(";

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => Source.IsBalanced(Open, null));
            Assert.Throws<ArgumentNullException>(() => Source.IsBalanced(Open, null, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_OpenAndCloseMarkersAreStringsAndCloseIsWhitespace_ThrowsArgumentException()
        {
            // Arrange
            int unbalancedPosition;
            const string Source = "this is the source";
            const string Open = "(";
            string close1 = string.Empty;
            const string Close2 = "  ";
            const string Close3 = "  \r\n  ";

            // Act, Assert
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open, close1));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open, close1, out unbalancedPosition));

            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open, Close2));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open, Close2, out unbalancedPosition));

            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open, Close3));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open, Close3, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_OpenAndCloseMarkersAreStringsAndOpenEqualsClose_ThrowsArgumentException()
        {
            // Arrange
            int unbalancedPosition;
            const string Source = "this is my source";
            const string Open = "(";
            const string Close = "(";

            // Act, Assert
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open, Close));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(Open, Close, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        /// <param name="toValidate">The string to validate.</param>
        /// <param name="opening">The opening marker(s).</param>
        /// <param name="closing">The closing marker(s).</param>
        /// <param name="expectedUnbalancedPosition">The expected unbalanced position.</param>
        [Theory]
        [InlineData("</b>to validate<b>", "<b>", "</b>", 0)]
        [InlineData("t[{o val[{id}]ate", "[{", "}]", 1)]
        public static void IsBalanced_OpenAndCloseMarkersAreStringsAndSourceIsNotBalanced_ReturnsFalseAndUnbalancedPosition(string toValidate, string opening, string closing, int expectedUnbalancedPosition)
        {
            // Arrange
            int unbalancedPosition;

            // Act, Assert
            Assert.False(toValidate.IsBalanced(opening, closing));
            Assert.False(toValidate.IsBalanced(opening, closing, out unbalancedPosition));
            Assert.Equal(expectedUnbalancedPosition, unbalancedPosition);
        }

        /// <summary>
        /// Test method.
        /// </summary>
        /// <param name="toValidate">The string to validate.</param>
        /// <param name="opening">The opening marker(s).</param>
        /// <param name="closing">The closing marker(s).</param>
        [Theory]
        [InlineData("to validate", "<b>", "</b>")]
        [InlineData("<b>to validate</b>", "<b>", "</b>")]
        [InlineData("}t]o validat[e{", "[{", "}]")]
        [InlineData("t[{o va}]lidate", "[{", "}]")]
        [InlineData("t[{o va[{l}]idat}]e", "[{", "}]")]
        public static void IsBalanced_OpenAndCloseMarkersAreStringsAndSourceIsBalanced_ReturnsTrueAndNegativeOneForUnbalancedPosition(string toValidate, string opening, string closing)
        {
            // Arrange
            int unbalancedPosition;

            // Act, Assert
            Assert.True(toValidate.IsBalanced(opening, closing));
            Assert.True(toValidate.IsBalanced(opening, closing, out unbalancedPosition));
            Assert.Equal(-1, unbalancedPosition);
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_MultipleOpenAndCloseMarkersAndSourceIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            int unbalancedPosition;
            var open = new[] { '(', '[' };
            var close = new[] { ')', ']' };

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => Balance.IsBalanced(null, open, close));
            Assert.Throws<ArgumentNullException>(() => Balance.IsBalanced(null, open, close, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_MultipleOpenAndCloseMarkersAndSourceIsWhitespace_ThrowsArgumentException()
        {
            // Arrange
            int unbalancedPosition;
            var open = new[] { '(', '[' };
            var close = new[] { ')', ']' };
            string source1 = string.Empty;
            const string Source2 = "  ";
            const string Source3 = "  \r\n  ";

            // Act, Assert
            Assert.Throws<ArgumentException>(() => source1.IsBalanced(open, close));
            Assert.Throws<ArgumentException>(() => source1.IsBalanced(open, close, out unbalancedPosition));

            Assert.Throws<ArgumentException>(() => Source2.IsBalanced(open, close));
            Assert.Throws<ArgumentException>(() => Source2.IsBalanced(open, close, out unbalancedPosition));

            Assert.Throws<ArgumentException>(() => Source3.IsBalanced(open, close));
            Assert.Throws<ArgumentException>(() => Source3.IsBalanced(open, close, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_MultipleOpenAndCloseMarkersAndOpenIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            int unbalancedPosition;
            const string Source = "this is the source";
            var close = new[] { ')', ']' };

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => Source.IsBalanced(null, close));
            Assert.Throws<ArgumentNullException>(() => Source.IsBalanced(null, close, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_MultipleOpenAndCloseMarkersAndOpenIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            int unbalancedPosition;
            const string Source = "this is the source";
            var close = new[] { ')', ']' };
            char[] open = { };

            // Act, Assert
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open, close));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open, close, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_MultipleOpenAndCloseMarkersAndCloseIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            int unbalancedPosition;
            const string Source = "this is the source";
            var open = new[] { '(', '[' };

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => Source.IsBalanced(open, null));
            Assert.Throws<ArgumentNullException>(() => Source.IsBalanced(open, null, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_MultipleOpenAndCloseMarkersAndCloseIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            int unbalancedPosition;
            const string Source = "this is the source";
            var open = new[] { '(', '[' };
            char[] close = { };

            // Act, Assert
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open, close));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open, close, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_MultipleOpenAndCloseMarkersAndCloseAndOpenContainDifferentNumberOfMarkers_ThrowsArgumentException()
        {
            // Arrange
            int unbalancedPosition;
            const string Source = "this is the source";

            var open1 = new[] { '(', '[' };
            var close1 = new[] { ')' };

            var open2 = new[] { '(' };
            var close2 = new[] { ')', ']', '/' };

            // Act, Assert
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open1, close1));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open1, close1, out unbalancedPosition));

            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open2, close2));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open2, close2, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        [Fact]
        public static void IsBalanced_MultipleOpenAndCloseMarkersAndAtLeastOneOfTheOpenAndCorrespondingCloseMarkersAreEqual_ThrowsArgumentException()
        {
            // Arrange
            int unbalancedPosition;
            const string Source = "this is the source";

            var open1 = new[] { '(', '[' };
            var close1 = new[] { '(', ']' };

            var open2 = new[] { '(', ']' };
            var close2 = new[] { ')', ']' };

            var open3 = new[] { ')', '[' };
            var close3 = new[] { ')', '[' };

            // Act, Assert
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open1, close1));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open1, close1, out unbalancedPosition));

            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open2, close2));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open2, close2, out unbalancedPosition));

            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open3, close3));
            Assert.Throws<ArgumentException>(() => Source.IsBalanced(open3, close3, out unbalancedPosition));
        }

        /// <summary>
        /// Test method.
        /// </summary>
        /// <param name="toValidate">The string to validate.</param>
        /// <param name="opening">The opening marker(s).</param>
        /// <param name="closing">The closing marker(s).</param>
        /// <param name="expectedUnbalancedPosition">The expected unbalanced position.</param>
        [Theory]
        [InlineData("to v[a(li]da)te", new[] { '(', '[' }, new[] { ')', ']' }, 9)]
        [InlineData("t<o v>a{l()i}date{}[", new[] { '(', '[', '<' }, new[] { ')', ']', '>' }, 19)]
        [InlineData("to v{al)(ida}te", new[] { '(' }, new[] { ')' }, 7)]
        public static void IsBalanced_MultipleOpenAndCloseMarkersAndSourceIsNotBalanced_ReturnsFalseAndUnbalancedPosition(string toValidate, ICollection<char> opening, ICollection<char> closing, int expectedUnbalancedPosition)
        {
            // Arrange
            int unbalancedPosition;

            // Act, Assert
            Assert.False(toValidate.IsBalanced(opening, closing));
            Assert.False(toValidate.IsBalanced(opening, closing, out unbalancedPosition));
            Assert.Equal(expectedUnbalancedPosition, unbalancedPosition);
        }

        /// <summary>
        /// Test method.
        /// </summary>
        /// <param name="toValidate">The string to validate.</param>
        /// <param name="opening">The opening marker(s).</param>
        /// <param name="closing">The closing marker(s).</param>
        [Theory]
        [InlineData("to validate", new[] { '(' }, new[] { ')' })]
        [InlineData("to (v)alidate", new[] { '(', '[' }, new[] { ')', ']' })]
        [InlineData("to [valid]ate", new[] { '(', '[' }, new[] { ')', ']' })]
        [InlineData("to [va{li}d]ate[]", new[] { '(', '[' }, new[] { ')', ']' })]
        [InlineData("to {[][]}validate", new[] { '(', '[' }, new[] { ')', ']' })]
        [InlineData("[{<><><t{o }v{a(li(d)ate)}>}]", new[] { '(', '[', '<' }, new[] { ')', ']', '>' })]
        public static void IsBalanced_MultipleOpenAndCloseMarkersAndSourceIsBalanced_ReturnsTrueAndNegativeOneForUnbalancedPosition(string toValidate, ICollection<char> opening, ICollection<char> closing)
        {
            // Arrange
            int unbalancedPosition;

            // Act, Assert
            Assert.True(toValidate.IsBalanced(opening, closing));
            Assert.True(toValidate.IsBalanced(opening, closing, out unbalancedPosition));
            Assert.Equal(-1, unbalancedPosition);
        }

        // ReSharper restore InconsistentNaming
        #endregion

        #region Internal Methods

        #endregion

        #region Protected Methods

        #endregion

        #region Private Methods

        #endregion
    }
}
