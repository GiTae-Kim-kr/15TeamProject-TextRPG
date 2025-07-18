using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class SaveLoadScene
    {
        SaveLoad saveLoad = new SaveLoad();
        Player player = Player.Instance;
        Inventory inventory = Inventory.Instance;
        public void SaveScene()
        {
            Console.WriteLine("지금까지 진행한 내용을 저장하시겠습니까? (Y/N)");
            

            while(true)
            {
                string input = Console.ReadLine().ToUpper();
                if (input == "Y")
                {
                    saveLoad.SaveGame(player, inventory);
                    Console.WriteLine("계속하려면 아무 키나 누르세오...");
                    Console.ReadKey();
                    StartScene.Instance.GameStartScene(); // 시작 화면으로 이동
                    return;
                }
                else if (input == "N")
                {
                    Console.WriteLine("저장하지 않고 진행합니다.");
                    Console.WriteLine("계속하려면 아무 키나 누르세오...");
                    Console.ReadKey();
                    StartScene.Instance.GameStartScene(); // 시작 화면으로 이동
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. Y 또는 N을 입력해주세요.");
                }
            }


        }

        public void LoadScene()
        {

            Console.WriteLine("\n게임을 진행하기에 앞서 저장된 게임이 있는지 확인해보겠습니다....");

            while (true)
            {
                
                SaveLoad load = saveLoad.LoadGame();   // 저장된 데이터 불러오기 
                if (load != null)                      // 로드된 데이터 있는지 여부 파악
                {
                    Console.WriteLine("\n...........! 저장된 게임이 존재합니다!");
                    Console.WriteLine("\n저장된 게임을 불러오시겠습니까? (Y/N)");
                    string input = Console.ReadLine().ToUpper();
                    if (input == "Y")
                    {
                        // 플레이어 정보 불러오기
                        player.name = load.player.name; // 플레이어 이름 불러오기
                        player.level = load.player.level; // 플레이어 레벨 불러오기
                        player.job = load.player.job; // 플레이어 직업 불러오기
                        player.hp = load.player.hp; // 플레이어 체력 불러오기
                        player.maxHp = load.player.maxHp; // 플레이어 최대 체력 불러오기
                        player.mp = load.player.mp; // 플레이어 마나 불러오기
                        player.maxMp = load.player.maxMp; // 플레이어 최대 마나 불러오기
                        player.atk = load.player.atk; // 플레이어 공격력 불러오기
                        player.def = load.player.def; // 플레이어 방어력 불러오기
                        player.gold = load.player.gold; // 플레이어 골드 불러오기
                        player.exp = load.player.exp; // 플레이어 경험치 불러오기
                        player.potionCount = load.player.potionCount; // 플레이어 포션 개수 불러오기
                        player.mpPotionCount = load.player.mpPotionCount; // 플레이어 마나 포션 개수 불러오기

                        // 인벤토리 정보 불러오기
                        Inventory.inventory = load.InventoryItems.ToList(); // 플레이어 인벤토리 불러오기
                        Inventory.equipList = Inventory.inventory.Where(item => load.equippedUids.Contains(item.UID)).ToList(); // 장착한 아이템 불러오기
                        Inventory.equipmentWeapon[0] = null;  // 무기 슬롯 장착 아이템 초기화
                        Inventory.equipmentArmor[0] = null;   // 방어구 슬롯 장착 아이템 초기화
                        if (load.equippedWeaponUid.HasValue)  // 무기 슬롯 장착 아이템이 있다면
                            Inventory.equipmentWeapon[0] = Inventory.inventory.FirstOrDefault(item => item.UID == load.equippedWeaponUid); // 무기 슬롯 장착 아이템 불러오기

                        if (load.equippedArmorUid.HasValue)   // 방어구 슬롯 장착 아이템이 있다면
                            Inventory.equipmentArmor[0] = Inventory.inventory.FirstOrDefault(item => item.UID == load.equippedArmorUid); // 방어구 슬롯 장착 아이템 불러오기

                        // 퀘스트 정보 불러오기
                        QuestDB.questList = load.questListData.Select(q => q).ToList();    // 퀘스트 리스트 정보 불러오기
                        // 상점 정보 불러오기
                        Shop.Shop1hasItem = load.shopItemData.Select(item => item).ToList(); // 상점 아이템 데이터 불러오기

                        Console.WriteLine("\n게임이 성공적으로 불러와졌습니다!");
                        Console.WriteLine("\n시작 화면으로 이동합니다!");
                        Console.ReadKey();
                        StartScene.Instance.GameStartScene(); // 시작 화면으로 이동
                        return;
                    }
                    else if (input == "N")
                    {
                        Console.WriteLine("\n저장하지 않고 진행합니다.");
                        Console.WriteLine("\n계속하려면 아무 키나 누르세오...\n");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. Y 또는 N을 입력해주세요.");
                    }
                }
                else
                {
                    Console.WriteLine("\n새로운 게임을 시작합니다!");
                    Console.WriteLine("계속하려면 아무 키나 누르세오...\n");
                    Console.ReadKey();
                    return;
                }

            }
        }

        
        //싱글톤 패턴
        private static SaveLoadScene? instance;
        public static SaveLoadScene Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SaveLoadScene();
                }
                return instance;
            }
        }

    }
}
