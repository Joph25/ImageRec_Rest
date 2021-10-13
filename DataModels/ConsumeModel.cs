using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ML;

namespace ImageRec_Rest.DataModels
{
    public class ConsumeModel
    {
        private static Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictionEngine(), true);

        // For more info on consuming ML.NET models, visit https://aka.ms/mlnet-consume
        // Method for consuming model in your app
        public static ModelOutput Predict(ModelInput input)
        {
            ModelOutput result = PredictionEngine.Value.Predict(input);
            return result;
        }

        public static PredictionEngine<ModelInput, ModelOutput> CreatePredictionEngine()
        {
            // Create new MLContext
            MLContext mlContext = new MLContext();

            // Load model & create prediction engine
            string modelPath = @"D:\Root_Philipp_Laptop\FH_AI\Visual_Studio_Projekte\ImageRec_Rest\ImageRec_Rest\MLModels\MLModel1.zip";
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var _);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            return predEngine;
        }
    }
}