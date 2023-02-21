using EnigmatShopAPI.Exceptions;
using EnigmatShopAPI.Message;
using EnigmatShopAPI.Models;
using System.ComponentModel;
using System.Net;

namespace EnigmatShopAPI.Middlewares
{
	public class HandleExceptionMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (NotFoundException e)
			{
				await HandleExceptionAsync(context, e);
			}
			catch (Exception e)
			{
				await HandleExceptionAsync(context, e);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception e)
		{
			var error = new ErrorDetails
			{
				status_code = context.Response.StatusCode,
				message = "Internal Server Error"
			};

			switch (e)
			{
				case NotFoundException:
					error.status_code = (int)HttpStatusCode.NotFound;
					error.message = e.Message;
					error.path = context.Request.Path;
					context.Response.StatusCode = (int)HttpStatusCode.NotFound;
					break;
				case TokenNotValidException:
					error.status_code = (int)HttpStatusCode.Unauthorized;
					error.message = e.Message;
					error.path = context.Request.Path;
					context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
					break;
				case Exception:
					error.status_code = (int)HttpStatusCode.InternalServerError;
					error.message = e.Message;
					error.path = context.Request.Path;
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					break;
			}

			return context.Response.WriteAsJsonAsync(error);
		}
	}
}
