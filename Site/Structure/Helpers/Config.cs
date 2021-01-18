﻿using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Structure.Helpers
{
	public static class Config
	{
		private static IConfiguration dic;

		public static String Login => dic["login"];

		public static String Pass => dic["pass"];

		public static String StoriesPath => dic["Stories"];

		public static String FtpAddress => dic["FtpAddress"];
		public static String FtpUrl => "ftp://" + FtpAddress;
		public static String FtpLogin => dic["FtpLogin"];
		public static String Site => dic["Site"];

		public static Boolean IsAuthor => Boolean.Parse(dic["IsAuthor"]);

		public static void Init(String environment = null)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appSettings.json", true)
				.AddJsonFile("db.json", true)
				.AddJsonFile("smtp.json", true);

			if (environment != null)
			{
				builder
					.AddJsonFile($"db.{environment}.json", true)
					.AddJsonFile($"smtp.{environment}.json", true);
			}

			dic = builder.Build();
		}
	}
}
