using System;

namespace Industree.Input
{
    public interface IAxis
    {
        event Action<float, float> Change;

        float Value { get; }
    }
}
