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
        public Folder? Parent { get; set; }
        public ICollection<Folder> Children { get; set; } = new List<Folder>();
        public ICollection<Creature> Creatures { get; set; } = new List<Creature>();

        public Folder(string name, string createUserId)
        {
            Name = name;
            CreateUserId = createUserId;
        }
        //Maybe if I'm bored one day I'll optimize this to not recurse
        public bool HasChildMonsters()
        {
            if(Creatures.Count > 0)
            {
                return true;
            }

            bool hasChildMonsters = false;
            foreach(Folder folder in Children)
            {
                if (folder.HasChildMonsters())
                {
                    hasChildMonsters = true;
                }
            }

            return hasChildMonsters;
        }
    }
}
