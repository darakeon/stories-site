using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Extensions;

namespace Presentation.Startup
{
	public class Tls
	{
		public static void Https(IApplicationBuilder app)
		{
			app.Use(async (context, next) =>
			{
				if (!context.Request.IsHttps)
				{
					var certPath = Path.Combine("..", "cert", "is_issued");

					if (File.Exists(certPath))
					{
						var url = context.Request
							.GetDisplayUrl()
							.Insert(4, "s");

						context.Response.Redirect(url, false);

						return;
					}
				}

				await next();
			});
		}
	}
}
