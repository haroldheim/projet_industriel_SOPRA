using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;

namespace Maps
{
	public class BienImmoDatabase
	{
		static object locker = new object ();
		SQLiteConnection database;

		public BienImmoDatabase()
		{
			database.CreateTable<BienImmo>();
		}

		public IEnumerable<BienImmo> GetBiens()
		{
			lock (locker)
			{
				return (from i in database.Table<BienImmo>() select i).ToList();
			}
		}

		public int SaveBien(BienImmo item)
		{
			lock (locker)
			{
				if (item.Id != 0)
				{
					database.Update(item);
					return item.Id;
				}
				else {
					return database.Insert(item);
				}
			}
		}
	}
}
