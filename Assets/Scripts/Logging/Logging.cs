using Steamworks;
using System;
using UnityEngine;

public class Logging : MonoBehaviour
{
    private DiscordWebhookAPI api;

    public void Log(DiscordWebhookAPI webhookAPI, string log, bool getmsgdata = false)
    {
        webhookAPI.SendMessage(false, $"[Log] {DateTime.Now.ToString("h:mm:ss tt")} {log}", SteamClient.Name, "https://cdn.discordapp.com/avatars/1026084150895202385/de808d42737bc91d34812c06d0e887ac.png", false, getmsgdata);
        if (getmsgdata)
        {
            // TODO: add in temp variable
        }
    }

    private void ErrorLog(DiscordWebhookAPI webhookAPI, string errorlog)
    {
        webhookAPI.SendMessage(false, $"[ERROR] {DateTime.Now.ToString("h:mm:ss tt")} {errorlog}", SteamClient.Name, "https://cdn.discordapp.com/avatars/1026084150895202385/de808d42737bc91d34812c06d0e887ac.png", false);
    }

    private void Awake()
    {
        // ���������� ��� ������� � ����� DiscordWebhookAPI
        foreach (var obj in FindObjectsOfType<DiscordWebhookAPI>())
        {
            // ���������, �������� �� ������ ����������� ���������� ArchorName � ������ ���������
            if (obj.ArchorName == "Logging")
            {
                // ������ ���-�� �� ��������� ��������
                api = obj;
            }
        }
        if (api == null)
        {
            Debug.Log("������ � ������ ������ �� ������!");
        }
    }

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Exception || type == LogType.Error)
        {
            ErrorLog(api, logString);
        }
        if (type == LogType.Log) Log(api, logString);
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        // Initialize
        Log(api, $"Build Pre-Alpha, {Screen.currentResolution}, GPU: {SystemInfo.graphicsDeviceName}, CPU: {SystemInfo.processorType}, OS: {SystemInfo.operatingSystem}, RAM Available: {SystemInfo.systemMemorySize}");
    }
}
