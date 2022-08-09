using System;

namespace MeetingPlanner.BusinessLayer.Entities
{
	public class Meeting
	{
		/// <summary>
		/// Unique identifier of meeting
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Name of meeting
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Start date of meeting: date + time
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// End date of meeting: date + time
		/// </summary>
		public DateTime EndDate { get; set; }

		/// <summary>
		/// Meeting place or link
		/// </summary>
		public string Place { get; set; }

		/// <summary>
		/// Notes about meeting
		/// </summary>
		public string Notes { get; set; }

		/// <summary>
		/// Period before showing a meeting notification (in minutes)
		/// </summary>
		public int NoticePeriodMin { get; set; }

		/// <summary>
		/// Notification start time
		/// </summary>
		public DateTime NotificationTime { get; set; }
	}
}
