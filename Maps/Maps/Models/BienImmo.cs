using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Maps
{
    public class BienImmo
    {
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
    }
}
