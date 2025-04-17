using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleAuthExceptionAsync(context, ex);
        }
        catch (BadHttpRequestException ex)
        {
            await HandleNotFoundExceptionAsync(context, ex);
        }
       
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var response = new GenericErrorDetail("ValidationError", "Validation Error", exception.Message);

        return SerializeResponse(context, response);
    }

    private static Task HandleAuthExceptionAsync(HttpContext context, UnauthorizedAccessException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

        var response = new GenericErrorDetail("ResourceNotFound", "Resource Not Found", exception.Message);

        return SerializeResponse(context, response);
    }
    private static Task HandleNotFoundExceptionAsync(HttpContext context, BadHttpRequestException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status404NotFound;

        var response = new GenericErrorDetail("ResourceNotFound", "Resource Not Found", exception.Message);

        return SerializeResponse(context, response);
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new GenericErrorDetail("InternalServerError", "Internal Server Error", exception.Message );
        

        return SerializeResponse(context, response);
    }

    private static Task SerializeResponse(HttpContext context, GenericErrorDetail response)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
}
