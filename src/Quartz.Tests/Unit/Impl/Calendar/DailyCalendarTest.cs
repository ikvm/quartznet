/*
 * Copyright 2004-2006 OpenSymphony 
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not 
 * use this file except in compliance with the License. You may obtain a copy 
 * of the License at 
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0 
 *   
 * Unless required by applicable law or agreed to in writing, software 
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations 
 * under the License.
 */
using NUnit.Framework;

using Quartz.Impl.Calendar;

namespace Quartz.Tests.Unit.Impl.Calendar
{
	/// <summary>
	/// Unit test for DailyCalendar.
	/// </summary>
	[TestFixture]
	public class DailyCalendarTest
	{
		private static string[] VERSIONS = new string[] {"1.5.2"};

		[Test]
		public void TestStringStartEndTimes()
		{
			DailyCalendar dailyCalendar = new DailyCalendar("TestCal", "1:20", "14:50");
			Assert.IsTrue(dailyCalendar.ToString().IndexOf("01:20:00:000 - 14:50:00:000") > 0);

			dailyCalendar = new DailyCalendar("TestCal", "1:20:1:456", "14:50:15:2");
			Assert.IsTrue(dailyCalendar.ToString().IndexOf("01:20:01:456 - 14:50:15:002") > 0);
		}

		[Test]
		public void TestStringInvertTimeRange()
		{
			DailyCalendar dailyCalendar = new DailyCalendar("TestCal", "1:20", "14:50");
			dailyCalendar.InvertTimeRange = true;
			Assert.IsTrue(dailyCalendar.ToString().IndexOf("inverted: True") > 0);

			dailyCalendar.InvertTimeRange = false;
			Assert.IsTrue(dailyCalendar.ToString().IndexOf("inverted: False") > 0);
		}

		/// <summary>
		/// Get the object to serialize when generating serialized file for future
		/// tests, and against which to validate deserialized object.
		/// </summary>
		/// <returns></returns>
		protected object GetTargetObject()
		{
			DailyCalendar c = new DailyCalendar("TestCal", "01:20:01:456", "14:50:15:002");
			c.Description = "description";
			c.InvertTimeRange = true;

			return c;
		}

		/// <summary>
		/// Get the Quartz versions for which we should verify
		/// serialization backwards compatibility.
		/// </summary>
		/// <returns></returns>
		protected string[] GetVersions()
		{
			return VERSIONS;
		}


		/// <summary>
		/// Verify that the target object and the object we just deserialized 
		/// match.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="deserialized"></param>
		protected void VerifyMatch(object target, object deserialized)
		{
			DailyCalendar targetCalendar = (DailyCalendar) target;
			DailyCalendar deserializedCalendar = (DailyCalendar) deserialized;

			Assert.IsNotNull(deserializedCalendar);
			Assert.AreEqual(targetCalendar.Description, deserializedCalendar.Description);
			Assert.IsTrue(deserializedCalendar.InvertTimeRange);
			//Assert.IsNull(deserializedCalendar.TimeZone);
			Assert.IsTrue(deserializedCalendar.ToString().IndexOf("01:20:01:456 - 14:50:15:002") > 0);
		}
	}
}