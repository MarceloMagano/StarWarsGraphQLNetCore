using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsGraphQLNetCore.Core.Models
{
    public class Human : Character
    {
        public Planet HomePlanet { get; set; }
    }
}
