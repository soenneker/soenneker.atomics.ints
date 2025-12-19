using System;
using System.Runtime.CompilerServices;
using Soenneker.Atomics.ValueInts;

namespace Soenneker.Atomics.Ints;

/// <summary>
/// A lightweight atomic <see cref="int"/> wrapper implemented as a <see langword="class"/>.
/// Internally delegates to <see cref="ValueAtomicInt"/> for the atomic operations.
/// </summary>
public sealed class AtomicInt
{
    private ValueAtomicInt _value;

    /// <summary>
    /// Initializes a new <see cref="AtomicInt"/> with an optional initial value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AtomicInt(int initialValue = 0)
    {
        _value = new ValueAtomicInt(initialValue);
    }

    /// <summary>
    /// Gets or sets the current value.
    /// </summary>
    public int Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _value.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _value.Value = value;
    }

    /// <summary>
    /// Reads the current value using acquire semantics.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Read() => _value.Read();

    /// <summary>
    /// Writes the value atomically.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(int value) => _value.Write(value);

    /// <summary>
    /// Atomically replaces the current value with <paramref name="value"/> and returns the previous value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Exchange(int value) => _value.Exchange(value);

    /// <summary>
    /// Atomically sets the value to <paramref name="value"/> if the current value equals <paramref name="comparand"/>.
    /// Returns the original value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareExchange(int value, int comparand) => _value.CompareExchange(value, comparand);

    /// <summary>
    /// Attempts to set the value to <paramref name="value"/> if the current value equals <paramref name="comparand"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryCompareExchange(int value, int comparand) => _value.TryCompareExchange(value, comparand);

    /// <summary>
    /// Atomically increments the value and returns the incremented value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Increment() => _value.Increment();

    /// <summary>
    /// Atomically decrements the value and returns the decremented value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Decrement() => _value.Decrement();

    /// <summary>
    /// Atomically adds <paramref name="delta"/> and returns the resulting value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Add(int delta) => _value.Add(delta);

    // ---- Get-and (returns previous) ----

    /// <summary>
    /// Atomically increments the value and returns the previous value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetAndIncrement() => _value.GetAndIncrement();

    /// <summary>
    /// Atomically decrements the value and returns the previous value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetAndDecrement() => _value.GetAndDecrement();

    /// <summary>
    /// Atomically adds <paramref name="delta"/> and returns the previous value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetAndAdd(int delta) => _value.GetAndAdd(delta);

    // ---- And-get (returns current) ----

    /// <summary>
    /// Atomically adds <paramref name="delta"/> and returns the resulting value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int AddAndGet(int delta) => _value.AddAndGet(delta);

    /// <summary>
    /// Atomically increments the value and returns the resulting value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int IncrementAndGet() => _value.IncrementAndGet();

    /// <summary>
    /// Atomically decrements the value and returns the resulting value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int DecrementAndGet() => _value.DecrementAndGet();

    // ---- Conditional set helpers ----

    /// <summary>
    /// Attempts to set the value to <paramref name="value"/> if it is greater than the current value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TrySetIfGreater(int value) => _value.TrySetIfGreater(value);

    /// <summary>
    /// Attempts to set the value to <paramref name="value"/> if it is less than the current value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TrySetIfLess(int value) => _value.TrySetIfLess(value);

    /// <summary>
    /// Sets the value to <paramref name="value"/> if it is greater than the current value, returning the effective value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int SetIfGreater(int value) => _value.SetIfGreater(value);

    /// <summary>
    /// Sets the value to <paramref name="value"/> if it is less than the current value, returning the effective value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int SetIfLess(int value) => _value.SetIfLess(value);

    // ---- CAS-loop transforms ----

    /// <summary>
    /// Atomically applies <paramref name="update"/> in a CAS loop and returns the updated value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Update(Func<int, int> update) => _value.Update(update);

    /// <summary>
    /// Atomically combines the current value with <paramref name="x"/> using <paramref name="accumulator"/>
    /// in a CAS loop and returns the resulting value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Accumulate(int x, Func<int, int, int> accumulator) => _value.Accumulate(x, accumulator);

    /// <summary>
    /// Attempts to set the value to <paramref name="value"/> if the current value equals <paramref name="expected"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TrySet(int value, int expected) => _value.TrySet(value, expected);

    /// <summary>
    /// Returns a string representation of the current value.
    /// </summary>
    public override string ToString() => _value.ToString();
}