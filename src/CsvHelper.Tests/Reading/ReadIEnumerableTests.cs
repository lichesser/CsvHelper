using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper.Configuration;
using CsvHelper.Tests.Mocks;
#if WINRT_4_5
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace CsvHelper.Tests.Reading
{
	[TestClass]
	public class ReadIEnumerableTests
	{
		[TestMethod]
		public void ListGenericRangeWithNoEndTest()
		{
			var queue = new Queue<string[]>();
			queue.Enqueue( new[] { "1", "one", "1", "2", "3" } );
			queue.Enqueue( null );
			var parserMock = new ParserMock( queue );
			var csv = new CsvReader( parserMock );
			csv.Configuration.HasHeaderRecord = false;
			csv.Configuration.RegisterClassMap<ListRangeWithNoEndMap>();
			var records = csv.GetRecords<ListRange>().ToList();

			Assert.IsNotNull( records[0].ListData );
			Assert.AreEqual( 3, records[0].ListData.Count );
			Assert.AreEqual( 1, records[0].ListData[0] );
			Assert.AreEqual( 2, records[0].ListData[1] );
			Assert.AreEqual( 3, records[0].ListData[2] );
		}

		[TestMethod]
		public void ListGenericRangeWithEndTest()
		{
			var queue = new Queue<string[]>();
			queue.Enqueue( new[] { "1", "one", "1", "2", "3" } );
			queue.Enqueue( null );
			var parserMock = new ParserMock( queue );
			var csv = new CsvReader( parserMock );
			csv.Configuration.HasHeaderRecord = false;
			csv.Configuration.RegisterClassMap<ListRangeWithEndMap>();
			var records = csv.GetRecords<ListRange>().ToList();

			Assert.IsNotNull( records[0].ListData );
			Assert.AreEqual( 2, records[0].ListData.Count );
			Assert.AreEqual( 1, records[0].ListData[0] );
			Assert.AreEqual( 2, records[0].ListData[1] );
		}

		[TestMethod]
		public void IListGenericTest()
		{
			var queue = new Queue<string[]>();
			queue.Enqueue( new[] { "1", "one", "1", "2", "3" } );
			queue.Enqueue( null );
			var parserMock = new ParserMock( queue );
			var csv = new CsvReader( parserMock );
			csv.Configuration.HasHeaderRecord = false;
			csv.Configuration.RegisterClassMap<IListGenericRangeWithEndMap>();
			var records = csv.GetRecords<IListGenericRange>().ToList();

			Assert.IsNotNull( records[0].ListData );
			Assert.AreEqual( 3, records[0].ListData.Count );
			Assert.AreEqual( 1, records[0].ListData.ElementAt( 0 ) );
			Assert.AreEqual( 2, records[0].ListData.ElementAt( 1 ) );
			Assert.AreEqual( 3, records[0].ListData.ElementAt( 2 ) );
		}

		[TestMethod]
		public void IListTest()
		{
			var queue = new Queue<string[]>();
			queue.Enqueue( new[] { "1", "one", "1", "2", "3" } );
			queue.Enqueue( null );
			var parserMock = new ParserMock( queue );
			var csv = new CsvReader( parserMock );
			csv.Configuration.HasHeaderRecord = false;
			csv.Configuration.RegisterClassMap<IListRangeWithEndMap>();
			var records = csv.GetRecords<IListRange>().ToList();

			Assert.IsNotNull( records[0].ListData );
			Assert.AreEqual( 3, records[0].ListData.Count );
			Assert.AreEqual( "1", records[0].ListData[0] );
			Assert.AreEqual( "2", records[0].ListData[1] );
			Assert.AreEqual( "3", records[0].ListData[2] );
		}

		[TestMethod]
		public void ArrayListTest()
		{
			var queue = new Queue<string[]>();
			queue.Enqueue( new[] { "1", "one", "1", "2", "3" } );
			queue.Enqueue( null );
			var parserMock = new ParserMock( queue );
			var csv = new CsvReader( parserMock );
			csv.Configuration.HasHeaderRecord = false;
			csv.Configuration.RegisterClassMap<ArrayListRangeWithNoEndMap>();
			var records = csv.GetRecords<ArrayListRange>().ToList();

			Assert.IsNotNull( records[0].ListData );
			Assert.AreEqual( 3, records[0].ListData.Count );
			Assert.AreEqual( "1", records[0].ListData[0] );
			Assert.AreEqual( "2", records[0].ListData[1] );
			Assert.AreEqual( "3", records[0].ListData[2] );
		}

		[TestMethod]
		public void ArrayTest()
		{
			var queue = new Queue<string[]>();
			queue.Enqueue( new[] { "1", "one", "1", "2", "3" } );
			queue.Enqueue( null );
			var parserMock = new ParserMock( queue );
			var csv = new CsvReader( parserMock );
			csv.Configuration.HasHeaderRecord = false;
			csv.Configuration.RegisterClassMap<ArrayRangeWithNoEndMap>();
			var records = csv.GetRecords<ArrayRange>().ToList();

			Assert.IsNotNull( records[0].ListData );
			Assert.AreEqual( 3, records[0].ListData.Length );
			Assert.AreEqual( 1, records[0].ListData[0] );
			Assert.AreEqual( 2, records[0].ListData[1] );
			Assert.AreEqual( 3, records[0].ListData[2] );
		}

		private class ListRange
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public List<int> ListData { get; set; }
		}

		private sealed class ListRangeWithNoEndMap : CsvClassMap<ListRange>
		{
			public ListRangeWithNoEndMap()
			{
				Map( m => m.Id );
				Map( m => m.Name );
				Map( m => m.ListData ).IndexRange( 2 );
			}
		}

		private sealed class ListRangeWithEndMap : CsvClassMap<ListRange>
		{
			public ListRangeWithEndMap()
			{
				Map( m => m.Id );
				Map( m => m.Name );
				Map( m => m.ListData ).IndexRange( 2, 3 );
			}
		}

		private class IListGenericRange
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public IList<int> ListData { get; set; }
		}

		private sealed class IListGenericRangeWithEndMap : CsvClassMap<IListGenericRange>
		{
			public IListGenericRangeWithEndMap()
			{
				Map( m => m.Id );
				Map( m => m.Name );
				Map( m => m.ListData ).IndexRange( 2 );
			}
		}

		private class IListRange
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public IList ListData { get; set; }
		}

		private sealed class IListRangeWithEndMap : CsvClassMap<IListRange>
		{
			public IListRangeWithEndMap()
			{
				Map( m => m.Id );
				Map( m => m.Name );
				Map( m => m.ListData ).IndexRange( 2 );
			}
		}

		private class ArrayListRange
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public ArrayList ListData { get; set; }
		}

		private sealed class ArrayListRangeWithNoEndMap : CsvClassMap<ArrayListRange>
		{
			public ArrayListRangeWithNoEndMap()
			{
				Map( m => m.Id );
				Map( m => m.Name );
				Map( m => m.ListData ).IndexRange( 2 );
			}
		}

		private class ArrayRange
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public int[] ListData { get; set; }
		}

		private sealed class ArrayRangeWithNoEndMap : CsvClassMap<ArrayRange>
		{
			public ArrayRangeWithNoEndMap()
			{
				Map( m => m.Id );
				Map( m => m.Name );
				Map( m => m.ListData ).IndexRange( 2 );
			}
		}
	}
}
