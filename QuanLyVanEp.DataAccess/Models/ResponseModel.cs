namespace QuanLyVanEp.DataAccess.Models
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public int StatusCode { get; set; } = 200;

        public ResponseModel<T> Successed(T data)
        {
            IsSuccess = true;
            Data = data;
            return this;
        }

        public ResponseModel<T> Successed()
        {
            IsSuccess = true;
            return this;
        }

        public ResponseModel<T> Failed(string msg = "")
        {
            IsSuccess = false;
            Message = msg;
            return this;
        }

        public ResponseModel<T> NotFound()
        {
            IsSuccess = false;
            Message = "Không tìm thấy dữ liệu";
            return this;
        }

        public ResponseModel<T> NotFound(string msg)
        {
            IsSuccess = false;
            Message = msg;
            return this;
        }

        public ResponseModel<T> Forbiden()
        {
            IsSuccess = false;
            Message = "Access denied! You do not have permission to access.";
            return this;
        }

        public ResponseModel<T> UnAuthorize()
        {
            IsSuccess = false;
            Message = "Please login first!";
            return this;
        }
    }

    public class MyException
    {
        public string Message { get; set; }

        public string InnerMessage { get; set; }

        public string StackTrace { get; set; }
    }
}
