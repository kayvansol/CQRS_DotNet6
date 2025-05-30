﻿
namespace Store.Domain.DTOs
{
    public class ErrorDto
    {
        public int? ErrorCode { get; set; }

        public int? StatusCode { get; set; }

        public string? ErrorDescription { get; set; }

        public string? ErrorDetail { get; set; }

        public IDictionary<string, string[]>? Errors { get; set; }
        
    }
}
