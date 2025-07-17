using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class Opening
    {
        public void Run()
        {
            // 화면 리셋
            Console.Clear();

            // 시작 문구 출력
            Console.WriteLine("TXT RPG에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.Write(">>");

            // 이름 입력 받기
            string name = Console.ReadLine();

            // 이름 확인
            Console.WriteLine($"\n 입력하신 이름이 {name} 이(가) 맞습니까?\n");
            Console.WriteLine("1. 확인");
            Console.WriteLine("0. 취소");
            Console.Write(">>");
            int num = Input.GetInt(0, 1);
            switch (num)
            {
                case 0:     // 취소 시 Run 재호출
                    Run();
                    return;
                case 1:     // 확인 시 다음으로
                    break;
            }

            // 화면 리셋
            Console.Clear();

            // 직업 선택 화면 출력
            Console.WriteLine("이제 직업을 선택할 차례입니다.");
            Console.WriteLine("원하시는 직업을 선택해주세요.\n");

            // 직업 목록 출력 예정
            Console.WriteLine("1. 전사");
            Console.WriteLine("방어력(구현 안됨)과 기본 체력이 높습니다,");
            Console.WriteLine("2. 검사");
            Console.WriteLine("상대적으로 낮은 체력에 높은 공격력을 가집니다.");
            Console.WriteLine("3. 거지");
            Console.WriteLine("가지지 못한 자입니다. 하드코어용입니다.");

            // 입력 받기
            int choiceJob = Input.GetInt(1, 3);

            // 캐릭터 생성
            switch (choiceJob)
            {
                case 1:
                    Player.Instance.Init(name, "전사", 10, 5, 100, 50, 15000);
                    break;
                case 2:
                    Player.Instance.Init(name, "검사", 15, 3, 80, 60, 15000);
                    break;
                case 3:
                    Player.Instance.Init(name, "거지", 5, 0, 50, 10, 1000);
                    break;
            }

            // 입력 확인 후 시작 씬으로 이동
            Console.WriteLine("캐릭터 생성이 완료되었습니다! 재미있게 즐겨주세요!");
            Console.WriteLine("(아무 키나 눌러서 시작합니다)");
            Console.ReadKey();
            return;

        }
    }
}
