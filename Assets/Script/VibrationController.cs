using UnityEngine;

public class VibrationController : MonoBehaviour
{
    public static void Vibrate(long milliseconds)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

        if (vibrator != null)
        {
            vibrator.Call("vibrate", milliseconds);
        }
#endif
        Debug.Log("Vibrating for " + milliseconds + "ms");
    }

    public static void CancelVibration()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

        if (vibrator != null)
        {
            vibrator.Call("cancel");
        }
#endif
        Debug.Log("Vibration canceled");
    }

    public void SmallVibration()
    {
        VibrationController.Vibrate(1000);
    }
}
