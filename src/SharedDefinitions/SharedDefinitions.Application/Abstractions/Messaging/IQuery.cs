// <copyright file="IQuery.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using FluentResults;
using MediatR;

namespace TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

/// <summary>
/// IQuery Interface.
/// </summary>
/// <typeparam name="TResponse">Type of response.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}