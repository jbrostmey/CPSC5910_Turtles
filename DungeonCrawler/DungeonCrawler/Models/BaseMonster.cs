using System;
using SQLite;
namespace DungeonCrawler.Models
{
    public class BaseMonster : Actor
    {
        public BaseMonster()
        {
        }

        public BaseMonster(BaseMonster monster)
        {
            AttributeString = monster.AttributeString;
            ImageURI = monster.ImageURI;
            Id = monster.Id;
            name = monster.name;
            description = monster.description;
            number = monster.number;
        }

        [PrimaryKey]
        public string Id { get; set; }

        public string ImageURI { get; set; }

        public string AttributeString { get; set; }
    }
}
