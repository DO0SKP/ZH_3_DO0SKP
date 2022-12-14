using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZH_3_DO0SKP_API.JokeModels;

namespace ZH_3_DO0SKP_API.Controllers
{
    [Route("api/Joke")]
    [ApiController]
    public class JokeController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            FunnyDatabaseContext context = new FunnyDatabaseContext();
            return Ok(context.Jokes.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            FunnyDatabaseContext context = new FunnyDatabaseContext();
            var lekerd = (from x in context.Jokes
                         where x.JokeSk == id
                         select x).FirstOrDefault();
            return Ok(lekerd);
        }

        [HttpPost]
        public void Post([FromBody] Joke ujvicc)
        {
            FunnyDatabaseContext context = new FunnyDatabaseContext();
            context.Jokes.Add(ujvicc);
            context.SaveChanges();
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            FunnyDatabaseContext context = new FunnyDatabaseContext();
            var torles = (from x in context.Jokes
                          where x.JokeSk == id
                          select x).FirstOrDefault();
            context.Remove(torles);
            context.SaveChanges();
        }


    }
}
