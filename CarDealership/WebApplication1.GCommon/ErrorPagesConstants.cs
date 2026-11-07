using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.GCommon
{
    public static class ErrorPagesConstants
    {
        //400 Bad request page
        public static class BadRequest
        {
            public const string Title = "400 Bad Request";
            public const string ErrorCode = "400";
            public const string ErrorTitle = "Bad Request";
            public const string ErrorMessage = "There was an error with your request. Please check your URL and try again!";
            public const string ErrorIcon = "bi-exclamation-triangle";
        }
        
        //401 Unauthorized
        public static class Unauthorized
        {
            public const string Title = "401 Unauthorized";
            public const string ErrorCode = "401";
            public const string ErrorTitle = "Unauthorized";
            public const string ErrorMessage = "You are NOT authorized to access this page";
            public const string ErrorIcon = "bi-shield-lock";
        }

        //403 Forbidden
        public static class Forbidden
        {
            public const string Title = "403 Forbidden";
            public const string ErrorCode = "403";
            public const string ErrorTitle = "Restricted";
            public const string ErrorMessage = "You have no permission to access this page";
            public const string ErrorIcon = "bi-shield-lock";
        }
        
        //404 Not Found
        public static class NotFound
        {
            public const string Title = "404 Not Found";
            public const string ErrorCode = "404";
            public const string ErrorTitle = "Not found";
            public const string ErrorMessage = "The page you are looking for has not been found. Please check your URL and try again!";
            public const string ErrorIcon = "bi-x-square";
        }

        //500 Internal Server Error
        public static class InternalServerError
        {
            public const string Title = "500 Internal Server Error";
            public const string ErrorCode = "500";
            public const string ErrorTitle = "Internal Server Error";
            public const string ErrorMessage = "There was an error with the server. Please come and try again!";
            public const string ErrorIcon = "bi-x-square";
        }
    }
}
