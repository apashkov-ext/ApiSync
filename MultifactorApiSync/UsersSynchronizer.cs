using System.Net.Http.Json;
using System.Text.Json;

namespace MultifactorApiSync
{
    internal record MultifactorApiResponse<T>(bool Success, string Message, T model);
    internal record RegisteredUserDto(string Id, string Identity);

    public class UsersSynchronizer : IHostedService, IDisposable
    {
        private Timer? _timer;

        private readonly ILogger<UsersSynchronizer> _logger;

        public UsersSynchronizer(ILogger<UsersSynchronizer> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Users Synchronizer running.");

            _timer = new Timer(Act, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Users Synchronizer is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void Act(object? state)
        {
            _ = ActAsync();
        }
        
        private async Task ActAsync()
        {
            //using var cli = _factory.CreateClient("UsersSynchronizer");

            //var body = new { };
            //var content = JsonContent.Create(body);

            //var response = await cli.PostAsync("", content);
            //var json = await response.Content.ReadAsStringAsync();
            //var deserialized = JsonSerializer.Deserialize<MultifactorApiResponse<RegisteredUserDto>>(json);

            _logger.LogInformation("Action");
        }

        public void Dispose() => _timer?.Dispose();       
    }
}
