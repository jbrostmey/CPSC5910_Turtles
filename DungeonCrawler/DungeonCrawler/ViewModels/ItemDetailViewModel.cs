using System;

namespace DungeonCrawler
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Data { get; set; }
        public ItemDetailViewModel(Item data = null)
        {
            Title = data?.Text;
            Data = data;
        }
    }
}
