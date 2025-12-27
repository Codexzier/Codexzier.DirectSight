using System.Device.Pwm;

namespace DirectSight.Components.Hardware;

    public sealed class ServoController : IDisposable
    {
        private readonly PwmChannel _servo0;
        private readonly PwmChannel _servo1;

        public ServoController()
        {
            // Hardware PWM via Linux
            this._servo0 = PwmChannel.Create(
                chip: 0,
                channel: 0,   // GPIO 12
                frequency: 50,
                dutyCyclePercentage: 0.075);

            this._servo1 = PwmChannel.Create(
                chip: 0,
                channel: 1,   // GPIO 13
                frequency: 50,
                dutyCyclePercentage: 0.075);

            this._servo0.Start();
            this._servo1.Start();
        }

        public void SetServo0(double duty)
        {
            this._servo0.DutyCycle = duty;
        }

        public void SetServo1(double duty)
        {
            this._servo1.DutyCycle = duty;
        }

        public void Dispose()
        {
            this._servo0?.Stop();
            this._servo1?.Stop();
            this._servo0?.Dispose();
            this._servo1?.Dispose();
        }
    }

