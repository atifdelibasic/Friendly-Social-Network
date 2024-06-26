﻿using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Data;
using System.IO;
using Microsoft.ML.Trainers;

namespace Friendly.Service
{

    public interface IRecommenderService
    {
        void TrainModel();
        bool IsModelTrained();
        bool Predict(int userId, int hobbyId);
        List<int> GetRecommendedHobbiesForUser(int userId);

    }
    public class RecommenderService : IRecommenderService
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public RecommenderService()
        {
            _mlContext = new MLContext();
            TrainModel();
        }


        public void TrainModel()
        {
            var (trainingData, _) = LoadData();
            _model = BuildAndTrainModel(_mlContext, trainingData);
        }

        public bool IsModelTrained()
        {
            return _model != null;
        }

        public bool Predict(int userId, int hobbyId)
        {
            (IDataView training, IDataView test) = LoadData();
            ITransformer model = BuildAndTrainModel(_mlContext, training);
            EvaluateModel(_mlContext, test, model);
            bool recommend = UseModelForSinglePrediction(_mlContext, model, userId, hobbyId);

            return recommend;
        }

        static bool UseModelForSinglePrediction(MLContext mlContext, ITransformer model, int userId, int hobbyId)
        {
            Console.WriteLine("=============== Making a prediction ===============");
            var predictionEngine = mlContext.Model.CreatePredictionEngine<UserHobbyInteraction, UserHobbyInteractionPrediction>(model);
            var testInput = new UserHobbyInteraction { UserId = userId, HobbyId = hobbyId };

            var userHobbyInteractionPrediction = predictionEngine.Predict(testInput);

            if (Math.Round(userHobbyInteractionPrediction.Score, 1) > 0.3)
            {
                Console.WriteLine("Hobby " + testInput.HobbyId + " in category is recommended for user " + testInput.UserId);
                return true;
            }
            else
            {
                Console.WriteLine("Hobby " + testInput.HobbyId + " in category is not recommended for user " + testInput.UserId);
            }
            Console.WriteLine("Score: " + Math.Round(userHobbyInteractionPrediction.Score, 1).ToString());

            return false;
        }

        static void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
        {
            Console.WriteLine("=============== Evaluating the model ===============");
            var prediction = model.Transform(testDataView);

            var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");

            Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
            Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
        }

        private (IDataView training, IDataView test) LoadData()
        {
            var trainingDataPath = Path.Combine(AppContext.BaseDirectory, "Data", "user-hobby-interactions-train.csv");
            var testDataPath = Path.Combine(AppContext.BaseDirectory, "Data", "user-hobby-interactions-test.csv");

            var trainingDataView = _mlContext.Data.LoadFromTextFile<UserHobbyInteraction>(trainingDataPath, hasHeader: true, separatorChar: ',');
            var testDataView = _mlContext.Data.LoadFromTextFile<UserHobbyInteraction>(testDataPath, hasHeader: true, separatorChar: ',');
            return (trainingDataView, testDataView);
        }

        public List<int> GetRecommendedHobbiesForUser(int userId)
        {
            List<int> recommendedHobbies = new List<int>();

            if (_model == null)
            {
                Console.WriteLine("Model is not trained yet. Please train the model first.");
                return recommendedHobbies;
            }

            int minHobbyId = 1; 
            int maxHobbyId = 20; 

            for (int hobbyId = minHobbyId; hobbyId <= maxHobbyId; hobbyId++)
            {
                if (Predict(userId, hobbyId))
                {
                    recommendedHobbies.Add(hobbyId);
                }
            }

            return recommendedHobbies;
        }


        private ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
        {
            var estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: "UserId")
                         .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "hobbyIdEncoded", inputColumnName: "HobbyId"))
                         .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "categoryIdEncoded", inputColumnName: "CategoryId"));

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "userIdEncoded",
                MatrixRowIndexColumnName = "hobbyIdEncoded",
                LabelColumnName = "Label",
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            Console.WriteLine("=============== Training the model ===============");
            ITransformer model = trainerEstimator.Fit(trainingDataView);

            return model;
        }

        private UserHobbyInteractionPrediction MakePrediction(int userId, int hobbyId)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<UserHobbyInteraction, UserHobbyInteractionPrediction>(_model);
            var testInput = new UserHobbyInteraction { UserId = userId, HobbyId = hobbyId };

            return predictionEngine.Predict(testInput);
        }
    }
}

public class UserHobbyInteraction
{
    [LoadColumn(0)]
    public float UserId;
    [LoadColumn(1)]
    public float HobbyId;
    [LoadColumn(2)]
    public float Label;
    [LoadColumn(3)]
    public int CategoryId;
}

public class UserHobbyInteractionPrediction
{
    public float Label;
    public float Score;
}
