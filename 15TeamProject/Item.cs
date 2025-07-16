using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class ItemData
    {
        public int ItemIDs;
        public string ItemNames;
        public int ItemTypes; // 0: 무기, 1: 방어구
        public int ItemValue;
        public string ItemDesc;
        public int ItemBuyPrice;
        public int ItemSellPrice;

        public ItemData(int ItemID, string ItemName, int ItemType, int ItemValue, string ItemDesc, int ItemBuyPrice, int ItemSellPrice)
        {
            ItemIDs = ItemID;
            ItemNames = ItemName;
            ItemTypes = ItemType;
            this.ItemValue = ItemValue;
            this.ItemDesc = ItemDesc;
            this.ItemBuyPrice = ItemBuyPrice;
            this.ItemSellPrice = ItemSellPrice;
        }
    }

    internal static class ItemDB
    {
        // ItemType 0 : 무기, 1: 방어구, 10 :회복표션, 11 : 소비아이템
        public static List<ItemData> ItemList = new List<ItemData>()
        {
            new ItemData(0,"수련자 갑옷", 1, 5, "수련에 도움을 주는 갑옷입니다.", 1000, 800),
            new ItemData(1, "무쇠갑옷", 1, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000, 1600),
            new ItemData(2, "스파르타의 갑옷", 1, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다..", 3500, 2800),
            new ItemData(10, "낡은 검", 0, 2, "쉽게 볼 수 있는 낡은 검입니다.", 600, 480),
            new ItemData(11,"청동 도끼", 0, 5, "어디선가 사용했던거 같은 도끼입니다.", 1000, 800),
            new ItemData(12, "스파르타의 창", 0, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500, 2000),
            new ItemData(100, "포션", 10, 30, "사실 김치불고기버거입니다. 맛있습니다.", 500, 200),
            new ItemData(200, "C# 체크리스트 모음집", 11, 30, "성장을 쭉쭉 하게됩니다. ", 1500, 1200),
        };

        public static string TypeText(int ItemType)
        {
            return ItemType switch
            {
                0 => "공격력",
                1 => "방어력",
                10 => "체력",
                11 => "경험치"
            };
        }
    }

    internal static class AddItem 
    {
        public static void Inven(params int[] ItemIDs)  // AddItem.Inven(ItemID,ItemID,ItemID);                 - AddItem.Inven(0, 1, 2, 10, 11, 12); 와 같은 형식으로 사용
        {
            foreach (int itemID in ItemIDs)
            {
                int Index = ItemDB.ItemList.FindIndex(item => item.ItemIDs == itemID);
                if (Index != -1)
                {
                    Inventory.inventory.Add(ItemDB.ItemList[Index]);
                }
            }
        }

        public static void Shop1(params int[] ItemIDs) // AddItem.Shop1(ItemID,ItemID,ItemID);                 - AddItem.Shop1(0, 1, 2, 10, 11, 12); 와 같은 형식으로 사용
        {
            foreach (int itemID in ItemIDs)
            {

                int Index = ItemDB.ItemList.FindIndex(item => item.ItemIDs == itemID);
                if (Index != -1)
                {
                    Shop.Shop1hasItem.Add(ItemDB.ItemList[Index]);
                }

                Shop.Shop1ItemCount.Add(1);
            }
        }
    }
}
