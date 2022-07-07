namespace WebApplication1
{
    public interface IMyDependency
    {
        void WriteMessage(string message);
    }

    public class MyDependency : IMyDependency
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine($"MyDependency.WriteMessage Message: {message}");
        }
    }

    public class MyDependency2 : IMyDependency
    {
        private readonly ILogger<MyDependency2> _logger;
        IConfiguration _configuration;
        Service1 _service1;
        Service2 _service2;
        Service3 _service3;
        public MyDependency2(ILogger<MyDependency2> logger,IConfiguration configuration,Service1 service1,Service2 service2)
        {
            Service3 service3 = new Service3("sdf");
            _logger = logger;
            _configuration = configuration;
            _service1 = service1;
            _service2 = service2; 
            _service3 = service3; 
        }

        public void WriteMessage(string message)
        {
          var   positionOptions = _configuration.GetSection(PositionOptions.Position)
                                                     .Get<PositionOptions>();

            var key1 = _configuration.GetSection("AllowedHosts");
            var key2 = _configuration.GetSection("AllowedHosts-0000:test");
            _service1.Write("service1");
            _service2.Write("service2");
            _service3.Write("service3");
            
            var   color = _configuration.GetSection(ColorOptions.Color)
                                                     .Get<ColorOptions>();

            _logger.LogInformation($"MyDependency2.WriteMessage Message: {message} {positionOptions.Name} {positionOptions.Title} {color.ColorTitle+"  "+color.ColorName}");
        }
    }

    public class PositionOptions
    {
        public const string Position = "Position";

        public string Title { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
    }  
    public class ColorOptions
    {
        public const string Color = "Color";

        public string ColorTitle { get; set; } = String.Empty;
        public string ColorName { get; set; } = String.Empty;
    }


    public static class MyConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddConfig(
             this IServiceCollection services, IConfiguration config)
        {
            services.Configure<PositionOptions>(
                config.GetSection(PositionOptions.Position));
            services.Configure<ColorOptions>(
                config.GetSection(ColorOptions.Color));

            return services;
        }
    }

    public class Service1 : IDisposable
    {
        private bool _disposed;

        public void Write(string message)
        {
            Console.WriteLine($"Service1: {message}");
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            Console.WriteLine("Service1.Dispose");
            _disposed = true;
        }
    }

    public class Service2 : IDisposable
    {
        private bool _disposed;

        public void Write(string message)
        {
            Console.WriteLine($"Service2: {message}");
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            Console.WriteLine("Service2.Dispose");
            _disposed = true;
        }
    }

    public interface IService3
    {
        public void Write(string message);
    }

    public class Service3 : IService3, IDisposable
    {
        private bool _disposed;

        public Service3(string myKey)
        {
            MyKey = myKey;
        }

        public string MyKey { get; set; }

        public void Write(string message)
        {
            Console.WriteLine($"Service3: {message}, MyKey = {MyKey}");
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            Console.WriteLine("Service3.Dispose");
            _disposed = true;
        }
    }
}
