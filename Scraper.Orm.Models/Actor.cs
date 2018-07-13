//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Actor.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the Actor type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Orm.Models
{
    public class Actor : IEntity
    {
        int IEntity.Id => this.ActorId;

        public int ActorId { get; set; }

        public string DateOfBirth { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public int ShowId { get; set; }

        public virtual Show Show { get; set; }
    }
}
