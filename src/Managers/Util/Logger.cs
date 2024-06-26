using System;
using NLog;

namespace SurvivorClone;

public class Logger
{
  private readonly NLog.Logger log;

  public Logger()
  {
    log = NLog.LogManager.GetCurrentClassLogger();
    string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
    string fileName = $"Debug/log-{time}.txt";

    NLog.LogManager.Setup()
      .LoadConfiguration(builder =>
      {
        builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToColoredConsole();
        builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: fileName);
        builder.ForLogger().FilterMinLevel(LogLevel.Error).WriteToColoredConsole();
      });

    Console("Logger initialized");
  }

  public void File(string message)
  {
    log.Debug(message);
  }

  public void Console(string message)
  {
    log.Info(message);
  }

  public void Error(Exception ex)
  {
    log.Error(ex.Message);
  }
}
