namespace Domain.Wrappers
{
    public class Response<T>
    {
        public bool Successed { get; set; }
        public List<string> Errors { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public Response() : this(default(T), null)
        {

        }

        public Response(T data, string message = null)
        {
            this.Successed = true;
            this.Data = data;
            this.Message = message;
        }

        public Response(string message = null)
        {
            this.Successed = false;
            this.Message = message;
        }


    }
}
