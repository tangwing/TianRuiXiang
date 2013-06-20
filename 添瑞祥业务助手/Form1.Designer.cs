namespace 添瑞祥业务助手
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpValidator = new System.Windows.Forms.TabPage();
            this.txtData = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbOperate = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPasteAndTest = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnTransform = new System.Windows.Forms.Button();
            this.rtxtInfo = new System.Windows.Forms.RichTextBox();
            this.gbSetup = new System.Windows.Forms.GroupBox();
            this.txtMeterIdLength = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSetDefault = new System.Windows.Forms.Button();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.lblPattern = new System.Windows.Forms.Label();
            this.tpGenerator = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCopyRoomNum = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaxRoomNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRoomNum = new System.Windows.Forms.TextBox();
            this.tpAnalyzer = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tpValidator.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbOperate.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbSetup.SuspendLayout();
            this.tpGenerator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpValidator);
            this.tabControl.Controls.Add(this.tpGenerator);
            this.tabControl.Controls.Add(this.tpAnalyzer);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(747, 577);
            this.tabControl.TabIndex = 0;
            // 
            // tpValidator
            // 
            this.tpValidator.Controls.Add(this.txtData);
            this.tpValidator.Controls.Add(this.panel1);
            this.tpValidator.Location = new System.Drawing.Point(4, 25);
            this.tpValidator.Name = "tpValidator";
            this.tpValidator.Padding = new System.Windows.Forms.Padding(3);
            this.tpValidator.Size = new System.Drawing.Size(739, 548);
            this.tpValidator.TabIndex = 0;
            this.tpValidator.Text = "数据验证";
            this.tpValidator.UseVisualStyleBackColor = true;
            // 
            // txtData
            // 
            this.txtData.AcceptsReturn = true;
            this.txtData.Location = new System.Drawing.Point(3, 3);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtData.Size = new System.Drawing.Size(264, 533);
            this.txtData.TabIndex = 4;
            this.txtData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtData_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbOperate);
            this.panel1.Controls.Add(this.gbSetup);
            this.panel1.Location = new System.Drawing.Point(273, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 541);
            this.panel1.TabIndex = 3;
            // 
            // gbOperate
            // 
            this.gbOperate.Controls.Add(this.panel2);
            this.gbOperate.Controls.Add(this.rtxtInfo);
            this.gbOperate.Font = new System.Drawing.Font("STKaiti", 10.2F);
            this.gbOperate.Location = new System.Drawing.Point(3, 101);
            this.gbOperate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbOperate.Name = "gbOperate";
            this.gbOperate.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbOperate.Size = new System.Drawing.Size(457, 436);
            this.gbOperate.TabIndex = 1;
            this.gbOperate.TabStop = false;
            this.gbOperate.Text = "操作";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPasteAndTest);
            this.panel2.Controls.Add(this.btnPaste);
            this.panel2.Controls.Add(this.btnTest);
            this.panel2.Controls.Add(this.btnCopy);
            this.panel2.Controls.Add(this.btnTransform);
            this.panel2.Location = new System.Drawing.Point(6, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(154, 390);
            this.panel2.TabIndex = 7;
            // 
            // btnPasteAndTest
            // 
            this.btnPasteAndTest.Font = new System.Drawing.Font("STKaiti", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPasteAndTest.Location = new System.Drawing.Point(22, 19);
            this.btnPasteAndTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPasteAndTest.Name = "btnPasteAndTest";
            this.btnPasteAndTest.Size = new System.Drawing.Size(107, 48);
            this.btnPasteAndTest.TabIndex = 8;
            this.btnPasteAndTest.Text = "粘贴并检测";
            this.btnPasteAndTest.UseVisualStyleBackColor = true;
            this.btnPasteAndTest.Click += new System.EventHandler(this.btnPasteAndTest_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.Font = new System.Drawing.Font("STKaiti", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPaste.Location = new System.Drawing.Point(22, 319);
            this.btnPaste.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(107, 48);
            this.btnPaste.TabIndex = 7;
            this.btnPaste.Text = "粘贴";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("STKaiti", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTest.Location = new System.Drawing.Point(22, 75);
            this.btnTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(107, 48);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "检测";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Font = new System.Drawing.Font("STKaiti", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCopy.Location = new System.Drawing.Point(22, 263);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(107, 48);
            this.btnCopy.TabIndex = 6;
            this.btnCopy.Text = "拷贝";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnTransform
            // 
            this.btnTransform.Font = new System.Drawing.Font("STKaiti", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTransform.Location = new System.Drawing.Point(22, 131);
            this.btnTransform.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTransform.Name = "btnTransform";
            this.btnTransform.Size = new System.Drawing.Size(107, 48);
            this.btnTransform.TabIndex = 5;
            this.btnTransform.Text = "模式变换";
            this.btnTransform.UseVisualStyleBackColor = true;
            this.btnTransform.Click += new System.EventHandler(this.btnTransform_Click);
            // 
            // rtxtInfo
            // 
            this.rtxtInfo.Location = new System.Drawing.Point(166, 25);
            this.rtxtInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rtxtInfo.Name = "rtxtInfo";
            this.rtxtInfo.ReadOnly = true;
            this.rtxtInfo.Size = new System.Drawing.Size(285, 390);
            this.rtxtInfo.TabIndex = 3;
            this.rtxtInfo.Text = "";
            // 
            // gbSetup
            // 
            this.gbSetup.Controls.Add(this.txtMeterIdLength);
            this.gbSetup.Controls.Add(this.label3);
            this.gbSetup.Controls.Add(this.btnSetDefault);
            this.gbSetup.Controls.Add(this.txtPattern);
            this.gbSetup.Controls.Add(this.lblPattern);
            this.gbSetup.Font = new System.Drawing.Font("STKaiti", 10.2F);
            this.gbSetup.Location = new System.Drawing.Point(3, 4);
            this.gbSetup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbSetup.Name = "gbSetup";
            this.gbSetup.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbSetup.Size = new System.Drawing.Size(457, 89);
            this.gbSetup.TabIndex = 0;
            this.gbSetup.TabStop = false;
            this.gbSetup.Text = "设置";
            // 
            // txtMeterIdLength
            // 
            this.txtMeterIdLength.Location = new System.Drawing.Point(125, 53);
            this.txtMeterIdLength.Name = "txtMeterIdLength";
            this.txtMeterIdLength.Size = new System.Drawing.Size(176, 25);
            this.txtMeterIdLength.TabIndex = 4;
            this.txtMeterIdLength.Text = "8";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "表号位数：";
            // 
            // btnSetDefault
            // 
            this.btnSetDefault.Font = new System.Drawing.Font("STKaiti", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetDefault.Location = new System.Drawing.Point(326, 26);
            this.btnSetDefault.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSetDefault.Name = "btnSetDefault";
            this.btnSetDefault.Size = new System.Drawing.Size(107, 48);
            this.btnSetDefault.TabIndex = 2;
            this.btnSetDefault.Text = "设为默认";
            this.btnSetDefault.UseVisualStyleBackColor = true;
            this.btnSetDefault.Click += new System.EventHandler(this.btnSetDefault_Click);
            // 
            // txtPattern
            // 
            this.txtPattern.Font = new System.Drawing.Font("STKaiti", 10.2F);
            this.txtPattern.Location = new System.Drawing.Point(125, 24);
            this.txtPattern.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(176, 25);
            this.txtPattern.TabIndex = 1;
            this.txtPattern.Text = "111100*";
            // 
            // lblPattern
            // 
            this.lblPattern.AutoSize = true;
            this.lblPattern.Font = new System.Drawing.Font("STKaiti", 10.2F);
            this.lblPattern.Location = new System.Drawing.Point(25, 28);
            this.lblPattern.Name = "lblPattern";
            this.lblPattern.Size = new System.Drawing.Size(78, 16);
            this.lblPattern.TabIndex = 0;
            this.lblPattern.Text = "变换模式：";
            // 
            // tpGenerator
            // 
            this.tpGenerator.Controls.Add(this.splitContainer1);
            this.tpGenerator.Location = new System.Drawing.Point(4, 25);
            this.tpGenerator.Name = "tpGenerator";
            this.tpGenerator.Padding = new System.Windows.Forms.Padding(3);
            this.tpGenerator.Size = new System.Drawing.Size(739, 548);
            this.tpGenerator.TabIndex = 1;
            this.tpGenerator.Text = "数据生成";
            this.tpGenerator.UseVisualStyleBackColor = true;
            this.tpGenerator.Click += new System.EventHandler(this.tpGenerator_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(6, 6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnCopyRoomNum);
            this.splitContainer1.Panel1.Controls.Add(this.btnGenerate);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txtMaxRoomNum);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtRoomNum);
            this.splitContainer1.Size = new System.Drawing.Size(727, 536);
            this.splitContainer1.SplitterDistance = 342;
            this.splitContainer1.TabIndex = 5;
            // 
            // btnCopyRoomNum
            // 
            this.btnCopyRoomNum.Location = new System.Drawing.Point(94, 150);
            this.btnCopyRoomNum.Name = "btnCopyRoomNum";
            this.btnCopyRoomNum.Size = new System.Drawing.Size(86, 39);
            this.btnCopyRoomNum.TabIndex = 4;
            this.btnCopyRoomNum.Text = "拷贝结果";
            this.btnCopyRoomNum.UseVisualStyleBackColor = true;
            this.btnCopyRoomNum.Click += new System.EventHandler(this.btnCopyRoomNum_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(94, 91);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(86, 40);
            this.btnGenerate.TabIndex = 3;
            this.btnGenerate.Text = "生成房号";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(313, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "例如3-403，意为共3个单元，每单元4层，每层3户";
            // 
            // txtMaxRoomNum
            // 
            this.txtMaxRoomNum.Location = new System.Drawing.Point(184, 8);
            this.txtMaxRoomNum.Name = "txtMaxRoomNum";
            this.txtMaxRoomNum.Size = new System.Drawing.Size(100, 25);
            this.txtMaxRoomNum.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入期望生成的最大房号：";
            // 
            // txtRoomNum
            // 
            this.txtRoomNum.AcceptsReturn = true;
            this.txtRoomNum.Location = new System.Drawing.Point(12, 12);
            this.txtRoomNum.Multiline = true;
            this.txtRoomNum.Name = "txtRoomNum";
            this.txtRoomNum.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRoomNum.Size = new System.Drawing.Size(352, 500);
            this.txtRoomNum.TabIndex = 4;
            this.txtRoomNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoomNum_KeyDown);
            // 
            // tpAnalyzer
            // 
            this.tpAnalyzer.Location = new System.Drawing.Point(4, 25);
            this.tpAnalyzer.Name = "tpAnalyzer";
            this.tpAnalyzer.Padding = new System.Windows.Forms.Padding(3);
            this.tpAnalyzer.Size = new System.Drawing.Size(739, 548);
            this.tpAnalyzer.TabIndex = 2;
            this.tpAnalyzer.Text = "数据分析";
            this.tpAnalyzer.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 601);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("STKaiti", 10.2F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添瑞祥";
            this.tabControl.ResumeLayout(false);
            this.tpValidator.ResumeLayout(false);
            this.tpValidator.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbOperate.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.gbSetup.ResumeLayout(false);
            this.gbSetup.PerformLayout();
            this.tpGenerator.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpValidator;
        private System.Windows.Forms.TabPage tpGenerator;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbOperate;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnTransform;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.RichTextBox rtxtInfo;
        private System.Windows.Forms.GroupBox gbSetup;
        private System.Windows.Forms.Button btnSetDefault;
        private System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.Label lblPattern;
        private System.Windows.Forms.TabPage tpAnalyzer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPasteAndTest;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaxRoomNum;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtRoomNum;
        private System.Windows.Forms.Button btnCopyRoomNum;
        private System.Windows.Forms.TextBox txtMeterIdLength;
        private System.Windows.Forms.Label label3;

    }
}

