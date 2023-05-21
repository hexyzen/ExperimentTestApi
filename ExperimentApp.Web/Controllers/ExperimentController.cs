using ExperimentApp.Common.Models;
using ExperimentApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExperimentApp.Web.Controllers
{
    [Route("api/experiments")]
    [ApiController]
    public class ExperimentsController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public readonly IUnitOfWork _unitOfWork;
        public ExperimentsController(
            IConfiguration configuration,
            IUnitOfWork unitOfWork)

        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("button-color")]
        public async Task<IActionResult> GetButtonColorExperiment([FromQuery] string deviceToken)
        {
            var experiment = await _unitOfWork.ExperimentRepository.GetByDeviceTokenAsync(deviceToken, "button_color");
            if (experiment != null)
            {
                return Ok(experiment);
            }

            var colorOptions = new List<string> { "#FF0000", "#00FF00", "#0000FF" };
            var random = new Random();
            var index = random.Next(colorOptions.Count);

            var newColorValue = colorOptions[index];

            var newExperiment = new Experiment
            {
                DeviceToken = deviceToken,
                Key = "button_color",
                Value = newColorValue
            };

            await _unitOfWork.ExperimentRepository.AddExperimentAsync(newExperiment);
            _unitOfWork.Commit();

            return Ok(newExperiment);
        }

        [HttpGet("price")]
        public async Task<IActionResult> GetPriceExperiment([FromQuery] string deviceToken)
        {
            var experiment = await _unitOfWork.ExperimentRepository.GetByDeviceTokenAsync(deviceToken, "price");
            if (experiment != null)
            {
                return Ok(experiment);
            }

            var random = new Random();
            var randomNumber = random.Next(100);

            string newPriceValue = randomNumber switch
            {
                _ when randomNumber < 75 => "10",
                _ when randomNumber < 85 => "20",
                _ when randomNumber < 90 => "50",
                _ => "5"
            };

            var newExperiment = new Experiment
            {
                DeviceToken = deviceToken,
                Key = "price",
                Value = newPriceValue
            };

            await _unitOfWork.ExperimentRepository.AddExperimentAsync(newExperiment);
            _unitOfWork.Commit();

            return Ok(newExperiment);
        }

        [HttpGet("statistic")]
        public async Task<IActionResult> GetPriceExperiment()
        {
            var result = await _unitOfWork.ExperimentRepository.GetExperimentStatisticAsync();
            return Ok(result);
        }
    }
}