using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace _15TeamProject
{
    internal class Inventory
    {
        public static List<ItemData> inventory = new List<ItemData>();        // 인벤토리에 있는 장비, itemID로 관리. item.cs에서 itemlist에 들어가있는 순서임
        public static List<ItemData> equipList = new List<ItemData>();       // 장착한 장비, itemID로 관리. item.cs에서 itemlist에 들어가있는 순서임
        public static ItemData[] equipmentWeapon = new ItemData[1] { null };   // 무기 칸에 어떤 장비가 있는지 확인.
        public static ItemData[] equipmentArmor = new ItemData[1] { null };    // 방어구 칸에 어떤 장비가 있는지 확인.
        private static List<ItemData> invenEquip = new List<ItemData>();        // 인벤 중 장비 아이템 모음
        private static List<ItemData> invenCons = new List<ItemData>();        // 인벤 중 소비 아이템 모음 

        public void InventoryUI() //Inventory UI Scene
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventory.Count; i++)
            {
                ItemData item = inventory[i];
                bool IsEquipped = equipList.Contains(item);
                if (IsEquipped)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                string EquipDisplay = IsEquipped ? "[E]" : "";
                Console.WriteLine($"- {EquipDisplay} {item.ItemNames}    |    {ItemDB.TypeText(item.ItemTypes)} + {item.ItemValue}    |    {item.ItemDesc}");
                if (IsEquipped)
                {
                    Console.ResetColor();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("2. 소비 아이템 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            int input = Input.GetInt(0, 2);
            switch (input)
            {
                case 0:
                    StartScene.Instance.GameStartScene();
                    break;

                case 1:
                    EquipUI();
                    break;

                case 2:
                    ConsumUI();
                    break;

            }

        }

        public void EquipUI() // 장비 장착 관리
        {
            invenEquip = inventory.Where(item => item.ItemTypes < 2).ToList();

            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < invenEquip.Count; i++)
            {
                ItemData item = invenEquip[i];
                bool IsEquipped = equipList.Contains(inventory[i]);
                if (IsEquipped)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                string EquipDisplay = IsEquipped ? "[E]" : "";
                Console.WriteLine($"- {i+1}. {EquipDisplay} {item.ItemNames}    |    {ItemDB.TypeText(item.ItemTypes)} + {item.ItemValue}    |    {item.ItemDesc}");
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

            int input = Input.GetInt(0, invenEquip.Count);

            
            if (input == 0) InventoryUI(); // 0번 누르면 인벤토리로 이동


            else if (input >= 1 && input <= invenEquip.Count) // 장비를 선택한 경우
            {
                ItemData item = invenEquip[input - 1];

                if (equipList.Contains(item)) // 이미 장착한 장비를 선택했을 때
                {
                    if (item.ItemTypes == 0)// 장착한 무기 해제
                    {
                        WeaponStatsOFF();                         // 스탯 적용(해제한 무기 공격력 적용)
                        equipList.Remove(item);                // 장착 리스트에서 제거
                        equipmentWeapon[0] = null;                // 무기 칸 초기화라는 의미
                        Console.Write("장비를 해제했습니다. (Enter키 입력 시 진행)");
                        Console.ReadLine();
                        EquipUI();
                    }
                    else if (item.ItemTypes == 1) // 장착한 방어구 해제
                    {

                        ArmorStatsOFF();                        // 스탯 적용(해제한 방어구 방어력 적용)
                        equipList.Remove(item);              // 장착 리스트 제거
                        equipmentArmor[0] = null;                 // 방어구 칸 초기화라는 의미
                        Console.Write("장비를 해제했습니다. (Enter키 입력 시 진행)");
                        Console.ReadLine();
                        EquipUI();
                    }
                }
                else // 새로운 장비를 선택했을 때 
                {
                    if (item.ItemTypes == 0) // 무기인 경우
                    {
                        if (equipmentWeapon[0] != null)             // 이미 "무기 칸"에 무기 장비가 있다면
                        {
                            WeaponStatsOFF();                         // 스탯 적용(해제한 무기 공격력 적용)
                            equipList.Remove(equipmentWeapon[0]); // 그 무기 장비를 "장착 리스트"에서 제거
                        }
                        equipmentWeapon[0] = item;           // "무기 칸"에 해당 장비가 있음을 표시
                        WeaponStatsON();                     // 스탯 적용(장착한 무기 공격력 적용)
                        equipList.Add(item);                 // "장착 리스트"에 장비(무기) 추가
                        Console.Write("장비를 장착했습니다. (Enter키 입력 시 진행)");
                        Console.ReadLine();
                        EquipUI();

                    }
                    else if (item.ItemTypes == 1) // 방어구인 경우
                    {
                        if (equipmentArmor[0] != null)                // 이미 "방어구 칸"에 방어구 장비가 있다면
                        {
                            ArmorStatsOFF();
                            equipList.Remove(equipmentArmor[0]);    // 그 방어구 장비를 "장착 리스트"에서 제거
                        }
                        equipmentArmor[0] = item;              // "방어구 칸"에 해당 방어구가 있음을 표시
                        ArmorStatsON();
                        equipList.Add(item);                   // "장착 리스트:에 장비(방어구) 추가
                        Console.Write("장비를 장착했습니다. (Enter키 입력 시 진행)");
                        Console.ReadLine();
                        EquipUI();

                    }
                }
            }

        }
        public void WeaponStatsOFF() // 장착했던 장비의 스탯 제거 // equipmentArmor에 장비를 할당하기 전에 써야함 ex) equipmentArmor[0] = item;의 윗줄에 작성
        {
            if (equipmentWeapon[0] != null)
            {
                Player.Instance.atk -= equipmentWeapon[0].ItemValue;

            }
        }
        public void WeaponStatsON() // 장착하는 장비의 스탯 추가
        {

            Player.Instance.atk += equipmentWeapon[0].ItemValue;


        }

        public void ArmorStatsOFF() // 장착했던 장비의 스탯 제거 // equipmentArmor에 장비를 할당하기 전에 써야함 ex) equipmentArmor[0] = item;의 윗줄에 작성
        {
            if (equipmentArmor[0] != null)
            {
                Player.Instance.def -= equipmentArmor[0].ItemValue;

            }
        }
        public void ArmorStatsON() // 장착하는 장비의 스탯 추가
        {

            Player.Instance.def += equipmentArmor[0].ItemValue;


        }

        public void ConsumUI() // 소비 아이템 관리
        {

            Console.Clear();
            Console.WriteLine("인벤토리 - 소비 아이템 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[소비 아이템 목록]");
            invenCons = inventory.Where(item => item.ItemTypes >= 10).ToList();
            for (int i = 0; i < invenCons.Count; i++)
            {
                ItemData item = invenCons[i];
                
                Console.WriteLine($"- {i + 1}. {item.ItemNames}    |    {ItemDB.TypeText(item.ItemTypes)} + {item.ItemValue}    |    {item.ItemDesc}");
                
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>    ");

            int input = Input.GetInt(0, invenCons.Count);

            if (input == 0) InventoryUI(); // 0번 누르면 인벤토리로 이동
            else
            {
                input = input - 1;
                if( invenCons[input].ItemTypes == 11)
                {
                    Player.Instance.exp += invenCons[input].ItemValue;
                    
                    Console.WriteLine($"{ItemDB.TypeText(invenCons[input].ItemTypes)}를 {invenCons[input].ItemValue}획득했습니다.  (Enter키 입력 시 진행)");
                    inventory.Remove(invenCons[input]);
                    invenCons.Remove(invenCons[input]);
                    Console.ReadLine();
                    ConsumUI();
                }
                else if (invenCons[input].ItemTypes == 20)
                {

                    if(invenCons[input].ItemIDs == 51)
                    {
                        if(AddItem.ChanceBox(13, 1)) Console.WriteLine("1% 확률로 [무형검]을 획득하였습니다.");
                        else Console.WriteLine("아무일도 일어나지 않았습니다.");
                        inventory.Remove(invenCons[input]);
                        invenCons.Remove(invenCons[input]);
                        Console.ReadLine();
                        ConsumUI();
                    }
                    
                    
                    if (invenCons[input].ItemIDs == 52)

                    {
                        if (AddItem.ChanceBox(13, 50)) Console.WriteLine("50% 확률로 [무형검]을 획득하였습니다.");
                        else Console.WriteLine("아무일도 일어나지 않았습니다.");
                        inventory.Remove(invenCons[input]);
                        invenCons.Remove(invenCons[input]);
                        Console.ReadLine();
                        ConsumUI();
                    }
                    
                    
                }
                else if (invenCons[input].ItemTypes == 12)
                {
                    Player.Instance.atk += invenCons[input].ItemValue;
                    
                    Console.WriteLine($"{ItemDB.TypeText(invenCons[input].ItemTypes)}을 {invenCons[input].ItemValue}만큼 획득했습니다.  (Enter키 입력 시 진행)");
                    inventory.Remove(invenCons[input]);
                    invenCons.Remove(invenCons[input]);
                    Console.ReadLine();
                    ConsumUI();
                }
                else if (invenCons[input].ItemTypes == 13)
                {
                    Player.Instance.def += invenCons[input].ItemValue;
                    
                    Console.WriteLine($"{ItemDB.TypeText(invenCons[input].ItemTypes)}을 {invenCons[input].ItemValue}만큼 획득했습니다.  (Enter키 입력 시 진행)");
                    inventory.Remove(invenCons[input]);
                    invenCons.Remove(invenCons[input]);
                    Console.ReadLine();
                    ConsumUI();
                }


            }
        }


        private static Inventory? instance;
        public static Inventory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Inventory();
                }
                return instance;
            }
        }
    }
}
