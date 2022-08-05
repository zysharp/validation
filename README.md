# ZySharp Validation

![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)
[![Build](https://github.com/flobernd/zysharp-validation/actions/workflows/build.yml/badge.svg)](https://github.com/flobernd/zysharp-validation/actions)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=flobernd_zysharp-validation&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=flobernd_zysharp-validation)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=flobernd_zysharp-validation&metric=coverage)](https://sonarcloud.io/summary/new_code?id=flobernd_zysharp-validation)
[![NuGet](https://img.shields.io/nuget/v/ZySharp.Validation.svg)](https://nuget.org/packages/ZySharp.Validation)
[![Nuget](https://img.shields.io/nuget/dt/ZySharp.Validation.svg)](https://nuget.org/packages/ZySharp.Validation)

A C# library that provides a fluent-api for basic argument validation.

## Introduction

The `ZySharp Validation` library is intended to replace repetitive validation/guarding code at the begin of public methods. Contrary to e.g. `FluentValidation` it is not designed to validate complex entities or to report meaningful validation messages to users.

In case of a validation failure, an exception is thrown immediately without performing any further validations. The exception does always contain the root argument name as well as the complete path of the property that caused the validation to fail (e.g. `assembly.Location` or `ids[i]`).

## Fluent API

`ZySharp Validation` provides a fluent API to validate arguments:

```csharp
ValidateArgument.For(args, nameof(args), v => v
    .NotNull()
    .NotEmpty()
    ...
);
```

Validations of sub-properties or collections are executed in separate contexts:

```csharp
ValidateArgument.For(args, nameof(args), v => v
    .NotNull()
    .For(x => x.SubProperty, v => v // <- Switch to sub-property context
        .NotEmpty()
        .InRange(1, 100)
    )                               // <- Switch back to the original context
    .NotEmpty()
);
```

## Versioning

Versions follow the [semantic versioning scheme](https://semver.org/).

## License

ZySharp.Validation is licensed under the MIT license.
