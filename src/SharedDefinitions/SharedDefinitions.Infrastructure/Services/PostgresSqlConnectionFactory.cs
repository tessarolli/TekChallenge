// <copyright file="PostgresSqlConnectionFactory.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using System.Text;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TekChallenge.SharedDefinitions.Infrastructure.Abstractions;

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
        var encoded = "U2VydmVyPWJhYmFyLmRiLmVsZXBoYW50c3FsLmNvbTtQb3J0PTU0MzI7RGF0YWJhc2U9Ynd2dW12Z2w7VXNlciBJZD1id3Z1bXZnbDtQYXNzd29yZD1BOWNqV2pYYWlmN1poNGNrcDRIV2k0VllXVndHRGNnODs=";
        var connectionString = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
        return new NpgsqlConnection(connectionString);
    }
}
