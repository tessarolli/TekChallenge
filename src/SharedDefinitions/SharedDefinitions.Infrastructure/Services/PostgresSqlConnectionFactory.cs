// <copyright file="PostgresSqlConnectionFactory.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TekChallenge.SharedDefinitions.Infrastructure.Abstractions;

namespace TekChallenge.SharedDefinitions.Infrastructure.Services;

/// <summary>
/// Postgres Sql Connection Factory.
/// </summary>
public sealed class PostgresSqlConnectionFactory<T> : ISqlConnectionFactory<DbConnection>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PostgresSqlConnectionFactory{T}"/> class.
    /// </summary>
    public PostgresSqlConnectionFactory()
    {
    }

    /// <inheritdoc/>
    public DbConnection CreateConnection()
    {
        var encoded = "U2VydmVyPWJhYmFyLmRiLmVsZXBoYW50c3FsLmNvbTtQb3J0PTU0MzI7RGF0YWJhc2U9Ynd2dW12Z2w7VXNlciBJZD1id3Z1bXZnbDtQYXNzd29yZD1BOWNqV2pYYWlmN1poNGNrcDRIV2k0VllXVndHRGNnODs=";
        var connectionString = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
        return new NpgsqlConnection(connectionString);
    }
}
