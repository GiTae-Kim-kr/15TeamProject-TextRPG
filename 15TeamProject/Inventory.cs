using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class Inventory
    {
        private static string[] ItemNames = { "수련자의 갑옷", "무쇠갑옷", "스파르타의 갑옷", "낡은 검", "청동 도끼", "스파르타의 창" };
        private static int[] ItemTypes = { 1, 1, 1, 0, 0, 0 }; //0: 무기, 1: 상의
        private static int[] ItemValue = { 5, 9, 15, 2, 5, 7 };
        private static string[] ItemDesc = { "수련에 도움을 주는 갑옷입니다.", "무쇠로 만들어져 튼튼한 갑옷입니다.", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", "쉽게 볼 수 있는 낡은 검입니다.", "어디선가 사용했던거 같은 도끼입니다.", "스파르타의 전사들이 사용했다는 전설의 창입니다." };
        private static int[] ItemPrice = { 1000, 2000, 3500, 600, 1000, 2500 };

        private static List<int> inventory = new List<int>();
        private static List<int> equipList = new List<int>();
        private static bool equipmentWeapon = false;
        private static bool equipmentArmor = false;


        public void InventoryUI() //Inventory UI Scene
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            if (inventory.Count == 0)
            {
                inventory.Add(1);
                inventory.Add(2);
                inventory.Add(3);
                inventory.Add(4);
                inventory.Add(5);
                inventory.Add(6);
            }
            for (int i = 0; i < inventory.Count; i++)
            {
                int TargetItem = inventory[i] - 1;
                bool IsEquipped = equipList.Contains(TargetItem);
                if (IsEquipped)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                string EquipDisplay = IsEquipped ? "[E]" : "";
                Console.WriteLine($"- {i + 1}. {EquipDisplay} {ItemNames[TargetItem]}    |    {(ItemTypes[TargetItem] == 0 ? "공격력" : "방어력")} + {ItemValue[TargetItem]}    |    {ItemDesc[TargetItem]}");
                if (IsEquipped)
                {
                    Console.ResetColor();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            while (true)
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>    ");

                int input;
                bool x = int.TryParse(Console.ReadLine(), out input);  // x는 TryParse를 사용하기 위한 임의적인 매개변

                switch (input)
                {
                    case 0:
                        StartScene.Instance.GameStartScene();
                        break;

                    case 1:
                        EquipUI();
                        break;
                    default:
                        Console.WriteLine("올바른 숫자를 입력해주세요.");
                        break;

                }
            }
        }

        public void EquipUI() // 장비 장착 관리
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventory.Count; i++)
            {
                int TargetItem = inventory[i] - 1;
                bool IsEquipped = equipList.Contains(TargetItem);
                if (IsEquipped)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                string EquipDisplay = IsEquipped ? "[E]" : "";
                Console.WriteLine($"- {i + 1}. {EquipDisplay} {ItemNames[TargetItem]}    |    {(ItemTypes[TargetItem] == 0 ? "공격력" : "방어력")} + {ItemValue[TargetItem]}    |    {ItemDesc[TargetItem]}");
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

            int input;
            bool x = int.TryParse(Console.ReadLine(), out input);  // x는 TryParse를 사용하기 위한 임의적인 매개변수

            if (x == false) // 입력 제대로 했는지 확인
            {
                Console.WriteLine("올바른 숫자를 입력해주세요.");
            }
            else // 원하는 번호를 제대로 입력한 경우
            {
                if (input == 0) InventoryUI(); // 0번 누르면 인벤토리로 이동

                else if (input >= 1 && input <= inventory.Count) // 장비를 선택한 경우
                {
                    if (equipList.Contains(input - 1)) // 이미 장착한 장비를 선택했을 때
                    {
                        if (Inventory.ItemTypes[input - 1] == 0)// 장착한 무기 해제
                        {
                            equipList.Remove(input - 1);
                            equipmentWeapon = false;
                            Console.Write("장비를 해제했습니다. (Enter키 입력 시 진행)");
                            Console.ReadLine();
                            EquipUI();
                        }
                        else if (Inventory.ItemTypes[input - 1] == 1)
                        {
                            equipList.Remove(input - 1);
                            equipmentArmor = false;
                            Console.Write("장비를 해제했습니다. (Enter키 입력 시 진행)");
                            Console.ReadLine();
                            EquipUI();
                        }
                    }
                    else // 올바른 장비를 선택했을 때
                    {
                        if (Inventory.ItemTypes[input - 1] == 0) // 무기인 경우
                        {
                            if (equipmentWeapon == false)
                            {
                                equipmentWeapon = true;
                                equipList.Add(input - 1);
                                Console.Write("장비를 장착했습니다. (Enter키 입력 시 진행)");
                                Console.ReadLine();
                                EquipUI();
                            }
                            else
                            {
                                Console.WriteLine("해당 부위는 이미 장비를 착용하고 있습니다.\n 장착한 장비를 해제후 사용해주세요. (Enter키 입력 시 진행)");
                                Console.ReadLine();
                                EquipUI();
                            }
                        }
                        else if (Inventory.ItemTypes[input - 1] == 1) // 방어구인 경우
                        {
                            if (equipmentArmor == false)
                            {
                                equipmentArmor = true;
                                equipList.Add(input - 1);
                                Console.Write("장비를 장착했습니다. (Enter키 입력 시 진행)");
                                Console.ReadLine();
                                EquipUI();
                            }
                            else
                            {
                                Console.WriteLine("해당 부위는 이미 장비를 착용하고 있습니다.\n 장착한 장비를 해제후 사용해주세요. (Enter키 입력 시 진행)");
                                Console.ReadLine();
                                EquipUI();
                            }
                        }
                    }
                }
            }

        }
    }
}
