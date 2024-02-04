namespace Typhoon.Core
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public byte[]? RowVersion { get; set; }
    }
}
