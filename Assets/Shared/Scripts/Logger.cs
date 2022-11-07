using UnityEngine;

public class Logger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] bool showLogs = true;
    [SerializeField] string prefix;
    [SerializeField] Color prefixColor = new Color(0, 0, 0, 1);

    string hexColor;

    void OnValidate()
    {
        hexColor = "#" + ColorUtility.ToHtmlStringRGBA(prefixColor);
    }

    public void Log(object message, Object sender)
    {
        if (!showLogs)
            return;

        Debug.Log($"<color={hexColor}>{prefix}</color>: {message}", sender);
    }
}
