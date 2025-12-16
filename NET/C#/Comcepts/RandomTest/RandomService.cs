using System;

public class RandomService
{
    public int GenRandNum()
    {
        return Random.Shared.Next(1, 11);
    }
}