using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessServiceTemplate.Shared.DataAccess.Models
{
    /// <summary>
    /// Base Class for all entities, to provide implementations of common features, e.g. Equals and GetHashCode
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DbEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
    }
}
