using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using System.IO;
using CsvHelper;

namespace CsvHelperSample
{
	[Register("SampleViewController")]
	public class SampleViewController : UIViewController
	{
		public SampleViewController()
		{
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view

			var path = Path.Combine(NSBundle.MainBundle.ResourcePath, "Sample.csv");

			var converter = new CustomTypeTypeConverter();

			// start reading
			using (var stream = File.OpenRead(path))
			using (var reader = new StreamReader(stream))
			using (var csv = new CsvReader(reader))
			{
				while (csv.Read())
				{
					var str = csv.GetField<string>("StringColumn");
					var num = csv.GetField<int>("NumberColumn");
					var cust = csv.GetField<CustomType>("CustomTypeColumn", converter);

					Console.WriteLine("{0} ({1}) = {2}", str, num, cust);
				}
			}
		}
	}
}