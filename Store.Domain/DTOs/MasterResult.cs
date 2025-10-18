
namespace Store.Domain.DTOs
{
    public class MasterResult<T>
    {
        public List<T> Data { get; set; }

        public int StatusCode { get; set; }

        public int? ResultCode { get; set; }

        public string? ErrorDescription { get; set; }

        public string? ErrorDetail { get; set; }

        public IDictionary<string, string[]>? Errors { get; set; }


        public static MasterResult<T> ReturnData(List<T> data, int statusCode, int? ResultCode, string? ErrorDescription, string? ErrorDetail = "", IDictionary<string, string[]>? Errors = null)
        {
            return new MasterResult<T>
            {
                Data = data,
                StatusCode = statusCode,
                ResultCode = ResultCode,
                ErrorDescription = ErrorDescription,
                ErrorDetail = ErrorDetail,
                Errors = Errors
            };
        }
    }
}
