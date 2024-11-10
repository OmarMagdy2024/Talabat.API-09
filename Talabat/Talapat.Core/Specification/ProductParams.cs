namespace Talabat.API.Helpers
{
    public class ProductParams
    {
        public string? sort { get; set; }
        public int? brandid { get; set; }
        public int? categoryid { get; set; }

        private int Pagesize;

        public int PageSize
        {
            get { return Pagesize; }
            set { Pagesize = value>10?10:value; }
        }
        public int pageindex { get; set; } = 1; 
        public bool Ispagination { get; set; }
    }
}
