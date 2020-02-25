using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public sealed class Rate : ValueObject
    {
        private const double MinRate = 0;
        private const double MaxRate = 100;

        private Rate()
        {
        }

        private Rate(double value)
            : this()
        {
            Value = value;
        }

        public static Rate Create(double value)
        {
            if (value <= MinRate)
            {
                value = MinRate;
            }

            if (value >= MaxRate)
            {
                value = MaxRate;
            }

            return new Rate(value);
        }

        public double Value { get; private set; }

        public bool Test()
        {
            var testRate = RandomProvider.Instance().NextDouble() * 100;
            return testRate >= MinRate && testRate < Value;
        }

        public static implicit operator double(Rate rate) => rate.Value;

        public static implicit operator Rate(double value) => Create(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}