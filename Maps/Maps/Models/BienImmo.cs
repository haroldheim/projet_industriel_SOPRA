using System;
using Newtonsoft.Json;
using SQLite;

namespace Maps
{
    public class BienImmo
    {
		[PrimaryKey]
		public int Id { get; set; }

		public string Titre { get; set; }

		public string Description { get; set ; }

		public string Type { get ; set ; }

		public double CoordLong { get ; set ; }

		public double CoordLat { get; set ; }

		public double Prix { get; set ; }

		public double Surface { get; set ; }

		public string Adresse { get; set ; }

		public byte[] Photo { get; set; }

		public string TypeBien
		{
			get;
			set;
		}

		public string Soustitre
		{
			get;
			set;
		}

		public string Telephone
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		public string Cp
		{
			get;
			set;
		}

		public string Ville
		{
			get;
			set;
		}
    }
}
