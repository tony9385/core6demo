namespace MES.ConfigService.Models
{
    public class AreaRepository : BaseRepository<AreaModel>, IAreaRepository
    {
        public AreaRepository(IConfiguration configuration):base(configuration)
        {

        }
        public async Task<List<AreaModel>> GetCount()
        {

            var i = await Task.Run(
                
                () =>  AreaDb.AsSugarClient().Queryable<AreaModel>().ToList()
                
            );
            return i;
        }

    }

    public interface IAreaRepository : IBaseRepository<AreaModel>
    {

        /// <summary>
        /// 获取用户数量
        /// </summary>
        /// <returns></returns>
        Task<List<AreaModel>> GetCount();

    }

    public interface IUserRepository : IBaseRepository<UserModel>
    {

        /// <summary>
        /// 获取用户数量
        /// </summary>
        /// <returns></returns>
        Task<List<UserModel>> GetCount();

        List<UserModel> GetList();

    }

    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<List<UserModel>> GetCount()
        {

            var i = await Task.Run(

                () => UserDb.AsSugarClient().Queryable<UserModel>().ToList()

            );
            return i;
        }

        public List<UserModel> GetList()
        {
            return Db.Queryable<UserModel>().ToList();
        }

    }
}
