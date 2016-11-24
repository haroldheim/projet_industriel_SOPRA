using System;
using SQLite;

namespace Maps
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}
