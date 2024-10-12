namespace Survivor.Entites
{
    public class CategoryEntity :BaseEntity
    {
        public string Name { get; set; }

        public ICollection<CompetitorEntity> Competitors { get; set; }
    }
}
