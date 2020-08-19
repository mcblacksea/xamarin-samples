//#define OFFLINE_SYNC_ENABLED
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using PaperBoy.Common;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using PaperBoy.Common;
#endif

namespace PaperBoy.Data
{
    public partial class FavoriteManager
    {
        static FavoriteManager defaultInstans = new FavoriteManager();
        MobileServiceClient client;
#if OFFLINE_SYNC_ENABLED
        IMobileServiceSyncTable<Favorite> favoritesTable;
#else
        IMobileServiceTable<Favorite> favoritesTable;
#endif
        private FavoriteManager()
        {
            this.client = new MobileServiceClient(CoreConstants.ApplicationURL);
#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore("localfavorites.db");
            store.DefineTable<Favorite>();

            this.client.SyncContext.InitializeAsync(store);

            this.favoritesTable = client.GetSyncTable<Favorite>();
#else
            this.favoritesTable=client.GetTable<Favorite>();
#endif

        }

        public static  FavoriteManager DefaultManager
        {
            get => defaultInstans;
            set => defaultInstans = value;
        }

        public MobileServiceClient CurrentClient { get=>client; }

        public bool IsOfflineEnabled
        {
            get=>favoritesTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<Favorite>;
        }

        public async Task<ObservableCollection<Favorite>> GetFavoritesAsync(bool syncItems=false)
        {
            try
            {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                IEnumerable<Favorite> items = await favoritesTable.ToEnumerableAsync();

                return new ObservableCollection<Favorite>(items);

            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch(Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task SaveFavoriteAsync(Favorite item)
        {
            try
            {
                if (item.Id == null)
                {
                    await favoritesTable.InsertAsync(item);
                }
                else
                {
                    await favoritesTable.UpdateAsync(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"SaveFavoriteAsync error: {0}", ex.Message);
            }
        }
#if OFFLINE_SYNC_ENABLED
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.client.SyncContext.PushAsync();

                await this.favoritesTable.PullAsync("allFavorites", this.favoritesTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult!=null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
                
            }

            if (syncErrors !=null)
            {
                foreach (var error in syncErrors)
                {
                    if(error.OperationKind==MobileServiceTableOperationKind.Update && error.Result !=null)
                    {
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);

                }
            }
        }
#endif
    }
}
