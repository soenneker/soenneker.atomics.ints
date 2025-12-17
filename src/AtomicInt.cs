using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Soenneker.Atomics.Ints.Abstract;

namespace Soenneker.Atomics.Ints;

/// <inheritdoc cref="IAtomicInt"/>
public sealed class AtomicInt : IAtomicInt
{
    private int _value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AtomicInt()
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AtomicInt(int initialValue) => _value = initialValue;

    public int Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Volatile.Read(ref _value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => Interlocked.Exchange(ref _value, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Read() => Volatile.Read(ref _value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(int value) => Interlocked.Exchange(ref _value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Exchange(int value) => Interlocked.Exchange(ref _value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareExchange(int value, int comparand) =>
        Interlocked.CompareExchange(ref _value, value, comparand);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryCompareExchange(int value, int comparand) =>
        Interlocked.CompareExchange(ref _value, value, comparand) == comparand;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Increment() => Interlocked.Increment(ref _value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Decrement() => Interlocked.Decrement(ref _value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Add(int delta) => Interlocked.Add(ref _value, delta);

    // ---- Get-and (returns previous) ----

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetAndIncrement() => Interlocked.Increment(ref _value) - 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetAndDecrement() => Interlocked.Decrement(ref _value) + 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetAndAdd(int delta) => Interlocked.Add(ref _value, delta) - delta;

    // ---- And-get (returns current) ----

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int AddAndGet(int delta) => Interlocked.Add(ref _value, delta);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int IncrementAndGet() => Interlocked.Increment(ref _value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int DecrementAndGet() => Interlocked.Decrement(ref _value);

    // ---- Conditional set helpers ----

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TrySetIfGreater(int value)
    {
        int current = Volatile.Read(ref _value);
        if (value <= current)
            return false;

        return Interlocked.CompareExchange(ref _value, value, current) == current;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TrySetIfLess(int value)
    {
        int current = Volatile.Read(ref _value);
        if (value >= current)
            return false;

        return Interlocked.CompareExchange(ref _value, value, current) == current;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int SetIfGreater(int value)
    {
        var spin = new SpinWait();

        while (true)
        {
            int current = Volatile.Read(ref _value);
            if (value <= current)
                return current;

            int prior = Interlocked.CompareExchange(ref _value, value, current);
            if (prior == current)
                return value;

            spin.SpinOnce();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int SetIfLess(int value)
    {
        var spin = new SpinWait();

        while (true)
        {
            int current = Volatile.Read(ref _value);
            if (value >= current)
                return current;

            int prior = Interlocked.CompareExchange(ref _value, value, current);
            if (prior == current)
                return value;

            spin.SpinOnce();
        }
    }

    // ---- CAS-loop transforms ----

    public int Update(Func<int, int> update)
    {
        if (update is null)
            throw new ArgumentNullException(nameof(update));

        var spin = new SpinWait();

        while (true)
        {
            int original = Volatile.Read(ref _value);
            int next = update(original);

            int prior = Interlocked.CompareExchange(ref _value, next, original);
            if (prior == original)
                return next;

            spin.SpinOnce();
        }
    }

    public bool TryUpdate(Func<int, int> update, out int original, out int updated)
    {
        if (update is null)
            throw new ArgumentNullException(nameof(update));

        original = Volatile.Read(ref _value);
        updated = update(original);

        return Interlocked.CompareExchange(ref _value, updated, original) == original;
    }

    public int Accumulate(int x, Func<int, int, int> accumulator)
    {
        if (accumulator is null)
            throw new ArgumentNullException(nameof(accumulator));

        var spin = new SpinWait();

        while (true)
        {
            int original = Volatile.Read(ref _value);
            int next = accumulator(original, x);

            int prior = Interlocked.CompareExchange(ref _value, next, original);
            if (prior == original)
                return next;

            spin.SpinOnce();
        }
    }

    public override string ToString() => Volatile.Read(ref _value).ToString();
}
