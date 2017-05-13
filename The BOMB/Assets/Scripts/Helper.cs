public static class Helper
{
    public static int ChangeIndexNum(int size, int current, bool increasing)
    {
        if (increasing)
        {
            current++;
            if (current >= size)
            {
                current = 0;
            }
        }
        else
        {
            current--;
            if (current < 0)
            {
                current = size - 1;
            }
        }
        return current;
    }

    public static class Prefs
    {
        public static float GetFloat(string keyName)
        {
            if (UnityEngine.PlayerPrefs.HasKey(keyName))
            {
                return UnityEngine.PlayerPrefs.GetFloat(keyName);
            }
            else
            {
                UnityEngine.PlayerPrefs.SetFloat(keyName, 0);
                return 0;
            }
        }

        public static string GetString(string keyName)
        {
            if (UnityEngine.PlayerPrefs.HasKey(keyName))
            {
                return UnityEngine.PlayerPrefs.GetString(keyName);
            }
            else
            {
                UnityEngine.PlayerPrefs.SetString(keyName, "");
                return "";
            }
        }

        public static int GetInt(string keyName)
        {
            if (UnityEngine.PlayerPrefs.HasKey(keyName))
            {
                return UnityEngine.PlayerPrefs.GetInt(keyName);
            }
            else
            {
                UnityEngine.PlayerPrefs.SetInt(keyName, 0);
                return 0;
            }
        }
    }

    public static int Abs (int x)
    {
        if (x < 0)
        {
            return (x * -1);
        }
        else
        {
            return x;
        }
    }

    public static float Abs(float x)
    {
        if (x < 0)
        {
            return (x * -1);
        }
        else
        {
            return x;
        }
    }
}
