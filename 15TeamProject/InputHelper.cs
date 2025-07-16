internal static class Input
{
    // 사용 예시) int choice = Input.GetInt(); 와 같이 사용
    public static int GetInt()
    {
        int num;

        while (true)
        {
            string? input = Console.ReadLine();

            if (int.TryParse(input, out num))
            {
                return num;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }

    // 최댓값, 최솟값 있는 버전, 기존 코드 수정 안해도 됩니다.
    // Input.GetInt(최솟값, 최댓값); 처럼 작성하시면 범위 외의 값을 제외시킵니다.
    public static int GetInt(int min, int max)
    {
        while (true)
        {
            int num = GetInt();

            if (num >= min && num <= max)
            {
                return num;
            }
            else
            {
                Console.WriteLine("잘못된 번호입니다.");
            }
        }
    }
}