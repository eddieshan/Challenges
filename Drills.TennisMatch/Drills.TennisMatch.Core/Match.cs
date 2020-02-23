using System;

namespace Hexagon.TennisMatch.Core
{
    public class InvalidMatch: Exception
    {
        public InvalidMatch(string message): base(message)
        {

        }
    }
    /// <summary>
    /// A simpler approach to SumTypes just with Type Matching.
    /// This implementation attempts to be free of the issues that usually make Type Matching an anti-pattern.
    /// Type Matching in this case is exhaustive, all the Type cases have to be declared as generic constraints.
    /// Any input that cannot be matched will throw an exception, this is exhaustive matching during run time.
    /// Exhaustive matching should be done in compile time eg. as in F#, but C# does not support this.
    /// Incidentally and without meaning to, usage of this Match will work as a Visitor patterns.
    /// One benefit of Functional Programming is that OOP Design Patterns tend to emerge naturally and effortlesstly.
    /// </summary>
    public static class MatchHelpers
    {
        public static TOut Match<TIn, TOut, T1, T2>(
            this TIn input,
            Func<T1, TOut> map1,
            Func<T2, TOut> map2
        )
            where TIn : class
            where T1 : class
            where T2 : class
        {
            var inputType = input.GetType();
            if (inputType.Equals(typeof(T1)))
            {
                return map1(input as T1);
            }
            else if (inputType.Equals(typeof(T2)))
            {
                return map2(input as T2);
            }
            else
            {
                throw new InvalidMatch($"Input value of type {inputType.ToString()} cannot be matched");
            }
        }

        public static void Match<TIn, T1, T2>(
            this TIn input,
            Action<T1> map1,
            Action<T2> map2
        )
            where TIn : class
            where T1 : class
            where T2 : class
        {
            var inputType = input.GetType();
            if (inputType.Equals(typeof(T1)))
            {
                map1(input as T1);
            }
            else if (inputType.Equals(typeof(T2)))
            {
                map2(input as T2);
            }
            else
            {
                throw new InvalidMatch($"Input value of type {inputType.ToString()} cannot be matched");
            }
        }

        public static TOut Match<TIn, TOut, T1, T2, T3>(
            this TIn input, 
            Func<T1, TOut> map1,
            Func<T2, TOut> map2,
            Func<T3, TOut> map3
        )
            where TIn: class
            where T1: class
            where T2 : class
            where T3 : class
        {
            var inputType = input.GetType();
            if (inputType.Equals(typeof(T1)))
            {
                return map1(input as T1);
            }
            else if (inputType.Equals(typeof(T2)))
            {
                return map2(input as T2);
            }
            else if (inputType.Equals(typeof(T3)))
            {
                return map3(input as T3);
            }
            else
            {
                throw new InvalidMatch($"Input value of type {inputType.ToString()} cannot be matched");
            }
        }

        public static TOut Match<TOut, T1, T2, T3, T4>(
            this object input,
            Func<T1, TOut> map1,
            Func<T2, TOut> map2,
            Func<T3, TOut> map3,
            Func<T4, TOut> map4
        )
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
        {
            var inputType = input.GetType();
            if (inputType.Equals(typeof(T1)))
            {
                return map1(input as T1);
            }
            else if (inputType.Equals(typeof(T2)))
            {
                return map2(input as T2);
            }
            else if (inputType.Equals(typeof(T3)))
            {
                return map3(input as T3);
            }
            else if (inputType.Equals(typeof(T4)))
            {
                return map4(input as T4);
            }
            else
            {
                throw new InvalidMatch($"Input value of type {inputType.ToString()} cannot be matched");
            }
        }

        public static TOut Match<TOut, T1, T2, T3, T4, T5>(
            this object input,
            Func<T1, TOut> map1,
            Func<T2, TOut> map2,
            Func<T3, TOut> map3,
            Func<T4, TOut> map4,
            Func<T5, TOut> map5
        )
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
        {
            var inputType = input.GetType();
            if (inputType.Equals(typeof(T1)))
            {
                return map1(input as T1);
            }
            else if (inputType.Equals(typeof(T2)))
            {
                return map2(input as T2);
            }
            else if (inputType.Equals(typeof(T3)))
            {
                return map3(input as T3);
            }
            else if (inputType.Equals(typeof(T4)))
            {
                return map4(input as T4);
            }
            else if (inputType.Equals(typeof(T5)))
            {
                return map5(input as T5);
            }
            else
            {
                throw new InvalidMatch($"Input value of type {inputType.ToString()} cannot be matched");
            }
        }

        public static void Match<TIn, T1, T2, T3, T4, T5>(
            this TIn input,
            Action<T1> map1,
            Action<T2> map2,
            Action<T3> map3,
            Action<T4> map4,
            Action<T5> map5
        )
            where TIn : class
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
        {
            var inputType = input.GetType();
            if (inputType.Equals(typeof(T1)))
            {
                map1(input as T1);
            }
            else if (inputType.Equals(typeof(T2)))
            {
                map2(input as T2);
            }
            else if (inputType.Equals(typeof(T3)))
            {
                map3(input as T3);
            }
            else if (inputType.Equals(typeof(T4)))
            {
                map4(input as T4);
            }
            else if (inputType.Equals(typeof(T5)))
            {
                map5(input as T5);
            }
            else
            {
                throw new InvalidMatch($"Input value of type {inputType.ToString()} cannot be matched");
            }
        }
    }

    public static class PMatchHelpers
    {
        public static TOut PMatch<TIn, TOut, T1>(
            this TIn input,
            Func<T1, TOut> map1,
            Func<TIn, TOut> mapDefault
        )
            where TIn: class
            where T1: class
        {
            var inputType = input.GetType();
            if (inputType.Equals(typeof(T1)))
            {
                return map1(input as T1);
            }
            else
            {
                return mapDefault(input);
            }
        }

        public static TOut PMatch<TIn, TOut, T1, T2>(
            this TIn input,
            Func<T1, TOut> map1,
            Func<T2, TOut> map2,
            Func<TIn, TOut> mapDefault
        )
            where TIn : class
            where T1 : class
            where T2 : class
        {
            var inputType = input.GetType();
            if (inputType.Equals(typeof(T1)))
            {
                return map1(input as T1);
            }
            else if (inputType.Equals(typeof(T2)))
            {
                return map2(input as T2);
            }
            else
            {
                return mapDefault(input);
            }
        }

        public static void PMatch<TIn, T1>(
            this TIn input,
            Action<T1> map1,
            Action<TIn> mapDefault
        )
            where TIn : class
            where T1 : class
        {
            var inputType = input.GetType();
            if (inputType.Equals(typeof(T1)))
            {
                map1(input as T1);
            }
            else
            {
                mapDefault(input);
            }
        }

    }
}