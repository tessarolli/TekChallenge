// <copyright file="IPostgresSqlConnectionFactory.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using Npgsql;
using System.Data.Common;

namespace TekChallenge.SharedDefinitions.Infrastructure.Abstractions;

/// <summary>
/// Defines the Factory method for Creating a Connection to a RDBMS.
/// </summary>
public interface ISqlConnectionFactory<T>
    where T : DbConnection
{
    /// <summary>
    /// Creates an instance of the <typeparamref name="T"/> class.
    /// </summary>
    /// <returns>The instace of the NpgsqlConnection class.</returns>
    T CreateConnection();
}