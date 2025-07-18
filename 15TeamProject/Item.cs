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
        private static int firstUID = 0;
        public int UID{get; private set;}
        public int ItemCount;
        

        public ItemData(int ItemID, string ItemName, int ItemType, int ItemValue, string ItemDesc, int ItemBuyPrice, int ItemSellPrice)
        {
            ItemIDs = ItemID;
            ItemNames = ItemName;
            ItemTypes = ItemType;
            this.ItemValue = ItemValue;
            this.ItemDesc = ItemDesc;
            this.ItemBuyPrice = ItemBuyPrice;
            this.ItemSellPrice = ItemSellPrice;
            this.UID = firstUID++;
            this.ItemCount = 1;
        }
    }

    internal static class ItemDB
    {
        // ItemType 0 : 무기, 1: 방어구, 10 :회복표션, 11 : 소비아이템
        public static List<ItemData> ItemList = new List<ItemData>()
        {
            new ItemData(0,"수련자 갑옷", 1, 5, "수련에 도움을 주는 갑옷입니다.", 1000, 800),
            new ItemData(1, "무쇠갑옷", 1, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000, 1600),
            new ItemData(2, "스파르타의 갑옷", 1, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, 2800),
            new ItemData(10, "낡은 검", 0, 2, "쉽게 볼 수 있는 낡은 검입니다.", 600, 480),
            new ItemData(11,"청동 도끼", 0, 5, "어디선가 사용했던거 같은 도끼입니다.", 1000, 800),
            new ItemData(12, "스파르타의 창", 0, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500, 2000),
            new ItemData(100, "포션", 10, 30, "사실 김치불고기버거입니다. 맛있습니다.", 500, 200),
            new ItemData(200, "C# 체크리스트 모음집", 11, 30, "성장을 쭉쭉 하게됩니다. ", 1500, 1200),
            new ItemData(500, "입장권", 50, 30, "펍에서 구매가능한 입장권 ", 500, 0),
            new ItemData(501, "노말 토큰", 50, 30, "펍에서 플레이시 획득 가능한 토큰", 0, 0),
            new ItemData(502, "프리미엄 토큰", 50, 30, "뭐? 이걸 갖고 있다고!!!!!! ", 0, 0),
            new ItemData(13, "무형검",0, 75, "공격력이 매우 강력하다고 알려진 희귀한 검입니다.", 200000, 150000),
            new ItemData(50, "무형검",0, 75, "공격력이 매우 강력하다고 알려진 희귀한 검입니다.", 3, 150000),
            new ItemData(51, "[1% 무형검] 상자",20, 1, "1% 확률로 무형검을 획득할 수 있는 상자입니다.", 10, 0),
            new ItemData(52, "[50% 무형검] 상자",20, 50, "50% 확률로 무형검을 획득할 수 있는 상자입니다.", 1, 0),
        };

        public static string TypeText(int ItemType)
        {
            return ItemType switch
            {
                0 => "공격력",
                1 => "방어력",
                10 => "체력",
                11 => "경험치",
                12 => "공격력",
                13 => "방어력",
                20 => "랜덤상자 (%)"
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
                    ItemData ItemTemplate = ItemDB.ItemList[Index];
                    ItemData newitem = new ItemData(ItemTemplate.ItemIDs, ItemTemplate.ItemNames, ItemTemplate.ItemTypes, ItemTemplate.ItemValue, ItemTemplate.ItemDesc, ItemTemplate.ItemBuyPrice, ItemTemplate.ItemSellPrice);
                    Inventory.inventory.Add(newitem);
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
                    ItemData ItemTemplate = ItemDB.ItemList[Index];
                    ItemData newitem = new ItemData(ItemTemplate.ItemIDs, ItemTemplate.ItemNames, ItemTemplate.ItemTypes, ItemTemplate.ItemValue, ItemTemplate.ItemDesc, ItemTemplate.ItemBuyPrice, ItemTemplate.ItemSellPrice);
                    Shop.Shop1hasItem.Add(newitem);
                }

            }
        }

        public static void TokenBoxAdd(int ItemID)
        {
            ItemData SearchItem = ItemDB.ItemList.Where(item => item.ItemIDs == ItemID).FirstOrDefault();
            if (SearchItem != null)
            {
                ItemData newItem = new ItemData(SearchItem.ItemIDs, SearchItem.ItemNames, SearchItem.ItemTypes, SearchItem.ItemValue, SearchItem.ItemDesc, SearchItem.ItemBuyPrice, SearchItem.ItemSellPrice);
                Pub.Tokenbox.Add(newItem);
            }
        }

        public static void NorTokenAdd(params int[] ItemIDs)
        {
            foreach (int ItemID in ItemIDs)
            {
                
                Pub.NorTokenbox.Add(ItemDB.ItemList.Where(item => item.ItemIDs == ItemID).FirstOrDefault());
            }
        }

        public static void PreTokenAdd(params int[] ItemIDs)
        {
            foreach (int ItemID in ItemIDs)
            {
                Pub.PreTokenbox.Add(ItemDB.ItemList.Where(item => item.ItemIDs == ItemID).FirstOrDefault());
            }
        }
        public static bool ChanceBox(int ItemID, int chance)
        { 
            int ITemChance = new Random().Next(0, 100);
            if(ITemChance < chance)
            {
                ItemData SearchItem = ItemDB.ItemList.Where(item => item.ItemIDs == ItemID).FirstOrDefault();
                ItemData newItem = new ItemData(SearchItem.ItemIDs, SearchItem.ItemNames, SearchItem.ItemTypes, SearchItem.ItemValue, SearchItem.ItemDesc, SearchItem.ItemBuyPrice, SearchItem.ItemSellPrice);
                Inventory.inventory.Add(newItem);
                return true;
            }
            return false;
        }
    }
}
