using System.ComponentModel;
using Unity;
using Unity.Lifetime;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        IUnityContainer container = null;
        public Form1()
        {
            container = new UnityContainer();

            //container.RegisterType<ICar, BMW>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICar, BMW>(new PerResolveLifetimeManager());
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var bgw = new BackgroundWorker();
            bgw.DoWork += (_, __) =>
            {
                Thread.Sleep(5000);
            };
            bgw.RunWorkerCompleted += (_, __) =>
            {
                MessageBox.Show("Hi from the UI thread!");
            };
            bgw.RunWorkerAsync();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(5000);
            });
            MessageBox.Show("Hi from the UI thread!");
        }

        private  void button3_Click(object sender, EventArgs e)
        {
            Task.Run( async () => { 
                await DoSomethingAsync();
                this.Invoke(() =>
                {
                    this.textBox1.Text = "finish a task;";
                });
                
            });

            this.textBox1.Text = "i'm here";
        }

        public async Task DoSomethingAsync()
        {
            // In the Real World, we would actually do something...
            // For this example, we're just going to (asynchronously) wait 100ms.
            await Task.Delay(5000);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Task.Delay(10000).Wait();
            this.textBox1.Text = "done";
        }
        Driver drv = null;
        private void button5_Click(object sender, EventArgs e)
        {
           

            drv = container.Resolve<Driver>();
            drv.showMessage += ShowMessage;
            drv.RunCar();


        }

        private void ShowMessage(string mes)
        {
            this.Invoke(() => { this.textBox1.Text += mes; });
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Driver drv2 = container.Resolve<Driver>();
            drv2.showMessage += ShowMessage;
            drv2.RunCar();
        }
    }



    public interface ICar
    {
        int Run();
    }

    public class BMW : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            _miles += 5;
            return _miles;
        }
    }

    public class Ford : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }
    }

    public class Audi : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }

    }
    public class Driver
    {
        private ICar _car = null;
        public Action<string> showMessage;

        public Driver(ICar car)
        {
            _car = car;
        }

        public void RunCar()
        {
            showMessage?.Invoke(String.Format("Running {0} - {1} mile ", _car.GetType().Name, _car.Run()));
            //Console.WriteLine("Running {0} - {1} mile ", _car.GetType().Name, _car.Run());
        }
    }
}