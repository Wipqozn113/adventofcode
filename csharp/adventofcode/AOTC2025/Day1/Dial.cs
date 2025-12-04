namespace AOTC2025.Day1
{
    public class Dial
    {
        private readonly int StartValue;         

        public Dial(int startValue = 50)
        {
            StartValue = startValue;
            CurrentValue = StartValue;
        }

        public int CurrentValue { get; private set; }
        public int PointsAtZeroCount { get; private set; }

        public int Rotate(Direction direction, int distance)
        {
            if(direction == Direction.Left)
                distance = -distance;

            var tempVal = (CurrentValue + distance) % 100;
            CurrentValue = tempVal >= 0 ? tempVal : 100 + tempVal;

            if (CurrentValue == 0) PointsAtZeroCount++;
            return CurrentValue;
        }

        public void RotateAlt(Direction direction, int distance)
        {
            if(direction == Direction.Right)
            {
                decimal rawValue = CurrentValue + distance;
                PointsAtZeroCount += (int)Math.Floor(rawValue / 100);
                CurrentValue = (int)Math.Floor(rawValue) % 100;
            }
            else
            {
                for (int i = 0; i < distance; i++)
                {
                    CurrentValue--;
                    if(CurrentValue == 0) PointsAtZeroCount++;
                    if (CurrentValue == -1) CurrentValue = 99;
                }
            }
        }


    }

    public enum Direction
    {
        Right,
        Left
    }
}
