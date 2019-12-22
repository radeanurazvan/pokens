using System.Collections.Generic;
using System.Threading;

namespace Pomelo.Kernel.Domain
{
    public class RandomProviderContext
    {
        private static readonly ThreadLocal<Stack<RandomProviderContext>> ThreadScopeStack
            = new ThreadLocal<Stack<RandomProviderContext>>(() => new Stack<RandomProviderContext>());

        private RandomProviderContext(double value)
        {
            this.RandomValue = value;
        }

        public static RandomProviderContext Current => ThreadScopeStack.Value.Count == 0 ? null : ThreadScopeStack.Value.Peek();

        public static void PredictDouble(double value)
        {
            ThreadScopeStack.Value.Push(new RandomProviderContext(value));
        }

        public double RandomValue { get; }

        public void Dispose()
        {
            ThreadScopeStack.Value.Pop();
        }
    }
}