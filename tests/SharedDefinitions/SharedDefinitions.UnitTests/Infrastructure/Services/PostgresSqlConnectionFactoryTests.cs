using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekChallenge.SharedDefinitions.Infrastructure.Services;

namespace TekChallenge.Tests.SharedDefinitions.Infrastructure.Services;

public class PostgresSqlConnectionFactoryTests
{
    [Fact]
    public void CreateConnection_Should_Decode_Connection_String_From_Base64()
    {
        // Arrange
        var configuration = Substitute.For<IConfiguration>();
        configuration[PostgresSqlConnectionFactory.ConnectionStringConfigurationName].Returns("U2VydmVyPWJhYmFyLmRiLmVsZXBoYW50c3FsLmNvbTtQb3J0PTU0MzI7RGF0YWJhc2U9Ynd2dW12Z2w7VXNlciBJZD1id3Z1bXZnbDtQYXNzd29yZD1BOWNqV2pYYWlmN1poNGNrcDRIV2k0VllXVndHRGNnODs=");

        var factory = new PostgresSqlConnectionFactory(configuration);

        // Act
        var connection = factory.CreateConnection();

        // Assert
        connection.Should().NotBeNull();
        connection.Should().BeOfType<NpgsqlConnection>();
        connection.ConnectionString.Should().NotBeNullOrWhiteSpace();
    }
}