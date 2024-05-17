using System.Collections.Generic;
using System.Linq;
using Test.Database.Helper;
using Test.Database.Model;

namespace Test.Database.Repository
{
    public abstract class BaseRepository<T>
        where T : BaseData
    {
        private Dictionary<int, T> datas = null;

        public BaseRepository(string path)
        {
            datas = JsonHelper.ReadDatas<T>(path).ToDictionary(x => x.id);
        }

        public T GetData(int id)
        {
            datas.TryGetValue(id, out var data);
            return data;
        }

        public List<T> GetDatas()
        {
            return datas.Values.ToList();
        }
    }

    public sealed class ItemQuantityModel
    {
        public Item item        = null;
        public int  quantity    = 0;

        public ItemQuantityModel(Item item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }

    public sealed class ItemGroupRepository
    {
        public Dictionary<int, List<ItemQuantityModel>> datas = null;


        public ItemGroupRepository(BaseRepository<Item> itemRepository, string path)
        {
            var itemGroups = JsonHelper.ReadDatas<ItemGroup>(path);
            foreach(var itemGroup in itemGroups)
            {
                if (itemGroup.itemQuantities == null)
                    continue;
                var itemQuantities = itemGroup.itemQuantities.Select(iq =>
                {
                    var item = itemRepository.GetData(iq.item_id);
                    return new ItemQuantityModel(item, iq.quantity);
                }).ToList();
                datas.Add(itemGroup.id, itemQuantities);
            }
        }
    }
}
