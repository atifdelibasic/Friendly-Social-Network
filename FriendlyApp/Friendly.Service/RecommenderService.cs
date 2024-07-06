using Microsoft.ML;
using Microsoft.ML.Data;
using System.Data;
using Microsoft.ML.Trainers;
using Friendly.Database;
using Microsoft.EntityFrameworkCore;

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
        private readonly FriendlyContext _context;

        public RecommenderService(FriendlyContext context)
        {
            _mlContext = new MLContext();
            _context = context;
            TrainModel();
        }


        public void TrainModel()
        {
            var trainingData = LoadData();
            _model = BuildAndTrainModel(_mlContext, trainingData);
        }

        public bool IsModelTrained()
        {
            return _model != null;
        }

        public bool Predict(int userId, int hobbyId)
        {
            bool recommend = UseModelForSinglePrediction(userId, hobbyId);
            return recommend;
        }

        public bool UseModelForSinglePrediction(int userId, int hobbyId)
        {
            Console.WriteLine("=============== Making a prediction ===============");
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<UserHobbyInteraction, UserHobbyInteractionPrediction>(_model);
            var testInput = new UserHobbyInteraction { UserId = userId, HobbyId = hobbyId };

            var userHobbyInteractionPrediction = predictionEngine.Predict(testInput);

            if (Math.Round(userHobbyInteractionPrediction.Score, 1) >= 50)
            {
                Console.WriteLine("Hobby " + testInput.HobbyId + " in category is recommended for user " + testInput.UserId);
                Console.WriteLine("label hoby id " + userHobbyInteractionPrediction.HobbyId);
                Console.WriteLine("Score: " + Math.Round(userHobbyInteractionPrediction.Score, 1).ToString());
                return true;
            }
            else
            {
                Console.WriteLine("Hobby " + testInput.HobbyId + " in category is not recommended for user " + testInput.UserId);
            }

            return false;
        }

        private IDataView LoadData()
        {
            var predictionData = _context.UserHobbies.Select(uhi => new UserHobbyInteraction { HobbyId = uhi.HobbyId, CategoryId = uhi.Hobby.HobbyCategoryId, UserId = uhi.UserId }).Distinct().ToList();
            Console.WriteLine(predictionData.Count);
            var trainingDataView = _mlContext.Data.LoadFromEnumerable(predictionData);

            return trainingDataView;
        }

        public List<int> GetRecommendedHobbiesForUser(int userId)
        {
            List<int> recommendedHobbies = new List<int>();

            if (_model == null)
            {
                Console.WriteLine("Model is not trained yet. Please train the model first. tala ga");
                return recommendedHobbies;
            }

            var userHobbies = _context.UserHobbies.Where(x => x.UserId == userId).Select(x => x.HobbyId).ToList();

            var hobbies = _context.Hobby.Where(x => !userHobbies.Contains(x.Id)).ToList();

            foreach (var hobby in hobbies)
            {
                if (Predict(userId, hobby.Id))
                {
                    recommendedHobbies.Add(hobby.Id);
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
                LabelColumnName = "HobbyId",
                NumberOfIterations = 50,
                ApproximationRank = 150
            };

            var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            Console.WriteLine("=============== Training the model ===============");
            ITransformer model = trainerEstimator.Fit(trainingDataView);

            return model;
        }

    }
}

public class UserHobbyInteraction
{
    [LoadColumn(0)]
    public float UserId;
    [LoadColumn(1)]
    public float HobbyId;
    [LoadColumn(3)]
    public int CategoryId;
}

public class UserHobbyInteractionPrediction
{
    public float HobbyId;
    public float Score;
}

