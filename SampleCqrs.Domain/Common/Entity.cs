using System.ComponentModel.DataAnnotations;

namespace SampleCqrs.Domain.Common
{
    public abstract class Entity
    {
        [Key]
        public string Id { get; set; }
        public DateTime LastModified { get; set; }
        public string RowVersion { get; set; }
        //
        protected Entity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.LastModified = DateTime.UtcNow;
            this.RowVersion = Guid.NewGuid().ToString();
        }
        public void Update()
        {
            this.LastModified = DateTime.UtcNow;
            this.RowVersion = Guid.NewGuid().ToString();
        }
        //

    }
}
