using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class Shop
    {
        public void ShopUI()
        {
            List<int> Shop1hasItem = new List<int>() { 0, 1, 2, 3, 4, 5 }; // shop1 이 가지고 있는 아이템ID 넣기, itemID는 item.cs에서 itemlist에 들어가있는 순서임
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine("얼마 있소(플레이어에서 가져오기)");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Shop1hasItem.Count; i++)
            {
                ItemData item = ItemDB.ItemList[Shop1hasItem[i]];
                Console.WriteLine($"- {i + 1}. {item.ItemNames}    |    {(item.ItemTypes == 0 ? "공격력" : "방어력")} + {item.ItemValue}    |    {item.ItemDesc}");
            }
        }

    }
}
