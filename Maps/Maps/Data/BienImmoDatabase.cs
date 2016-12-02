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
			database = DependencyService.Get<ISQLite>().GetConnection();
			database.CreateTable<BienImmo>();
			database.CreateTable<BienImmoLight>();
		}

		public IEnumerable<BienImmo> GetBiens()
		{
			lock (locker)
			{
				return (from i in database.Table<BienImmo>() select i).ToList();
			}
		}

		public IEnumerable<BienImmoLight> GetBiensLight()
		{
			lock (locker)
			{
				return (from i in database.Table<BienImmoLight>() select i).ToList();
			}
		}

		public int SaveBien(BienImmoLight item)
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
