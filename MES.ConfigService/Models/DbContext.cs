using SqlSugar;

namespace MES.ConfigService.Models
{
    public class DbContext<T> where T : class, new()
    {
        IConfiguration _configuration;
        public DbContext(IConfiguration configuration)
        {

            _configuration = configuration;
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = configuration["ConnectionStrings"],                
                DbType = DbType.MySql,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            });

            Console.WriteLine("init sql sugar");


            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

        }
        //注意：不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来操作当前表的数据

        public SimpleClient<AreaModel> AreaDb { get { return new SimpleClient<AreaModel>(Db); } }
        public SimpleClient<UserModel> UserDb { get { return new SimpleClient<UserModel>(Db); } }

    }
}
