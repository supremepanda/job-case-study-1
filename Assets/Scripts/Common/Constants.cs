namespace Common
{
    public static class SceneBuildIndex
    {
        public const int MainMenu = 0;
        public const int Level1 = 1;
        public const int Level2 = 2;
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

    public enum FoodType
    {
        Olive,
        Cherry,
        Banana,
        HotDog,
        Hamburger,
        CheeseSlice
    }

    public enum Rewards
    {
        Olive,
        Banana,
        HotDog,
        Hamburger
    }
}