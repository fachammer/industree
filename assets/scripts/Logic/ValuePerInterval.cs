namespace Industree.Logic
{
    public struct ValuePerInterval<T> where T : struct
    {
        public T Value { get; private set; }
        public float Interval { get; private set; }

        public ValuePerInterval(T value, float interval)
        {
            Value = value;
            Interval = interval;
        }
    }
}
