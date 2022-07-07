namespace MES.ConfigService.Models
{
    [SqlSugar.SugarTable("mes_area")]
    public class AreaModel
    {
        public int ID { get; set; }
        public string AreaName { get; set; }
        //public int AreaType { get; set; }   
        public string Remark { get; set; }  
    }

    [SqlSugar.SugarTable("mes_user")]
    public class UserModel
    {
        [SqlSugar.SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int ID { get; set; }
        public string UserName { get; set; }
        //public int AreaType { get; set; }   
        public string Remark { get; set; }
    }
}
