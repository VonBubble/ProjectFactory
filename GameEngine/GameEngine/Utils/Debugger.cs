using System;
using System.IO;
using System.Reflection;


public class Debugger
{
    private string m_exePath = string.Empty;
    
    public Debugger(string logMessage)
    {
        LogWrite(logMessage);
    }
    
    public void LogWrite(string logMessage)
    {
        m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
        {
            Log(logMessage, w);
        }
    }

    public void Log(string logMessage, TextWriter txtWriter)
    {
        txtWriter.WriteLine("{0} {1}: {2}", DateTime.Now.ToShortDateString(), 
        	DateTime.Now.ToLongTimeString(), logMessage);
    }
}