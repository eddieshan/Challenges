using System;

namespace Drills.TennisMatch.Core
{
    /// <summary>
    /// An ad hoc implementation of SumTypes. Ensures exhaustive and safe Type Matching in compile time.
    /// - Allows representing groups of types that share a logical relation but are not polymorphic.
    /// - Solves the issues that usually make Type Matching an anti-pattern.
    /// Type Matching in this case is exhaustive, all the Type cases have to be declared as generic constraints.
    /// Incidentally and without meaning to, usage of this Match will work as a Visitor patterns.
    /// One benefit of Functional Programming is that OOP Design Patterns tend to emerge naturally and effortlesstly.
    /// </summary>
    public class Union<T1, T2>
        where T1 : class
        where T2 : class
    {
        private readonly object _value;
        private readonly bool _case;

        private Union(object value, bool valueCase)
        {
            _value = value;
            _case = valueCase;
        }

        public Union(T1 value): this(value, true) { }
        public Union(T2 value) : this(value, false) { }

        public TOut Match<TOut>(Func<T1, TOut> map1, Func<T2, TOut> map2) => _case? map1(_value as T1) : map2(_value as T2);
        public void Match(Action<T1> map1, Action<T2> map2) 
        {
            if (_case)
            {
                map1(_value as T1);
            }
            else
            {
                map2(_value as T2);
            }
        }
    }

    public class Union<T1, T2, T3, T4>
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
    {
        private readonly object _value;
        private readonly byte _case;

        private Union(object value, byte valueCase)
        {
            _value = value;
            _case = valueCase;
        }

        public Union(T1 value) : this(value, 1) { }
        public Union(T2 value) : this(value, 2) { }
        public Union(T3 value) : this(value, 3) { }
        public Union(T4 value) : this(value, 4) { }

        public TOut Match<TOut>(Func<T1, TOut> map1, Func<T2, TOut> map2, Func<T3, TOut> map3, Func<T4, TOut> map4)
        {
            if (_case == 1)
            {
                return map1(_value as T1);
            }
            else if (_case == 2)
            {
                return map2(_value as T2);
            }
            else if (_case == 3)
            {
                return map3(_value as T3);
            }
            else
            {
                return map4(_value as T4);
            }
        }
    }

    public class Union<T1, T2, T3, T4, T5>
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
    {
        private readonly object _value;
        private readonly byte _case;

        private Union(object value, byte valueCase)
        {
            _value = value;
            _case = valueCase;
        }

        public Union(T1 value) : this(value, 1) { }
        public Union(T2 value) : this(value, 2) { }
        public Union(T3 value) : this(value, 3) { }
        public Union(T4 value) : this(value, 4) { }
        public Union(T5 value) : this(value, 5) { }

        public TOut Match<TOut>(
            Func<T1, TOut> map1, 
            Func<T2, TOut> map2,
            Func<T3, TOut> map3,
            Func<T4, TOut> map4,
            Func<T5, TOut> map5)
        {
            if(_case == 1)
            {
                return map1(_value as T1);
            }
            else if(_case == 2)
            {
                return map2(_value as T2);
            }
            else if (_case == 3)
            {
                return map3(_value as T3);
            }
            else if (_case == 4)
            {
                return map4(_value as T4);
            }
            else
            {
                return map5(_value as T5);
            }
        }

        public void Match(
            Action<T1> map1,
            Action<T2> map2,
            Action<T3> map3,
            Action<T4> map4,
            Action<T5> map5)
        {
            if (_case == 1)
            {
                map1(_value as T1);
            }
            else if (_case == 2)
            {
                map2(_value as T2);
            }
            else if (_case == 3)
            {
                map3(_value as T3);
            }
            else if (_case == 4)
            {
                map4(_value as T4);
            }
            else
            {
                map5(_value as T5);
            }
        }

    }
}