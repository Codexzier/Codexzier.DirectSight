using System.Device.Pwm;

namespace DirectSight.Components.Hardware;

public sealed class ServoController : IServoController, IDisposable
{
    private readonly PwmChannel[] _servos = new PwmChannel[2];

    public ServoController()
    {
        // Hardware PWM via Linux
        for (int index = 0; index < this._servos.Length; index++)
        {
            this._servos[index] = PwmChannel.Create(
                chip: 0,
                channel: index,   // 0 = GPIO 12, 1 = GPIO 13
                frequency: 50,
                dutyCyclePercentage: 0.075);
            
            this._servos[index].Start();
        }
    }

    public void SetPosition(int servoIndex, double dutyCycle)
    {
        this._servos[servoIndex].DutyCycle = dutyCycle;
    }
    
    public void Dispose()
    {
        foreach (var servo in this._servos) 
        {
            servo.Stop();
            servo.Dispose();
        }
    }
}