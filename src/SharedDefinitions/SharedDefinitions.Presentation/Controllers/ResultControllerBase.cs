// <copyright file="ResultControllerBase.cs" company="Tek">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

using System.Runtime.CompilerServices;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TekChallenge.SharedDefinitions.Application.Abstractions.Services;
using TekChallenge.SharedDefinitions.Application.Common.Errors;
using TekChallenge.SharedDefinitions.Domain.Common;

namespace TekChallenge.SharedDefinitions.Presentation.Controllers;

/// <summary>
/// Base Class for Api Controller that Handles Validation Results.
/// </summary>
/// <typeparam name="TController">Type.</typeparam>
[ApiController]
[Authorize]
public class ResultControllerBase<TController> : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IExceptionHandlingService _exceptionHandlingService;

    /// <summary>
    /// ILogger instance.
    /// </summary>
    protected readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResultControllerBase{T}"/> class.
    /// </summary>
    /// <param name="mediator">Injected _mediator.</param>
    /// <param name="mapper">Injected _mapper.</param>
    /// <param name="logger">ILogger injected.</param>
    /// <param name="exceptionHandlingService">IExceptionHandlingService injected.</param>
    public ResultControllerBase(IMediator mediator, IMapper mapper, ILogger<TController> logger, IExceptionHandlingService exceptionHandlingService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
        _exceptionHandlingService = exceptionHandlingService;
    }

    /// <summary>
    /// Validates the result.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    /// <param name="result">FluentResult.</param>
    /// <param name="success">Action to perform when success.</param>
    /// <param name="failure">Action to perform when failure.</param>
    /// <returns>ActionResult according to Result.</returns>
    [NonAction]
    public ActionResult ValidateResult<T>(Result<T> result, Func<ActionResult> success, Action failure)
    {
        if (result.IsSuccess)
        {
            _logger.LogInformation("Request Success");

            return success.Invoke();
        }

        failure.Invoke();

        var errorMessages = result.Errors.Select(e => e.Message).ToList();

        _logger.LogInformation("Request Failure with message(s):\n{errorMessages}", errorMessages);

        var status = GetStatusCode(result);

        var problemDetails = new ProblemDetails
        {
            Instance = HttpContext.Request.Path,
            Status = status.Item1,
            Title = status.Item2,
            Detail = "One or more erros ocurred.",
            Type = $"https://httpstatuses.com/{GetStatusCode(result)}",
            Extensions = { { "errors", errorMessages } },
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status,
        };
    }

    /// <summary>
    /// Handler for received requests.
    /// </summary>
    /// <typeparam name="TCommandOrQuery">The Type of the Command or Query to execute.</typeparam>
    /// <typeparam name="TDto">The Type of the Command or Query Result.</typeparam>
    /// <typeparam name="TResponse">The Type of the Response (contract).</typeparam>
    /// <param name="request">The input received in the request.</param>
    /// <param name="caller">The Name of the Method that Invoked this method.</param>
    /// <returns>An ActionResult for sending to the client.</returns>
    [NonAction]
    public async Task<IActionResult> HandleRequestAsync<TCommandOrQuery, TDto, TResponse>(object? request = null, [CallerMemberName] string caller = "")
    {
        _logger.LogInformation($"{caller}: {request}");

        Result<TDto> result;
        TResponse? response = default;

        try
        {
            object command;
            if (request is null)
            {
                command = Activator.CreateInstance(typeof(TCommandOrQuery))!;
            }
            else
            {
                if (request is long idRequest)
                {
                    command = Activator.CreateInstance(typeof(TCommandOrQuery), idRequest)!;
                }
                else
                {
                    command = _mapper.Map<TCommandOrQuery>(request)!;
                }
            }

            if (typeof(TDto) == typeof(Result))
            {
                result = await _mediator.Send((IRequest<Result>)command);
            }
            else
            {
                result = await _mediator.Send((IRequest<Result<TDto>>)command);
            }

            if (result.IsSuccess)
            {
                response = _mapper.Map<TResponse>(result.Value!);
            }
        }
        catch (Exception ex)
        {
            result = _exceptionHandlingService.HandleException(ex, _logger);
        }

        return ValidateResult(
                   result,
                   () => Ok(response),
                   () => Problem());
    }

    /// <summary>
    /// Get Http Status Code from Result.
    /// </summary>
    /// <typeparam name="T">Result Type.</typeparam>
    /// <param name="result">instance.</param>
    /// <returns>Http Status code.</returns>
    private static (int, string) GetStatusCode<T>(Result<T> result)
    {
        if (result.HasError<ValidationError>())
        {
            return (400, "Validation Error");
        }

        if (result.HasError<UnauthorizedError>())
        {
            return (403, "You dont have permission to access this resource.");
        }

        if (result.HasError<NotFoundError>())
        {
            return (404, "Resource Not Found.");
        }

        if (result.HasError<ConflictError>())
        {
            return (409, "A resource with the same content already exists.");
        }

        return (500, "Internal Server Error");
    }
}
