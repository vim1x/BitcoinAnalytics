using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

namespace BitcoinAnalytics
{
    public partial class BitcoinChart : Form
    {
        private static readonly string Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(); // Створення мітки часу
        ApiHandler apiHandler = new ApiHandler(); // Ініціалізація об'єкта для обробки API
        TechAnalysis techAnalysis; // Ініціалізація об'єкта для технічного аналізу

        public BitcoinChart()
        {
            InitializeComponent(); // Ініціалізація компонентів форми
        }

        // Подія завантаження форми
        private void Form1_Load(object sender, EventArgs e)
        {
            int period = 86400; // Секунди (доба)
            updateChart(86400); // Оновлення графіка
        }

        // Асинхронний метод для оновлення графіка
        private async void updateChart(int period)
        {
            // Отримання даних від API
            string[][] list = await apiHandler.GetSth(period);
            if (list.Length == 0) // Перевірка наявності даних
                return;

            // Перетворення міток часу в DateTime
            DateTime[] datetimes = list.Select(arr => DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(arr[0])).DateTime).ToArray();
            double[] priceslistOpen = list.Select(arr => double.Parse(arr[1])).ToArray(); // Масив відкритих цін
            double[] priceslistHigh = list.Select(arr => double.Parse(arr[2])).ToArray(); // Масив найвищих цін
            double[] priceslistLow = list.Select(arr => double.Parse(arr[3])).ToArray(); // Масив найнижчих цін
            double[] priceslistClose = list.Select(arr => double.Parse(arr[4])).ToArray(); // Масив закритих цін

            chart.Series.Clear(); // Очищення серій графіка
            chart.Series.Add("BTC/USDT"); // Додавання серії для відображення даних
            chart.Series[0].ChartType = SeriesChartType.Candlestick; // Встановлення типу графіка
            chart.Series[0].XValueType = ChartValueType.DateTime; // Встановлення типу значень по осі X
            chart.Series[0].YValueType = ChartValueType.Double; // Встановлення типу значень по осі Y
            for (int i = 0; i < datetimes.Length / 2; i++) // Заповнення графіка даними
            {
                chart.Series[0].Points.AddXY(datetimes[i], priceslistHigh[i], priceslistLow[i], priceslistOpen[i], priceslistClose[i]);
            }
            chart.Series[0]["PriceUpColor"] = "Green"; // Встановлення кольору для зростаючої ціни
            chart.Series[0]["PriceDownColor"] = "Red"; // Встановлення кольору для спадаючої ціни
            chart.ChartAreas[0].AxisY.Minimum = priceslistLow.Min(); // Встановлення мінімального значення осі Y
            chart.ChartAreas[0].AxisY.Maximum = priceslistHigh.Max(); // Встановлення максимального значення осі Y

            // Додавання ковзної середньої (MA10) до графіка
            chart.Series.Add("MA10");
            techAnalysis = new TechAnalysis(list); // Ініціалізація об'єкта технічного аналізу
            double[] sma10 = techAnalysis.CalculateMA(priceslistClose, 10); // Обчислення MA10
            chart.Series[1].ChartType = SeriesChartType.Line; // Встановлення типу графіка
            chart.Series[1].XValueType = ChartValueType.DateTime; // Встановлення типу значень по осі X
            chart.Series[1].YValueType = ChartValueType.Double; // Встановлення типу значень по осі Y
            chart.Series[1].Color = Color.BlueViolet; // Встановлення кольору лінії
            for (int i = 0; i < datetimes.Length / 2; i++) // Заповнення графіка даними MA10
            {
                chart.Series[1].Points.AddXY(datetimes[i], sma10[i]);
            }

            // Додавання ковзної середньої (MA20) до графіка
            chart.Series.Add("MA20");
            double[] sma20 = techAnalysis.CalculateMA(priceslistClose, 20); // Обчислення MA20
            chart.Series[2].ChartType = SeriesChartType.Line; // Встановлення типу графіка
            chart.Series[2].XValueType = ChartValueType.DateTime; // Встановлення типу значень по осі X
            chart.Series[2].YValueType = ChartValueType.Double; // Встановлення типу значень по осі Y
            chart.Series[2].Color = Color.HotPink; // Встановлення кольору лінії
            for (int i = 0; i < datetimes.Length / 2; i++) // Заповнення графіка даними MA20
            {
                chart.Series[2].Points.AddXY(datetimes[i], sma20[i]);
            }

            // Додавання ковзної середньої (MA50) до графіка
            chart.Series.Add("MA50");
            double[] sma50 = techAnalysis.CalculateMA(priceslistClose, 50); // Обчислення MA50
            chart.Series[3].ChartType = SeriesChartType.Line; // Встановлення типу графіка
            chart.Series[3].XValueType = ChartValueType.DateTime; // Встановлення типу значень по осі X
            chart.Series[3].YValueType = ChartValueType.Double; // Встановлення типу значень по осі Y
            chart.Series[3].Color = Color.LightBlue; // Встановлення кольору лінії
            for (int i = 0; i < datetimes.Length / 2; i++) // Заповнення графіка даними MA50
            {
                chart.Series[3].Points.AddXY(datetimes[i], sma50[i]);
            }

            double[][] smas = { sma10, sma20, sma50 }; // Створення масиву ковзних середніх
            CryptoForecast(list, smas); // Виклик методу прогнозування
        }

        // Метод для обробки натискання кнопки оновлення на 6 годин
        private void button1_Click(object sender, EventArgs e)
        {
            updateChart(21600); // Оновлення графіка для періоду 6 годин
        }

        // Метод для обробки натискання кнопки оновлення на добу
        private void button2_Click(object sender, EventArgs e)
        {
            updateChart(86400); // Оновлення графіка для періоду доби
        }

        // Метод для обробки натискання кнопки оновлення на тиждень
        private void button3_Click(object sender, EventArgs e)
        {
            updateChart(604800); // Оновлення графіка для періоду тижня
        }

        // Метод для обробки натискання кнопки оновлення на місяць
        private void button4_Click(object sender, EventArgs e)
        {
            updateChart(2628000); // Оновлення графіка для періоду місяця
        }

        // Метод для прогнозування криптовалюти
        private void CryptoForecast(string[][] list, double[][] smas)
        {
            double[] sma10 = smas[0]; // Ковзна середня для періоду 10
            double[] sma20 = smas[1]; // Ковзна середня для періоду 20
            double[] sma50 = smas[2]; // Ковзна середня для періоду 50

            // Перетворення міток часу в DateTime
            DateTime[] datetimes = list.Select(arr => DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(arr[0])).DateTime).ToArray();
            double[] priceslistOpen = list.Select(arr => double.Parse(arr[1])).ToArray(); // Масив відкритих цін
            double[] priceslistHigh = list.Select(arr => double.Parse(arr[2])).ToArray(); // Масив найвищих цін
            double[] priceslistLow = list.Select(arr => double.Parse(arr[3])).ToArray(); // Масив найнижчих цін
            double[] priceslistClose = list.Select(arr => double.Parse(arr[4])).ToArray(); // Масив закритих цін

            // Обчислення RSI (Relative Strength Index) для періоду 14
            double[] rsi = techAnalysis.CalculateRSI(priceslistClose, 14);

            // Встановлення початкових значень для міток прогнозування
            label3.Text = "Нейтрально";
            label3.ForeColor = Color.Black;
            label5.Text = "Нейтрально";
            label5.ForeColor = Color.Black;
            label7.Text = "Нейтрально";
            label7.ForeColor = Color.Black;

            // Логіка прогнозування на основі ковзних середніх
            if (sma10[0] > priceslistClose[0])
            {
                label3.Text = "Продавати";
                label3.ForeColor = Color.Red;
            }
            if (sma10[0] < priceslistClose[0])
            {
                label3.Text = "Покупати";
                label3.ForeColor = Color.Green;
            }
            if (sma20[0] > priceslistClose[0])
            {
                label5.Text = "Продавати";
                label5.ForeColor = Color.Red;
            }
            if (sma20[0] < priceslistClose[0])
            {
                label5.Text = "Покупати";
                label5.ForeColor = Color.Green;
            }
            if (sma50[0] > priceslistClose[0])
            {
                label7.Text = "Продавати";
                label7.ForeColor = Color.Red;
            }
            if (sma50[0] < priceslistClose[0])
            {
                label7.Text = "Покупати";
                label7.ForeColor = Color.Green;
            }

            // Обчислення AO (Awesome Oscillator)
            double[] ao = techAnalysis.CalculateAO();
            label8.Text = $"Awesome Oscillator: {Math.Round(ao[0], 2)}";

            // Логіка прогнозування на основі AO
            if (ao[0] > 0)
            {
                label9.Text = "Покупати";
                label9.ForeColor = Color.Green;
            }
            else if (ao[0] < 0)
            {
                label9.Text = "Продавати";
                label9.ForeColor = Color.Red;
            }

            // Встановлення значення RSI
            label10.Text = $"RSI(14): {Math.Round(rsi[0], 2)}";

            // Логіка прогнозування на основі RSI
            if (rsi[0] > 70)
            {
                label11.Text = "Продавати";
                label11.ForeColor = Color.Red;
            }
            else if (rsi[0] < 30)
            {
                label11.Text = "Покупати";
                label11.ForeColor = Color.Green;
            }
            else
            {
                label11.Text = "Нейтрально";
                label11.ForeColor = Color.Black;
            }
        }
    }
}