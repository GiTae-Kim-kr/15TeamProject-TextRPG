using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class BattleScene
{

    public void DroppedPotion()
    {
        Random random = new Random();
        int dromHPPotion = random.Next(0, 100);
        int dromMPPotion = random.Next(0, 100);
        if (dromHPPotion < 15)
        {
            droppedHPPotion++;
        }
        if (dromMPPotion < 10)
        {
            droppedMPPotion++;
        }
    }

    public void GetPotion()
    {
        player.potionCount += droppedHPPotion;
        player.mpPotionCount += droppedMPPotion;
        Console.WriteLine($"체력포션을 {droppedHPPotion} 개 획득 하셨습니다.");
        Console.WriteLine($"마나포션을 {droppedMPPotion} 개 획득 하셨습니다.");
        droppedHPPotion = 0;
        droppedMPPotion = 0;
    }
    public void DroppedItems(int target)
    {
        Monster monster = monsterInfo[target];
        
    }
    public void GetItems()
    {

    }
}

