using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _15TeamProject
{
    internal class SaveLoad
    {

        // 세이브, 로드 했을때 기억하고 있어야 할 정보 담은것만 새로 인스턴스
        public Player player { get; set; }
        public List<ItemData> InventoryItems { get; set; }      // 플레이어 인벤토리 전체
        public List<int> equippedUids { get; set; }             // 장착한 아이템의 UID(고유번호) 리스트
                                                                // 또는 List<ItemData> EquipItems 할 수도 있음
        public int? equippedWeaponUid { get; set; }             // 무기 슬롯 장착 UID
        public int? equippedArmorUid { get; set; }              // 방어구 슬롯 장착 UID
        public List<Quest> questListData { get; set; }    // 실제 퀘스트 데이터 저장된곳
        public List<ItemData> shopItemData { get; set; }   // 상점 아이템 데이터 저장용

        public void SaveGame(Player player, Inventory inventory)
        {
            
            List<ItemData> currentInventory = Inventory.inventory.ToList();   // 인벤토리 아이템 리스트 복사
            List<int> equippedUids = Inventory.equipList.Select(item => item.UID).ToList(); // 장착한 아이템의 UID 리스트
            int? equippedWeaponUid = Inventory.equipmentWeapon[0]?.UID; // 무기 슬롯 장착 UID
            int? equippedArmorUid = Inventory.equipmentArmor[0]?.UID; // 방어구 슬롯 장착 UID
            List<Quest> questListCopy = QuestDB.questList.ToList(); // 퀘스트 리스트 복사
            List<ItemData> shopItemData = Shop.Shop1hasItem.ToList(); // 상점 아이템 데이터 복사

            SaveLoad save = new SaveLoad
            {
                player = player,
                InventoryItems = currentInventory,
                equippedUids = equippedUids,
                equippedWeaponUid = equippedWeaponUid,
                equippedArmorUid = equippedArmorUid,
                questListData = questListCopy,
                shopItemData = shopItemData,
            };

            string json = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("15TeamSave.json", json);
            Console.WriteLine(".......! 게임이 성공적으로 저장되었습니다!\n");

        }

        public SaveLoad LoadGame()
        {
            if (!File.Exists("15TeamSave.json"))
            {
                Console.WriteLine("저장된 게임이 없습니다.");
                return null;
            }
            string json = File.ReadAllText("15TeamSave.json");
            SaveLoad save = JsonConvert.DeserializeObject<SaveLoad>(json);
            Console.WriteLine("성공적으로 게임을 불러왔습니다!");

            return save;
        }
        

    }
}
