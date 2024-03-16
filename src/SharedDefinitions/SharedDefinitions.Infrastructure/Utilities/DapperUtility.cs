// <copyright file="DapperUtility.cs" company="Tek">
// Copyright (c) TekChallenge.SharedDefinitions. All rights reserved.
// </copyright>

using System.Data;
using System.Data.Common;
using Dapper;
using Npgsql;
using TekChallenge.SharedDefinitions.Infrastructure.Abstractions;

namespace TekChallenge.SharedDefinitions.Infrastructure.Utilities;

/// <summary>
/// Provides Dapper Accessibility Helpers Functunalities.
/// </summary>
public sealed class DapperUtility
{
    private readonly IPostgresSqlConnectionFactory _postgresSqlConnectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="DapperUtility"/> class.
    /// </summary>
    /// <param name="postgresSqlConnectionFactory">PostgresConnectionFactory injected.</param>
    public DapperUtility(IPostgresSqlConnectionFactory postgresSqlConnectionFactory)
    {
        _postgresSqlConnectionFactory = postgresSqlConnectionFactory;
    }

    /// <summary>
    /// Query the database.
    /// </summary>
    /// <typeparam name="T">The type to map the result.</typeparam>
    /// <param name="sql">sql or stored procedure to call.</param>
    /// <param name="parameters">parmeters.</param>
    /// <param name="commandType">type of command.</param>
    /// <param name="transaction">Transcation.</param>
    /// <returns>A IEnumerable of TResult.</returns>
    public async Task<IEnumerable<T>> QueryAsync<T>(
        string sql,
        object? parameters = null,
        CommandType commandType = CommandType.Text,
        DbTransaction? transaction = null)
    {
        var connection = transaction?.Connection ?? _postgresSqlConnectionFactory.CreateConnection();

        var result = await connection.QueryAsync<T>(sql, parameters, commandType: commandType, transaction: transaction);

        await ProperlyDisposeConnectionAsync(connection, transaction);

        return result;
    }

    /// <summary>
    /// Query the First result from the database or return null if it doesnt exists.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="sql">sql or stored procedure to call.</param>
    /// <param name="parameters">parmeters.</param>
    /// <param name="commandType">type of command.</param>
    /// <param name="transaction">Transaction.</param>
    /// <returns>A TResult instance or null.</returns>
    public async Task<T?> QueryFirstOrDefaultAsync<T>(
      string sql,
      object? parameters = null,
      CommandType commandType = CommandType.Text,
      DbTransaction? transaction = null)
    {
        var connection = transaction?.Connection ?? _postgresSqlConnectionFactory.CreateConnection();

        var result = await connection.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType, transaction: transaction);

        await ProperlyDisposeConnectionAsync(connection, transaction);

        return result;
    }

    /// <summary>
    /// Executes an stored procedure or an sql statement.
    /// </summary>
    /// <param name="sql">sql statement or store procedure name.</param>
    /// <param name="parameters">parameters.</param>
    /// <param name="commandType">command type.</param>
    /// <param name="transaction">Transaction.</param>
    /// <returns>Number of rows affected.</returns>
    public async Task<long> ExecuteAsync(
        string sql,
        object? parameters = null,
        CommandType commandType = CommandType.Text,
        DbTransaction? transaction = null)
    {
        var connection = transaction?.Connection ?? _postgresSqlConnectionFactory.CreateConnection();

        var rowsAffectedCount = await connection.ExecuteAsync(sql, parameters, commandType: commandType, transaction: transaction);

        await ProperlyDisposeConnectionAsync(connection, transaction);

        return rowsAffectedCount;
    }

    /// <summary>
    /// Executes an stored procedure or an sql statement.
    /// </summary>
    /// <param name="sql">sql statement or store procedure name.</param>
    /// <param name="parameters">parameters.</param>
    /// <param name="commandType">command type.</param>
    /// <param name="transaction">Transaction.</param>
    /// <returns>An scalar number from the executed command.</returns>
    public async Task<long> ExecuteScalarAsync(
        string sql,
        object? parameters = null,
        CommandType commandType = CommandType.Text,
        DbTransaction? transaction = null)
    {
        var connection = transaction?.Connection ?? _postgresSqlConnectionFactory.CreateConnection();

        var scalar = await connection.ExecuteScalarAsync<long>(sql, parameters, commandType: commandType, transaction: transaction);

        await ProperlyDisposeConnectionAsync(connection, transaction);

        return scalar;
    }

    /// <summary>
    /// Opens a transaction.
    /// </summary>
    /// <returns>The Transaction.</returns>
    public async Task<DbTransaction> BeginTransactionAsync()
    {
        var connection = _postgresSqlConnectionFactory.CreateConnection();

        await connection.OpenAsync();

        return await connection.BeginTransactionAsync();
    }

    /// <summary>
    /// Closes a transaction.
    /// </summary>
    /// <param name="transaction">The transaction to properly close.</param>
    /// <param name="commitBeforeClosing">Commit the transaction before closing the connection.</param>
    /// <returns>The Transaction.</returns>
    public async Task CloseTransactionAsync(DbTransaction transaction, bool commitBeforeClosing = false)
    {
        if (commitBeforeClosing)
        {
            await transaction.CommitAsync();
        }

        if(transaction.Connection is not null)
        {
            NpgsqlConnection.ClearPool((NpgsqlConnection)transaction.Connection);
            await transaction.Connection.DisposeAsync();
            await transaction.DisposeAsync();
        }
    }

    private async Task ProperlyDisposeConnectionAsync(DbConnection connection, DbTransaction? transaction)
    {
        if (transaction is null)
        {
            await connection.CloseAsync();
            await connection.DisposeAsync();
            NpgsqlConnection.ClearPool((NpgsqlConnection)connection);
        }
    }
}
