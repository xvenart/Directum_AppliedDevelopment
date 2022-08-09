using MeetingPlanner.BusinessLayer.Constants;
using MeetingPlanner.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MeetingPlanner.BusinessLayer.Utils
{
	/// <summary>
	/// Helper for console output
	/// </summary>
	public class ConsoleHelper
	{
		/// <summary>
		/// Displaying a header with a hint about working keys
		/// </summary>
		public static void ShowHeader()
		{
			Console.WriteLine($"==================================================================");
			Console.WriteLine($"Use the keyboard to perform operations");
			Console.WriteLine($"==================================================================");
			Console.WriteLine($"C - create meeting");
			Console.WriteLine($"U - update meeting");
			Console.WriteLine($"D - delete meeting");
			Console.WriteLine($"E - export to file");
			Console.WriteLine($"S - show meetings");
			Console.WriteLine($"Ctrl + Q - exit");
			Console.WriteLine($"==================================================================");
		}

		/// <summary>
		/// Displaying a header with a hint about the keys for displaying meetings
		/// </summary>
		public static void ShowHeaderForDisplay()
		{
			Console.Clear();
			Console.WriteLine($"==================================================================");
			Console.WriteLine("A - for show all meetings");
			Console.WriteLine("O - for show meetings per day");
			Console.WriteLine($"==================================================================");
		}

		/// <summary>
		/// Show list of meetings
		/// </summary>
		/// <param name="meetings">List of displayed meetings</param>
		public static void ShowMeetings(List<Meeting> meetings)
		{
			foreach (Meeting meeting in meetings)
			{
				ShowMeeting(meeting);
			}
		}

		/// <summary>
		/// Get a meeting as a string
		/// </summary>
		/// <param name="meeting">Info about meeting</param>
		/// <returns></returns>
		public static string GetMeetingString(Meeting meeting)
		{
			return $"***\n" +
				$"\tName: {meeting.Name} (ID: {meeting.Id})\n" +
				$"\tStart date: {meeting.StartDate}\n" +
				$"\tEnd date: {meeting.EndDate}\n" +
				$"\tPlace: {meeting.Place}\n" +
				$"\tNotes: {meeting.Notes}\n" +
				$"\tNotification time: {meeting.NotificationTime}\n" +
				$"***";
		}

		/// <summary>
		/// Create meeting
		/// </summary>
		/// <param name="id">Future meeting ID (current count of meetings)</param>
		/// <returns>Meeting object</returns>
		public static Meeting CreateMeeting(int id)
		{
			Console.Clear();

			Console.WriteLine("*** CREATE MEETING ***");
			Meeting meeting = new Meeting();

			meeting.Id = id;

			Console.WriteLine($"Enter name:");
			meeting.Name = Console.ReadLine();

			Console.WriteLine($"Enter start date (Format: {MessageConstants.DateFormat}):");
			string startDateString = Console.ReadLine();
			if (DateTime.TryParse(startDateString, out DateTime startDate))
			{
				if (startDate < DateTime.Now)
				{
					Console.WriteLine("Error with meeting Start Date:\nCan't create a meeting for the previous day!");
					return null;
				}
				meeting.StartDate = startDate;
			}
			else
			{
				Console.WriteLine("Error while parse meeting Start Date");
				return null;
			}

			Console.WriteLine($"Enter end date (Format: {MessageConstants.DateFormat}):");
			string endDateString = Console.ReadLine();
			if (DateTime.TryParse(endDateString, out DateTime endDate))
			{
				if (endDate < DateTime.Now)
				{
					Console.WriteLine("Error with meeting End Date:\n\tThe End Date of the meeting cannot be earlier than the Start Date!");
					return null;
				}
				meeting.EndDate = endDate;
			}
			else
			{
				Console.WriteLine("Error while parse meeting End Date");
				return null;
			}

			Console.WriteLine($"Enter place:");
			meeting.Place = Console.ReadLine();

			Console.WriteLine($"Enter notes:");
			meeting.Notes = Console.ReadLine();

			Console.WriteLine($"Enter notification time (min):");
			string notificationTimeString = Console.ReadLine();
			int noticePeriodMin = default;
			if (string.IsNullOrEmpty(notificationTimeString) || int.TryParse(notificationTimeString, out noticePeriodMin))
			{
				if (noticePeriodMin < 0)
				{
					Console.WriteLine("Error with meeting Notification Time:\n\tNotification time cannot be negative!");
					return null;
				}
				meeting.NoticePeriodMin = noticePeriodMin;
				meeting.NotificationTime = noticePeriodMin > 0
					? meeting.StartDate.AddMinutes(noticePeriodMin * (-1))
					: meeting.StartDate;
			}
			else
			{
				Console.WriteLine("Error while parse meeting Notification Time");
				return null;
			}

			ShowMeeting(meeting);

			return meeting;
		}

		/// <summary>
		/// Update meeting
		/// </summary>
		/// <param name="meeting">Updated meeting</param>
		public static void UpdateMeeting(Meeting meeting)
		{
			Console.Clear();

			Console.WriteLine($"*** UPDATE MEETING ({meeting.Id}) ***");

			Console.WriteLine($"Enter name:");
			var nameString = Console.ReadLine();
			if (!string.IsNullOrEmpty(nameString))
			{
				meeting.Name = nameString;
			}

			Console.WriteLine($"Enter start date (Format: {MessageConstants.DateFormat}):");
			string startDateString = Console.ReadLine();
			if (!string.IsNullOrEmpty(startDateString) && DateTime.TryParse(startDateString, out DateTime startDate))
			{
				if (startDate < DateTime.Now)
				{
					Console.WriteLine("Error with meeting Start Date:\nCan't create a meeting for the previous day!");
					return;
				}
				meeting.StartDate = startDate;
				meeting.NotificationTime = meeting.StartDate.AddMinutes(meeting.NoticePeriodMin * (-1));
			}
			else
			{
				Console.WriteLine("Error while parse meeting Start Date");
				return;
			}

			Console.WriteLine($"Enter end date (Format: {MessageConstants.DateFormat}):");
			string endDateString = Console.ReadLine();
			if (!string.IsNullOrEmpty(endDateString) && DateTime.TryParse(endDateString, out DateTime endDate))
			{
				if (endDate < DateTime.Now)
				{
					Console.WriteLine("Error with meeting End Date:\n\tThe End Date of the meeting cannot be earlier than the Start Date!");
					return;
				}
				meeting.EndDate = endDate;
			}
			else
			{
				Console.WriteLine("Error while parse meeting End Date");
				return;
			}

			Console.WriteLine($"Enter place:");
			var placeString = Console.ReadLine();
			if (!string.IsNullOrEmpty(placeString))
			{
				meeting.Place = placeString;
			}

			Console.WriteLine($"Enter notes:");
			var notesString = Console.ReadLine();
			if (!string.IsNullOrEmpty(notesString))
			{
				meeting.Notes = notesString;
			}

			Console.WriteLine($"Enter notification time (min):");
			string notificationTimeString = Console.ReadLine();
			if (int.TryParse(notificationTimeString, out int noticePeriodMin))
			{
				if (noticePeriodMin < 0)
				{
					Console.WriteLine("Error with meeting Notification Time:\n\tNotification time cannot be negative!");
					return;
				}
				meeting.NoticePeriodMin = noticePeriodMin;
				meeting.NotificationTime = noticePeriodMin > 0
					? meeting.StartDate.AddMinutes(noticePeriodMin * (-1))
					: meeting.StartDate;
			}
			else
			{
				Console.WriteLine("Error while parse meeting Notification Time");
				return;
			}

			ShowMeeting(meeting);
		}

		/// <summary>
		/// Trying to get ID value
		/// </summary>
		/// <param name="message">Message for console output</param>
		/// <returns>Meeting ID</returns>
		public static int? TryGetIdValue(string message)
		{
			Console.WriteLine(message);
			var meetingIdString = Console.ReadLine();
			bool isSuccessParsing = int.TryParse(meetingIdString, out int meetingId);

			if (!isSuccessParsing)
			{
				Console.WriteLine("Error while parse meeting ID");
				return null;
			}

			return meetingId;
		}

		/// <summary>
		/// Get all meetings for a given date
		/// </summary>
		/// <param name="meetings">List of all meetings</param>
		/// <returns>List of meetings per day</returns>
		public static List<Meeting> GetMeetingsPerDay(List<Meeting> meetings)
		{
			Console.WriteLine("Enter date of meetings (Format: yyyy-MM-dd):");
			var periodStartString = Console.ReadLine();
			DateTime.TryParse(periodStartString, out DateTime periodStart);

			var periodEnd = periodStart.AddDays(1);

			var oneDayMeetings = meetings
				.Where(m => m.StartDate >= periodStart && m.StartDate <= periodEnd)
				.ToList();

			return oneDayMeetings;
		}

		/// <summary>
		/// Get parts of export file path
		/// </summary>
		/// <returns>Destination of the file</returns>
		public static string GetExportPath()
		{
			Console.WriteLine("Enter folder for file:");
			string folder = Console.ReadLine();

			Console.WriteLine("Enter file name:");
			string filename = Console.ReadLine();

			string destination = Path.Combine(folder, filename + MessageConstants.DefaultExtension);
			return destination;
		}

		/// <summary>
		/// Notification console output
		/// </summary>
		/// <param name="meeting"></param>
		public static void PrintNotification(Meeting meeting)
		{
			Console.WriteLine("!!! NOTIFICATION !!!");
			Console.WriteLine(GetMeetingString(meeting));
		}

		private static void ShowMeeting(Meeting meeting)
		{
			Console.WriteLine(GetMeetingString(meeting));
		}
	}
}
