using MeetingPlanner.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MeetingPlanner.BusinessLayer.Utils
{
	/// <summary>
	/// Meeting exporter
	/// </summary>
	public class FileExporter
	{
		/// <summary>
		/// Export meetings to a text file
		/// </summary>
		public static void ExportToTxt(List<Meeting> meetings, string fileDestination)
		{
			if (!File.Exists(fileDestination))
			{
				using (FileStream fs = File.Create(fileDestination))
				{
					string fileContent = GetContentByMeetings(meetings);
					byte[] bytes = new UTF8Encoding(true).GetBytes(fileContent);

					fs.Write(bytes, 0, bytes.Length);
				}
			}

			Console.WriteLine("Export done");
		}

		private static string GetContentByMeetings(List<Meeting> meetings)
		{
			StringBuilder sb = new StringBuilder();

			foreach (Meeting meeting in meetings)
			{
				sb.AppendLine(ConsoleHelper.GetMeetingString(meeting));
			}

			return sb.ToString();
		}
	}
}
