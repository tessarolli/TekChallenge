// <copyright file="IQueryHandler.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using FluentResults;
using MediatR;

namespace TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

/// <summary>
/// IQueryHandler Interface.
/// </summary>
/// <typeparam name="TQuery">Type of Query.</typeparam>
/// <typeparam name="TResponse">Type of response.</typeparam>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}