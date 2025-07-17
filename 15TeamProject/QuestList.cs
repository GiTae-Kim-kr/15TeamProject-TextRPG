using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class QuestList
    {
        
        public int num { get; private set; }   //내가 선택한 퀘스트 인덱스
        
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
                    
                    if (QuestDB.questList[i].currentProgressCount == QuestDB.questList[i].questProgress)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue; // 완료 가능 퀘스트
                        Console.WriteLine($"{i + 1}. {QuestDB.questList[i].questName} - 완료 가능!"); // 퀘스트 완료 가능 표시
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{i + 1}. {QuestDB.questList[i].questName} - 수행중!");
                        Console.ResetColor();
                    }
                }
                else if (!QuestDB.questList[i].isQuestAccept)
                {
                    if (QuestDB.questList[i].isQuestComplete)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray; // 완료한 퀘스트 회색 표시
                        Console.WriteLine($"{i + 1}. {QuestDB.questList[i].questName} - 완료!"); // 퀘스트 완료 표시
                        Console.ResetColor(); // 색상 초기화
                    }
                    else Console.WriteLine($"{i + 1}. {QuestDB.questList[i].questName}");
                }
            }
            Console.WriteLine("\n\n0. 시작 화면으로 돌아가기"); // 0 입력시 시작 화면으로 돌아감
            Console.Write("\n원하시는 퀘스트를 선택해주세요.\n>>");

            // 퀘스트 선택 입력
            while (true)
            {
                int questChoice = Input.GetInt();
                if (questChoice == 0)
                {
                    StartScene.Instance.GameStartScene(); // 0 입력시 시작 화면으로 돌아감
                    return;
                }
                else if (questChoice <= QuestDB.questList.Count && questChoice > 0)
                {
                    if (QuestDB.questList[questChoice - 1].isQuestComplete)
                    {
                        Console.WriteLine("이미 완료한 퀘스트입니다. 다른 퀘스트를 선택해주세요.");
                    }
                    else
                    {
                        QuestAcceptScene(questChoice);
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 퀘스트 번호입니다. 다시 시도해주세요.");
                }
            }
        }

        public void QuestAcceptScene(int questChoice)
        {
            bool isLevelUp;
            Console.Clear();
            //리스트는 0부터 시작이라 -1 해줌.
            num = questChoice - 1;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Quest!!\n");
            Console.ResetColor();

            // 선택한 퀘스트 이름과 정보 나열
            Console.WriteLine($"{QuestDB.questList[num].questName}\n");  //제목
            Console.WriteLine($"{QuestDB.questList[num].questDescription}\n");  //설명
            Console.Write($"-{QuestDB.questList[num].questCondition}  ");  //완료 조건, 진행도 추가
            Console.WriteLine($"({QuestDB.questList[num].currentProgressCount}/{QuestDB.questList[num].questProgress})\n");  // 진행도 추가
            Console.WriteLine($"-보상-");
            Console.WriteLine($"금액: {QuestDB.questList[num].questGoldReward} G");
            Console.WriteLine($"경험치: {QuestDB.questList[num].questExpReward} EXP");
            Console.WriteLine($"아이템: {QuestDB.questList[num].questItemReward}\n");

            // 퀘스트 진행도에 따라 출력되는 선택지 다르게 설정
            if (QuestDB.questList[num].isQuestAccept && QuestDB.questList[num].currentProgressCount >= QuestDB.questList[num].questProgress)
            {
                
                Console.WriteLine($"1. 보상 받기");
                Console.WriteLine($"2. 돌아가기\n");
            }
            else
            {
                Console.WriteLine("1. 퀘스트 수락");
                Console.WriteLine("2. 퀘스트 취소\n");
                Console.WriteLine("0. 퀘스트 목록으로 돌아가기\n");
            }
                
            Console.Write("원하시는 행동을 입력해주세요. \n>>");
            int choice = Input.GetInt();

            // 선택지에 따라서 행동도 다르게 설정하기.  보상 선택지 / 퀘스트 수락,취소 선택지 
            if (QuestDB.questList[num].isQuestAccept && QuestDB.questList[num].currentProgressCount >= QuestDB.questList[num].questProgress)
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("보상이 무사히 지급되었습니다!");
                        Player.Instance.gold += QuestDB.questList[num].questGoldReward;
                        // 나중에 플레이어 경험치 추가하면 주석 해제
                        Player.Instance.exp += QuestDB.questList[num].questExpReward;    
                        Console.WriteLine($"경험치 : {QuestDB.questList[num].questExpReward}를 획득하셨습니다!  [현재 경험치: {Player.Instance.exp}] ");
                        Console.WriteLine($"골드 : {QuestDB.questList[num].questGoldReward}를 획득하셨습니다!  [현재 골드: {Player.Instance.gold}] ");
                        AddItem.Inven(QuestDB.questList[num].rewardItemIndex);  // 인벤토리에 아이템 추가
                        if (QuestDB.questList[num].questItemReward != "없음")
                        {
                            Console.WriteLine($"아이템 : {QuestDB.questList[num].questItemReward}를 획득하셨습니다!");  // 아이템 획득 메시지
                        }
                        else Console.WriteLine();

                        LevelUp(out isLevelUp);             // 레벨업 함수 호출. 퀘스트 완료 보상으로 경험치 받고 레벨업했는지 여부 확인.
                        
                        if (isLevelUp)                     // 레벨업 여부 확인 후 작동.
                        {
                            Console.WriteLine("\n축하합니다! 레벨업 하셨습니다!!\n");
                            Console.WriteLine($"Lv.{Player.Instance.level - 1} -> Lv.{Player.Instance.level}");
                        }
                        QuestDB.questList[num].isQuestAccept = false;  // 보상받고 일단은 다시 수락 안한 상태로
                        QuestDB.questList[num].isQuestComplete = true;  // 퀘스트 완료 상태로 변경
                        QuestDB.questList[num].currentProgressCount = 0;
                        Console.ReadKey();
                        QuestScene();
                        break;
                    case 2:
                        QuestScene();
                        break;
                }
            }
            else
            {
                switch (choice)
                {
                    case 1:
                        QuestDB.questList[num].isQuestAccept = true;  // 퀘스트 수락
                        Console.WriteLine($"퀘스트 '{QuestDB.questList[num].questName}'을(를) 수락했습니다.");
                        Console.ReadKey();            // 아무키 누르면 계속 진행
                        QuestAcceptScene(questChoice);
                        break;
                    case 2:
                        QuestDB.questList[num].isQuestAccept = false;  // 퀘스트 거절
                        Console.WriteLine($"퀘스트 '{QuestDB.questList[num].questName}'을(를) 취소했습니다.");
                        QuestDB.questList[num].currentProgressCount = 0;  // 퀘스트 진행도 초기화
                        Console.ReadKey();
                        QuestAcceptScene(questChoice);
                        break;
                    case 0:
                        QuestScene();  // 퀘스트 목록으로 돌아가기
                        break;
                }
            }

        }


        private static QuestList? instance;  // 싱글톤을 위한 필드 선언
        public static QuestList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestList();
                }
                return instance;
            }
        }

        //임시로 레벨업 메서드 제작.
        public void LevelUp(out bool isLevelUp)
        {
            isLevelUp = false;     // LevelUp 함수가 쓰이는 스크립트에서 레벨업 했는지 안했는지 구분 해줌. 
            int needPlusExp = 0;   // 총 필요 경험치량 (레벨마다 다르게 설정)
            int levelLimit = 5;    //우선 만렙 5렙으로 설정
            for (int i = 1; i < levelLimit; i++)
            {
                if (Player.Instance.level == i)
                {
                    needPlusExp = NeedTotalExp(i);
                    if (Player.Instance.exp == needPlusExp)  //레벨마다 다른 경험치량 요구
                    {
                        Player.Instance.level += 1;
                        Player.Instance.exp = 0;  // 레벨업 하면 경험치 초기화
                        Player.Instance.atk += 1;  // 레벨업 하면 공격력 1 증가. 아래 방어력도 마찬가지
                        Player.Instance.def += 1;
                        isLevelUp = true;
                    }
                    else if (Player.Instance.exp > needPlusExp)   // 레벨업 필요 용량보다 많이 얻었을 때
                    {
                        Player.Instance.level += 1;
                        Player.Instance.exp = Player.Instance.exp - needPlusExp;     //레벨업 하면 남은 만큼만 표시하기
                        Player.Instance.atk += 1;
                        Player.Instance.def += 1;
                        isLevelUp = true;
                    }
                }


            }
        }

        // 재귀 함수 이용해서 레벨업에 필요한 경험치 총량 계산하기
        public static int NeedTotalExp(int level)
        {  // level 4 일때 10 + 25 + 30 + 35 ..
            int defaultExp = 10;
            if (level <= 0) return 0;

            if (level % 2 == 0)   //레벨이 짝수일때
            {
                return (defaultExp * level + 5) + NeedTotalExp(level - 1);
            }
            else     // 레벨이 홀수일때.
            {
                return (defaultExp * level) + NeedTotalExp(level - 1);
            }
        }
    }

    // 퀘스트 진행상황 달성 하는 메서드 모음 클래스
    internal class QuestConditioning
    {
        

        // 몬스터 처치 시 퀘스트 진행도 증가하는 메서드
        public void OnMonsterKilled(Monster monster)
        {
            // for문 써줘야 퀘스트가 여러개일 때도 진행도 증가 가능함. 안 쓰면 마지막 선택한 퀘스트 진행도만 오름.
            for (int i= 0; i < QuestDB.questList.Count; i++)
            {
                if (QuestDB.questList[i].isQuestAccept)  // 수주한 퀘스트 모두 확인
                {
                    if (monster.name == QuestDB.questList[i].targetMonsterName)
                    {
                        if (QuestDB.questList[i].currentProgressCount >= QuestDB.questList[i].questProgress)
                        {
                            QuestDB.questList[i].currentProgressCount = QuestDB.questList[i].questProgress;  // 퀘스트 완료시 진행도 맞춰주기
                        }
                        else QuestDB.questList[i].currentProgressCount++;
                    }
                    
                }
            }
                
        }



        // 싱글톤 : BattleScene에 퀘스트 진행상황 메서드 추가할 때마다 필요해서 생성
        private static QuestConditioning? instance;
        public static QuestConditioning Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestConditioning();
                }
                return instance;
            }
        }

    }

}
