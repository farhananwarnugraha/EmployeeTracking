namespace employeetracking.Models
{
    public class PaginatiosViewModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRows { get; set; }
        public int TotalPage {
            get
            {
                return (int)Math.Ceiling((double)TotalRows / (double)PageSize);
            } 
        }
    }
}
