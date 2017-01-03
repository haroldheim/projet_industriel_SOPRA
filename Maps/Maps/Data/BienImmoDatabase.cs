﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

		public IEnumerable<BienImmoLight> GetBiensLight(RequestGPSDto req)
		{
			String isMaison = "";
			String isAppartement = "";

			if (req.filtre.isMaison)
				isMaison = "Maison";
			if (req.filtre.isAppartement)
				isAppartement = "Appart";

			double earthRadius = 6378137;
			double latPlus = req.coordLat + (req.filtre.aireRecherche / earthRadius) * (180 / Math.PI);
			double latMoins = req.coordLat - (req.filtre.aireRecherche / earthRadius) * (180 / Math.PI);
			double longMoins = req.coordLong - (req.filtre.aireRecherche / (earthRadius * Math.Cos(Math.PI * req.coordLat / 180))) * (180 / Math.PI);
			double longPlus = req.coordLong + (req.filtre.aireRecherche / (earthRadius * Math.Cos(Math.PI * req.coordLat / 180))) * (180 / Math.PI);


			Debug.WriteLine("reqfiltre isMaison : " + req.filtre.surfaceMin);
			Debug.WriteLine("reqfiltre isAppart : " + req.filtre.surfaceMax);
			lock (locker)
			{
				return (from i in database.Table<BienImmoLight>().Where(u=>(u.typeBien == isMaison
				                                                            || u.typeBien == isAppartement)
				                                                        && u.coordLat >= latMoins
				                                                        && u.coordLat <= latPlus
				                                                        && u.coordLong >= longMoins
				                                                        && u.coordLong <= longPlus
				                                                        && u.surface >= req.filtre.surfaceMin
				                                                        && u.surface <= req.filtre.surfaceMax
				                                                        && u.prix >= req.filtre.prixMin
				                                                        && u.prix <= req.filtre.prixMax) select i).ToList();
			}
		}

		public BienImmo GetSingleBien(int id)
		{
			lock (locker)
			{
				return database.Table<BienImmo>().FirstOrDefault(u => u.Id == id);
			}
		}

		public BienImmoLight GetSingleBienLight(int id)
		{
			lock (locker)
			{
				return database.Table<BienImmoLight>().FirstOrDefault(u => u.Id == id);
			}
		}

		public int SaveBien(BienImmoLight item)
		{
			lock (locker)
			{
				if (GetSingleBienLight(item.Id) != null)
				{
					//Debug.WriteLine(item.Titre + " est deja dans la base");
					return database.Update(item);
				}
				else
					return database.Insert(item);
			}
		}

		public int SaveBienDetailed(BienImmo item)
		{
			lock (locker)
			{
				if (GetSingleBien(item.Id) != null)
				{
					Debug.WriteLine(item.Titre + " est deja dans la base");
					return database.Update(item);
				}
				else
					return database.Insert(item);
			}
		}

		public void displayTable()
		{
			var query = database.Table<BienImmoLight>();

			foreach (var stock in query)
				Debug.WriteLine("Stock: " + stock.Titre);
		}
	}
}
