using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    static string defaultIfSavedValue = "False";
    static string SAVE_NAMES = "SAVE_NAMES";

	public static void savePlayerNames(string areSaved)
    {
        PlayerPrefs.SetString(SAVE_NAMES, areSaved);
    }

    public static bool CheckIfPlayerNamesAreSaved()
    {
        string output = PlayerPrefs.GetString(SAVE_NAMES, defaultIfSavedValue);
        Debug.Log("PlayerPrefs " + output);
        Debug.Log("PlayerPrefs def " + defaultIfSavedValue);
        if(output.Equals(defaultIfSavedValue))
        {
            return false;
        }
        return true;
    }
}
