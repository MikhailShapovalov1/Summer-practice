namespace task04
{
    public class Fighter : ISpaceship
    {
        public int Speed { get; private set; } 

        public int FirePower { get; private set; }

        public int TotalDistance { get; private set; }

        public int TotalDamage { get; private set; }

        public int RotationAngle { get; private set; }

        public Fighter()
        {
            Speed = 100;
            FirePower = 50;
            TotalDistance = 0;
            TotalDamage = 0;
            RotationAngle = 0;
        }

        public void Fire()
        {
            TotalDamage += FirePower;
        }

        public void MoveForward()
        {
            TotalDistance += Speed;
        }

        public void Rotate(int angle)
        {
            RotationAngle = (RotationAngle + angle) % 360;
        }
    }
}
