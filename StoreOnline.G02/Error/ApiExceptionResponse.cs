namespace StoreOnline.G02.Error
{
    public class ApiExceptionResponse:ApiErrorResponse
    {
        public ApiExceptionResponse(int statuscode,string?message =null ,string?details =null):base(statuscode,message)
        {
            Details = details;
        }
        public string? Details { get; set; }
    }
}
