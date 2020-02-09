using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using TrainingDataAccess;
using TrainingModels;
using TrainingServer.Models;

namespace TrainingServer.Controllers
{

    public class TrainingController : ApiController
    {

        // GET api/values
        public IEnumerable<TrainingModel> Get()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TrainingEntityContext>());
            var model = new List<TrainingModel>();
            
            try
            {
                using (var context = new TrainingEntityContext())
                {
                    var value = context.Training.ToList();
                    foreach (var employee in value)
                    {
                        var trainingModel = new TrainingModel();
                        trainingModel.TrainingName = employee.TrainingName;
                        trainingModel.StartDate = employee.StartDate;
                        trainingModel.EndDate = employee.EndDate;
                        model.Add(trainingModel);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
        /// <summary>
        /// Post Method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post([FromBody] Training model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.StartDate > model.EndDate)
            {
                string InvalidErrorMessage = ConfigurationManager.AppSettings["InvalidErrorMessage"];
                ModelState.AddModelError("4004", InvalidErrorMessage);
                return BadRequest(ModelState);
            }

            await Task.Factory.StartNew(async () =>
            {
                model.DayDiff = (model.EndDate - model.StartDate).Days;

                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TrainingEntityContext>());
                using (var context = new TrainingEntityContext())
                {
                    context.Training.Add(model);
                    context.SaveChanges();
                }

            });
            return Ok(model);

            //var addModel = await _accessRequestService.AddAsync(model);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
