using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingServer;
using TrainingServer.Controllers;
using TrainingModels;
using System.Web.Http.Results;
using System.Threading.Tasks;
using System.Configuration;

namespace TrainingServer.Tests.Controllers
{
    [TestClass]
    public class TrainingControllersTest
    {

        /// <summary>
        /// Test Post Method With test data
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        [DataTestMethod]
        [DataRow("0", "01-Jan-2020", "14-Jan-2020")]
        [DataRow("1", "14-Jan-2020", "01-Jan-2020")]
        public async Task Post(string rowIndex, string startdate, string enddate)
        {
            // Arrange
            TrainingController controller = new TrainingController();
            Training trainingModel = new Training();
            trainingModel.TrainingName = "TestNames" + rowIndex;
            trainingModel.StartDate = Convert.ToDateTime(startdate);
            trainingModel.EndDate = Convert.ToDateTime(enddate);

            // Act
            var controllerResult = await controller.Post(trainingModel);
            var validResults = controllerResult as OkNegotiatedContentResult<Training>;
            int DayDiff = 0;
            if (validResults != null)
            {
                DayDiff = (Convert.ToDateTime(enddate) - Convert.ToDateTime(startdate)).Days;
                Assert.AreEqual(DayDiff, validResults.Content.DayDiff);

            }
            else if (((InvalidModelStateResult)controllerResult).ModelState != null)
            {
                string InvalidErrorMessage = ConfigurationManager.AppSettings["InvalidErrorMessage"];
                var InvalidcontrollerResult = ((InvalidModelStateResult)controllerResult).ModelState["4004"].Errors[0].ErrorMessage;
                Assert.AreEqual(InvalidcontrollerResult, InvalidErrorMessage);
            }

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            TrainingController controller = new TrainingController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            TrainingController controller = new TrainingController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
