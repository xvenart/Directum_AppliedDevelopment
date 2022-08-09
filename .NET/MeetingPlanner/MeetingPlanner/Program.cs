using MeetingPlanner.BusinessLayer.Entities;
using MeetingPlanner.BusinessLayer.Utils;
using System;
using System.Collections.Generic;

namespace MeetingPlanner
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<Meeting> meetings = new List<Meeting>();

			ConsoleKeyInfo button;

			do
			{
				Console.Clear();

				NotificationManager.CheckStartDates(meetings);

				ConsoleHelper.ShowHeader();

				button = Console.ReadKey();
				Console.WriteLine();

				KeyboardHelper.DefineButtons(meetings, button);
			}
			while (button.Key != ConsoleKey.Q && !button.Modifiers.HasFlag(ConsoleModifiers.Control));
		}
	}
}
