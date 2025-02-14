namespace Store.Domain.Objects
{
    public class BusinessException : Exception
    {

        public int Code { get; set; }

        public string Error { get; set; }

        public BusinessException(int Code, string Error)
        {
            this.Code = Code;
            this.Error = Error;
        }

        public override string ToString()
        {
            return $"ErrorCode: {Code} - ErrorMessage: {Error}";
        }

    }
}
