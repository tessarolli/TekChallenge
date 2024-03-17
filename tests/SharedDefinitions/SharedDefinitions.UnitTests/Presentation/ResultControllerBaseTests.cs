using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekChallenge.SharedDefinitions.Application.Abstractions.Services;
using TekChallenge.SharedDefinitions.Application.Common.Errors;
using TekChallenge.SharedDefinitions.Presentation.Controllers;

namespace TekChallenge.Tests.SharedDefinitions.Presentation;

public class ResultControllerBaseTests
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<TestController> _logger;
    private readonly IExceptionHandlingService _exceptionHandlingService;

    public ResultControllerBaseTests()
    {
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<TestController>>();
        _exceptionHandlingService = Substitute.For<IExceptionHandlingService>();
    }

    [Fact]
    public void ValidateResult_WhenResultIsSuccess_ReturnsSuccess()
    {
        // Arrange
        var controller = new ResultControllerBase<TestController>(_mediator, _mapper, _logger, _exceptionHandlingService);
        var result = Result.Ok(true);
        Func<ActionResult> successAction = () => new OkResult();
        Action failureAction = () => { };

        // Act
        var actionResult = controller.ValidateResult(result, successAction, failureAction);

        // Assert
        actionResult.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task HandleRequestAsync_WhenResultIsSuccessAndDtoIsResult_ReturnsOkObjectResult()
    {
        // Arrange
        var controller = new ResultControllerBase<TestController>(_mediator, _mapper, _logger, _exceptionHandlingService);
        var request = new object();
        var expectedResult = Result.Ok();
        _mediator.Send(Arg.Any<IRequest<Result>>()).Returns(expectedResult);

        // Act
        var actionResult = await controller.HandleRequestAsync<object, Result, object>(request);

        // Assert
        actionResult.Should().BeOfType<OkObjectResult>();
    }
}

public class TestController { }