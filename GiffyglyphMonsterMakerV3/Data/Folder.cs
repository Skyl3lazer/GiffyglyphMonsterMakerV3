using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiffyglyphMonsterMakerV3.Data
{
    [Table("Folders")]
    public class Folder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Id")]
        public string CreateUserId { get; set; }
        public HierarchyId HierarchyId { get; set; }
        [NotMapped]
        public HierarchyId OldHierarchyId { get; set; }

        public Folder(string name)
        {
            Name = name;
        }
    }
}
