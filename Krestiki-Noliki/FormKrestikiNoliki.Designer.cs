namespace Krestiki_Noliki
{
    partial class FormKrestikiNoliki
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonNull = new System.Windows.Forms.RadioButton();
            this.radioButtonKrest = new System.Windows.Forms.RadioButton();
            this.buttonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "3x3",
            "4x4",
            "5x5"});
            this.comboBox1.Location = new System.Drawing.Point(314, 736);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(69, 21);
            this.comboBox1.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(43, 710);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(340, 23);
            this.label3.TabIndex = 20;
            this.label3.Text = "Выберите размер игрового поля:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(477, 617);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 23);
            this.label1.TabIndex = 19;
            this.label1.Text = "Выберите фигуру:";
            // 
            // radioButtonNull
            // 
            this.radioButtonNull.AutoSize = true;
            this.radioButtonNull.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonNull.ForeColor = System.Drawing.Color.Red;
            this.radioButtonNull.Location = new System.Drawing.Point(620, 676);
            this.radioButtonNull.Name = "radioButtonNull";
            this.radioButtonNull.Size = new System.Drawing.Size(43, 27);
            this.radioButtonNull.TabIndex = 18;
            this.radioButtonNull.TabStop = true;
            this.radioButtonNull.Text = "O";
            this.radioButtonNull.UseVisualStyleBackColor = true;
            // 
            // radioButtonKrest
            // 
            this.radioButtonKrest.AutoSize = true;
            this.radioButtonKrest.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonKrest.ForeColor = System.Drawing.Color.Red;
            this.radioButtonKrest.Location = new System.Drawing.Point(620, 643);
            this.radioButtonKrest.Name = "radioButtonKrest";
            this.radioButtonKrest.Size = new System.Drawing.Size(39, 27);
            this.radioButtonKrest.TabIndex = 17;
            this.radioButtonKrest.TabStop = true;
            this.radioButtonKrest.Text = "x";
            this.radioButtonKrest.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonStart.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStart.ForeColor = System.Drawing.Color.Red;
            this.buttonStart.Location = new System.Drawing.Point(226, 610);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(205, 86);
            this.buttonStart.TabIndex = 16;
            this.buttonStart.Text = "Старт";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // FormKrestikiNoliki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 782);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButtonNull);
            this.Controls.Add(this.radioButtonKrest);
            this.Controls.Add(this.buttonStart);
            this.Name = "FormKrestikiNoliki";
            this.Text = "Крестики-нолики";
            this.Load += new System.EventHandler(this.FormKrestikiNoliki_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonNull;
        private System.Windows.Forms.RadioButton radioButtonKrest;
        private System.Windows.Forms.Button buttonStart;
    }
}

