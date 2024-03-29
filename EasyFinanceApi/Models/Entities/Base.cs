namespace EasyFinanceApi.Models.Entities
{
    public class Base
    {
        public int Id { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? CreationUserId { get; set; }

        public int? UpdateUserId { get; set; }
    }
}
