using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _15TeamProject
{
    internal class Inventory
    {
        private static List<int> inventory = new List<int>();       // 인벤토리에 있는 장비
        private static List<int> equipList = new List<int>();       // 장착한 장비
        private static int[] equipmentWeapon = new int[1] { -1 };   // 무기 칸에 어떤 장비가 있는지 확인. -1 이 초기화값
        private static int[] equipmentArmor = new int[1] { -1 };    // 방어구 칸에 어떤 장비가 있는지 확인. -1 이 초기화값


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
                ItemData item = ItemDB.ItemList[TargetItem];
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
                ItemData item = ItemDB.ItemList[TargetItem];
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
                    ItemData item = ItemDB.ItemList[input -1 ];
                    if (equipList.Contains(input - 1)) // 이미 장착한 장비를 선택했을 때
                    {
                        if (item.ItemTypes == 0)// 장착한 무기 해제
                        {
                            equipList.Remove(input - 1);            // 장착 리스트에서 제거
                            equipmentWeapon[0] = -1;                // 무기 칸 초기화라는 의미
                            Console.Write("장비를 해제했습니다. (Enter키 입력 시 진행)");
                            Console.ReadLine();
                            EquipUI();
                        }
                        else if (item.ItemTypes == 1) // 장착한 방어구 해제
                        {
                            equipList.Remove(input - 1);            // 장착 리스트 제거
                            equipmentArmor[0] = -1;                 // 방어구 칸 초기화라는 의미
                            Console.Write("장비를 해제했습니다. (Enter키 입력 시 진행)");
                            Console.ReadLine();
                            EquipUI();
                        }
                    }
                    else // 새로운 장비를 선택했을 때 
                    {
                        if (item.ItemTypes == 0) // 무기인 경우
                        {
                            if (equipmentWeapon[0] != -1)             // 이미 "무기 칸"에 무기 장비가 있다면
                            {
                                equipList.Remove(equipmentWeapon[0]); // 그 무기 장비를 "장착 리스트"에서 제거
                            }
                            equipmentWeapon[0] = input - 1;           // "무기 칸"에 해당 장비가 있음을 표시
                            equipList.Add(input - 1);                 // "장착 리스트"에 장비(무기) 추가
                            Console.Write("장비를 장착했습니다. (Enter키 입력 시 진행)");
                            Console.ReadLine();
                            EquipUI();

                        }
                        else if (item.ItemTypes == 1) // 방어구인 경우
                        {
                            if (equipmentArmor[0] != -1)                // 이미 "방어구 칸"에 방어구 장비가 있다면
                            {
                                equipList.Remove(equipmentArmor[0]);    // 그 방어구 장비를 "장착 리스트"에서 제거
                            }
                            equipmentArmor[0] = input - 1;              // "방어구 칸"에 해당 방어구가 있음을 표시
                            equipList.Add(input - 1);                   // "장착 리스트:에 장비(방어구) 추가
                            Console.Write("장비를 장착했습니다. (Enter키 입력 시 진행)");
                            Console.ReadLine();
                            EquipUI();
                           
                        }
                    }
                }
            }

        }
    }
}
