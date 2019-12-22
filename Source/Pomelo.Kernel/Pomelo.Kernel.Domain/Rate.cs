using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Pomelo.Kernel.Domain
{
    public sealed class Rate : ValueObject
    {
        private Rate(double value)
        {
            Value = value;
        }

        public static Rate Create(double value)
        {
            if (value <= 0)
            {
                value = 0;
            }

            if (value >= 100)
            {
                value = 100;
            }

            return new Rate(value);
        }

        public double Value { get; private set; }

        public static implicit operator double(Rate rate) => rate.Value;
        
        public static implicit operator Rate(double value) => Create(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}