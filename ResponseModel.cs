using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinAnalytics
{
    internal class ResponseModel
    {
        // Поле для коду відповіді сервера
        public int retCode { get; set; }

        // Поле для повідомлення від сервера
        public string retMsg { get; set; }

        // Поле для результату відповіді, містить основні дані
        public Result result { get; set; }

        // Поле для додаткової інформації, яка може бути повернена сервером
        public Retextinfo retExtInfo { get; set; }

        // Поле для часу відповіді сервера в мілісекундах
        public long time { get; set; }

        // Внутрішній клас для результату відповіді
        public class Result
        {
            // Поле для символу, до якого відносяться дані (наприклад, BTCUSDT)
            public string symbol { get; set; }

            // Поле для категорії даних (наприклад, linear)
            public string category { get; set; }

            // Поле для списку свічок, де кожна свічка представлена як масив строк
            public string[][] list { get; set; }
        }

        // Внутрішній клас для додаткової інформації (може бути розширений в майбутньому)
        public class Retextinfo
        {
        }
    }
}