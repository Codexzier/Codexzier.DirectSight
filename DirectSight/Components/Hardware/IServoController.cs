namespace DirectSight.Components.Hardware;

public interface IServoController
{
    void SetPosition(int servoIndex, double dutyCycle);
}