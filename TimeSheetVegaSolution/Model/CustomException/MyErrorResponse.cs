using System;

namespace Model.CustomException
{
    public class MyErrorResponse
    {
        public MyErrorResponse(Exception ex, int code)
        {
            Status = code;
            Type = ex.GetType().Name;
            Message = ex.Message;
        }

        public int Status { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }  
    }
}
