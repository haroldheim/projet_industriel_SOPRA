﻿using System;
using SQLite;

namespace Maps
{
	public class BienImmoLight
	{
		[PrimaryKey, AutoIncrement]
		public int Id
		{
			get;
			set;
		}

		public string Titre
		{
			get;
			set;
		}

		public string sousTitre
		{
			get;
			set;
		}

		public double coordLong
		{
			get;
			set;
		}

		public double coordLat
		{
			get;
			set;
		}

		public string typeBien
		{
			get;
			set;
		}

		public double prix
		{
			get;
			set;
		}
	}
}
