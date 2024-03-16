// <copyright file="EntityValidator.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using FluentValidation;
using TekChallenge.SharedDefinitions.Domain.Common.DDD;

namespace TekChallenge.SharedDefinitions.Domain.Common;

/// <summary>
/// Abstract Base Class for Domain Entities Validators.
/// </summary>
/// <typeparam name="T">Type of the Domain Entity.</typeparam>
/// <typeparam name="TId">Type of the Id Property.</typeparam>
public abstract class EntityValidator<T, TId> : AbstractValidator<Entity<TId>>
where T : Entity<TId>
where TId : notnull
{
}
