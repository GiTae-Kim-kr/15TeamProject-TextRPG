using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class ItemData
    {
        public string ItemNames;
        public int ItemTypes; // 0: 무기, 1: 방어구
        public int ItemValue;
        public string ItemDesc;
        public int ItemBuyPrice;
        public int ItemSellPrice;

        public ItemData(string ItemName, int ItemType, int ItemValue, string ItemDesc, int ItemBuyPrice, int ItemSellPrice)
        {
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
        public static List<ItemData> ItemList = new List<ItemData>()
        {
            new ItemData("수련자 갑옷", 1, 5, "수련에 도움을 주는 갑옷입니다.", 1000, 800),
            new ItemData("무쇠갑옷", 1, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000, 1600),
            new ItemData("스파르타의 갑옷", 1, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다..", 3500, 2800),
            new ItemData("낡은 검", 0, 2, "쉽게 볼 수 있는 낡은 검입니다.", 600, 480),
            new ItemData("청동 도끼", 0, 5, "어디선가 사용했던거 같은 도끼입니다.", 1000, 800),
            new ItemData("스파르타의 창", 0, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500, 2000),
        };
    }
}
