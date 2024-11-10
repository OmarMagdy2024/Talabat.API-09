namespace Talabat.API.Helpers
{
    public class OrderParams
    {
        public int? Id {  get; set; }
        public string Email { get; set; }
        public string? sort { get; set; }

        private int Pagesize;
        public int PageSize
        {
            get { return Pagesize; }
            set { Pagesize = value>10?10:value; }
        }
        public int pageindex { get; set; } = 1; 
        public bool Ispagination { get; set; }
        public string PaymentIntentId { get; set; }

    }
}
