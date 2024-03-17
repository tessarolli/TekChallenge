// <copyright file="PostgresSqlConnectionFactory.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using TekChallenge.SharedDefinitions.Infrastructure.Abstractions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace TekChallenge.SharedDefinitions.Infrastructure.Services;

/// <summary>
/// Postgres Sql Connection Factory.
/// </summary>
public class PostgresSqlConnectionFactory : IPostgresSqlConnectionFactory
{
    /// <summary>
    /// Holds the Connection String Configuration name on the appsettings.json file on the API project.
    /// </summary>
    public static readonly string ConnectionStringConfigurationName = "connectionstring-postgresql";

    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="PostgresSqlConnectionFactory"/> class.
    /// </summary>
    /// <param name="configuration">Injected _configuration.</param>
    public PostgresSqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc/>
    public NpgsqlConnection CreateConnection()
    {
        var connectionString = _configuration[ConnectionStringConfigurationName];
        return new NpgsqlConnection(connectionString);
    }
}
