using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace aoc.puzzles
{
	abstract class Day
	{
		#region Public

		public const string projectFolderName = "aoc";
		public const string codeFolderName = "puzzles";

		public static string GetBasePath()
		{
			return IsLinux() ? Environment.CurrentDirectory : Assembly.GetExecutingAssembly().Location
						.Substring(0, Assembly.GetExecutingAssembly().Location.LastIndexOf(projectFolderName) + projectFolderName.Length);
		}

		public abstract string ExecuteFirst();
		public abstract string ExecuteSecond();

		#endregion

		#region Private

		private static bool IsLinux()
		{
			int p = (int)Environment.OSVersion.Platform;
			return (p == 4) || (p == 6) || (p == 128);
		}

		private static string GetRessourcePath(string fileName = null)
		{
			try
			{
				// Get longest caller path
				var caller = new StackTrace().GetFrames().Select(x =>
				{
					string s = x.GetMethod().DeclaringType.AssemblyQualifiedName;
					return s.Substring(0, s.IndexOf(',')).Split('.');
				}).ToArray().Aggregate((x, y) => x.Length > y.Length ? x : y);

				string path = Path.Combine(GetBasePath(), codeFolderName, caller[2].Substring(1), "ressources", $"day_{caller[3].Substring(3)}", fileName);
				return path;
			}
			catch (Exception) { return null; }
		}

		private static string GetUrlFromPath(string path)
		{
			int year = int.Parse(path.Substring(path.IndexOf($"{codeFolderName}/") + $"{codeFolderName}/".Length, 4));
			int day = int.Parse(path.Substring(path.IndexOf("day_") + "day_".Length, 2));
			return $"https://adventofcode.com/{year}/day/{day}/input";
		}

		private static string DownloadIfNotExists(string path)
		{
			if (path == null)
				return path;

			if (!File.Exists(path))
			{
				Console.WriteLine("File not found locally. Proceeding to download from AoC's website.");
				using (var client = new WebClient())
				{
					string session = "53616c7465645f5fbd365523b8606559343c4883fdee8f3c75cd846ae012b5be9cd1e9437f6d10c15ec8cb265d80a1ea";
					client.Headers.Add(HttpRequestHeader.Cookie, $"session={session}");

					string url = GetUrlFromPath(path);
					client.DownloadFile(url, path);
					Console.WriteLine($"File downloaded from url '{url}'.");
				}
			}

			return path;
		}

		#endregion

		#region Protected

		protected static string[] GetFileLines(string fileName = "input.txt")
		{
			try
			{
				return File.ReadAllLines(DownloadIfNotExists(GetRessourcePath(fileName)));
			}
			catch (Exception e) { Console.WriteLine(e.Message); return null; }
		}
		protected static string GetFileText(string fileName = "input.txt")
		{
			try { return File.ReadAllText(DownloadIfNotExists(GetRessourcePath(fileName))); }
			catch (Exception e) { Console.WriteLine(e.Message); return null; }
		}

		protected static void WriteToFile(string line, string fileName = "output.txt")
				=> WriteToFile(new List<string>() { line }, fileName);

		protected static void WriteToFile(List<string> lines, string fileName = "output.txt")
				=> File.WriteAllLines(GetRessourcePath(fileName), lines);

		#endregion
	}
}
