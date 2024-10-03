﻿using Bookstore.Core.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Core.Base
{
	public class BaseResponse<T>
	{
		public T? Data { get; set; }
		public string? Message { get; set; }
		public StatusCodeHelper StatusCode { get; set; }
		public string? Code { get; set; }
		public BaseResponse(StatusCodeHelper statusCode, string code, T? data, string? message)
		{
			Data = data;
			Message = message;
			StatusCode = statusCode;
			Code = code;
		}

		public BaseResponse(StatusCodeHelper statusCode, string code, T? data)
		{
			Data = data;
			StatusCode = statusCode;
			Code = code;
		}

		public BaseResponse(StatusCodeHelper statusCode, string code, string? message)
		{
			Message = message;
			StatusCode = statusCode;
			Code = code;
		}

		public static BaseResponse<T> OkResponse(T? data, string? mess)
		{
			return new BaseResponse<T>(StatusCodeHelper.OK, StatusCodeHelper.OK.Name(), data, mess);
		}
		public static BaseResponse<T> OkResponse(string? mess)
		{
			return new BaseResponse<T>(StatusCodeHelper.OK, StatusCodeHelper.OK.Name(), mess);
		}

		public static BaseResponse<T> UnauthorizedResponse(string mess)
		{
			return new BaseResponse<T>(StatusCodeHelper.Unauthorized, StatusCodeHelper.Unauthorized.Name(), mess);
		}

		public static BaseResponse<T> NotFoundResponse(string mess)
		{
			return new BaseResponse<T>(StatusCodeHelper.NotFound, StatusCodeHelper.NotFound.Name(), mess);
		}

		public static BaseResponse<T> BadRequestResponse(string mess)
		{
			return new BaseResponse<T>(StatusCodeHelper.BadRequest, StatusCodeHelper.BadRequest.Name(), mess);
		}

		public static BaseResponse<T> InternalErrorResponse(string mess)
		{
			return new BaseResponse<T>(StatusCodeHelper.ServerError, StatusCodeHelper.ServerError.Name(), mess);
		}

	}
}
