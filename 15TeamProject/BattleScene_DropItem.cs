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
        Console.WriteLine($"포션을 {droppedHPPotion} 개 획득 하셨습니다.");
        droppedHPPotion = 0;
    }
}

