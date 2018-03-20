using System;
using System.Collections.Generic;
using SQLite;

namespace DungeonCrawler.Models
{
    //Class build to deal with the inventory for the character
    public class BaseCharacter : Actor
    {
        public BaseCharacter()
        {
            MiracleMaxLive = true;
        }

        //Init base character attributes
        public BaseCharacter(Character character)
        {
            MiracleMaxLive = character.MiracleMaxLive;
            AttributeString = character.AttributeString;
            ImageURI = character.ImageURI;
            characterClass = character.characterClass;
            Id = character.Id;
            name = character.name;
            description = character.description;
            number = character.number;
        }

        // for the MiracleMax event
        public bool MiracleMaxLive { get; set; }

        public string AttributeString { get; set; }

        public string ImageURI { get; set; }

        //Allows for getting/setting of the character class. Players can choose from
        //  one of 6 character classes. This is where it is stored. May change
        //  to an enum in the future.
        public string characterClass { get; set; }

        [PrimaryKey]
        public string Id { get; set; }

    }
}
