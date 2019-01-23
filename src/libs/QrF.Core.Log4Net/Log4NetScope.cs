using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace QrF.Core.Log4Net
{
    public class Log4NetScope
    {
        private readonly string _name;
        private readonly object _state;

        public Log4NetScope(string name, object state)
        {
            _name = name;
            _state = state;
        }
        public Log4NetScope Parent { get; private set; }

        private static AsyncLocal<Log4NetScope> _value = new AsyncLocal<Log4NetScope>();

        public static Log4NetScope Current
        {
            get { return _value.Value; }
            set { _value.Value = value; }
        }

        public static IDisposable Push(string name, object state)
        {
            var temp = Current;
            Current = new Log4NetScope(name, state);
            Current.Parent = temp;

            return new DisposableScope();
        }

        private sealed class DisposableScope : IDisposable
        {
            public void Dispose()
            {
                Current = Current.Parent;
            }
        }

        public override string ToString()
        {
            return _state?.ToString();
        }
    }
}
