# Changelog

All notable changes to Pure.Collections.Generic are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [0.1.0-preview.3.0.0] — 2025-12-08

### Added

- Multi-targeting: the package now targets `net8.0`, `net9.0`, and `net10.0`,
  in addition to `net9.0`.

### Changed

- Removed the direct package dependency on `Pure.HashCodes`; the library now
  depends only on `Pure.HashCodes.Abstractions`.
- `OrderedDictionary<TSource, TKey, TValue>` no longer copies its source
  sequence eagerly on construction; the source is enumerated lazily again,
  restoring deferred-evaluation semantics (reverts the eager copy introduced
  in `0.1.0-preview.2.0.1`).

---

## [0.1.0-preview.2.0.1] — 2025-11-18

### Fixed

- `OrderedDictionary<TSource, TKey, TValue>` no longer enumerated its source
  sequence more than once during construction.

---

## [0.1.0-preview.2.0.0] — 2025-11-04

### Changed

- **Breaking:** `IDeterminedHash` — used throughout every collection
  constructor's `determinedHashFactory` parameter — moved from the
  `Pure.HashCodes` namespace/package to `Pure.HashCodes.Abstractions`.
  Consumers must update their `using` directives and package references
  accordingly.

---

## [0.1.0-preview.1.1.0] — 2025-10-12

### Added

- **`OrderedDictionary<TSource, TKey, TValue>`** — an
  `IReadOnlyDictionary<TKey, TValue>` that preserves the source sequence's
  enumeration order while providing key-based lookup, keyed by
  `IDeterminedHash` equality.

---

## [0.1.0-preview.1.0.0] — 2025-10-06

### Added

- **`Lookup<TSource, TKey, TValue>`** implementing `ILookup<TKey, TValue>`,
  grouping multiple values per key using `IDeterminedHash`-based equality.

### Changed

- Declared the package as AOT-compatible (`IsAotCompatible`), replacing the
  separate trim/AOT analyzer flags.

---

## [0.1.0-preview.0.1.0] — 2025-08-07

Initial preview release.

### Added

- **`Array<T>`** — an immutable, lazily materialized wrapper over
  `IEnumerable<T>`.
- **`Set<T>`** — a lazily materialized, immutable hash set with membership
  and equality determined by `IDeterminedHash`.
- **`Dictionary<TSource, TKey, TValue>`** — a lazily materialized, immutable
  `IReadOnlyDictionary<TKey, TValue>` keyed by `IDeterminedHash` equality.
- **`EqualityComparerByDeterminedHash<T>`** — an `IEqualityComparer<T>` that
  compares and hashes values via their `IDeterminedHash` byte sequences.
