# Pure.Collections.Generic

Generic collection types for the **Pure** ecosystem — immutable-friendly collections backed by `IDeterminedHash` equality.

[![.NET build & test](https://github.com/kudima03/Pure.Collections.Generic/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Collections.Generic/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.Collections.Generic/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Collections.Generic/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.Collections.Generic)](https://www.nuget.org/packages/Pure.Collections.Generic)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`Pure.Collections.Generic` provides collection types designed to integrate with the `IDeterminedHash` equality model. Each collection uses `EqualityComparerByDeterminedHash` as its default equality comparer, enabling content-addressed lookups over any type that implements `IDeterminedHash`.

## Types

| Type | Description |
|------|-------------|
| `Dictionary<TKey, TValue>` | Hash map keyed by `IDeterminedHash` equality |
| `Set<T>` | Hash set with `IDeterminedHash` membership checks |
| `Array<T>` | Immutable array wrapper over `IDeterminedHash` elements |
| `OrderedDictionary<TKey, TValue>` | Insertion-ordered dictionary with `IDeterminedHash` keys |
| `Lookup<TKey, TElement>` | Multi-value lookup grouped by `IDeterminedHash` keys |
| `EqualityComparerByDeterminedHash` | `IEqualityComparer<T>` using `IDeterminedHash` byte sequences |

All types live in the `Pure.Collections.Generic` namespace.

## Design Principles

- **Hash-based equality** — collections compare elements by their `IDeterminedHash` byte sequences, not by `object.GetHashCode()`.
- **AOT-compatible** — no reflection; all generics are constrained at compile time.

## Dependencies

- [`Pure.HashCodes.Abstractions`](https://github.com/kudima03/Pure.HashCodes.Abstractions) — `IDeterminedHash` interface
