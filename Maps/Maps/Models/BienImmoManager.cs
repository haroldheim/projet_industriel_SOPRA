//#define OFFLINE_SYNC_ENABLED

using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
#endif

namespace Maps
{
	public partial class BienImmoManager
	{
		static BienImmoManager defaultInstance = new BienImmoManager();
		MobileServiceClient client;

#if OFFLINE_SYNC_ENABLED
        IMobileServiceSyncTable<BienImmo> bienTable;
#else
		IMobileServiceTable<BienImmo> bienTable;
#endif

		const string offlineDbPath = @"localstore.db";

		private BienImmoManager()
		{
			this.client = new MobileServiceClient(Constants.ApplicationURL);

#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore(offlineDbPath);
            store.DefineTable<BienImmo>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            this.client.SyncContext.InitializeAsync(store);

            this.bienTable = client.GetSyncTable<BienImmo>();
#else
			this.bienTable = client.GetTable<BienImmo>();
#endif
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

		public bool IsOfflineEnabled
		{
			get { return bienTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<BienImmo>; }
		}

		public async Task<List<BienImmo>> GetBienImmo()
		{
			return new List<BienImmo>(await bienTable.ReadAsync());
		}

		public async Task<ObservableCollection<BienImmo>> GetTodoItemsAsync(bool syncItems = false)
		{
			try
			{
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
				IEnumerable<BienImmo> items = await bienTable
					.ToEnumerableAsync();

				return new ObservableCollection<BienImmo>(items);
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

		public async Task SaveTaskAsync(BienImmo item)
		{
			if (item.Id == null)
			{
				await bienTable.InsertAsync(item);
			}
			else
			{
				await bienTable.UpdateAsync(item);
			}
		}

#if OFFLINE_SYNC_ENABLED
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.client.SyncContext.PushAsync();

				await this.bienTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allBienImmo",
					this.bienTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }
#endif
	}
}
