// <copyright file="IPostgresSqlConnectionFactory.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using Npgsql;

namespace TekChallenge.SharedDefinitions.Infrastructure.Abstractions;

/// <summary>
/// Defines the methods for the Postgre Sql Connection Factory.
/// </summary>
public interface IPostgresSqlConnectionFactory
{
    /// <summary>
    /// Creates an instance of the NpgsqlConnection class.
    /// </summary>
    /// <returns>The instace of the NpgsqlConnection class.</returns>
    NpgsqlConnection CreateConnection();
}