using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Friendly.WebAPI.Utils
{
    public static class ModelStateExtension
    {
        public static string GetErrMessage(this ModelStateDictionary ModelState)
        {
            var model = ModelState.Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(x => x.Key, y => y.Value.Errors
                .Select(z => z.ErrorMessage));

            return model.ToString();
        }
    }
}
