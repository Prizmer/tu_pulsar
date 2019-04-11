namespace elfextendedapp
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonImport = new System.Windows.Forms.Button();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonPing = new System.Windows.Forms.Button();
            this.sfd1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonPoll = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonStop = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxComPorts = new System.Windows.Forms.ComboBox();
            this.numericUpDownComReadTimeout = new System.Windows.Forms.NumericUpDown();
            this.cbFromFileTcp = new System.Windows.Forms.CheckBox();
            this.numericUpDownComWriteTimeout = new System.Windows.Forms.NumericUpDown();
            this.btnIndPollCurrent = new System.Windows.Forms.Button();
            this.btnIndPollDaily = new System.Windows.Forms.Button();
            this.checkBoxPollOffline = new System.Windows.Forms.CheckBox();
            this.cbConfiguration = new System.Windows.Forms.ComboBox();
            this.cbOnlyDateUpd = new System.Windows.Forms.CheckBox();
            this.gbAdditionalSettings = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnIndPollInfo = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbJustRead = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbConnection = new System.Windows.Forms.GroupBox();
            this.btnApplyConnectionSettings = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rbCom = new System.Windows.Forms.RadioButton();
            this.rbTcp = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxIp = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownComReadTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownComWriteTimeout)).BeginInit();
            this.gbAdditionalSettings.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.gbConnection.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonImport
            // 
            this.buttonImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonImport.Location = new System.Drawing.Point(191, 17);
            this.buttonImport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(129, 31);
            this.buttonImport.TabIndex = 3;
            this.buttonImport.Text = "Импорт (*.xls)";
            this.toolTip1.SetToolTip(this.buttonImport, "Загрузить таблицу содержающую столбец с номерами квартир и столбец с заводскими н" +
        "омерами счетчиков");
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AllowUserToDeleteRows = false;
            this.dgv1.AllowUserToResizeRows = false;
            this.dgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv1.Location = new System.Drawing.Point(12, 65);
            this.dgv1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgv1.Name = "dgv1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv1.RowTemplate.Height = 28;
            this.dgv1.Size = new System.Drawing.Size(944, 366);
            this.dgv1.TabIndex = 4;
            // 
            // ofd1
            // 
            this.ofd1.FileName = "openFileDialog1";
            // 
            // buttonPing
            // 
            this.buttonPing.Enabled = false;
            this.buttonPing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPing.Location = new System.Drawing.Point(480, 17);
            this.buttonPing.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPing.Name = "buttonPing";
            this.buttonPing.Size = new System.Drawing.Size(111, 31);
            this.buttonPing.TabIndex = 5;
            this.buttonPing.Text = "Тест связи";
            this.toolTip1.SetToolTip(this.buttonPing, "Выполняется только проверка связи без получения каких-либо данных со счетчика");
            this.buttonPing.UseVisualStyleBackColor = true;
            this.buttonPing.Click += new System.EventHandler(this.buttonPing_Click);
            // 
            // buttonPoll
            // 
            this.buttonPoll.Enabled = false;
            this.buttonPoll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPoll.Location = new System.Drawing.Point(597, 17);
            this.buttonPoll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPoll.Name = "buttonPoll";
            this.buttonPoll.Size = new System.Drawing.Size(111, 31);
            this.buttonPoll.TabIndex = 6;
            this.buttonPoll.Text = "Опрос";
            this.toolTip1.SetToolTip(this.buttonPoll, "Выполняются проверка связи и опрос счетчика по текущим значениям");
            this.buttonPoll.UseVisualStyleBackColor = true;
            this.buttonPoll.Click += new System.EventHandler(this.buttonPoll_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Enabled = false;
            this.buttonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExport.Location = new System.Drawing.Point(326, 17);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(129, 31);
            this.buttonExport.TabIndex = 41;
            this.buttonExport.Text = "Экспорт (*.xls)";
            this.toolTip1.SetToolTip(this.buttonExport, "Сохранить полученные в программе данные");
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 616);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1255, 26);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 43;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(177, 20);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 21);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 21);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.Location = new System.Drawing.Point(758, 17);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(103, 31);
            this.buttonStop.TabIndex = 44;
            this.buttonStop.Text = "Стоп";
            this.toolTip1.SetToolTip(this.buttonStop, "Прекращает длительные процессы в программе и закрывает системный порт");
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // comboBoxComPorts
            // 
            this.comboBoxComPorts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxComPorts.FormattingEnabled = true;
            this.comboBoxComPorts.Location = new System.Drawing.Point(89, 64);
            this.comboBoxComPorts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxComPorts.Name = "comboBoxComPorts";
            this.comboBoxComPorts.Size = new System.Drawing.Size(161, 24);
            this.comboBoxComPorts.TabIndex = 58;
            this.toolTip1.SetToolTip(this.comboBoxComPorts, "Системный последовательный порт");
            // 
            // numericUpDownComReadTimeout
            // 
            this.numericUpDownComReadTimeout.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownComReadTimeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownComReadTimeout.Location = new System.Drawing.Point(143, 146);
            this.numericUpDownComReadTimeout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDownComReadTimeout.Maximum = new decimal(new int[] {
            1600,
            0,
            0,
            0});
            this.numericUpDownComReadTimeout.Minimum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDownComReadTimeout.Name = "numericUpDownComReadTimeout";
            this.numericUpDownComReadTimeout.Size = new System.Drawing.Size(61, 18);
            this.numericUpDownComReadTimeout.TabIndex = 59;
            this.toolTip1.SetToolTip(this.numericUpDownComReadTimeout, "Время ожидания ответа одного счетчика");
            this.numericUpDownComReadTimeout.Value = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            // 
            // cbFromFileTcp
            // 
            this.cbFromFileTcp.AutoSize = true;
            this.cbFromFileTcp.Enabled = false;
            this.cbFromFileTcp.Location = new System.Drawing.Point(17, 193);
            this.cbFromFileTcp.Margin = new System.Windows.Forms.Padding(4);
            this.cbFromFileTcp.Name = "cbFromFileTcp";
            this.cbFromFileTcp.Size = new System.Drawing.Size(192, 21);
            this.cbFromFileTcp.TabIndex = 65;
            this.cbFromFileTcp.Text = "Данные порта из файла";
            this.toolTip1.SetToolTip(this.cbFromFileTcp, "Брать адрес и порт из загружаемой таблицы");
            this.cbFromFileTcp.UseVisualStyleBackColor = true;
            // 
            // numericUpDownComWriteTimeout
            // 
            this.numericUpDownComWriteTimeout.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownComWriteTimeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownComWriteTimeout.Location = new System.Drawing.Point(143, 117);
            this.numericUpDownComWriteTimeout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDownComWriteTimeout.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownComWriteTimeout.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownComWriteTimeout.Name = "numericUpDownComWriteTimeout";
            this.numericUpDownComWriteTimeout.Size = new System.Drawing.Size(61, 18);
            this.numericUpDownComWriteTimeout.TabIndex = 62;
            this.toolTip1.SetToolTip(this.numericUpDownComWriteTimeout, "Время по прошествии которого происходит таймаут записи. Не используется в данной " +
        "версии.");
            this.numericUpDownComWriteTimeout.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // btnIndPollCurrent
            // 
            this.btnIndPollCurrent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIndPollCurrent.Location = new System.Drawing.Point(185, 24);
            this.btnIndPollCurrent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnIndPollCurrent.Name = "btnIndPollCurrent";
            this.btnIndPollCurrent.Size = new System.Drawing.Size(51, 28);
            this.btnIndPollCurrent.TabIndex = 60;
            this.btnIndPollCurrent.Text = "Т";
            this.toolTip1.SetToolTip(this.btnIndPollCurrent, "Текущий параетр");
            this.btnIndPollCurrent.UseVisualStyleBackColor = true;
            this.btnIndPollCurrent.Click += new System.EventHandler(this.btnIndPollCurrent_Click);
            // 
            // btnIndPollDaily
            // 
            this.btnIndPollDaily.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIndPollDaily.Location = new System.Drawing.Point(185, 58);
            this.btnIndPollDaily.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnIndPollDaily.Name = "btnIndPollDaily";
            this.btnIndPollDaily.Size = new System.Drawing.Size(51, 28);
            this.btnIndPollDaily.TabIndex = 61;
            this.btnIndPollDaily.Text = "С";
            this.toolTip1.SetToolTip(this.btnIndPollDaily, "Суточный параметр");
            this.btnIndPollDaily.UseVisualStyleBackColor = true;
            this.btnIndPollDaily.Click += new System.EventHandler(this.btnIndPollDaily_Click);
            // 
            // checkBoxPollOffline
            // 
            this.checkBoxPollOffline.AutoSize = true;
            this.checkBoxPollOffline.Enabled = false;
            this.checkBoxPollOffline.Location = new System.Drawing.Point(14, 24);
            this.checkBoxPollOffline.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxPollOffline.Name = "checkBoxPollOffline";
            this.checkBoxPollOffline.Size = new System.Drawing.Size(192, 21);
            this.checkBoxPollOffline.TabIndex = 55;
            this.checkBoxPollOffline.Text = "Опрос по неответившим";
            this.toolTip1.SetToolTip(this.checkBoxPollOffline, "Если флаг снят, работаем в режиме записи");
            this.checkBoxPollOffline.UseVisualStyleBackColor = true;
            this.checkBoxPollOffline.CheckedChanged += new System.EventHandler(this.checkBoxPollOffline_CheckedChanged);
            // 
            // cbConfiguration
            // 
            this.cbConfiguration.FormattingEnabled = true;
            this.cbConfiguration.Location = new System.Drawing.Point(14, 98);
            this.cbConfiguration.Name = "cbConfiguration";
            this.cbConfiguration.Size = new System.Drawing.Size(234, 24);
            this.cbConfiguration.TabIndex = 56;
            this.toolTip1.SetToolTip(this.cbConfiguration, "Подгружаемый xml для счетчиков тепла или воды?");
            // 
            // cbOnlyDateUpd
            // 
            this.cbOnlyDateUpd.AutoSize = true;
            this.cbOnlyDateUpd.Checked = true;
            this.cbOnlyDateUpd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOnlyDateUpd.Location = new System.Drawing.Point(14, 64);
            this.cbOnlyDateUpd.Name = "cbOnlyDateUpd";
            this.cbOnlyDateUpd.Size = new System.Drawing.Size(185, 21);
            this.cbOnlyDateUpd.TabIndex = 57;
            this.cbOnlyDateUpd.Text = "Обновлять только дату";
            this.toolTip1.SetToolTip(this.cbOnlyDateUpd, "Обновлять только дату в режиме инициализации приборов");
            this.cbOnlyDateUpd.UseVisualStyleBackColor = true;
            // 
            // gbAdditionalSettings
            // 
            this.gbAdditionalSettings.Controls.Add(this.panel3);
            this.gbAdditionalSettings.Controls.Add(this.panel1);
            this.gbAdditionalSettings.Controls.Add(this.richTextBox1);
            this.gbAdditionalSettings.Location = new System.Drawing.Point(12, 446);
            this.gbAdditionalSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbAdditionalSettings.Name = "gbAdditionalSettings";
            this.gbAdditionalSettings.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbAdditionalSettings.Size = new System.Drawing.Size(1233, 156);
            this.gbAdditionalSettings.TabIndex = 49;
            this.gbAdditionalSettings.TabStop = false;
            this.gbAdditionalSettings.Text = "Расширенные настройки";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnIndPollInfo);
            this.panel3.Controls.Add(this.btnIndPollDaily);
            this.panel3.Controls.Add(this.btnIndPollCurrent);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.numericUpDown2);
            this.panel3.Controls.Add(this.numericUpDown1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Location = new System.Drawing.Point(283, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(336, 138);
            this.panel3.TabIndex = 63;
            // 
            // btnIndPollInfo
            // 
            this.btnIndPollInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIndPollInfo.Location = new System.Drawing.Point(185, 92);
            this.btnIndPollInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnIndPollInfo.Name = "btnIndPollInfo";
            this.btnIndPollInfo.Size = new System.Drawing.Size(133, 28);
            this.btnIndPollInfo.TabIndex = 62;
            this.btnIndPollInfo.Text = "О приборе";
            this.btnIndPollInfo.UseVisualStyleBackColor = true;
            this.btnIndPollInfo.Click += new System.EventHandler(this.btnIndPollInfo_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 17);
            this.label8.TabIndex = 58;
            this.label8.Text = "Канал:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 57;
            this.label3.Text = "Парам.:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(85, 96);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(75, 22);
            this.numericUpDown2.TabIndex = 56;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(85, 66);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(75, 22);
            this.numericUpDown1.TabIndex = 55;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 17);
            this.label2.TabIndex = 52;
            this.label2.Text = "Адрес прибора:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(7, 24);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(153, 22);
            this.textBox1.TabIndex = 51;
            this.textBox1.Text = "1119271";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbOnlyDateUpd);
            this.panel1.Controls.Add(this.cbConfiguration);
            this.panel1.Controls.Add(this.checkBoxPollOffline);
            this.panel1.Controls.Add(this.cbJustRead);
            this.panel1.Location = new System.Drawing.Point(6, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(271, 138);
            this.panel1.TabIndex = 61;
            // 
            // cbJustRead
            // 
            this.cbJustRead.AutoSize = true;
            this.cbJustRead.Location = new System.Drawing.Point(14, 44);
            this.cbJustRead.Margin = new System.Windows.Forms.Padding(4);
            this.cbJustRead.Name = "cbJustRead";
            this.cbJustRead.Size = new System.Drawing.Size(128, 21);
            this.cbJustRead.TabIndex = 54;
            this.cbJustRead.Text = "Только чтение";
            this.cbJustRead.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(625, 13);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(602, 139);
            this.richTextBox1.TabIndex = 51;
            this.richTextBox1.Text = "";
            this.richTextBox1.DoubleClick += new System.EventHandler(this.richTextBox1_DoubleClick);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxLogo.Image = global::elfextendedapp.Properties.Resources.pi_logo_2;
            this.pictureBoxLogo.InitialImage = null;
            this.pictureBoxLogo.Location = new System.Drawing.Point(902, 9);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(54, 52);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 50;
            this.pictureBoxLogo.TabStop = false;
            this.pictureBoxLogo.Click += new System.EventHandler(this.pictureBoxLogo_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 31);
            this.label5.TabIndex = 51;
            this.label5.Text = "Пульсар";
            // 
            // gbConnection
            // 
            this.gbConnection.Controls.Add(this.btnApplyConnectionSettings);
            this.gbConnection.Controls.Add(this.label10);
            this.gbConnection.Controls.Add(this.listBox1);
            this.gbConnection.Controls.Add(this.label7);
            this.gbConnection.Controls.Add(this.label6);
            this.gbConnection.Controls.Add(this.rbCom);
            this.gbConnection.Controls.Add(this.rbTcp);
            this.gbConnection.Controls.Add(this.cbFromFileTcp);
            this.gbConnection.Controls.Add(this.label4);
            this.gbConnection.Controls.Add(this.numericUpDownComWriteTimeout);
            this.gbConnection.Controls.Add(this.label1);
            this.gbConnection.Controls.Add(this.numericUpDownComReadTimeout);
            this.gbConnection.Controls.Add(this.comboBoxComPorts);
            this.gbConnection.Controls.Add(this.textBoxPort);
            this.gbConnection.Controls.Add(this.textBoxIp);
            this.gbConnection.Location = new System.Drawing.Point(973, 12);
            this.gbConnection.Name = "gbConnection";
            this.gbConnection.Size = new System.Drawing.Size(272, 419);
            this.gbConnection.TabIndex = 59;
            this.gbConnection.TabStop = false;
            this.gbConnection.Text = "Подключение";
            // 
            // btnApplyConnectionSettings
            // 
            this.btnApplyConnectionSettings.Location = new System.Drawing.Point(17, 362);
            this.btnApplyConnectionSettings.Name = "btnApplyConnectionSettings";
            this.btnApplyConnectionSettings.Size = new System.Drawing.Size(233, 35);
            this.btnApplyConnectionSettings.TabIndex = 72;
            this.btnApplyConnectionSettings.Text = "Применить";
            this.btnApplyConnectionSettings.UseVisualStyleBackColor = true;
            this.btnApplyConnectionSettings.Click += new System.EventHandler(this.btnApplyConnectionSettings_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 236);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(169, 17);
            this.label10.TabIndex = 71;
            this.label10.Text = "Выберите локальный ip:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(17, 257);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(233, 84);
            this.listBox1.TabIndex = 70;
            this.listBox1.Visible = false;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(216, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 17);
            this.label7.TabIndex = 69;
            this.label7.Text = "(мс)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 17);
            this.label6.TabIndex = 68;
            this.label6.Text = "Таймаут чтение:";
            // 
            // rbCom
            // 
            this.rbCom.AutoSize = true;
            this.rbCom.Location = new System.Drawing.Point(17, 65);
            this.rbCom.Name = "rbCom";
            this.rbCom.Size = new System.Drawing.Size(60, 21);
            this.rbCom.TabIndex = 67;
            this.rbCom.TabStop = true;
            this.rbCom.Tag = "com";
            this.rbCom.Text = "COM";
            this.rbCom.UseVisualStyleBackColor = true;
            // 
            // rbTcp
            // 
            this.rbTcp.AutoSize = true;
            this.rbTcp.Location = new System.Drawing.Point(17, 39);
            this.rbTcp.Name = "rbTcp";
            this.rbTcp.Size = new System.Drawing.Size(56, 21);
            this.rbTcp.TabIndex = 66;
            this.rbTcp.TabStop = true;
            this.rbTcp.Tag = "tcp";
            this.rbTcp.Text = "TCP";
            this.rbTcp.UseVisualStyleBackColor = true;
            this.rbTcp.CheckedChanged += new System.EventHandler(this.rbTcp_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 17);
            this.label4.TabIndex = 64;
            this.label4.Text = "Таймаут запись: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 60;
            this.label1.Text = "(мс)";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(194, 38);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(56, 22);
            this.textBoxPort.TabIndex = 56;
            this.textBoxPort.Text = "4001";
            // 
            // textBoxIp
            // 
            this.textBoxIp.Location = new System.Drawing.Point(89, 38);
            this.textBoxIp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxIp.Name = "textBoxIp";
            this.textBoxIp.Size = new System.Drawing.Size(99, 22);
            this.textBoxIp.TabIndex = 55;
            this.textBoxIp.Text = "10.0.0.11";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 642);
            this.Controls.Add(this.gbConnection);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.gbAdditionalSettings);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonPoll);
            this.Controls.Add(this.buttonPing);
            this.Controls.Add(this.dgv1);
            this.Controls.Add(this.buttonImport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Заголовок генерируется автоматически";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownComReadTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownComWriteTimeout)).EndInit();
            this.gbAdditionalSettings.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.gbConnection.ResumeLayout(false);
            this.gbConnection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.OpenFileDialog ofd1;
        private System.Windows.Forms.Button buttonPing;
        private System.Windows.Forms.SaveFileDialog sfd1;
        private System.Windows.Forms.Button buttonPoll;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gbAdditionalSettings;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbConnection;
        private System.Windows.Forms.Button btnApplyConnectionSettings;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbCom;
        private System.Windows.Forms.RadioButton rbTcp;
        private System.Windows.Forms.CheckBox cbFromFileTcp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownComWriteTimeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownComReadTimeout;
        private System.Windows.Forms.ComboBox comboBoxComPorts;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxIp;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnIndPollInfo;
        private System.Windows.Forms.Button btnIndPollDaily;
        private System.Windows.Forms.Button btnIndPollCurrent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbJustRead;
        private System.Windows.Forms.CheckBox checkBoxPollOffline;
        private System.Windows.Forms.ComboBox cbConfiguration;
        private System.Windows.Forms.CheckBox cbOnlyDateUpd;
    }
}

