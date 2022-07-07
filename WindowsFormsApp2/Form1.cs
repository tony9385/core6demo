using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        IUnityContainer container = new Unity.UnityContainer();
        
        public Form1()
        {
            InitializeComponent();
            container.RegisterType<ICar, BMW>();
            container.RegisterType<ICar, Audi>("LuxuryCar");

            //container.RegisterType<Driver>("LuxuryCarDriver",
            //    new InjectionConstructor(container.Resolve<ICar>("LuxuryCar")));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string URI = "http://localhost:5098/api/app";

            AppModel mondel = new AppModel() { ControlType = "close" };
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");
                var userJsonString = client.UploadString(URI,
                    JsonConvert.SerializeObject(mondel));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string URI = "http://localhost:5098/api/app";

            AppModel mondel = new AppModel() { ControlType = "open" };
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");
                var userJsonString = client.UploadString(URI,
                    JsonConvert.SerializeObject(mondel));
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            
            var driver=container.Resolve<Driver>();
            driver.RunCar();

            //Driver driver2 = container.Resolve<Driver>();
            //driver2.RunCar();

          

            Driver driver2 = container.Resolve<Driver>("LuxuryCarDriver");// injects Audi
            driver2.RunCar();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var container = new UnityContainer();
            ICar audi = new Audi();
            container.RegisterInstance<ICar>(audi);

            Driver driver1 = container.Resolve<Driver>();
            driver1.RunCar();
            driver1.RunCar();

            Driver driver2 = container.Resolve<Driver>();
            driver2.RunCar();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //var container = new UnityContainer();

            //container.RegisterType<ICar, Audi>();
            //container.RegisterType<ICarKey, AudiKey>();

            //var driver = container.Resolve<Driver>();
            //driver.RunCar();


            var container = new UnityContainer()
                .RegisterType<ICar, BMW>();

            var driver1 = container.Resolve<Driver>();
            driver1.RunCar();

            var driver2 = container.Resolve<Driver>(new ResolverOverride[] {
        new ParameterOverride("car", new Ford())
});
            driver2.RunCar();
        }
    }



    public class AppModel
    {
        public string ControlType { get; set; }
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
            return ++_miles;
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

        public Driver(ICar car)
        {
            _car = car;
        }

        public void RunCar()
        {
            Console.WriteLine("Running {0} - {1} mile ", _car.GetType().Name, _car.Run());
        }
    }

    public interface ICarKey
    {

    }

    public class BMWKey : ICarKey
    {

    }

    public class AudiKey : ICarKey
    {

    }

    public class FordKey : ICarKey
    {

    }

    //public class Driver
    //{
    //    private ICar _car = null;
    //    private ICarKey _key = null;

    //    public Driver(ICar car, ICarKey key)
    //    {
    //        _car = car;
    //        _key = key;
    //    }

    //    public void RunCar()
    //    {
    //        Console.WriteLine("Running {0} with {1} - {2} mile ", _car.GetType().Name, _key.GetType().Name, _car.Run());
    //    }
    //}
}
