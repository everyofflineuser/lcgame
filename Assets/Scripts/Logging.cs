using System;
using UnityEngine;

public class Logging : MonoBehaviour
{
    private DiscordWebhookAPI api;

    private void Log(DiscordWebhookAPI webhookAPI, string log)
    {
        webhookAPI.SendMessage(false, $"[Log] {DateTime.Now.ToString("h:mm:ss tt")} {log}", null, "https://cdn.discordapp.com/avatars/1026084150895202385/de808d42737bc91d34812c06d0e887ac.png", false);
    }

    private void ErrorLog(DiscordWebhookAPI webhookAPI, string errorlog)
    {
        webhookAPI.SendMessage(false, $"[ERROR] {DateTime.Now.ToString("h:mm:ss tt")} {errorlog}", null, "https://cdn.discordapp.com/avatars/1026084150895202385/de808d42737bc91d34812c06d0e887ac.png", false);
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
        if (type == LogType.Error)
        {
            ErrorLog(api, logString);
        }
    }

    private void Start()
    {
        // Initialize
        Log(api, "Func Start started Succerfull");
        Log(api, "Logging started.");
        Log(api, "[Client] Client Started.");
        Log(api, $"Build v0.1, {Screen.currentResolution}, GPU: {SystemInfo.graphicsDeviceName}, CPU: {SystemInfo.processorType}, OS: {SystemInfo.operatingSystem}, RAM Available: {SystemInfo.systemMemorySize}");
        //Debug.LogError("Test");
    }
}
