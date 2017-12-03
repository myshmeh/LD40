using UnityEngine;

public class KeyManager
{
    private int key;

    public KeyManager()
    {
        key = KeyBit.none;
    }

    /// <summary>
    /// Updates key status
    /// </summary>
    public void UpdateKey()
    {
        //*-- up --*
        if (Input.GetKey(MyKeyCode.up) || Input.GetKey(MyKeyCode.up2))
        {
            key |= KeyBit.up; //flag up "up"
        }
        else if (!Input.GetKey(MyKeyCode.up) && !Input.GetKey(MyKeyCode.up2))
        {
            key &= ~KeyBit.up; //flag down "up"
        }

        //*-- right --*
        if (Input.GetKey(MyKeyCode.right) || Input.GetKey(MyKeyCode.right2))
        {
            key |= KeyBit.right; //flag up "right"
        }
        else if (!Input.GetKey(MyKeyCode.right) && !Input.GetKey(MyKeyCode.right2))
        {
            key &= ~KeyBit.right; //flag down "right"
        }

        //*-- down --*
        if (Input.GetKey(MyKeyCode.down) || Input.GetKey(MyKeyCode.down2))
        {
            key |= KeyBit.down; //flag up "down"
        }
        else if (!Input.GetKey(MyKeyCode.down) && !Input.GetKey(MyKeyCode.down2))
        {
            key &= ~KeyBit.down; //flag down "down"
        }

        //*-- left --*
        if (Input.GetKey(MyKeyCode.left) || Input.GetKey(MyKeyCode.left2))
        {
            key |= KeyBit.left; //flag up "left"
        }
        else if (!Input.GetKey(MyKeyCode.left) || !Input.GetKey(MyKeyCode.left2))
        {
            key &= ~KeyBit.left; //flag down "left"
        }

        //*-- enter --*
        if (Input.GetKeyDown(MyKeyCode.enter))
        {
            key |= KeyBit.enter; //flag up "enter"
        }
        else if (!Input.GetKeyDown(MyKeyCode.enter))
        {
            key &= ~KeyBit.enter; //flag down "enter"
        }

        //*-- gun --*
        if (Input.GetKey(MyKeyCode.gun))
        {
            key |= KeyBit.gun; //flag up "gun"
        }
        else if (!Input.GetKey(MyKeyCode.gun))
        {
            key &= ~KeyBit.gun; //flag down "gun"
        }

        //*-- guard --*
        if (Input.GetKey(MyKeyCode.guard))
        {
            key |= KeyBit.guard; //flag up "guard"
        }
        else if (!Input.GetKey(MyKeyCode.guard))
        {
            key &= ~KeyBit.guard; //flag down "guard"
        }
    }

    /// <summary>
    /// Determines whether to be pressed with the given key
    /// </summary>
    /// <param name="keyAssessed">
    /// (KeyBit)key value you want to assess whether to be pressed
    /// </param>
    /// <returns></returns>
    public bool IsPressed(int keyAssessed)
    {
        if ((key & keyAssessed) == keyAssessed)
            return true;

        return false;
    }

    /// <summary>
    /// return int key(bitwise)
    /// </summary>
    public int Key{
        get { return key; }
    }
}

public static class KeyBit
{
    public const int none = 0;
    public const int up = 1;
    public const int right = 1 << 1;
    public const int down = 1 << 1 << 1;
    public const int left = 1 << 1 << 1 << 1;
    public const int enter = 1 << 1 << 1 << 1 << 1;
    public const int gun = 1 << 1 << 1 << 1 << 1 << 1;
    public const int guard = 1 << 1 << 1 << 1 << 1 << 1 << 1;
}

public static class MyKeyCode
{
    public const KeyCode up = KeyCode.W;
    public const KeyCode right = KeyCode.D;
    public const KeyCode down = KeyCode.S;
    public const KeyCode left = KeyCode.A;
    public const KeyCode enter = KeyCode.L;
    public const KeyCode gun = KeyCode.K;
    public const KeyCode guard = KeyCode.J;

    //alternative keycodes
    public const KeyCode up2 = KeyCode.UpArrow;
    public const KeyCode right2 = KeyCode.RightArrow;
    public const KeyCode down2 = KeyCode.DownArrow;
    public const KeyCode left2 = KeyCode.LeftArrow;
}