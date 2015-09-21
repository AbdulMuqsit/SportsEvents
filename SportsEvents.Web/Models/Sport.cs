﻿using System.Collections.Generic;

namespace SportsEvents.Web.Models
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Event> Events { get; set; }

    }
}