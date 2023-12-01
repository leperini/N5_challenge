namespace Domain.Models
{
    public class PaginatorModel
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }


        public string OrderBy { get; set; }

        public PaginatorModel()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
            this.OrderBy = "ASC";
        }

        public PaginatorModel(int pageNumber, int pageSize, string orderBy)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize < 1 ? 10 : pageSize;
            this.OrderBy = orderBy;
        }
    }
}
