using System.Data.Common;

using System.Data;

namespace TekChallenge.SharedDefinitions.Infrastructure.Abstractions;

/// <summary>
/// The interface for Dapper ORM interactions.
/// </summary>
/// <remarks>
/// This interface defines methods for interacting with a database using Dapper ORM.
/// </remarks>
public interface IDapperUtility
{
    /// <summary>
    /// Begins a database transaction.
    /// </summary>
    /// <returns>An object representing the new transaction.</returns>
    DbTransaction BeginTransaction();

    /// <summary>
    /// Closes the specified transaction.
    /// </summary>
    /// <param name="transaction">The transaction to close.</param>
    /// <param name="commitBeforeClosing">A flag indicating whether to commit changes before closing the transaction. Default is false.</param>
    void CloseTransaction(IDbTransaction transaction, bool commitBeforeClosing = false);

    /// <summary>
    /// Asynchronously executes a SQL command that does not return any result set.
    /// </summary>
    /// <param name="sql">The SQL command to execute.</param>
    /// <param name="parameters">The parameters for the SQL command. Default is null.</param>
    /// <param name="commandType">The type of command to execute. Default is CommandType.Text.</param>
    /// <param name="transaction">The transaction within which to execute the command. Default is null.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the number of rows affected.</returns>
    Task<long> ExecuteAsync(string sql, object? parameters = null, CommandType commandType = CommandType.Text, DbTransaction? transaction = null);

    /// <summary>
    /// Asynchronously executes a SQL command that returns a single scalar value.
    /// </summary>
    /// <param name="sql">The SQL command to execute.</param>
    /// <param name="parameters">The parameters for the SQL command. Default is null.</param>
    /// <param name="commandType">The type of command to execute. Default is CommandType.Text.</param>
    /// <param name="transaction">The transaction within which to execute the command. Default is null.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the scalar value returned by the command.</returns>
    Task<long> ExecuteScalarAsync(string sql, object? parameters = null, CommandType commandType = CommandType.Text, DbTransaction? transaction = null);

    /// <summary>
    /// Asynchronously executes a SQL command and returns a sequence of typed results.
    /// </summary>
    /// <typeparam name="T">The type of objects to return.</typeparam>
    /// <param name="sql">The SQL command to execute.</param>
    /// <param name="parameters">The parameters for the SQL command. Default is null.</param>
    /// <param name="commandType">The type of command to execute. Default is CommandType.Text.</param>
    /// <param name="transaction">The transaction within which to execute the command. Default is null.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the sequence of typed results returned by the command.</returns>
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text, DbTransaction? transaction = null);

    /// <summary>
    /// Asynchronously executes a SQL command and returns the first result, or a default value if no result is found.
    /// </summary>
    /// <typeparam name="T">The type of object to return.</typeparam>
    /// <param name="sql">The SQL command to execute.</param>
    /// <param name="parameters">The parameters for the SQL command. Default is null.</param>
    /// <param name="commandType">The type of command to execute. Default is CommandType.Text.</param>
    /// <param name="transaction">The transaction within which to execute the command. Default is null.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the first result returned by the command, or a default value if no result is found.</returns>
    Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text, DbTransaction? transaction = null);
}
