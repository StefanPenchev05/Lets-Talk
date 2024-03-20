using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

// Interface for the service that will render a view to a string
public interface IViewRenderService
{
    // Method to render a view to a string
    Task<string> RenderToStringAsync(string viewName, object model, Dictionary<string, object> additionalData = null);
}

public class ViewRenderService : IViewRenderService
{
    // Razor view engine to find and render views
    private readonly IRazorViewEngine razorViewEngine;

    // Provider for TempData, which stores data between requests
    private readonly ITempDataProvider tempDataProvider;

    // Service provider to create instances of services
    private readonly IServiceProvider serviceProvider;

    // Constructor that takes dependencies
    public ViewRenderService(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
    {
        this.razorViewEngine = razorViewEngine;
        this.tempDataProvider = tempDataProvider;
        this.serviceProvider = serviceProvider;
    }

    // Method to render a view to a string
    public async Task<string> RenderToStringAsync(string viewName, object model, Dictionary<string, object> additionalData = null)
    {
        // Check if model is null
        if (model == null)
        {
            throw new ArgumentException(nameof(model), "Model cannot be null");
        }

        try
        {
            // Create a new HttpContext and ActionContext
            var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            // StringWriter will capture the rendered view
            using (var sw = new StringWriter())
            {
                // Find the view using the view engine
                var viewResult = razorViewEngine.FindView(actionContext, viewName, false);

                // If the view doesn't exist, throw an exception
                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                // Create a ViewDataDictionary and set the model
                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                // If additionalData is not null, add each item to the ViewDataDictionary
                if (additionalData != null)
                {
                    foreach(var item in additionalData){
                        viewDictionary.Add(item.Key, item.Value);
                    }
                }

                // Create a ViewContext and render the view
                var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, new TempDataDictionary(actionContext.HttpContext, tempDataProvider), sw, new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);

                // Return the rendered view as a string
                return sw.ToString();
            }
        }
        catch (Exception ex)
        {
            // If an exception occurs, write it to the console and rethrow it
            Console.WriteLine(ex);
            throw;
        }
    }
}