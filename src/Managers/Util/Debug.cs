using System;

namespace SurvivorClone;

public static class Debug
{
  private static bool isActive;
  private static Logger logger;

  public static void Init(bool debug)
  {
    logger = new Logger();
    isActive = debug;
  }

  public static void ThrowError(string message)
  {
    Exception ex = new InvalidOperationException(message);
    logger.Error(ex);
    throw ex;
  }

  public static void HandleError(Exception ex)
  {
    logger.Error(ex);
    NLog.LogManager.Shutdown(); // Flush and close down internal threads and timers
    isActive = false;
  }

  public static void Log(string message)
  {
    logger.Console(message);
  }

  public static void WriteFile(string message)
  {
    logger.File(message);
  }

  // --- SET --- //
  public static bool IsActive() => isActive;

  public static void SetActive(bool _isActive) => isActive = _isActive;
}
