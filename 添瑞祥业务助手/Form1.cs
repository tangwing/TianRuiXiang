using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using HtmlAgilityPack;
using BorderStyle = NPOI.SS.UserModel.BorderStyle;

namespace 添瑞祥业务助手
{
    public partial class Form1 : Form
    {
        ArrayList alConfig = new ArrayList();//store config
        ArrayList alCurrentGoodMeters =null;
        private const string cmdFlag = "MBUS";
        private const string CONFIG_FILE = "config.ini";
        private const string CONFIG_SECTION_1 = "MeterIdTransform";
        private const string CONFIG_SECTION_4 = "FeeCalculate";
        private System.Windows.Forms.SaveFileDialog sfdOut;

        public Form1()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            InitializeComponent();
            sfdOut = new SaveFileDialog();
            sfdOut.Filter = "Excel Files (*.xls)|*.xls";
            loadConfig();
        }

        private void loadConfig()
        {
            if (!File.Exists(CONFIG_FILE)) 
                File.Create(CONFIG_FILE).Dispose();
            string pat = "111100*";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_1, "pattern", ref pat);
            txtPattern.Text = pat;pat = "8";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_1, "idlength", ref pat);
            txtMeterIdLength.Text = pat; pat = "22";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "tbQuNuanFeiPT", ref pat);
            tbQuNuanFeiPT.Text = pat; pat = "38";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "tbQuNuanFeiSY", ref pat);
            tbQuNuanFeiSY.Text = pat; pat = "0.157";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "tbReJiLiangPT", ref pat);
            tbReJiLiangPT.Text = pat; pat = "0.157";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "tbReJiLiangSY", ref pat);
            tbReJiLiangSY.Text = pat; pat = "0.3";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "tbJiBenRenFei", ref pat);
            tbJiBenRenFei.Text = pat; pat = ".";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "defaultdir", ref pat);
            ofdIn.InitialDirectory = ofdOut.InitialDirectory = sfdOut.InitialDirectory = (pat); pat = "2013";

            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "tbSrc1", ref pat);
            tbSrc1.Text = pat; pat = "2014";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "tbDest1", ref pat);
            tbDest1.Text = pat; pat = "热力提供表格";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "tbSrc2", ref pat);
            tbSrc2.Text = pat; pat = "算费生成表格";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "tbDest2", ref pat);
            tbDest2.Text = pat; pat = "True";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "cbAutoOpen", ref pat);
            cbAutoOpen.Checked = pat.Equals("True");

            //新增的3个热费计算参数
            pat = "M-BUS";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "tbDTU", ref pat);
            tbDTU.Text = pat;

            pat = "添瑞祥";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "tbReBiaoChangJia", ref pat);
            tbReBiaoChangJia.Text = pat;

            pat = "DN20";
            IniFile.GetString(CONFIG_FILE, CONFIG_SECTION_4, "tbReBiaoKouJing", ref pat);
            tbReBiaoKouJing.Text = pat;
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
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_1, "pattern", txtPattern.Text);
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_1, "idlength", txtMeterIdLength.Text);
        }

        //抽取并修复log行中的数据部分
        private string extractDataStream(string logLine)
        {
            string result="";
            int i;
            for (i = 0; i < logLine.Length; i++)
            {
                if(char.IsDigit(logLine, i))break;
            }

            if (i<logLine.Length)result=logLine.Substring(i);
            if (result.Length < 32 && result.Length >= 28)//抄表命令 不全
            {
                result = "6820".Substring(0, 32 - result.Length)+result;
            }
            else if (result.Length < 118 && result.Length >= 114)//接收数据 不全
            {
                result = "6820".Substring(0, 118 - result.Length) + result;
            }
            return result;
        }

        //抽取表号
        private string extractMeterIdFromDataStream(string stream)
        {
            char[] id = stream.Substring(4, 14).ToCharArray();
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
            alCurrentGoodMeters = null;
            ArrayList alGoodMeters = new ArrayList();
            ArrayList alBadMeters = new ArrayList();
            StringReader sr = new StringReader(txtLog.Text);
            string line = sr.ReadLine();
            while (line != null)
            {
                line=line.Replace(" ","");
                if (line.Contains(cmdFlag))
                {
                    string data = extractDataStream(line);
                    if (data.Length == 32)//mbus抄表命令
                    {
                        string id = extractMeterIdFromDataStream(data);
                        line = sr.ReadLine();
                        while (line != null && !line.Contains(cmdFlag))
                            line = sr.ReadLine();
                        if (line == null)
                        {
                            if (!alBadMeters.Contains(id))
                                alBadMeters.Add(id);
                            break;
                        }
                        line = line.Replace(" ", "");
                        data = extractDataStream(line);
                        if (data.Length == 118)//"MBUS接收数据"成功
                        {
                            if (alBadMeters.Contains(id))
                                alBadMeters.Remove(id);
                            successCount++;
                            alGoodMeters.Add(new MeterResponse(data));
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
            alCurrentGoodMeters = alGoodMeters;
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

        private void btnExportMeterResponses(object sender, EventArgs e)
        { 
            if (alCurrentGoodMeters != null)
            {
                try
                {
                    var utf8WithoutBom = new System.Text.UTF8Encoding(true);
                    StreamWriter sw = new StreamWriter("export.csv",false,utf8WithoutBom);
                    //MessageBox.Show(sw.Encoding.ToString());
                    sw.WriteLine("序号;表地址;当前热量(" + MeterResponse.reLiangDanWei + ");供水温度(℃);回水温度(℃);温差(℃);累计流量(" + MeterResponse.leiJiLiuLiangDanWei + ");报警信息;表内时间");
                    int counter = 1;
                    foreach (MeterResponse mr in alCurrentGoodMeters)
                    {
                        sw.Write(counter + ";");
                        sw.WriteLine(mr.toCSVString(";"));
                        counter++;
                    }
                    sw.Close();
                    MessageBox.Show("数据已导出为当前文件夹下的export.csv文件！");
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show("info.csv文件正在使用！请将其关闭后重试");
                }
            }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
        private void saveFeeCalculateConfig()
        {
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "tbQuNuanFeiPT", tbQuNuanFeiPT.Text);
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "tbQuNuanFeiSY", tbQuNuanFeiSY.Text);
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "tbReJiLiangPT",tbReJiLiangPT.Text);
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "tbReJiLiangSY",tbReJiLiangSY.Text );
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "tbJiBenRenFei", tbJiBenRenFei.Text );
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "tbInputDir", tbInputDir.Text );
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "tbSrc1", tbSrc1.Text );
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "tbDest1", tbDest1.Text);
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "tbSrc2", tbSrc2.Text );
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "tbDest2", tbDest2.Text );
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "cbAutoOpen", cbAutoOpen.Checked.ToString());
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "defaultdir", Path.GetDirectoryName(ofdIn.FileName));
            //新增的3个热费计算参数
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "tbDTU", tbDTU.Text);
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "tbReBiaoChangJia", tbReBiaoChangJia.Text);
            IniFile.Write(CONFIG_FILE, CONFIG_SECTION_4, "tbReBiaoKouJing", tbReBiaoKouJing.Text);
        }

        //热费计算 导出计算后表格
        private void btnExport_Click(object sender, EventArgs e)
        {
            saveFeeCalculateConfig();
            rtbLog.Text="正在处理...";
            ISheet sheetIn = null;
            ISheet sheetIn2 = null;
            //Deal with html file
            if (rbHtml.Checked)
            {
                sheetIn = HtmlTableToXlsSheet(tbInputDir.Text);
                if (!string.IsNullOrEmpty(tbInputLastY.Text))
                    sheetIn2 =HtmlTableToXlsSheet(tbInputLastY.Text);
            }
            else
            {
                sheetIn = WorkbookFactory.Create(File.OpenRead(tbInputDir.Text)).GetSheetAt(0);
                if (!string.IsNullOrEmpty(tbInputLastY.Text))
                    sheetIn2 = WorkbookFactory.Create(File.OpenRead(tbInputLastY.Text)).GetSheetAt(0);
            }

            //Init result file header
            HSSFWorkbook output = new HSSFWorkbook();
            ISheet sheetOut = output.CreateSheet(sheetIn.SheetName);
            IRow rowOut = sheetOut.CreateRow(0);
            //Border style
            ICellStyle style = output.CreateCellStyle();
            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            //header style
            var hearderFont = output.CreateFont();
            hearderFont.Boldweight = 700;
            hearderFont.Color = HSSFColor.White.Index;
            ICellStyle headerstyle = output.CreateCellStyle();
            headerstyle.CloneStyleFrom(style);
            headerstyle.FillForegroundColor = HSSFColor.Grey50Percent.Index;
            headerstyle.FillPattern = FillPattern.SolidForeground;
            headerstyle.SetFont(hearderFont);

            rowOut.CreateCell(0).SetCellValue("序号");
            rowOut.CreateCell(1,CellType.String).SetCellValue("DTU设备");
            rowOut.CreateCell(2,CellType.String).SetCellValue("小区");
            rowOut.CreateCell(3,CellType.String).SetCellValue("用户姓名");
            rowOut.CreateCell(4,CellType.String).SetCellValue("用户地址");
            rowOut.CreateCell(5,CellType.String).SetCellValue("表地址");
            rowOut.CreateCell(6,CellType.String).SetCellValue("热表厂家");
            rowOut.CreateCell(7,CellType.String).SetCellValue("热表口径");
            rowOut.CreateCell(8, CellType.Numeric).SetCellValue("上年度热量");
            rowOut.CreateCell(9, CellType.Numeric).SetCellValue("本年度热量");
            rowOut.CreateCell(10, CellType.Numeric).SetCellValue("消耗热量");
            rowOut.CreateCell(11,CellType.String).SetCellValue("抄表时间");
            rowOut.CreateCell(12,CellType.String).SetCellValue("用户类别");
            rowOut.CreateCell(13,CellType.Numeric).SetCellValue("房产面积");
            rowOut.CreateCell(14,CellType.Numeric).SetCellValue("居民热价(元/M²)");
            double reJiLiang = double.Parse(rbPT.Checked ? tbReJiLiangPT.Text : tbReJiLiangSY.Text);
            rowOut.CreateCell(15,CellType.Numeric).SetCellValue("计量价格("+reJiLiang+"元/KWH）");
            rowOut.CreateCell(16,CellType.Numeric).SetCellValue("基础热价(元)");
            rowOut.CreateCell(17,CellType.Numeric).SetCellValue("按面积收费额(元)");
            rowOut.CreateCell(18,CellType.Numeric).SetCellValue("计量收费额(元)");
            rowOut.CreateCell(19, CellType.Numeric).SetCellValue("核算结果(元)");
                

            //Get residence name
            string residence = sheetIn.GetRow(1).GetCell(0).StringCellValue;
            residence = residence.Substring(residence.IndexOf("小区:", System.StringComparison.Ordinal));
            residence = residence.Substring(3, residence.IndexOf("(", System.StringComparison.Ordinal)-3).Trim();
            Console.WriteLine(residence);
            //Loop
            int rowCount = sheetIn.LastRowNum - 4;
            for(int ind=0; ind<rowCount; ind++)
            {
                IRow rowSrc = sheetIn.GetRow(ind + 3);
                IRow row = sheetOut.CreateRow(ind+1);
                row.CreateCell(0).SetCellValue(ind + 1);
                row.CreateCell(1).SetCellValue(tbDTU.Text);
                row.CreateCell(2).SetCellValue(residence);
                row.CreateCell(3).SetCellValue(rowSrc.GetCell(1).StringCellValue);
                row.CreateCell(4).SetCellValue(rowSrc.GetCell(2).StringCellValue);
                row.CreateCell(5).SetCellValue(rowSrc.GetCell(3).StringCellValue);
                row.CreateCell(6).SetCellValue(tbReBiaoChangJia.Text);
                row.CreateCell(7).SetCellValue(tbReBiaoKouJing.Text);
                row.CreateCell(8).SetCellValue(0);//上热量 预置为零
                if (sheetIn2 != null)
                {// 寻找对应项如存在则提取热量
                    string meterId = rowSrc.GetCell(3).StringCellValue.Trim();
                    int i;
                    for (i = 3; i < sheetIn2.LastRowNum - 1; i++)
                    {
                        // Compare the meter id to find the correspondance
                        if (sheetIn2.GetRow(i).GetCell(3).StringCellValue.Trim().Equals(meterId))
                        {
                            row.GetCell(8).SetCellValue(sheetIn2.GetRow(i).GetCell(4).NumericCellValue);//上热量
                            Console.WriteLine(meterId + "上年热量: " + row.GetCell(8).NumericCellValue);
                            break;
                        }
                    }
                    if(i==sheetIn2.LastRowNum-1) //显示新用户
                       log("用户"+row.GetCell(0).NumericCellValue+": "+row.GetCell(3).StringCellValue+"，无上年度数据");
                }
                row.CreateCell(9).SetCellValue(rowSrc.GetCell(4).NumericCellValue);//本年热量. 
                row.CreateCell(10).SetCellFormula("J"+(ind+2)+"-I"+(ind+2));//热量
                DateTime date = rowSrc.GetCell(11).DateCellValue;
                row.CreateCell(11).SetCellValue(date.ToString("d"));
                //row.CreateCell(11).SetCellValue(date.Substring(0, date.IndexOf(" ", System.StringComparison.Ordinal)));
                row.CreateCell(12).SetCellValue(rbPT.Checked ? "居民" : "商业");
                row.CreateCell(13).SetCellValue(rowSrc.GetCell(10).NumericCellValue);
                row.CreateCell(14).SetCellValue(double.Parse(rbPT.Checked ? tbQuNuanFeiPT.Text: tbQuNuanFeiSY.Text));
                string tmp = "K" + (ind + 2) + "*\"" + reJiLiang.ToString(CultureInfo.CurrentCulture)+"\"";
                row.CreateCell(15).SetCellFormula(tmp);
                row.CreateCell(16).SetCellFormula("\""+tbJiBenRenFei.Text + "\"*R" + (ind + 2));//JiChuReJia
                row.CreateCell(17).SetCellFormula("N" + (ind + 2) + "*O" + (ind + 2));//
                row.CreateCell(18).SetCellFormula("Q" + (ind + 2) + "+P" + (ind + 2));//
                row.CreateCell(19).SetCellFormula("R" + (ind + 2) + "-S" + (ind + 2));//
                //set style
                for (var i = 0; i < 20; i++) row.GetCell(i).CellStyle = style;
            }
            for(var i=0; i<20; i++)
            {
                sheetOut.AutoSizeColumn(i);
                sheetOut.GetRow(0).GetCell(i).CellStyle = headerstyle;
            }
                
            //sheetOut
            int splitPoint = tbInputDir.Text.LastIndexOf("\\");
            string outName = tbInputDir.Text.Substring(splitPoint+1);
            Console.WriteLine(outName);
            if(!string.IsNullOrEmpty(tbSrc1.Text))outName = outName.Replace(tbSrc1.Text, tbDest1.Text);
            if (!string.IsNullOrEmpty(tbSrc2.Text)) outName = outName.Replace(tbSrc2.Text, tbDest2.Text);
            if(cbOutputToSrc.Checked)
                outName = tbInputDir.Text.Substring(0, splitPoint + 1) + outName;
            else
            {
                sfdOut.InitialDirectory = ofdOut.InitialDirectory = ofdIn.InitialDirectory;//ofdOut not used
                if (sfdOut.ShowDialog() == DialogResult.OK)
                    outName = sfdOut.FileName;
                else outName = null;
            }
            if (outName != null)
            {
                while (File.Exists(outName))
                    outName=outName.Insert(outName.LastIndexOf("."), "(new)");
                FileStream of = File.OpenWrite(outName);
                output.Write(of);
                of.Dispose();
                if (cbAutoOpen.Checked)
                    System.Diagnostics.Process.Start(outName);
            }
        }

        //将html文件转化为excel
        private ISheet HtmlTableToXlsSheet(String html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(html);
            List< List<String>> table = doc.DocumentNode.SelectNodes("//tr")
                .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                .ToList();

            String sheetname = Path.GetFileNameWithoutExtension(html);
            HSSFWorkbook output = new HSSFWorkbook();
            ISheet sheetOut = output.CreateSheet(sheetname);
            IRow rowOut = sheetOut.CreateRow(1);
            rowOut.CreateCell(0).SetCellValue(table[1][0]);
                        
            for(int i=3; i<table.Count-2; i++)
            {
                rowOut = sheetOut.CreateRow(i);
                rowOut.CreateCell(0).SetCellValue(i-2);
                for(int j=1; j<table[i].Count; j++)
                {
                    rowOut.CreateCell(j).SetCellValue(table[i][j]);
                    if(j == 4 || j==10)
                        rowOut.CreateCell(j).SetCellValue(double.Parse(table[i][j]));
                    else if (j == 11)
                    {
                        // Parse the datetime field. The format sometimes causes pb
                        DateTime dt;
                        string[] formats= {"yyyy/M/d H:mm:ss"};//, "yyyy/M/d hh:mm:ss"};
                        if (!DateTime.TryParseExact(table[i][j], formats,
                              null,
                              DateTimeStyles.None,
                              out dt))
                        {
                            MessageBox.Show("日期格式出现问题，如果格式规范有变化请联系开发者。文件未能正确生成");
                            return sheetOut;
                        }
                        //DateTime.ParseExact(table[i][j], "yyyy/M/d h:mm:ss", null)
                        else
                            rowOut.CreateCell(j).SetCellValue(dt);
                    }
                        
                }
            }
            
            //For the last 2 rows
            rowOut = sheetOut.CreateRow(table.Count-2);
            rowOut.CreateCell(0).SetCellValue(table[table.Count-2][0]);
            rowOut = sheetOut.CreateRow(table.Count - 1);
            rowOut.CreateCell(0).SetCellValue(table[table.Count - 1][0]);
            return sheetOut;
        }

        private void btnOpenThisYear_Click(object sender, EventArgs e)
        {
            if (ofdIn.ShowDialog() == DialogResult.OK)
            {
                tbInputDir.Text = ofdIn.FileName;
            }
        }

        private void btnOpenLastYear_Click(object sender, EventArgs e)
        {
            if (ofdIn.ShowDialog() == DialogResult.OK)
            {
                tbInputLastY.Text = ofdIn.FileName;
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void log(string msg)
        {
            rtbLog.AppendText("\n"+msg);
        }

    }
}
