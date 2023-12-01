namespace Domain.Wrappers
{
    public class PageResponse<T> : Response<T>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public PageResponse(T data, int pageNumber, int pageSize, int totalCount = 0)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            /*this.totalCount = (int)data.GetType().
                GetProperty("Count").GetValue(data);*/
            this.Data = data;
            this.Message = null;
            this.Successed = true;
            this.Errors = null;
        }


    }
}
