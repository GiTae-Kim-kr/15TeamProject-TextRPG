using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class Pub
    {   public static List<ItemData> Tokenbox = new List<ItemData>();
        public static List<ItemData> NorTokenbox = new List<ItemData>();
        public static List<ItemData> PreTokenbox = new List<ItemData>();
        static Random dicerandom = new Random();
        public static int firsttime { get; set; }

        public Pub()
        {
            firsttime = 0;
        }
        
        public static void PubMainUI() 
        {
            Console.Clear();
            Console.WriteLine("펍 안쪽");
            Console.WriteLine("자~ 돈 놓고 돈 먹기~~");
            Console.WriteLine("");
            Console.WriteLine("좋은게 있어 자네에게만 알려주지");
            Console.WriteLine("이 주사위를 3개 던져서 같은 숫자가 나오면");
            Console.WriteLine("생각만해도 짜릿하다네");
            Console.WriteLine("어때? 도전해 보겠나?");

            if (firsttime == 0 && Player.Instance.job == "거지")
            {
                Console.ReadLine();
                Console.WriteLine("잠깐만...");
                Console.ReadLine();
                Console.WriteLine("아닛! 자네처럼 누추한 사람이 이런 귀한 곳에 오다니");
                Console.WriteLine("어서 나가게나");
                Console.ReadLine();
                Console.WriteLine("잠깐 자네 관상을 보니 예사롭지 않구만");
                Console.WriteLine("흠.. 한 번 기대를 걸어봐야 하나");
                Console.WriteLine("한 번 참여해보시오");
                for (int i = 0; i<10; i++) AddItem.TokenBoxAdd(500);
                firsttime++;
                Console.ReadLine();
                Console.WriteLine("입장권 10장을 획득했습니다.");
                Console.WriteLine("잘됐을 때 나를 잊으면 안돼오!!(Enter키 입력 시 진행)");
                Console.ReadLine();
                
            }
            if (Tokenbox.Where(item => item.ItemIDs == 500).Count() == 0) Console.WriteLine("입장권이 없으면 입장할 수 없다네. 입장권을 구매하게나.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Player.Instance.gold} G");
            Console.WriteLine("");
            Console.WriteLine("[보유 입장권]");
            Console.WriteLine($"{Tokenbox.Where(item => item.ItemIDs == 500).Count()} 개");
            Console.WriteLine("");

            Console.WriteLine("[보유 노말 토큰]");
            Console.WriteLine($"{Tokenbox.Where(item => item.ItemIDs == 501).Count()} 개");
            Console.WriteLine("");

            Console.WriteLine("[보유 프리미엄 토큰]");
            Console.WriteLine($"{Tokenbox.Where(item => item.ItemIDs == 502).Count()} 개");
            Console.WriteLine("");

            
            Console.WriteLine("1. 게임 참여");
            Console.WriteLine("2. 프리미엄 토큰 상점");
            Console.WriteLine("3. 노말 토큰 상점");
            Console.WriteLine("4. 입장권 구매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>    ");
            int input = Input.GetInt(0, 4);
            switch (input)
            {
                case 1:
                    if (Tokenbox.Where(item => item.ItemIDs == 500).Count() > 0)
                    {
                        ItemData enter = Tokenbox.Where(item => item.ItemIDs == 500).FirstOrDefault();
                        if(enter != null) Tokenbox.Remove(enter);
                        PubPlayUI();
                    }
                    else
                    {
                        Console.WriteLine("입장권을 구매해주세요.(Enter키 입력 시 진행)");
                        Console.ReadLine();
                        PubMainUI();
                    }
                    break;
                case 2:
                    PreTokenShopUI();
                    break;
                case 3:
                    NorTokenShopUI();
                    break;
                case 4:
                    Console.WriteLine("몇 장 구매할것인가? (한 장당 500G입니다.)");

                    int inputToken = Input.GetInt();

                    if (Player.Instance.gold >= inputToken*500)
                    {
                        for (int i = 0; i < inputToken; i++) AddItem.TokenBoxAdd(500);
                        Player.Instance.gold -= inputToken * 500;
                        Console.WriteLine($"입장권 {inputToken}장을 구매했습니다. (Enter키 입력 시 진행)");
                        Console.ReadLine();
                        PubMainUI();
                    }
                    else Console.WriteLine("자네, 돈이 부족하지 않은가... (Enter키 입력 시 진행)");
                    Console.ReadLine();
                    PubMainUI(); ;
                    break;
                case 0:
                    StartScene.Instance.GameStartScene();
                    break;
            }
        }
        
        public static void PubPlayUI()
        {
            Console.Clear();
            Console.WriteLine("펍 안쪽 - 게임시작");
            Console.WriteLine("자~ 돈 놓고 돈 먹기~~");
            Console.WriteLine("");
            Console.Write("자! 주사위를 3번 굴려서 3번 다 같은 숫자를 뽑으면 승리!");
            Console.ReadLine();
            Console.Write("그럼 시작하도록 하지");
            Console.ReadLine();
            Console.WriteLine("두구두구두구두구");
            Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("");
            while (true)
            {
                Console.WriteLine("[보유 입장권]");
                Console.WriteLine($"{Tokenbox.Where(item => item.ItemIDs == 500).Count()} 개");
                Console.WriteLine("[보유 노말 토큰]");
                Console.WriteLine($"{Tokenbox.Where(item => item.ItemIDs == 501).Count()} 개");
                Console.WriteLine("");
                Console.WriteLine("[보유 프리미엄 토큰]");
                Console.WriteLine($"{Tokenbox.Where(item => item.ItemIDs == 502).Count()} 개");
                Console.WriteLine("");
                int first = 0;
                int second = 0;
                int third = 0;
                if (first == 0 && second == 0 && third == 0)
                {
                    first = dicerandom.Next(1, 7);
                    second = dicerandom.Next(1, 7);
                    third = dicerandom.Next(1, 7);
                }
                
                
                Console.WriteLine("");
                Console.Write("첫 번째 주사위        | ");
                Console.Read();
                Console.Write("...");
                Console.Read();
                Console.Write($"   {first}");
                Console.ReadLine();
                Console.Write("두 번째 주사위        | ");
                Console.Read();
                Console.Write("...");
                Console.Read();
                Console.Write($"   {second}");
                Console.ReadLine();
                Console.Write("세 번째 주사위        | ");
                Console.Read();
                Console.Write("...");
                Console.Read();
                Console.Write($"   {third}");
                Console.ReadLine();
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("[결과]");
                if (first == second && first == third)
                {
                    AddItem.TokenBoxAdd(502);
                    Console.WriteLine("오!! 축하하네 자네는 정말 대단하구만. 여기 프리미엄 토큰을 받아가게. (Enter키 입력 시 진행)");
                    Console.Read();
                    Console.WriteLine("프리미엄 토큰을 획득하였습니다. (Enter키 입력 시 진행)");
                    Console.Read();
                }
                else
                {
                    AddItem.TokenBoxAdd(501);
                    Console.WriteLine("좌절하지 말게나. 보통의 일이라네.\n노말 토큰을 획득하였습니다. (Enter키 입력 시 진행)");
                    Console.ReadLine();
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("1. 다시하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                int input = Input.GetInt(0, 1);
                switch (input)
                {
                    case 1:
                        if (Tokenbox.Where(item => item.ItemIDs == 500).Count() > 0)
                        {
                            ItemData enter = Tokenbox.Where(item => item.ItemIDs == 500).FirstOrDefault();
                            if (enter != null) Tokenbox.Remove(enter);
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("입장권을 구매해주세요.(Enter키 입력 시 진행)");
                            Console.ReadLine();
                            PubMainUI();
                        }
                        break;
                    case 0:
                        PubMainUI();
                        break;
                }
            }
        }

        public static void PreTokenShopUI()
        {
            Console.Clear();
            Console.WriteLine("펍 안쪽 - 프리미엄 토큰 상점");
            Console.WriteLine("프리미엄 토큰으로 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 프리미엄 토큰]");
            Console.WriteLine($"{Tokenbox.Where(item => item.ItemIDs == 502).Count()} 개");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            if (PreTokenbox.Count == 0) AddItem.PreTokenAdd(52, 50);
            for (int i = 0; i < PreTokenbox.Count; i++)
            {
                ItemData item = PreTokenbox[i];
                if (PreTokenbox[i].ItemCount == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"- {i + 1}. {item.ItemNames}    |    {ItemDB.TypeText(PreTokenbox[i].ItemTypes)} + {item.ItemValue}    |    {item.ItemDesc}    |    구매완료");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"- {i + 1}. {item.ItemNames}    |    {ItemDB.TypeText(PreTokenbox[i].ItemTypes)} + {item.ItemValue}    |    {item.ItemDesc}    |    {item.ItemBuyPrice} 프리미엄 토큰");
                }

            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>    ");
            int input = Input.GetInt(0, PreTokenbox.Count);
            if (input == 0)
            {
                PubMainUI();
            }
            else
            {
                input -= 1;
                ItemData item = PreTokenbox[input];
                if (Tokenbox.Where(item => item.ItemIDs == 502).Count() >= item.ItemBuyPrice)  // 프리미엄 토큰이 있을 때
                    {
                    ItemData SearchItem = PreTokenbox[input];
                    ItemData item1 = new ItemData(SearchItem.ItemIDs, SearchItem.ItemNames, SearchItem.ItemTypes, SearchItem.ItemValue, SearchItem.ItemDesc, SearchItem.ItemBuyPrice, SearchItem.ItemSellPrice);
                    Inventory.inventory.Add(item1);
                    for (int i = 0; i < item.ItemBuyPrice; i++) Tokenbox.Remove(Tokenbox.Where(item => item.ItemIDs == 502).FirstOrDefault());
                    Console.WriteLine("구매를 완료하였습니다. (Enter키 입력 시 진행)");
                    Console.ReadLine();
                    PreTokenShopUI();
                    }
                else                                            //  프리미엄 토큰이 없을 때
                    {
                        Console.WriteLine("프리미엄 토큰이 부족합니다. (Enter키 입력 시 진행)");
                        Console.ReadLine();
                        PreTokenShopUI();
                    }
                
                
            }
        }
        public static void NorTokenShopUI()
        {
            Console.Clear();
            Console.WriteLine("펍 안쪽 - 노말 토큰 상점");
            Console.WriteLine("노말 토큰으로 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 노말 토큰]");
            Console.WriteLine($"{Tokenbox.Where(item => item.ItemIDs == 501).Count()} 개");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            if (NorTokenbox.Count == 0) AddItem.NorTokenAdd(51);
            for (int i = 0; i < NorTokenbox.Count; i++)
            {
                ItemData item = NorTokenbox[i];
                Console.WriteLine($"- {i + 1}. {item.ItemNames}    |    {ItemDB.TypeText(NorTokenbox[i].ItemTypes)} + {item.ItemValue}    |    {item.ItemDesc}    |    {item.ItemBuyPrice} 노말 토큰");
                

            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>    ");
            int input = Input.GetInt(0, NorTokenbox.Count);
            if (input == 0)
            {
                PubMainUI();
            }
            else
            {
                input -= 1;
                ItemData item = NorTokenbox[input];
                if (Tokenbox.Where(item => item.ItemIDs == 501).Count() >= item.ItemBuyPrice)  // 노말 토큰이 있을 때
                    {
                    ItemData SearchItem = NorTokenbox[input];
                    ItemData item1 = new ItemData(SearchItem.ItemIDs, SearchItem.ItemNames, SearchItem.ItemTypes, SearchItem.ItemValue, SearchItem.ItemDesc, SearchItem.ItemBuyPrice, SearchItem.ItemSellPrice);
                    Inventory.inventory.Add(item1);
                    for(int i =0; i < item.ItemBuyPrice; i++) Tokenbox.Remove(Tokenbox.Where(item => item.ItemIDs == 501).FirstOrDefault());
                    Console.WriteLine("구매를 완료하였습니다. (Enter키 입력 시 진행)");
                    Console.ReadLine();
                    NorTokenShopUI();
                    }
                    else                                            //  프리미엄 토큰이 없을 때
                    {
                        Console.WriteLine("노말 토큰이 부족합니다. (Enter키 입력 시 진행)");
                        Console.ReadLine();
                        NorTokenShopUI();
                    }
                
                
            }
        }
    }
}
