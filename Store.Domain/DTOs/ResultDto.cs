using System.ComponentModel.DataAnnotations;

namespace Store.Domain.DTOs
{
    public class ResultDto<T>
    {
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public int? ResultCode  { get; set; }
        public string? Description { get; set; } = null;
        public string? ErrorDetail { get; set; }
        public IDictionary<string, string[]>? Errors { get; set; }

        public static ResultDto<T> ReturnData(T? data,int statuscode= (int) EnumResponseStatus.OK,int? ResulCode = (int)EnumResultCode.Success, string? Description = null, string? ErrorDetail = "", IDictionary<string,string[]>? Errors = null)
        {
            var enumResultCode = (EnumResultCode)ResulCode;

            return new ResultDto<T>
            {
                Data = data,
                StatusCode = 200,
                ResultCode = ResulCode,
                Description = Description ?? enumResultCode.GetType().GetMember(enumResultCode.ToString()).First().GetCustomAttribute<DisplayAttribute>()?.GetName(),
                ErrorDetail = ErrorDetail,
                Errors = Errors
            };
        }
    }
}
