using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinAnalytics
{
    public class TechAnalysis
    {
        int length; // Довжина масиву даних
        double[] timelist; // Масив часу
        double[] priceslistOpen; // Масив відкритих цін
        double[] priceslistHigh; // Масив найвищих цін
        double[] priceslistLow; // Масив найнижчих цін
        double[] priceslistClose; // Масив закритих цін

        // Конструктор класу, який приймає двовимірний масив рядків та перетворює його у масиви чисел
        public TechAnalysis(string[][] list)
        {
            length = list.Length; // Встановлюємо довжину масиву
            timelist = list.Select(arr => double.Parse(arr[0])).ToArray(); // Перетворюємо першу колонку в масив чисел
            priceslistOpen = list.Select(arr => double.Parse(arr[1])).ToArray(); // Перетворюємо другу колонку в масив чисел
            priceslistHigh = list.Select(arr => double.Parse(arr[2])).ToArray(); // Перетворюємо третю колонку в масив чисел
            priceslistLow = list.Select(arr => double.Parse(arr[3])).ToArray(); // Перетворюємо четверту колонку в масив чисел
            priceslistClose = list.Select(arr => double.Parse(arr[4])).ToArray(); // Перетворюємо п'яту колонку в масив чисел
        }

        // Метод для обчислення ковзної середньої (Moving Average)
        public double[] CalculateMA(double[] data, int period)
        {
            double[] sma = new double[data.Length]; // Масив для збереження ковзної середньої
            for (int i = 0; i < data.Length - period; i++) // Проходимо по всьому масиву даних
            {
                double sum = 0; // Змінна для зберігання суми
                for (int j = 0; j < period; j++) // Проходимо по кожному періоду
                {
                    sum += data[i + j]; // Додаємо значення до суми
                }
                sma[i] = sum / period; // Обчислюємо середнє значення
            }
            return sma; // Повертаємо масив ковзної середньої
        }

        // Метод для обчислення Індикатора Осцилятора (AO)
        public double[] CalculateAO()
        {
            double[] medianPrices = new double[length]; // Масив для зберігання медіанних цін
            for (int i = 0; i < length; i++) // Проходимо по всьому масиву даних
            {
                medianPrices[i] = (priceslistHigh[i] + priceslistLow[i]) / 2.0; // Обчислюємо медіанну ціну
            }
            double[] sma34 = CalculateMA(medianPrices, 34); // Обчислюємо 34-періодну ковзну середню
            double[] sma5 = CalculateMA(medianPrices, 5); // Обчислюємо 5-періодну ковзну середню
            double[] ao = new double[length]; // Масив для збереження значень AO
            for (int i = 0; i < length; i++) // Проходимо по всьому масиву даних
            {
                ao[i] = sma5[i] - sma34[i]; // Обчислюємо значення AO як різницю між SMA5 та SMA34
            }
            return ao; // Повертаємо масив значень AO
        }

        // Метод для обчислення Індексу Відносної Сили (RSI)
        public double[] CalculateRSI(double[] closePrices, int period)
        {
            double[] rsi = new double[closePrices.Length]; // Масив для збереження значень RSI
            double[] gain = new double[closePrices.Length]; // Масив для збереження підвищення цін
            double[] loss = new double[closePrices.Length]; // Масив для збереження пониження цін
            for (int i = 1; i < closePrices.Length; i++) // Проходимо по всьому масиву закритих цін
            {
                double diff = closePrices[i - 1] - closePrices[i]; // Обчислюємо різницю між поточним та попереднім значенням
                if (diff >= 0)
                {
                    gain[i] = diff; // Якщо різниця позитивна, додаємо до gain
                }
                else
                {
                    loss[i] = Math.Abs(diff); // Якщо різниця негативна, додаємо до loss
                }
            }
            double avgGain = gain.Take(period).Average(); // Обчислюємо середнє значення підвищення за період
            double avgLoss = loss.Take(period).Average(); // Обчислюємо середнє значення пониження за період
            rsi[period] = 100 - (100 / (1 + (avgGain / avgLoss))); // Обчислюємо перше значення RSI за формулою
            for (int i = 0; i < closePrices.Length - period; i++) // Проходимо по всьому масиву даних, починаючи з періоду
            {
                avgGain = (avgGain * (period - 1) + (gain[i])) / period; // Оновлюємо середнє значення підвищення
                avgLoss = (avgLoss * (period - 1) + (loss[i])) / period; // Оновлюємо середнє значення пониження

                rsi[i] = 100 - (100 / (1 + (avgGain / avgLoss))); // Обчислюємо значення RSI за формулою
            }
            return rsi; // Повертаємо масив значень RSI
        }
    }
}