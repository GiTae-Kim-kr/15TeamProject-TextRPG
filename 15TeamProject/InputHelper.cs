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
}