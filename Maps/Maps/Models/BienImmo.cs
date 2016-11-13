using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Maps
{
    public class BienImmo
    {
        string id;
		string titre;
		string description;
		double coordLong;
		double coordLat;
		double prix;
		double surface;

		[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		[JsonProperty(PropertyName = "titre")]
		public string Titre
		{
			get { return titre; }
			set { titre = value; }
		}

		[JsonProperty(PropertyName = "description")]
		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		[JsonProperty(PropertyName = "coordlong")]
		public double CoordLong
		{
			get { return coordLong; }
			set { coordLong = value; }
		}

		[JsonProperty(PropertyName = "coordlat")]
		public double CoordLat
		{
			get { return coordLat; }
			set { coordLat = value; }
		}

		[JsonProperty(PropertyName = "prix")]
		public double Prix
		{
			get { return prix; }
			set { prix = value; }
		}

		[JsonProperty(PropertyName = "surface")]
		public double Surface
		{
			get { return surface; }
			set { surface = value; }
		}

		[Version]
		public string Version { get; set; }

    }
}
