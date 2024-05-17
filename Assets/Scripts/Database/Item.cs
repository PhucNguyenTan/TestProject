using System;


namespace Test.Database.Model
{
    [Serializable]
    public abstract class BaseData
    {
        public int id = 0;
    }

    [Serializable]
    public class Item : BaseData
    {
        public int type = 0;
        public string name = string.Empty;
        public string description = string.Empty;
        public int imageId = 0;
    }

    [Serializable]
    public class ItemQuantity : BaseData
    {
        public int item_id = 0;
        public int quantity = 0;
    }

    [Serializable]
    public class ItemGroup : BaseData
    {
        public ItemQuantity[] itemQuantities = null;
    }

    [Serializable]
    public class ItemExchange : BaseData
    {
        public int item_exchange_id_trade = 0;
        public int item_exchange_id_gain  = 0;
    }
}
