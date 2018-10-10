namespace MyTelescope.Api.Binders
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

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
