using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Maps
{
	public partial class BienImmoManager
	{
		static BienImmoManager defaultInstance = new BienImmoManager();
		MobileServiceClient client;

		IMobileServiceTable<BienImmo> bienimmoTable;

		private BienImmoManager()
		{
			this.client = new MobileServiceClient(Constants.ApplicationURL);

			this.bienimmoTable = client.GetTable<BienImmo>();
		}

		public static BienImmoManager DefaultManager
		{
			get
			{
				return defaultInstance;
			}
			private set
			{
				defaultInstance = value;
			}
		}

		public MobileServiceClient CurrentClient
		{
			get { return client; }
		}

		public async Task<ObservableCollection<BienImmo>> GetBienImmoAsync(bool syncItems = false)
		{
			try
			{
				IEnumerable<BienImmo> biens = await bienimmoTable
					.ToEnumerableAsync();

				return new ObservableCollection<BienImmo>(biens);
			}
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
			}
			catch (Exception e)
			{
				Debug.WriteLine(@"Sync error: {0}", e.Message);
			}
			return null;
		}

		public async Task SaveTaskAsync(BienImmo bien)
		{
			if (bien.Id == null)
			{
				await bienimmoTable.InsertAsync(bien);
			}
			else 
			{
				await bienimmoTable.UpdateAsync(bien);
			}

		}
	}
}
