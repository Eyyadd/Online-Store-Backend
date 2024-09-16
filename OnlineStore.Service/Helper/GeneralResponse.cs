using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Helper
{
    public class GeneralResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public GeneralResponse(bool IsSuccess,string Message,T data)
        {
            Success = IsSuccess;
            this.Message = Message;
            this.Data = data;
        }
        public GeneralResponse(bool IsSuccess,string Message)
        {
            Success = IsSuccess;
            this.Message = Message;
            Data = default;
        }

    }
}
