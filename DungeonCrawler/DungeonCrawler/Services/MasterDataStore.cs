

using DungeonCrawler.ViewModels;
using DungeonCrawler.Models;

namespace DungeonCrawler.Services
{
    public static class MasterDataStore
    {
        // Holds which datastore to use.

        private static DataStoreEnum _dataStoreEnum = DataStoreEnum.Sql;

        // Returns which dtatstore to use
        public static DataStoreEnum GetDataStoreMockFlag()
        {
            return _dataStoreEnum;
        }

        // Switches the datastore values.
        // Loads the databases
        public static void ToggleDataStore(DataStoreEnum dataStoreEnum)
        {
            switch (dataStoreEnum)
            {

                case DataStoreEnum.Mock:
                    _dataStoreEnum = DataStoreEnum.Mock;

                    ItemsViewModel.Instance.SetDataStore((BaseViewModel.DataStoreEnum)_dataStoreEnum);
                    MonsterViewModel.Instance.SetDataStore((BaseViewModel.DataStoreEnum)_dataStoreEnum);
                    CharacterViewModel.Instance.SetDataStore((BaseViewModel.DataStoreEnum)_dataStoreEnum);
                    ScoresViewModel.Instance.SetDataStore((BaseViewModel.DataStoreEnum)_dataStoreEnum);

                    break;

                case DataStoreEnum.Sql:
                default:
                    _dataStoreEnum = DataStoreEnum.Sql;
                    ItemsViewModel.Instance.SetDataStore((BaseViewModel.DataStoreEnum)_dataStoreEnum);
                    MonsterViewModel.Instance.SetDataStore((BaseViewModel.DataStoreEnum)_dataStoreEnum);
                    CharacterViewModel.Instance.SetDataStore((BaseViewModel.DataStoreEnum)_dataStoreEnum);
                    ScoresViewModel.Instance.SetDataStore((BaseViewModel.DataStoreEnum)_dataStoreEnum);
                    break;
            }

            // Load the Data
            ItemsViewModel.Instance.LoadDataCommand.CanExecute(null);
            ItemsViewModel.Instance.LoadDataCommand.Execute(null);

            MonsterViewModel.Instance.LoadDataCommand.CanExecute(null);
            MonsterViewModel.Instance.LoadDataCommand.Execute(null);

            CharacterViewModel.Instance.LoadDataCommand.CanExecute(null);
            CharacterViewModel.Instance.LoadDataCommand.Execute(null);

            ScoresViewModel.Instance.LoadDataCommand.CanExecute(null);
            ScoresViewModel.Instance.LoadDataCommand.Execute(null);


            // Have data refresh...
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            MonsterViewModel.Instance.SetNeedsRefresh(true);
            CharacterViewModel.Instance.SetNeedsRefresh(true);
            ScoresViewModel.Instance.SetNeedsRefresh(true);
        }
    }
}
