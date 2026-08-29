[![](https://img.shields.io/nuget/v/soenneker.atomics.ints.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.atomics.ints/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.atomics.ints/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.atomics.ints/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.atomics.ints.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.atomics.ints/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.atomics.ints/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.atomics.ints/actions/workflows/codeql.yml)

# Soenneker.Atomics.Ints

A lightweight atomic `int` wrapper implemented as a `class`. Internally delegates to `ValueAtomicInt` for the atomic operations.

## Install

```bash
dotnet add package Soenneker.Atomics.Ints
```

## What you get

- `AtomicInt` — A lightweight atomic `int` wrapper implemented as a `class`. Internally delegates to `ValueAtomicInt` for the atomic operations.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `AtomicInt.Value` | Gets or sets the current value. | Gets or sets the current value. |
| `AtomicInt.Read()` | Reads the current value using acquire semantics. | The current value observed with acquire memory-ordering semantics. |
| `AtomicInt.Exchange(value)` | Atomically replaces the current value with `value` and returns the previous value. | The value that was stored before the atomic update. |
| `AtomicInt.CompareExchange(value, comparand)` | Atomically sets the value to `value` if the current value equals `comparand`. Returns the original value. | The value observed before the compare-and-exchange attempt. |
| `AtomicInt.TryCompareExchange(value, comparand)` | Attempts to set the value to `value` if the current value equals `comparand`. | true if the requested update was applied; otherwise, false. |
| `AtomicInt.Increment()` | Atomically increments the value and returns the incremented value. | The incremented value. |
| `AtomicInt.Decrement()` | Atomically decrements the value and returns the decremented value. | The decremented value. |
| `AtomicInt.Add(delta)` | Atomically adds `delta` and returns the resulting value. | The resulting value. |
| `AtomicInt.GetAndIncrement()` | Atomically increments the value and returns the previous value. | The value that was stored before the atomic update. |
| `AtomicInt.GetAndDecrement()` | Atomically decrements the value and returns the previous value. | The value that was stored before the atomic update. |
| `AtomicInt.GetAndAdd(delta)` | Atomically adds `delta` and returns the previous value. | The value that was stored before the atomic update. |
| `AtomicInt.AddAndGet(delta)` | Atomically adds `delta` and returns the resulting value. | The resulting value. |
| `AtomicInt.IncrementAndGet()` | Atomically increments the value and returns the resulting value. | The resulting value. |
| `AtomicInt.DecrementAndGet()` | Atomically decrements the value and returns the resulting value. | The resulting value. |
| `AtomicInt.TrySetIfGreater(value)` | Attempts to set the value to `value` if it is greater than the current value. | true if the requested update was applied; otherwise, false. |
| `AtomicInt.TrySetIfLess(value)` | Attempts to set the value to `value` if it is less than the current value. | true if the requested update was applied; otherwise, false. |
| `AtomicInt.SetIfGreater(value)` | Sets the value to `value` if it is greater than the current value, returning the effective value. | The effective value. |
| `AtomicInt.SetIfLess(value)` | Sets the value to `value` if it is less than the current value, returning the effective value. | The effective value. |
