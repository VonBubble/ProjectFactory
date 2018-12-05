using System;
using System.IO;
using System.Reflection;


public class Debug
{
    private string m_exePath = string.Empty;
    
    public Debug(string logMessage)
    {
        LogWrite(logMessage);
    }
    
    public void LogWrite(string logMessage)
    {
        m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        try
        {
            using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
            {
                Log(logMessage, w);
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void Log(string logMessage, TextWriter txtWriter)
    {
        try
        {
            txtWriter.WriteLine("{0} {1}: {2}", DateTime.Now.ToShortDateString(), 
                DateTime.Now.ToLongTimeString(), logMessage);
        }
        catch (Exception ex)
        {
        }
    }
}