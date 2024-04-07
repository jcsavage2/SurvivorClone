using System;
using NLog;

namespace SurvivorClone;

public static class Debug
{
  private static Logger logger;

  public static void InitLog(bool debug)
  {
    logger = NLog.LogManager.GetCurrentClassLogger();
    string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
    string fileName = $"Debug/log-{time}.txt";

    NLog.LogManager.Setup()
      .LoadConfiguration(builder =>
      {
        builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToColoredConsole();
        builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: fileName);
        builder.ForLogger().FilterMinLevel(LogLevel.Error).WriteToColoredConsole();
      });

    if (debug)
    {
      Enable();
    }
    else
    {
      Disable();
    }

    LogConsole("Logger initialized");
  }

  public static void Enable()
  {
    NLog.LogManager.ResumeLogging();
  }

  public static void Disable()
  {
    NLog.LogManager.SuspendLogging();
  }

  public static void Shutdown()
  {
    NLog.LogManager.Shutdown(); // Flush and close down internal threads and timers
  }

  public static void LogFile(string message)
  {
    logger.Debug(message);
  }

  public static void LogConsole(string message)
  {
    logger.Info(message);
  }

  public static void ThrowErrorLog(string message)
  {
    logger.Error(message);
    throw new InvalidOperationException(message);
  }

  public static void HandleError(Exception ex, string message)
  {
    logger.Error(ex, message);
    Shutdown();
  }
}
