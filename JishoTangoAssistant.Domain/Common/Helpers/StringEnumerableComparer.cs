namespace JishoTangoAssistant.Domain.Models.Common.Helpers;

public class EnumerableComparer<T> : IEqualityComparer<IEnumerable<T>>
{
    public bool Equals(IEnumerable<T>? x, IEnumerable<T>? y)
    {
        if (x == null || y == null)
            return object.Equals(x, y);

        return x.SequenceEqual(y);
    }

    public int GetHashCode(IEnumerable<T>? obj)
    {
        return obj != null ? obj.GetHashCode() : 0;
    }
}