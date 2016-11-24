using System;
using Maps;
using Xamarin.Forms;
using Maps.iOS;
using System.IO;

[assembly: Dependency(typeof(SQLite_iOS))]

namespace Maps.iOS
{
	public class SQLite_iOS : ISQLite
	{
		public SQLite_iOS()
		{
		}

		#region ISQLite implementation
		public SQLite.SQLiteConnection GetConnection()
		{
			var sqliteFilename = "BienImmo.db3";
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);

			// This is where we copy in the prepopulated database
			Console.WriteLine(path);
			if (!File.Exists(path))
			{
				File.Copy(sqliteFilename, path);
			}

			var conn = new SQLite.SQLiteConnection(path);

			// Return the database connection 
			return conn;
		}
		#endregion
	}
}
