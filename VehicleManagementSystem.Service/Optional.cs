using System.ComponentModel;

namespace VehicleManagementSystem.Service;

public readonly struct Optional<T>
{
    private readonly T _value;

    private Optional(T value)
    {
        HasValue = true;
        _value = value;
    }

    public static Optional<T> Of(T value) => new(value: value);
    
    // Instance of this struct with all its fields set to their default values.
    public static Optional<T> Empty() => default;

    public bool HasValue { get; }

    public T Value
    {
        get
        {
            if (!HasValue)
            {
                throw new InvalidOperationException(message: "Optional<T> does not have a value.");
            }

            return _value;
        }
    }
    
    public T OrElse(T defaultValue) => HasValue ? _value : defaultValue;
}