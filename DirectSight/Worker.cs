using DirectSight.Components.Hardware;

namespace DirectSight;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    private ServoController? _servos;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        TcpTestServer.StartAsync(stoppingToken, logger);
        DataExchangeServer.StartAsync(stoppingToken, logger);
        this._servos = new ServoController();
        
        
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            
            // Test: Move servos back and forth
            this._servos.SetServo0(0.05);
            this._servos.SetServo1(0.10);

            await Task.Delay(1000, stoppingToken);
        }
    }
}