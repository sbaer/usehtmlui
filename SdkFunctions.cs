using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UseHtmlUi
{
  static class SdkFunctions
  {
    public static void ShowHtmlDialog(System.IO.Stream htmlStream, BrowserController controller)
    {
      System.Windows.Forms.Form f = new System.Windows.Forms.Form();
      System.Windows.Forms.WebBrowser browser = new System.Windows.Forms.WebBrowser();
      f.Controls.Add(browser);
      browser.Dock = System.Windows.Forms.DockStyle.Fill;
      BrowserGlue glue = new BrowserGlue(browser, controller);
      browser.ObjectForScripting = glue;
      browser.DocumentStream = htmlStream;
      f.ShowDialog();
    }
  }


  public class BrowserController
  {
    BrowserGlue m_glue;
    internal void SetGlue(BrowserGlue glue)
    {
      m_glue = glue;
    }

    public void InvokeScript(string functionName)
    {
      m_glue.InvokeScript(functionName);
    }

    public void InvokeScript(string functionName, string arg)
    {
      m_glue.InvokeScript(functionName, arg);
   }

    public void InvokeScript(string functionName, string arg1, string arg2)
    {
      m_glue.InvokeScript(functionName, arg1, arg2);
    }

    public virtual string Callback()
    {
      return string.Empty;
    }

    public virtual string Callback1(string arg)
    {
      return string.Empty;
    }

    public virtual string Callback2(string arg1, string arg2)
    {
      return string.Empty;
    }
  }

  /// <summary>
  /// Used internally to bind a WebBrowser to a BrowserController class
  /// </summary>
  [System.Runtime.InteropServices.ComVisible(true)]
  public class BrowserGlue
  {
    System.Windows.Forms.WebBrowser m_browser;
    BrowserController m_controller;
    internal BrowserGlue(System.Windows.Forms.WebBrowser browser, BrowserController controller)
    {
      m_browser = browser;
      m_controller = controller;
      m_controller.SetGlue(this);
    }

    public void InvokeScript(string functionName)
    {
      m_browser.Document.InvokeScript(functionName);
    }

    public void InvokeScript(string functionName, string arg)
    {
      m_browser.Document.InvokeScript(functionName, new object[] { arg });
    }

    public void InvokeScript(string functionName, string arg1, string arg2)
    {
      m_browser.Document.InvokeScript(functionName, new object[] { arg1, arg2 });
    }

    public virtual string Callback()
    {
      return m_controller.Callback();
    }

    public virtual string Callback1(string arg)
    {
      return m_controller.Callback1(arg);
    }

    public virtual string Callback2(string arg1, string arg2)
    {
      return m_controller.Callback2(arg1, arg2);
    }
  }
}
