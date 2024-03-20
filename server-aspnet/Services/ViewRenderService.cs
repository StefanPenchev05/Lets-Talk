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
        // Console.WriteLine(model);
        if(model == null){
            throw new ArgumentException(nameof(model), "Model cannot be null");
        }

        try
        {
            var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = razorViewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, new TempDataDictionary(actionContext.HttpContext, tempDataProvider), sw, new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);

                return sw.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}