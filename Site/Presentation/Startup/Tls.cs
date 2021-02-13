using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Presentation.Startup
{
	public class Tls
	{
		public static void Https(IApplicationBuilder app)
		{
			app.Use(async (context, next) =>
			{
				var redirect = await genCertAndCheckRedirect(context);

				if (redirect)
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

		private static readonly String issuedPath = getPath("is_issued");
		private static readonly String letsEncryptPath = getPath("cert-lets-encrypt.sh");

		private static readonly Process process = new Process
		{
			StartInfo = new ProcessStartInfo
			{
				FileName = "bash",
				Arguments = $"-c {letsEncryptPath}",
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				UseShellExecute = false,
				CreateNoWindow = true,
			}
		};

		private static async Task<Boolean> genCertAndCheckRedirect(HttpContext context)
		{
			if (context.Request.IsHttps)
				return false;

			if (File.Exists(issuedPath))
			{
				var issuedContent = await File.ReadAllTextAsync(issuedPath);
				return Boolean.Parse(issuedContent);
			}

			await File.WriteAllTextAsync(issuedPath, false.ToString());

			if (!File.Exists(letsEncryptPath))
			{
				Console.WriteLine($"{letsEncryptPath} does not exist");
				return false;
			}

			var started = process.Start();

			Console.WriteLine(await File.ReadAllTextAsync("/etc/nginx/conf.d/default.conf"));

			if (!started)
			{
				Console.WriteLine($"Process could not be executed: {letsEncryptPath}");
				return false;
			}

			await process.WaitForExitAsync();
			var isOk = process.ExitCode == 0;

			var result = isOk
				? process.StandardOutput
				: process.StandardError;

			Console.WriteLine(
				await result.ReadToEndAsync()
			);

			if (isOk)
			{
				await File.WriteAllTextAsync(issuedPath, true.ToString());
			}

			return isOk;
		}

		private static string getPath(String file)
		{
			var relative = Path.Combine("..", "cert", file);
			var info = new FileInfo(relative);
			return info.FullName;
		}
	}
}
