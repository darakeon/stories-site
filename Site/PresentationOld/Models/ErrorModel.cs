﻿using System;

namespace Presentation.Models
{
	public class ErrorModel : BaseModel
	{
		public String Message { get; set; }
		public String Stacktrace { get; set; }
	}
}