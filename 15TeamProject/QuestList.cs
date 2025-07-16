using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class QuestList
    {
        public void QuestScene()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("QuestList!!\n");
            Console.ResetColor();
            // 옆에 퀘스트를 수주했는지 안했는지 여부 나타내기.
            for (int i = 0; i < QuestDB.questList.Count; i++)
            {
                if (QuestDB.questList[i].isQuestAccept)
                {
                    Console.WriteLine($"{i + 1}. {QuestDB.questList[i].questName} - 수행중!");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {QuestDB.questList[i].questName}");
                }
            }
            // 퀘스트를 리스트로 관리해서 출력.
            Console.WriteLine("\n\n원하시는 퀘스트를 선택해주세요.\n>>");
            int questChoice = Input.GetInt();
            
            if (questChoice == 0)
            {
                StartScene.Instance.GameStartScene(); // 0 입력시 시작 화면으로 돌아감
            }
            else if ( questChoice <= QuestDB.questList.Count && questChoice > 0)
            {
                    QuestAcceptScene(questChoice);
            }
            else
            {
                    Console.WriteLine("잘못된 퀘스트 번호입니다. 다시 시도해주세요.");
                    QuestScene(); // 잘못된 입력시 다시 퀘스트 목록으로 돌아감
            }
            
        }

        public void QuestAcceptScene(int questChoice)
        {
            Console.Clear();
            //리스트는 0부터 시작이라 -1 해줌.
            int num = questChoice - 1;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Quest!!\n");
            Console.ResetColor();
            // 선택한 퀘스트 이름과 정보 나열
            Console.WriteLine($"{QuestDB.questList[num].questName}\n");  //제목
            Console.WriteLine($"{QuestDB.questList[num].questDescription}\n");  //설명
            Console.WriteLine($"-{QuestDB.questList[num].questCondition}\n");  //완료 조건, 진행도 추가
            Console.WriteLine($"-보상-");
            Console.WriteLine($"금액: {QuestDB.questList[num].questMoneyReward} G");
            Console.WriteLine($"경험치: {QuestDB.questList[num].questExpReward} EXP");
            Console.WriteLine($"아이템: {QuestDB.questList[num].questItemReward}\n");
            Console.WriteLine("1. 퀘스트 수락");
            Console.WriteLine("2. 퀘스트 취소");
            Console.WriteLine("0. 퀘스트 목록으로 돌아가기");
            Console.Write("원하시는 행동을 입력해주세요. \n>>");

            int choice = Input.GetInt();
            switch(choice)
            {
                case 1:
                    QuestDB.questList[num].isQuestAccept = true;  // 퀘스트 수락
                    Console.WriteLine($"퀘스트 '{QuestDB.questList[num].questName}'을(를) 수락했습니다.");
                    break;
                case 2:
                    QuestDB.questList[num].isQuestAccept = false;  // 퀘스트 거절
                    Console.WriteLine($"퀘스트 '{QuestDB.questList[num].questName}'을(를) 취소했습니다.");
                    break;
                case 0:
                    QuestScene();  // 퀘스트 목록으로 돌아가기
                    break;
            }

        }
    }
}
