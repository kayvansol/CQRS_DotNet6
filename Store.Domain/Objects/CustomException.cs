namespace Store.Domain.Objects
{
    public class CustomException : Exception
    {

        public int StatusCode { get; set; }

        public string Message { get; set; }

    }
}
