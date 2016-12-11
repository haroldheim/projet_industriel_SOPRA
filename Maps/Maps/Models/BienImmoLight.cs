﻿using System;
using SQLite;

namespace Maps
{
	public class BienImmoLight
	{
		[PrimaryKey]
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

		public string type
		{
			get;
			set;
		}

		public string photo
		{
			get;
			set;
		}

		public double prix
		{
			get;
			set;
		}

		public double surface
		{
			get;
			set;
		}

		public string typeTitre => string.Format("{0} - {1}", type, Titre);
		public string prixEuro=> string.Format("{0} € - {1} m²", prix, surface);
	}
}
