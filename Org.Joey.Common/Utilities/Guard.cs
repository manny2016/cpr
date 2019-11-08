using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common
{
    public static class Guard
    {
        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if the given argument is null.
        /// </summary>
        /// <exception cref="ArgumentNullException">The value is null.</exception>
        /// <param name="argumentValue">The argument value to test.</param>
        /// <param name="argumentName">The name of the argument to test.</param>
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null) throw new ArgumentNullException(argumentName);
        }

        /// <summary>
        /// Throws an exception if the tested string argument is null or an empty string.
        /// </summary>
        /// <exception cref="ArgumentNullException">The string value is null.</exception>
        /// <exception cref="ArgumentException">The string is empty.</exception>
        /// <param name="argumentValue">The argument value to test.</param>
        /// <param name="argumentName">The name of the argument to test.</param>
        public static void ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (argumentValue == null) throw new ArgumentNullException(argumentName);
            if (argumentValue.Length == 0) throw new ArgumentException("Value can't be empty", argumentName);
        }
        public static void ArgumentNotNullOrEmpty(string[] argumentValues, string[] argumentNames = null)
        {
            ArgumentNotNull(argumentValues, "argumentValues");
            for (var i = 0; i < argumentValues.Length; i++)
            {
                if (argumentNames == null || argumentNames.Length != argumentValues.Length)
                {
                    ArgumentNotNull(argumentValues[i], $"argumentValues[{i}]");
                }
                else
                {
                    ArgumentNotNull(argumentValues[i], argumentNames[i]);
                }

            }
        }


    }
}
