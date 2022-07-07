using System.Net;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string URI = "http://localhost:5098/WeatherForecast";

            int batchIndex = 1;
            Task.Run(() => { 
            while (true)
                {
                    List<SignalModel> dd = new List<SignalModel>();
                    dd.Add(new SignalModel() { SignalId = 10001, SignalValue = "1", ReceivedTime = DateTime.Now , BatchIndex=batchIndex});
                    dd.Add(new SignalModel() { SignalId = 10002, SignalValue = "2", ReceivedTime = DateTime.Now, BatchIndex = batchIndex });

                    using (WebClient client = new WebClient())
                    {
                        //System.Collections.Specialized.NameValueCollection postData =
                        //    new System.Collections.Specialized.NameValueCollection();
                        //postData.Add("info", "22");
                        client.Headers.Add("Content-Type", "application/json");




                        var userJsonString = client.UploadString(URI, JsonConvert.SerializeObject(dd));

                        batchIndex++;
                    }

                    Thread.Sleep(1000); 
                }
            
            });
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Counter = 64;
            int initialCapacity = 101;
            int numProcs = Environment.ProcessorCount;
            int concurrencyLevel = numProcs * 2;
            ConcurrentDictionary<int, int> cd = new ConcurrentDictionary<int, int>(concurrencyLevel, initialCapacity);
            for (int i = 1; i <= Counter; i++) cd[i] = i * i;
            Console.WriteLine("The square of 23 is {0} (should be {1})", cd[23], 23 * 23);
            for (int i = 1; i <= Counter + 1; i++)
                cd.AddOrUpdate(i, i * i, (k, v) => v / i);
            Console.WriteLine("The square root of 529 is {0} (should be {1})", cd[23], 529 / 23);
            Console.WriteLine("The square of 65 is {0} (should be {1})", cd[Counter + 1], ((Counter + 1) * (Counter + 1)));

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            string URI = "http://localhost:5098/app";

            Task.Run(() => {
                while (true)
                {
                    AppModel mondel = new AppModel() {  ControlType="close"};

                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("Content-Type", "application/json");
                        var userJsonString = client.UploadString(URI, 
                            JsonConvert.SerializeObject(mondel));
                    }

                    Thread.Sleep(1000);
                }
            });
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //var path = @"C:\Users\tony\source\repos\WebApplication1\WindowsFormsApp1\bin\Debug\WindowsFormsApp1.exe"; ;
            //Process.Start(path);

            string URI = "http://localhost:5098/app";

            Task.Run(() => {
                while (true)
                {
                    AppModel mondel = new AppModel() { ControlType = "open" };

                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("Content-Type", "application/json");
                        var userJsonString = client.UploadString(URI,
                            JsonConvert.SerializeObject(mondel));
                    }

                    Thread.Sleep(1000);
                }
            });
        }
    }



    public class SignalModel
    {
        public int BatchIndex { get; set; }
        public int SignalId { get; set; }
        public string SignalValue { get; set; }
        public DateTime ReceivedTime { get; set; }
    }


    public class AppModel
    {
        public string ControlType { get; set; }
    }
}