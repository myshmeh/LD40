using System;

public static class Common
{
    public const float PPS = 32; //pixel per sprite (basic size of this pj)
    public const int FPS = 60;

    public static int SecondToFrame(float second)
    {
        return (int)Math.Round(second * FPS);
    }

    public static float FrameToSecond(int frame)
    {
        return (float)frame / (float)FPS;
    }
}

public static class State
{
    public const int Dead = -1;
    public const int Idle = 0;
    public const int Damaged = 1;
    public const int Attack = 2;
    public const int Attack2 = 3;
    public const int Guard = 4;
    public const int Rest = 5;

    public const int Length = 7;
}

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}