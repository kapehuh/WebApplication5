using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebApplication5.Domain.Entity;

internal class Program
{
    private static async Task Main(string[] args)
    {
        //сервисы
        var services = new ServiceCollection();
        //добавляем сервисы связанные с HttpClient
        services.AddHttpClient();
        //провайдер сервисов
        var serviceProvider = services.BuildServiceProvider();
        //IHttpClientFactory
        var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
        //объект HttpClient
        var httpClient = httpClientFactory?.CreateClient();
        //httpClient.BaseAddress = new Uri("https://localhost:7191/api/User");
        int i;
        do
        {
            Console.Out.WriteLine("Введите число:\n" +
            "1 - Найти пользователя по ID\n" +
            "2 - Создать нового пользователя\n" +
            "3 - Выход\n");
            if (int.TryParse(Console.ReadLine(), out i))
            {
                switch (i)
                {
                    case 1:
                        if (httpClient!=null) await FindUserById(httpClient);
                        continue;
                    case 2:
                        if (httpClient != null) await AddNewUserData(httpClient);
                        continue;
                    case 3:
                        continue;
                    default:
                        Console.Out.WriteLine("Введите число от 1 до 3");
                        continue;
                }
            }
        } while (i != 3);
    }

    private static async Task AddNewUserData(HttpClient httpClient)
    {
        User n = new User();
        n.Age = GenerateAge();
        n.Name = GenerateName(new Random().Next(9));
        n.Email = n.Name + "@gmail.com";
        var data = new System.Net.Http.StringContent(JsonSerializer.Serialize(n), Encoding.UTF8, "application/json");

        using HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7191/api/User/", data);
        Console.WriteLine($"Status: {response.StatusCode}");
        Console.WriteLine("Создан новый пользователь:");
        string content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content + "\n");
    }

    private static async Task FindUserById(HttpClient httpClient)
    {
        Console.Out.WriteLine("Введите id пользователя:\n");
        if (int.TryParse(Console.ReadLine(), out int ii))
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7191/api/User/{ii}");
            using HttpResponseMessage response = await httpClient.SendAsync(request);
            Console.WriteLine($"Status: {response.StatusCode}");
            Console.WriteLine("User data:");
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content + "\n");
        }
        else
        {
            Console.WriteLine($"Id - должно быть числом\n");
        } 
    }

    // ================================================================================================
    // ================================================================================================
    // ================================================================================================
    // CREATE new user
    public static string GenerateName(int len)
    {
        Random r = new Random();
        string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
        string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
        string Name = "";
        Name += consonants[r.Next(consonants.Length)].ToUpper();
        Name += vowels[r.Next(vowels.Length)];
        int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
        while (b < len)
        {
            Name += consonants[r.Next(consonants.Length)];
            b++;
            Name += vowels[r.Next(vowels.Length)];
            b++;
        }
        return Name;
    }
    public static int GenerateAge()
    {
        Random r = new Random();
        int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };
        int n = r.Next(nums.Length);
        return n;
    }
}