using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class HpPotion
    {
        Player player = Player.Instance;
        StartScene startScene = StartScene.Instance;
        

        int addHp = 30;
        int addMp = 15;
        public void UsePotion()
        {            
            Console.WriteLine();
            Console.WriteLine("사용할 포션을 선택해주세요");
            Console.WriteLine();
            Console.Write("1. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("체력");
            Console.ResetColor();
            Console.WriteLine("포션");
            Console.Write("2. ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("마나");
            Console.ResetColor();
            Console.WriteLine("포션");
            Console.WriteLine();
            Console.Write("사용할 포션 : ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int num))
            {
                if (num == 1)
                {
                    if (player.potionCount > 0)
                    {
                        if (player.hp == player.maxHp)
                        {
                            Console.Clear();
                            Console.WriteLine($"체력이 가득 찼습니다. 현재 체력 : {player.hp} 남은포션개수 : {player.potionCount}");
                        }
                        else if (player.hp > player.maxHp - 30 )
                        {
                            Console.Clear();
                            player.hp = player.maxHp;
                            player.potionCount--;
                            Console.WriteLine($"회복을 완료했습니다. 현재 체력 : {player.hp} 남은포션개수 : {player.potionCount}");
                        }
                        else
                        {
                            Console.Clear();
                            player.hp += addHp;
                            player.potionCount--;
                            Console.WriteLine($"회복을 완료했습니다. 현재 체력 : {player.hp} 남은포션개수 : {player.potionCount}");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("포션이 부족합니다.");
                    }
                }
                else if (num == 2)
                {
                    if (player.mpPotionCount > 0)
                    {
                        if (player.mp == player.maxMp)
                        {
                            Console.Clear();
                            Console.WriteLine($"마나가 가득 찼습니다. 현재 마나 : {player.mp} 남은포션개수 : {player.mpPotionCount}");
                        }
                        else if (player.mp > player.maxMp - 15)
                        {
                            Console.Clear();
                            player.mp = player.maxMp;
                            player.mpPotionCount--;
                            Console.WriteLine($"회복을 완료했습니다. 현재 마나 : {player.mp} 남은포션개수 : {player.mpPotionCount}");
                        }
                        else
                        {
                            Console.Clear();
                            player.mp += addMp;
                            player.mpPotionCount--;
                            Console.WriteLine($"회복을 완료했습니다. 현재 마나 : {player.mp} 남은포션개수 : {player.mpPotionCount}");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("포션이 부족합니다.");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("사용할 포션을 정확히 선택하세요");
                }
            }
            else 
            {
                Console.Clear();
                Console.WriteLine("숫자를 입력해주세요.");
                Console.WriteLine("");
                UsePotion();
            }
        }
        
        public void ViewPotionInfo()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("(회복)");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"체력포션을 사용하면 체력을 {addHp} 회복 할 수 있습니다. (남은 포션 : {player.potionCount})");
            Console.WriteLine($"마나포션을 사용하면 마나를 {addMp} 회복 할 수 있습니다. (남은 포션 : {player.mpPotionCount})");
            Console.WriteLine();
            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요 : ");
            string input = Console.ReadLine();
            if(int.TryParse(input,out int num))
            {
                if (num == 1)
                {
                    UsePotion();                    
                }
                else if (num == 0)
                {
                    startScene.GameStartScene();
                }
                else 
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.Write("정확한 숫자를 입력해주세요.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("숫자를 입력해주세요.");
                Console.WriteLine("");
            }
            ViewPotionInfo();
        }

    }
}
