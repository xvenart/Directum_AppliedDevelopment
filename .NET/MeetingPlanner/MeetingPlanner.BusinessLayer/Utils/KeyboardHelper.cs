using MeetingPlanner.BusinessLayer.Constants;
using MeetingPlanner.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingPlanner.BusinessLayer.Utils
{
	/// <summary>
	/// Keyboard assistant
	/// </summary>
	public class KeyboardHelper
	{
		/// <summary>
		/// Determine pressed key
		/// </summary>
		/// <param name="meetings">List of meetings</param>
		/// <param name="button">Key pressed information</param>
		public static void DefineButtons(List<Meeting> meetings, ConsoleKeyInfo button)
		{
			switch (button.Key)
			{
				case ConsoleKey.C:
					CreateMeeting(meetings);
					break;
				case ConsoleKey.U:
					UpdateMeeting(meetings);
					break;
				case ConsoleKey.D:
					DeleteMeeting(meetings);
					break;
				case ConsoleKey.S:
					ShowMeetings(meetings);
					break;
				case ConsoleKey.E:
					ExportMeetings(meetings);
					break;

				default:
					break;
			}

			Console.WriteLine(MessageConstants.AnyKeyMessage);
			Console.ReadKey();
		}

		private static void CreateMeeting(List<Meeting> meetings)
		{
			Meeting newMeeting = ConsoleHelper.CreateMeeting(meetings.Count);
			if (newMeeting is null) return;
			meetings.Add(newMeeting);
		}

		private static void UpdateMeeting(List<Meeting> meetings)
		{
			int? meetingId = ConsoleHelper.TryGetIdValue("Enter meeting ID:");

			if (meetingId == null) return;

			Meeting meeting = meetings.FirstOrDefault(m => m.Id == meetingId);

			if (meeting is null)
			{
				Console.WriteLine(MessageConstants.NotFoundError, meetingId);
				return;
			}

			ConsoleHelper.UpdateMeeting(meeting);
		}

		private static void DeleteMeeting(List<Meeting> meetings)
		{
			int? meetingId = ConsoleHelper.TryGetIdValue("Enter ID of deleting meeting:");

			if (meetingId == null) return;

			Meeting removingMeeting = meetings.FirstOrDefault(m => m.Id == meetingId);

			if (removingMeeting is null)
			{
				Console.WriteLine(MessageConstants.NotFoundError, meetingId);
				return;
			}

			meetings.Remove(removingMeeting);
		}

		private static void ShowMeetings(List<Meeting> meetings)
		{
			ConsoleHelper.ShowHeaderForDisplay();
			DefineShowButtons(meetings);
		}

		private static void DefineShowButtons(List<Meeting> meetings)
		{
			ConsoleKeyInfo button = Console.ReadKey();
			Console.WriteLine();

			switch (button.Key)
			{
				case ConsoleKey.A:
					ConsoleHelper.ShowMeetings(meetings);
					break;
				case ConsoleKey.O:
					ConsoleHelper.ShowMeetings(ConsoleHelper.GetMeetingsPerDay(meetings));
					break;

				default:
					break;
			}
		}

		private static void ExportMeetings(List<Meeting> meetings)
		{
			List<Meeting> oneDayMeetings = ConsoleHelper.GetMeetingsPerDay(meetings);
			string destination = ConsoleHelper.GetExportPath();
			FileExporter.ExportToTxt(oneDayMeetings, destination);
		}
	}
}
