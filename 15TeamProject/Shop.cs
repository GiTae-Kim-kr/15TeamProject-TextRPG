using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _15TeamProject;

namespace _15TeamProject
{
    internal class Shop
    {

        public static List<ItemData> Shop1hasItem = new List<ItemData> { };
        public static List<int> Shop1ItemCount = new List<int>() { }; // shop1 이 가지고 있는 아이템 수량


        public void ShopMainUI()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Player.Instance.gold} G");
            Console.WriteLine("");
            Console.WriteLine("[상점 목록]");
            Console.WriteLine("1. 구매 상점");
            Console.WriteLine("2. 판매 상점");
            Console.WriteLine("3. 포션 상점");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>    ");
            int input = Input.GetInt(0, 3);
            switch (input)
            {
                case 1:
                    ShopUI();
                    break;
                case 2:
                    ShopSellUI();
                    break;
                case 3:
                    ShopPotionUI();
                    break;
                case 0:
                    StartScene.Instance.GameStartScene();
                    break;
            }
        }


        public void ShopUI()
        {

            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Player.Instance.gold} G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            if (Shop1hasItem.Count == 0) AddItem.Shop1(0, 1, 2, 10, 11, 12, 12,12,12);

            for (int i = 0; i < Shop1hasItem.Count; i++)
            {
                ItemData item = Shop1hasItem[i];
                if (Shop1ItemCount[i] == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"- {item.ItemNames}    |    {ItemDB.TypeText(Shop1hasItem[i].ItemTypes)} + {item.ItemValue}    |    {item.ItemDesc}    |    구매완료");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"- {item.ItemNames}    |    {ItemDB.TypeText(Shop1hasItem[i].ItemTypes)} + {item.ItemValue}    |    {item.ItemDesc}    |    {item.ItemBuyPrice} G");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>    ");
            int input = Input.GetInt(0, 1);
            switch (input)
            {
                case 1:
                    ShopBuyUI();
                    break;
                case 0:
                    ShopMainUI();
                    break;
            }
        }

        public void ShopBuyUI()
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Player.Instance.gold} G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Shop1hasItem.Count; i++)
            {
                ItemData item = Shop1hasItem[i];
                if (Shop1ItemCount[i] == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"- {i + 1}. {item.ItemNames}    |    {ItemDB.TypeText(Shop1hasItem[i].ItemTypes)} + {item.ItemValue}    |    {item.ItemDesc}    |    구매완료");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"- {i + 1}. {item.ItemNames}    |    {ItemDB.TypeText(Shop1hasItem[i].ItemTypes)} + {item.ItemValue}    |    {item.ItemDesc}    |    {item.ItemBuyPrice} G");
                }

            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>    ");
            int input = Input.GetInt(0, Shop1hasItem.Count);
            if (input == 0)
            {
                ShopUI();
            }
            else
            {
                input -= 1;
                ItemData item = Shop1hasItem[input];
                if (Shop1ItemCount[input] >= 1)
                {
                    if (Player.Instance.gold >= item.ItemBuyPrice)  // 구매할 돈이 있을 때
                    {


                        int index = ItemDB.ItemList.IndexOf(item);
                        Inventory.inventory.Add(Shop1hasItem[input]);
                        Shop1ItemCount[input] -= 1;
                        Player.Instance.gold -= item.ItemBuyPrice;
                        if (item.ItemTypes == 10)
                        {
                            Player.Instance.potionCount++;
                            Inventory.inventory.Remove(Shop1hasItem[input]);
                        }
                        Console.WriteLine("구매를 완료하였습니다. (Enter키 입력 시 진행)");
                        Console.ReadLine();
                        ShopBuyUI();
                    }
                    else                                            //  구매할 돈이 없을 때
                    {
                        Console.WriteLine("골드가 부족합니다. (Enter키 입력 시 진행)");
                        Console.ReadLine();
                        ShopBuyUI();
                    }
                }
                else
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                }
            }
        }

        public void ShopSellUI()
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("아이템을 판매할 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Player.Instance.gold} G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Inventory.inventory.Count; i++)
            {
                ItemData item = Inventory.inventory[i];
                bool IsEquipped = Inventory.equipList.Contains(item);
                if (IsEquipped)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                string EquipDisplay = IsEquipped ? "[E]" : "";
                Console.WriteLine($"- {i + 1}. {EquipDisplay} {item.ItemNames}    |    {(item.ItemTypes == 0 ? "공격력" : "방어력")} + {item.ItemValue}    |    {item.ItemDesc}");
                if (IsEquipped)
                {
                    Console.ResetColor();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>    ");
            int input = Input.GetInt(0, Inventory.inventory.Count);
            if (input == 0)
            {
                ShopMainUI();
            }
            else
            {
                input -= 1;
                ItemData item = Inventory.inventory[input];
                bool IsEquipped = Inventory.equipList.Contains(item);
                if (IsEquipped)
                {
                    Console.WriteLine("장착한 장비는 판매할 수 없습니다. 장착 해제 후 다시 시도해주세요.(Enter키 입력 시 진행)");
                    Console.ReadLine();
                    ShopSellUI();
                }
                else
                {
                    Inventory.inventory.Remove(Inventory.inventory[input]);
                    Player.Instance.gold += item.ItemSellPrice;
                    Console.WriteLine($"판매하였습니다. {item.ItemSellPrice}G를 획득하였습니다.(Enter키 입력 시 진행)");
                    Console.ReadLine();
                    ShopSellUI();
                }
            }


            
        }

        int Shop1HPCount = -1; // 회복 포션 수량 초기화
        int Shop1MPCount = -1; // 회복 포션 수량 초기화

        public void ShopPotionUI()
        {
        if (Shop1HPCount ==-1 && Shop1MPCount ==-1) // 최초 1회 상점에 회복 포션 수량 세팅
            {
                Shop1HPCount = 3;
                Shop1MPCount = 3;
            }
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Player.Instance.gold} G");
            Console.WriteLine("");
            Console.WriteLine("[포션 목록]");
            if(Shop1HPCount == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(" - 1. 회복 포션    |    체력 + 30    |     500 G    |    구매완료");
                Console.ResetColor();
            }
            else Console.WriteLine($" - 1. 회복 포션    |    체력 + 30    |     500 G    |    남은 수량  {Shop1HPCount}개");
            if (Shop1MPCount == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($" - 2. 마나 포션    |    마나 + 30    |     500 G    |    구매완료");
                Console.ResetColor();
            }
            else Console.WriteLine($" - 2. 마나 포션    |    마나 + 30    |     500 G    |    남은 수량  {Shop1MPCount}개");
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>    ");
            int input = Input.GetInt(0, 2);
            switch (input)
            {
                case 1:
                    if (Shop1HPCount > 0)
                    {
                        Player.Instance.potionCount++;
                        Shop1HPCount--;
                        Player.Instance.gold -= 500;
                        Console.WriteLine("회복 포션을 구매하였습니다.(Enter키 입력 시 진행)");
                        Console.ReadLine();
                        ShopPotionUI();
                    }
                    else if (Shop1HPCount == 0)
                    {
                        Console.WriteLine("품절되었습니다.(Enter키 입력 시 진행)");
                        Console.ReadLine();
                        ShopPotionUI();
                    }
                    break;
                case 2:
                    if (Shop1MPCount > 0)
                    {
                        Shop1MPCount--;
                        Player.Instance.gold -= 500;
                        Console.WriteLine("마나 포션 획득(Enter키 입력 시 진행)");
                        Console.ReadLine();
                        ShopPotionUI();
                    }
                    else if (Shop1MPCount == 0)
                    {
                        Console.WriteLine("품절되었습니다.(Enter키 입력 시 진행)");
                        Console.ReadLine();
                        ShopPotionUI();
                    }
                    break;
                case 0:
                    ShopMainUI();
                    break;
            }
        }
    }

    
}
