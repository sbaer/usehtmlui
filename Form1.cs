using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UseHtmlUi
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var controller = new MyController(textBox1);
      var stream = this.GetType().Assembly.GetManifestResourceStream("UseHtmlUi.HTMLPage1.htm");
      SdkFunctions.ShowHtmlDialog(stream, controller);
    }
  }

  class MyController : BrowserController
  {
    System.Windows.Forms.TextBox m_stringbox;
    public MyController(System.Windows.Forms.TextBox stringbox)
    {
      m_stringbox = stringbox;
    }

    public override string Callback1(string arg)
    {
      m_stringbox.Text = m_stringbox.Text + arg + "\r\n";
      int iVal = int.Parse(arg);
      return (iVal * 2).ToString();
    }

    public override string Callback2(string arg1, string arg2)
    {
      if (arg1.Equals("show"))
      {
        m_stringbox.Text = m_stringbox.Text + arg1 + " : " + arg2 + "\r\n";
        System.Windows.Forms.MessageBox.Show(arg2);
        InvokeScript("setText", "Callback2 was called");
      }
      return string.Empty;
    }
  }
}
