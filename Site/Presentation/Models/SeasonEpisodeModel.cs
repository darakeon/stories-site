﻿using System;
using Structure.Entities;

namespace Presentation.Models
{
    public class SeasonEpisodeModel : BaseModel
    {
        public SeasonEpisodeModel()
        {
            Story = new Episode();
        }

        public Episode Story { get; set; }

        public String ReadingScene { get; set; }
        
    }
}