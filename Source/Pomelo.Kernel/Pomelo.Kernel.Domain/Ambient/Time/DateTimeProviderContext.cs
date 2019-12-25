using System;
using System.Collections.Generic;
using System.Threading;

namespace Pomelo.Kernel.Domain
{
    public class DateTimeProviderContext
    {
        private static readonly ThreadLocal<Stack<DateTimeProviderContext>> ThreadScopeStack
            = new ThreadLocal<Stack<DateTimeProviderContext>>(() => new Stack<DateTimeProviderContext>());

        private DateTimeProviderContext(DateTime value)
        {
            this.UtcNow = value;
        }

        public static DateTimeProviderContext Current => ThreadScopeStack.Value.Count == 0 ? null : ThreadScopeStack.Value.Peek();

        public static void AdvanceUtcTimeTo(DateTime value)
        {
            ThreadScopeStack.Value.Push(new DateTimeProviderContext(value));
        }

        public DateTime UtcNow { get; }

        public void Dispose()
        {
            ThreadScopeStack.Value.Pop();
        }
    }
}