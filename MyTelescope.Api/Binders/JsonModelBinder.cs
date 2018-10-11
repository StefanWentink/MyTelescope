namespace MyTelescope.Api.Binders
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.IO;
    using System.Threading.Tasks;

    public class JsonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var request = bindingContext.HttpContext.Request;
            var jsonStringData = new StreamReader(request.Body).ReadToEnd();

            bindingContext.Result = ModelBindingResult.Success(jsonStringData);

            return Task.CompletedTask;
        }
    }
}