using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    public class Quest
    {

        public string questName { get; set; }  // 퀘스트 이름
        public string questDescription { get; set; }  // 퀘스트 설명
        public string questCondition { get; set; }  // 퀘스트 완료 조건
        public int questProgress { get; set; }  // 퀘스트 진행도 (예: 몇 마리 처치했는지 등)
        public int questMoneyReward { get; set; }  // 퀘스트 완료시 보상 금액
        public int questExpReward { get; set; }  // 퀘스트 완료시 보상 경험치
        public string questItemReward { get; set; }  // 퀘스트 완료시 보상 아이템
        public bool isQuestAccept { get; set; }  // 퀘스트 수락 여부
        public int currentProgressCount { get; set; }  // 현재 진행도 카운트
        public string targetMonsterName { get; set; }  // 퀘스트 대상 몬스터
        public Quest(string name, string description, string condition, string targetName, int progress, int moneyReward, int expReward, string itemReward)
        {
            questName = name;
            questDescription = description;
            questCondition = condition;
            questProgress = progress;  
            questMoneyReward = moneyReward;
            questExpReward = expReward;
            questItemReward = itemReward;
            targetMonsterName = targetName; 
            isQuestAccept = false;  // 기본적으로 퀘스트는 수락하지 않은 상태로 시작
            currentProgressCount = 0;  // 현재 진행도 카운트는 0으로 초기화

        }

    }

    public static class QuestDB
    {
        public static List<Quest> questList = new List<Quest>()
        {
            new Quest("마을을 위협하는 미니언 처치", "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나? \n마을 주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고! \n모험가인 자네가 좀 처치해주게!", "미니언 다섯 마리 처치", "미니언", 5, 500, 15, "쓸만한 방패 x 1"),
            new Quest("위험한 대포를 지닌 미니언 처치", "근래에 미니언들 중에서 대포를 가지고 다니는 녀석들이 나타났다고 들었네, 그들이 마을의 목책을 부수진 않을까 걱정이야. \n마을의 안전을 위해 그들을 처치해 줄 수 있겠나?", "대포 미니언 3마리 처치", "대포미니언", 3, 1000, 30, "쓸만한 검 x 1")
        };
    }

}
