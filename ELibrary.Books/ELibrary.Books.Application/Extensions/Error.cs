﻿namespace ELibrary.Books.Application.Extensions
{
    public class Error
    {
        public string Message { get; set; }

        public Error(string message)
        {
            Message = message;
        }
    }
}
