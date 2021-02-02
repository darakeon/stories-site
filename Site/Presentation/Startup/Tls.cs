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
				var certPath = Path.Combine("..", "cert", "is_issued");

				if (!context.Request.IsHttps && File.Exists(certPath))
				{
					var url = context.Request
						.GetDisplayUrl()
						.Insert(4, "s");

					context.Response.Redirect(url, false);
				}
				else
				{
					await next();
				}
			});
		}
	}
}
