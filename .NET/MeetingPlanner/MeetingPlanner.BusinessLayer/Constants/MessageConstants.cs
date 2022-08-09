namespace MeetingPlanner.BusinessLayer.Constants
{
	/// <summary>
	/// Constant values for planner
	/// </summary>
	public class MessageConstants
	{
		public static string AnyKeyMessage = "Press any key to exit the menu";

		public static string NotFoundError
		{
			get
			{
				return "Meeting with ID = {0} not found";
			}
		}

		public static string DefaultExtension = ".txt";

		public static string DateFormat = "yyyy-MM-dd HH:mm:ss";
	}
}
