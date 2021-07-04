namespace Common
{
    public static class SceneBuildIndex
    {
        public const int MainMenu = 0;
        public const int Level1 = 1;
        public const int Level2 = 2;
        public const int Level3 = 3;
    }

    public static class OliveSpawnRange
    {
        public const float X = 3f;
        public const float Y = 0f;
        public const float NegativeZ = -3.5f;
        public const float PositiveZ = 6f;
    }

    public static class Level1Constants
    {
        public const int OliveAmount = 64;
    }

    public static class Level2Constants
    {
        public const int OliveAmount = 32;
    }

    public static class Level4Constants
    {
        public const float DefaultSpeed = 7f;
        public const float SpeedUpSpeed = 10f;

        public const float PaddleLengthUp = 6f;
        public const float PaddleLengthDown = 4f;

        public const float BiggerBall = 3f;
        public const float DefaultBall = 2f;
    }

    public enum FoodType
    {
        Olive,
        Cherry,
        Banana,
        HotDog,
        Hamburger,
        CheeseSlice,
        Watermelon
    }

    public enum Rewards
    {
        Olive,
        Banana,
        HotDog,
        Hamburger
    }

    public enum CandyCrushFoodType
    {
        Cherry,
        Banana,
        Watermelon
    }
}