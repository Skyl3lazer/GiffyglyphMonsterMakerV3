using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class Folder
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Id")]
        public Folder ParentEncounter { get; set; }
        public ICollection<Folder> Children { get; set;}
        //public HeirarchyId HeirarchyId { get; set; }
    }
}
