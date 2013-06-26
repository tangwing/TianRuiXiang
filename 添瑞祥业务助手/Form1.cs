using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace 添瑞祥业务助手
{
    public partial class Form1 : Form
    {
        ArrayList alConfig = new ArrayList();//store config
        string cmdFlag = "MBUS";

        public Form1()
        {
            InitializeComponent();
            if (!File.Exists("config.ini"))
            {
                StreamWriter sw = new StreamWriter("config.ini");
                sw.WriteLine("#数据变换的模式");
                sw.WriteLine("111100*");
                sw.WriteLine("#表号的位数");
                sw.WriteLine("8");
                sw.Close();
            }
            loadConfig();
            
        }

        private void loadConfig()
        {
            StreamReader sr = new StreamReader("config.ini");
            string line = sr.ReadLine();
            while (line != null)
            {
                if (line.Length > 0 && !line.StartsWith("#"))
                    alConfig.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();
            if(alConfig.Count>0)
                txtPattern.Text = alConfig[0].ToString();
            if (alConfig.Count > 1)
                txtMeterIdLength.Text = alConfig[1].ToString();
        }

        /* 检测按钮*/
        private void btnTest_Click(object sender, EventArgs e)
        {
            string data = txtData.Text.Trim().Replace(Environment.NewLine, "\n");
            String[] items = data.Split(new char[1] { '\n' });
            ArrayList sameItemList = new ArrayList();
            ArrayList shortItems = new ArrayList();
            int stdLength = int.Parse(txtMeterIdLength.Text);
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Equals("-")) continue;
                Console.WriteLine(items[i] + ";" + items[i].Length+"; "+stdLength);
                if (items[i].Length < stdLength)
                {
                    shortItems.Add((i + 1) + " ： " + items[i]);
                    continue;
                }
                
                ArrayList lineNo = new ArrayList();
                for (int j = i + 1; j < items.Length; j++)
                {
                    if (items[i].Equals(items[j]))
                    {
                        if (lineNo.Count == 0)
                            lineNo.Add(i);
                        lineNo.Add(j);
                        items[j] = "-";
                    }
                }
                if (lineNo.Count > 0)
                    sameItemList.Add(lineNo);
            }
            
            rtxtInfo.Text = "";
            if (sameItemList.Count == 0 && shortItems.Count == 0)
            {
                rtxtInfo.Text = "检测通过，未发现问题";
                return;
            }

            //处理重复数据
            if( sameItemList.Count > 0 )
            {
                for(int i=0; i<sameItemList.Count;i++)
                {
                    ArrayList lineNos = (ArrayList)sameItemList[i];
                    rtxtInfo.SelectionFont = new Font(rtxtInfo.SelectionFont, FontStyle.Bold);
                    rtxtInfo.AppendText("重复数据" + (i + 1) + "\n");
                    rtxtInfo.AppendText("数值：" + items[(int)lineNos[0]] + Environment.NewLine+"行号：");
                    foreach (int number in lineNos)
                    {
                        rtxtInfo.AppendText( number+1 + ", ");

                    }
                    rtxtInfo.Select(rtxtInfo.TextLength-2, 2);
                    rtxtInfo.SelectedText="\n\n";
                }
                
            }

            //处理过短数据
            if (shortItems.Count > 0)
            {
                rtxtInfo.SelectionFont = new Font(rtxtInfo.SelectionFont, FontStyle.Bold);
                rtxtInfo.AppendText("数据位数错误(行号：数值)"+ Environment.NewLine);
                foreach (string shorts in shortItems)
                {
                    rtxtInfo.AppendText(" "+shorts + Environment.NewLine);
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            txtData.SelectAll();
            txtData.Copy();
        }

        //模式变换
        private void btnTransform_Click(object sender, EventArgs e)
        {
            string data = txtData.Text.Trim().Replace(Environment.NewLine, "\n");
            String[] items = data.Split(new char[1] { '\n' });
            txtData.Clear();
            for (int i = 0; i < items.Length; i++)
            {
                txtData.AppendText(txtPattern.Text.Replace("*", items[i]) + Environment.NewLine);
            }
        }

        //粘贴并测试
        private void btnPasteAndTest_Click(object sender, EventArgs e)
        {
            txtData.Clear();
            txtData.Paste();
            btnTest.PerformClick();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            txtData.Paste();
        }

        
        private void btnCopyRoomNum_Click(object sender, EventArgs e)
        {
            txtRoomNum.SelectAll();
            txtRoomNum.Copy();
        }

        //生成房号
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (txtMaxRoomNum.Text.Length < 5 ||
                !txtMaxRoomNum.Text.Contains('-'))
                MessageBox.Show("房号格式错误！", "输入不合法", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    int index = txtMaxRoomNum.Text.IndexOf('-');
                    int unitNum = Convert.ToInt32(txtMaxRoomNum.Text.Substring(0, index));
                    int floorNum = Convert.ToInt32(txtMaxRoomNum.Text.Substring(index + 1,
                        txtMaxRoomNum.Text.Length-index-3));
                    int roomNum = Convert.ToInt32(txtMaxRoomNum.Text.Substring(txtMaxRoomNum.Text.Length-2));
                    txtRoomNum.Clear();
                    for (int i = 1; i <= unitNum; i++)
                    {
                        for (int j = 1; j <= floorNum; j++)
                        {
                            for (int k = 1; k <= roomNum; k++)
                            {
                                txtRoomNum.AppendText(i+"-"+j+k.ToString( "D2")+Environment.NewLine);
                            }
                        }
                    }
                    
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("发生异常，请检查房号格式！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }


        //支持ctrl+A
        private void txtData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                txtData.SelectAll();
        }

        private void txtRoomNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                txtRoomNum.SelectAll();
        }

        //默认配置
        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("config.ini");
            sw.WriteLine("#数据变换的模式");
            sw.WriteLine(txtPattern.Text);
            sw.WriteLine("#表号的位数");
            sw.WriteLine(txtMeterIdLength.Text);
            sw.Close();
        }

        //抽取log行中的数据部分
        private string extractDataStream(string logLine)
        {
            int i;
            for (i = 0; i < logLine.Length; i++)
            {
                if(char.IsDigit(logLine, i))break;
            }

            return (i==logLine.Length)?"":logLine.Substring(i);
        }
        //抽取表号
        private string extractMeterIdFromDataStream(string stream)
        {
            char[] id = stream.Substring(7, 12).ToCharArray();
            char tmp;
            for (int i = 0; i < 3; i++)
            {
                int left = 2 * i;
                int right = id.Length - (i + 1) * 2;
                tmp = id[left];
                id[left] = id[right];
                id[right] = tmp;

                tmp = id[left+1];
                id[left+1] = id[right+1];
                id[right+1] = tmp;
            }
            return new string(id);
        }

        //抄表数据分析
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            int successCount = 0;
            ArrayList alBadMeters = new ArrayList();
            StringReader sr = new StringReader(txtLog.Text);
            string line = sr.ReadLine();
            while (line != null)
            {
                if (line.Contains(cmdFlag))
                {
                    string data = extractDataStream(line);
                    if (data.Length <= 32 && data.Length > 16)//mbus抄表命令
                    {
                        string id = extractMeterIdFromDataStream(data);
                        line = sr.ReadLine();
                        while (line != null && !line.Contains(cmdFlag))
                            line = sr.ReadLine();
                        if (line == null) break;
                        data = extractDataStream(line);
                        if (data.Length <= 118 && data.Length > 32)//"MBUS接收数据"成功
                        {
                            if (alBadMeters.Contains(id))
                                alBadMeters.Remove(id);
                            successCount++;
                        }
                        else //超时，失败
                        {
                            if (!alBadMeters.Contains(id))
                                alBadMeters.Add(id);
                        }
                    }
                }
                line = sr.ReadLine();
            }
            txtResult.Clear();
            txtResult.AppendText("分析完毕！"+Environment.NewLine);
            txtResult.AppendText("抄表总数："+(successCount+alBadMeters.Count) + Environment.NewLine);
            txtResult.AppendText("成功个数：" + successCount + Environment.NewLine);
            txtResult.AppendText("失败个数：" + alBadMeters.Count + Environment.NewLine);
            txtResult.AppendText("失败表号：" + Environment.NewLine);
            foreach(string meter in alBadMeters)
                txtResult.AppendText(meter+Environment.NewLine);
        }

        private void txtLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                txtLog.SelectAll();
        }

        private void btnPasteAndAnalyze_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            txtLog.Paste();
            btnAnalyze.PerformClick();
        }



    }
}
