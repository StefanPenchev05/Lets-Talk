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
    Task<string> RenderToStringAsync(string viewName, object model);
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
    public async Task<string> RenderToStringAsync(string viewName, object model)
    {
        // Create a new HttpContext and ActionContext for rendering the view
        var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };
        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

        using (var sw = new StringWriter())
        {
            // Find the view with the given name
            var viewResult = razorViewEngine.FindView(actionContext, viewName, false);

            // If the view doesn't exist, throw an exception
            if (viewResult.View == null)
            {
                throw new ArgumentException($"{viewName} does not match with any available view");
            }

            // Create a ViewDataDictionary for the view
            var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };

            // Create a ViewContext for rendering the view
            var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, new TempDataDictionary(actionContext.HttpContext, tempDataProvider), sw, new HtmlHelperOptions());

            // Render the view to the StringWriter
            await viewResult.View.RenderAsync(viewContext);
            Console.WriteLine(sw.ToString());

            // Return the rendered view as a string
            return sw.GetStringBuilder().ToString();
        }
    }
}