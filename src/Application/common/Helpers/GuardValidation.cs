using System;
using System.Collections;
using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace Application.Common.Helpers
{
    public static class GuardValidation
    {
        public static void AgainstNull<T>(T value, string message = null, string parameterName = null)
        {
            Guard.Against.Null(value, parameterName, message);
        }

        public static void AgainstNullOrEmpty(string value, string message = null, string parameterName = null)
        {
            Guard.Against.NullOrEmpty(value, parameterName, message);
        }

        public static void AgainstNullOrEmpty(Guid value, string message = null, string parameterName = null)
        {
            Guard.Against.NullOrEmpty(value, parameterName, message);
        }

        public static void AgainstNullOrEmpty<T>(IEnumerable<T> value, string message = null, string parameterName = null)
        {
            Guard.Against.NullOrEmpty(value, parameterName, message);
        }

        public static void AgainstNullOrWhiteSpace(string value, string message = null, string parameterName = null)
        {
            Guard.Against.NullOrWhiteSpace(value, parameterName, message);
        }

        public static void AgainstOutOfRange<T>(T value, T min, T max, string message = null)
            where T : IComparable<T>, IComparable
        {
            Guard.Against.OutOfRange(value, message, min, max);
        }

        public static void AgainstEnumOutOfRange<TEnum>(TEnum value, string message = null, string parameterName = null)
            where TEnum : struct, Enum
        {
            Guard.Against.EnumOutOfRange(value, parameterName, message);
        }

        public static void AgainstOutOfSQLDateRange(DateTime value, string message = null, string parameterName = null)
        {
            Guard.Against.OutOfSQLDateRange(value, parameterName, message);
        }

        public static void AgainstZero(int value, string message = null, string parameterName = null)
        {
            Guard.Against.Zero(value, parameterName, message);
        }

        public static void AgainstExpression<T>(
            Func<T, bool> func,
            T input,
            string message = null,
            string parameterName = null) where T : struct
        {
            Guard.Against.Expression(func, input, parameterName, message);
        }

        public static void AgainstInvalidFormat(
            string value,
            string parameterName,
            string regexPattern,
            string message = null)
        {
            Guard.Against.InvalidFormat(value, parameterName, regexPattern, message);
        }

        public static void AgainstNotFound<T>(T value, string message = null, string parameterName = null) where T : struct
        {
            Guard.Against.NotFound(value, parameterName, message);
        }
    }
}
