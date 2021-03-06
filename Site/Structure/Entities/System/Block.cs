﻿using System;
using System.Collections.Generic;
using System.Linq;
using Structure.Enums;

namespace Structure.Entities.System
{
	public class Block
	{
		public Block()
		{
			Episode = new Episode();

			ParagraphTypeList = new List<ParagraphType>();
			TalkList = new List<Talk>();
			TellerList = new List<Teller>();
		}

		public Block(Int32 position) : this()
		{
			var @char = Convert.ToChar('a' + position);
			ID = @char.ToString();
		}


		public String ID { get; set; }

		public Episode Episode { get; set; }

		public IList<ParagraphType> ParagraphTypeList { get; set; }
		public IList<Talk> TalkList { get; set; }
		public IList<Teller> TellerList { get; set; }
		public Int32 PageCount { get; set; }

		public override String ToString()
		{
			return ID;
		}

		public Int32 ParagraphCount => 
			TalkList.Where(p => p.IsFilled()).Count()
			+ TellerList.Where(p => p.IsFilled()).Count()
			+ PageCount;
	}
}
