using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace BitcoinAnalytics
{
    public class ApiHandler
    {
        private const string ApiKey = "Lftq2kmGvN7Lbf88Za"; // API ключ
        private const string ApiSecret = "t6gZKLtEEh3jvlCMscL8V5JJZ6MhcQKVudbN"; // Секретний ключ API
        private static readonly string Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(); // Поточний час в мілісекундах
        private const string RecvWindow = "5000"; // Часове вікно для прийому запиту в мілісекундах

        // Метод для генерації рядка запиту з параметрів
        private static string GenerateQueryString(Dictionary<string, object> parameters)
        {
            return string.Join("&", parameters.Select(p => $"{p.Key}={p.Value}"));
        }

        // Метод для обчислення підпису за допомогою HMACSHA256
        private static string ComputeSignature(string data)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(ApiSecret));
            byte[] signature = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(signature).Replace("-", "").ToLower();
        }

        // Метод для генерації підпису GET-запиту
        private static string GenerateGetSignature(Dictionary<string, object> parameters)
        {
            string queryString = GenerateQueryString(parameters);
            string rawData = Timestamp + ApiKey + RecvWindow + queryString;

            return ComputeSignature(rawData);
        }

        // Асинхронний метод для отримання даних про свічки за певний період
        async public Task<string[][]> GetSth(int period)
        {
            // Встановлення інтервалу залежно від вибраного періоду
            string interval = "3";
            if (period == 21600)
            {
                interval = "3"; // 6 годин - 3 хвилини
            }
            if (period == 86400)
            {
                interval = "15"; // доба - 15 хвилин
            }
            if (period == 604800)
            {
                interval = "60"; // тиждень - 1 година
            }
            if (period == 2628000)
            {
                interval = "360"; // місяць - 6 годин
            }

            // Визначення початкового часу для запиту
            Int64 start = Int64.Parse(Timestamp) - Convert.ToInt64(period) * 1000 * 2; // Удвоє більше для коректного обчислення всіх ковзних середніх
            var parameters = new Dictionary<string, object>
            {
                {"category", "linear"},
                {"symbol", "BTCUSDT"},
                {"interval", interval },
                {"start", start },
                {"end",  Timestamp},
                {"limit", 400 }
            };

            // Генерація підпису для запиту
            string signature = GenerateGetSignature(parameters);
            string queryString = GenerateQueryString(parameters);

            using var client = new HttpClient();
            // Створення HTTP-запиту
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api-testnet.bybit.com/v5/market/index-price-kline?{queryString}");

            // Додавання заголовків до запиту
            request.Headers.Add("X-BAPI-API-KEY", ApiKey);
            request.Headers.Add("X-BAPI-SIGN", signature);
            request.Headers.Add("X-BAPI-SIGN-TYPE", "2");
            request.Headers.Add("X-BAPI-TIMESTAMP", Timestamp);
            request.Headers.Add("X-BAPI-RECV-WINDOW", RecvWindow);

            // Надсилання запиту та отримання відповіді
            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            ResponseModel responseBody = JsonConvert.DeserializeObject<ResponseModel>(jsonString);

            // Повернення результатів запиту
            return responseBody.result.list;
        }
    }
}