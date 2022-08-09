using MeetingPlanner.BusinessLayer.Entities;
using System;
using System.Collections.Generic;

namespace MeetingPlanner.BusinessLayer.Utils
{
	/// <summary>
	/// Manager of notifications
	/// </summary>
	public class NotificationManager
	{
		/// <summary>
		/// Checking start dates for notification display
		/// </summary>
		/// <param name="meetings">List of all meetings</param>
		public static void CheckStartDates(List<Meeting> meetings)
		{
			foreach (Meeting meeting in meetings)
			{
				if (meeting?.NotificationTime <= DateTime.Now && meeting?.StartDate >= DateTime.Now)
				{
					ConsoleHelper.PrintNotification(meeting);
				}
			}
		}
	}
}
