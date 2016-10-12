namespace WindowsFormsApp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTestDeadLocked = new System.Windows.Forms.Button();
            this.btnTest2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestDeadLocked
            // 
            this.btnTestDeadLocked.Location = new System.Drawing.Point(36, 27);
            this.btnTestDeadLocked.Name = "btnTestDeadLocked";
            this.btnTestDeadLocked.Size = new System.Drawing.Size(146, 43);
            this.btnTestDeadLocked.TabIndex = 0;
            this.btnTestDeadLocked.Text = "死锁的测试,ConfigureAwait解决";
            this.btnTestDeadLocked.UseVisualStyleBackColor = true;
            this.btnTestDeadLocked.Click += new System.EventHandler(this.btnTestDeadLocked_Click);
            // 
            // btnTest2
            // 
            this.btnTest2.Location = new System.Drawing.Point(36, 94);
            this.btnTest2.Name = "btnTest2";
            this.btnTest2.Size = new System.Drawing.Size(146, 43);
            this.btnTest2.TabIndex = 1;
            this.btnTest2.Text = "async标记事件解决";
            this.btnTest2.UseVisualStyleBackColor = true;
            this.btnTest2.Click += new System.EventHandler(this.btnTest2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 158);
            this.Controls.Add(this.btnTest2);
            this.Controls.Add(this.btnTestDeadLocked);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestDeadLocked;
        private System.Windows.Forms.Button btnTest2;
    }
}

