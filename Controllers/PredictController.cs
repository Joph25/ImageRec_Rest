using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.IO;
using ImageRec_Rest.DataModels;

namespace ImageRec_Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredictController : ControllerBase
    {

        private readonly ILogger<PredictController> _logger;

        public PredictController(ILogger<PredictController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public async Task<ModelOutput> OnPostPredictImageAsync(IFormFile file)
        {
            var filePath = "";
            long size = file.Length;

            if (file.Length > 0)
            {
                filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
            }

            ModelInput sampleData = new ModelInput()
            {
                ImageSource = @filePath,
            };

            var predictionResult = ConsumeModel.Predict(sampleData);

            return predictionResult;
        }
    }

}
