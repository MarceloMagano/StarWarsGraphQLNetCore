using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsGraphQLNetCore.Core.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CharacterEpisode> CharacterEpisodes { get; set; }
        public virtual ICollection<CharacterFriend> CharacterFriends { get; set; }
        public virtual ICollection<CharacterFriend> FriendCharacters { get; set; }
    }
}
