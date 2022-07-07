using MES.ConfigService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MES.ConfigService.Controllers
{
    //[SampleActionFilter]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository _areaRepository;
        public UserController(IUserRepository areaRepository)
        {
            _areaRepository = areaRepository;   
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var user=_areaRepository.GetList().Select(p=> JsonConvert.SerializeObject(p)).ToList();
            return user;
           // return Newtonsoft.Json.JsonConvert.SerializeObject(user);              
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var user = _areaRepository.QueryByID(id).Result;

            return Newtonsoft.Json.JsonConvert.SerializeObject(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserModel value)
        {
            _areaRepository.Update(value);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            int[] ids=new int[1];
            ids[0] = id;
            _areaRepository.DeleteByIds(new [] { id.ToString()});
        }
    }
}
