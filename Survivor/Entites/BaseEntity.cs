namespace Survivor.Entites
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } =DateTime.Now;
        public DateTime ModifiedDate { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
