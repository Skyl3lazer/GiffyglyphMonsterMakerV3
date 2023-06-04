using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiffyglyphMonsterMakerV3.Data
{
    [Table("Folders")]
    public class Folder
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        [ForeignKey("Id")]
        public string CreateUserId { get; set; }
        public Guid? ParentId { get; set; }
        public Folder Parent { get; set; }
        public ICollection<Folder> Children { get; set; }

        public Folder(string name)
        {
            Name = name;
        }
    }
}
