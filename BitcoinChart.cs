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
        private static readonly string Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(); // ��������� ���� ����
        ApiHandler apiHandler = new ApiHandler(); // ����������� ��'���� ��� ������� API
        TechAnalysis techAnalysis; // ����������� ��'���� ��� ��������� ������

        public BitcoinChart()
        {
            InitializeComponent(); // ����������� ���������� �����
        }

        // ���� ������������ �����
        private void Form1_Load(object sender, EventArgs e)
        {
            int period = 86400; // ������� (����)
            updateChart(86400); // ��������� �������
        }

        // ����������� ����� ��� ��������� �������
        private async void updateChart(int period)
        {
            // ��������� ����� �� API
            string[][] list = await apiHandler.GetSth(period);
            if (list.Length == 0) // �������� �������� �����
                return;

            // ������������ ���� ���� � DateTime
            DateTime[] datetimes = list.Select(arr => DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(arr[0])).DateTime).ToArray();
            double[] priceslistOpen = list.Select(arr => double.Parse(arr[1])).ToArray(); // ����� �������� ���
            double[] priceslistHigh = list.Select(arr => double.Parse(arr[2])).ToArray(); // ����� �������� ���
            double[] priceslistLow = list.Select(arr => double.Parse(arr[3])).ToArray(); // ����� ��������� ���
            double[] priceslistClose = list.Select(arr => double.Parse(arr[4])).ToArray(); // ����� �������� ���

            chart.Series.Clear(); // �������� ���� �������
            chart.Series.Add("BTC/USDT"); // ��������� ��� ��� ����������� �����
            chart.Series[0].ChartType = SeriesChartType.Candlestick; // ������������ ���� �������
            chart.Series[0].XValueType = ChartValueType.DateTime; // ������������ ���� ������� �� �� X
            chart.Series[0].YValueType = ChartValueType.Double; // ������������ ���� ������� �� �� Y
            for (int i = 0; i < datetimes.Length / 2; i++) // ���������� ������� ������
            {
                chart.Series[0].Points.AddXY(datetimes[i], priceslistHigh[i], priceslistLow[i], priceslistOpen[i], priceslistClose[i]);
            }
            chart.Series[0]["PriceUpColor"] = "Green"; // ������������ ������� ��� ��������� ����
            chart.Series[0]["PriceDownColor"] = "Red"; // ������������ ������� ��� �������� ����
            chart.ChartAreas[0].AxisY.Minimum = priceslistLow.Min(); // ������������ ���������� �������� �� Y
            chart.ChartAreas[0].AxisY.Maximum = priceslistHigh.Max(); // ������������ ������������� �������� �� Y

            // ��������� ������ �������� (MA10) �� �������
            chart.Series.Add("MA10");
            techAnalysis = new TechAnalysis(list); // ����������� ��'���� ��������� ������
            double[] sma10 = techAnalysis.CalculateMA(priceslistClose, 10); // ���������� MA10
            chart.Series[1].ChartType = SeriesChartType.Line; // ������������ ���� �������
            chart.Series[1].XValueType = ChartValueType.DateTime; // ������������ ���� ������� �� �� X
            chart.Series[1].YValueType = ChartValueType.Double; // ������������ ���� ������� �� �� Y
            chart.Series[1].Color = Color.BlueViolet; // ������������ ������� ��
            for (int i = 0; i < datetimes.Length / 2; i++) // ���������� ������� ������ MA10
            {
                chart.Series[1].Points.AddXY(datetimes[i], sma10[i]);
            }

            // ��������� ������ �������� (MA20) �� �������
            chart.Series.Add("MA20");
            double[] sma20 = techAnalysis.CalculateMA(priceslistClose, 20); // ���������� MA20
            chart.Series[2].ChartType = SeriesChartType.Line; // ������������ ���� �������
            chart.Series[2].XValueType = ChartValueType.DateTime; // ������������ ���� ������� �� �� X
            chart.Series[2].YValueType = ChartValueType.Double; // ������������ ���� ������� �� �� Y
            chart.Series[2].Color = Color.HotPink; // ������������ ������� ��
            for (int i = 0; i < datetimes.Length / 2; i++) // ���������� ������� ������ MA20
            {
                chart.Series[2].Points.AddXY(datetimes[i], sma20[i]);
            }

            // ��������� ������ �������� (MA50) �� �������
            chart.Series.Add("MA50");
            double[] sma50 = techAnalysis.CalculateMA(priceslistClose, 50); // ���������� MA50
            chart.Series[3].ChartType = SeriesChartType.Line; // ������������ ���� �������
            chart.Series[3].XValueType = ChartValueType.DateTime; // ������������ ���� ������� �� �� X
            chart.Series[3].YValueType = ChartValueType.Double; // ������������ ���� ������� �� �� Y
            chart.Series[3].Color = Color.LightBlue; // ������������ ������� ��
            for (int i = 0; i < datetimes.Length / 2; i++) // ���������� ������� ������ MA50
            {
                chart.Series[3].Points.AddXY(datetimes[i], sma50[i]);
            }

            double[][] smas = { sma10, sma20, sma50 }; // ��������� ������ ������� �������
            CryptoForecast(list, smas); // ������ ������ �������������
        }

        // ����� ��� ������� ���������� ������ ��������� �� 6 �����
        private void button1_Click(object sender, EventArgs e)
        {
            updateChart(21600); // ��������� ������� ��� ������ 6 �����
        }

        // ����� ��� ������� ���������� ������ ��������� �� ����
        private void button2_Click(object sender, EventArgs e)
        {
            updateChart(86400); // ��������� ������� ��� ������ ����
        }

        // ����� ��� ������� ���������� ������ ��������� �� �������
        private void button3_Click(object sender, EventArgs e)
        {
            updateChart(604800); // ��������� ������� ��� ������ �����
        }

        // ����� ��� ������� ���������� ������ ��������� �� �����
        private void button4_Click(object sender, EventArgs e)
        {
            updateChart(2628000); // ��������� ������� ��� ������ �����
        }

        // ����� ��� ������������� ������������
        private void CryptoForecast(string[][] list, double[][] smas)
        {
            double[] sma10 = smas[0]; // ������ ������� ��� ������ 10
            double[] sma20 = smas[1]; // ������ ������� ��� ������ 20
            double[] sma50 = smas[2]; // ������ ������� ��� ������ 50

            // ������������ ���� ���� � DateTime
            DateTime[] datetimes = list.Select(arr => DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(arr[0])).DateTime).ToArray();
            double[] priceslistOpen = list.Select(arr => double.Parse(arr[1])).ToArray(); // ����� �������� ���
            double[] priceslistHigh = list.Select(arr => double.Parse(arr[2])).ToArray(); // ����� �������� ���
            double[] priceslistLow = list.Select(arr => double.Parse(arr[3])).ToArray(); // ����� ��������� ���
            double[] priceslistClose = list.Select(arr => double.Parse(arr[4])).ToArray(); // ����� �������� ���

            // ���������� RSI (Relative Strength Index) ��� ������ 14
            double[] rsi = techAnalysis.CalculateRSI(priceslistClose, 14);

            // ������������ ���������� ������� ��� ���� �������������
            label3.Text = "����������";
            label3.ForeColor = Color.Black;
            label5.Text = "����������";
            label5.ForeColor = Color.Black;
            label7.Text = "����������";
            label7.ForeColor = Color.Black;

            // ����� ������������� �� ����� ������� �������
            if (sma10[0] > priceslistClose[0])
            {
                label3.Text = "���������";
                label3.ForeColor = Color.Red;
            }
            if (sma10[0] < priceslistClose[0])
            {
                label3.Text = "��������";
                label3.ForeColor = Color.Green;
            }
            if (sma20[0] > priceslistClose[0])
            {
                label5.Text = "���������";
                label5.ForeColor = Color.Red;
            }
            if (sma20[0] < priceslistClose[0])
            {
                label5.Text = "��������";
                label5.ForeColor = Color.Green;
            }
            if (sma50[0] > priceslistClose[0])
            {
                label7.Text = "���������";
                label7.ForeColor = Color.Red;
            }
            if (sma50[0] < priceslistClose[0])
            {
                label7.Text = "��������";
                label7.ForeColor = Color.Green;
            }

            // ���������� AO (Awesome Oscillator)
            double[] ao = techAnalysis.CalculateAO();
            label8.Text = $"Awesome Oscillator: {Math.Round(ao[0], 2)}";

            // ����� ������������� �� ����� AO
            if (ao[0] > 0)
            {
                label9.Text = "��������";
                label9.ForeColor = Color.Green;
            }
            else if (ao[0] < 0)
            {
                label9.Text = "���������";
                label9.ForeColor = Color.Red;
            }

            // ������������ �������� RSI
            label10.Text = $"RSI(14): {Math.Round(rsi[0], 2)}";

            // ����� ������������� �� ����� RSI
            if (rsi[0] > 70)
            {
                label11.Text = "���������";
                label11.ForeColor = Color.Red;
            }
            else if (rsi[0] < 30)
            {
                label11.Text = "��������";
                label11.ForeColor = Color.Green;
            }
            else
            {
                label11.Text = "����������";
                label11.ForeColor = Color.Black;
            }
        }
    }
}