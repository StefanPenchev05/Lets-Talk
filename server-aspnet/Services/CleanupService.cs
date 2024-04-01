using Server.Interface;
using Server.Data;

namespace Server.Services
{
    // This class implements a background service that periodically cleans up expired TempData entries.
    public class CleanupService : IHostedService, IDisposable, ICleanupService
    {
        // The database context and the web host environment are injected into the service.
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IWebHostEnvironment _webHostEnvironment;
        // A Timer is used to trigger the cleanup task periodically.
        private Timer _timer;

        public CleanupService(IServiceScopeFactory serviceScopeFactory, IWebHostEnvironment webHostEnvironment)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _webHostEnvironment = webHostEnvironment;
        }

        // This method is called when the application starts. It sets up the timer to trigger the cleanup task every 15 seconds.
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));
            return Task.CompletedTask;
        }

        // This method contains the cleanup task. It checks all TempData entries and deletes the ones that have expired.
        public void DoWork(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<UserManagerDB>();
                // Retrieve all TempData entries.
                var tempDatas = _context.tempDatas.ToList();
                // Get the path to the web root directory.
                string wwwRoot = _webHostEnvironment.WebRootPath;
                Console.WriteLine("The count of the tempdata is " + tempDatas.Count);
                foreach (var tempData in tempDatas)
                {
                    // Check if the TempData has expired.
                    if (DateTime.UtcNow >= tempData.ExpiryDate)
                    {
                        Console.WriteLine("The count of the tempdata is " + tempDatas.Count);
                        // If it has, delete the associated directory.
                        string uploadDir = Path.Combine(wwwRoot, "uploadsTemp", tempData.Id.ToString());
                        if (Directory.Exists(uploadDir))
                        {
                            Directory.Delete(uploadDir);
                        }

                        // Also remove the TempData entry from the database.
                        _context.tempDatas.Remove(tempData);
                    }
                    // Save the changes to the database.
                    _context.SaveChanges();
                }
            }
        }

        // This method is called when the application stops. It stops the timer.
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        // This method is called when the service is disposed. It disposes the timer.
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}