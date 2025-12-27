using DirectSight.Components.Hardware;
using DirectSight.Components.Network;

namespace DirectSight;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    private IServoController _servos;
    private double _positionValue = 0.05;
    
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
            this._servos.SetPosition(0, this._positionValue);
            this._servos.SetPosition(1, this._positionValue);
            
            if(this._positionValue >= 0.10)
                this._positionValue = 0.05;
            else
                this._positionValue += 0.01;

            await Task.Delay(1000, stoppingToken);
        }
    }
}