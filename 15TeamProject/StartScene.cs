using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class StartScene
    {
        public int pastPlayerHP { get; set; }
        public int pastPlayerMP { get; set; }

        public void GameStartScene()
        {
            BattleScene battleScene = new BattleScene();
            HpPotion hpPotion = new HpPotion();
            QuestList questList= new QuestList();
            Inventory inventory = new Inventory();
            Shop shop = new Shop();
            StatusScene statusScene = new StatusScene();

            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 전투 시작");
            Console.WriteLine("4. 상점");
            Console.WriteLine("5. 퀘스트 목록");
            Console.WriteLine("6. 회복 아이템");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요. \n>>");
            int choice = Input.GetInt();
            switch (choice)
            {
                case 1:
                    statusScene.StatusViewScene();
                    break;
                case 2:                   
                    inventory.InventoryUI();
                    break;
                case 3:
                    // 몬스터 랜덤으로 생성해서 monsterInfo에 저장.
                    battleScene.RandomMonster();
                    pastPlayerHP = Player.Instance.hp; // 현재 플레이어의 HP를 pastPlayerHP에 저장
                    pastPlayerMP = Player.Instance.mp; // 현재 플레이어의 MP를 pastPlayerMP에 저장
                    // battleScene의 전투 시작화면으로 이동.
                    battleScene.Run();
                    break;
                case 4:
                    shop.ShopMainUI();
                    break;
                case 5:
                    Console.Clear();
                    questList.QuestScene();
                    break;
                case 6:
                    Console.Clear();
                    // 회복 아이템 사용 화면으로 이동
                    hpPotion.ViewPotionInfo();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    GameStartScene();  // 잘못된 입력시 다시 시작 화면으로 돌아감
                    break;
            }
        }

        // 싱글톤 만들기 위한 필드 선언
        private static StartScene? instance;
        // 싱글톤 인스턴스 생성 (프로퍼티)
        public static StartScene Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StartScene();
                }
                return instance;
            }
        }


    }
}
