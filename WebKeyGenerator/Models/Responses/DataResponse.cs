﻿namespace WebKeyGenerator.Models.Responses
{
    public class DataResponse<T> : ErrorResponse
    {
        public T Data { get; set; }
    }
}
