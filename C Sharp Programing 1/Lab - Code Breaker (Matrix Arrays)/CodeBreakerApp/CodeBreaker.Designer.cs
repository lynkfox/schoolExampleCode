namespace CodeBreakerApp
{
    partial class CodeBreaker
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
            this.magicWord_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.startPhrase_txt = new System.Windows.Forms.TextBox();
            this.endPhrase_txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.translate_btn = new System.Windows.Forms.Button();
            this.Clear_btn = new System.Windows.Forms.Button();
            this.Close_btn = new System.Windows.Forms.Button();
            this.ShowKey_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // magicWord_txt
            // 
            this.magicWord_txt.Location = new System.Drawing.Point(235, 68);
            this.magicWord_txt.Name = "magicWord_txt";
            this.magicWord_txt.Size = new System.Drawing.Size(566, 38);
            this.magicWord_txt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Key Phrase";
            // 
            // startPhrase_txt
            // 
            this.startPhrase_txt.Location = new System.Drawing.Point(46, 214);
            this.startPhrase_txt.Multiline = true;
            this.startPhrase_txt.Name = "startPhrase_txt";
            this.startPhrase_txt.Size = new System.Drawing.Size(979, 226);
            this.startPhrase_txt.TabIndex = 2;
            // 
            // endPhrase_txt
            // 
            this.endPhrase_txt.Location = new System.Drawing.Point(46, 558);
            this.endPhrase_txt.Multiline = true;
            this.endPhrase_txt.Name = "endPhrase_txt";
            this.endPhrase_txt.Size = new System.Drawing.Size(979, 226);
            this.endPhrase_txt.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(523, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Enter your phrase to Code/Decode here:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 491);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(464, 32);
            this.label3.TabIndex = 5;
            this.label3.Text = "Your Encocded/Decoded Phrase is:";
            // 
            // translate_btn
            // 
            this.translate_btn.Location = new System.Drawing.Point(1092, 71);
            this.translate_btn.Name = "translate_btn";
            this.translate_btn.Size = new System.Drawing.Size(218, 84);
            this.translate_btn.TabIndex = 6;
            this.translate_btn.Text = "Translate";
            this.translate_btn.UseVisualStyleBackColor = true;
            this.translate_btn.Click += new System.EventHandler(this.translate_btn_Click);
            // 
            // Clear_btn
            // 
            this.Clear_btn.Location = new System.Drawing.Point(1093, 190);
            this.Clear_btn.Name = "Clear_btn";
            this.Clear_btn.Size = new System.Drawing.Size(217, 84);
            this.Clear_btn.TabIndex = 7;
            this.Clear_btn.Text = "Clear";
            this.Clear_btn.UseVisualStyleBackColor = true;
            this.Clear_btn.Click += new System.EventHandler(this.Clear_btn_Click);
            // 
            // Close_btn
            // 
            this.Close_btn.Location = new System.Drawing.Point(1092, 318);
            this.Close_btn.Name = "Close_btn";
            this.Close_btn.Size = new System.Drawing.Size(217, 84);
            this.Close_btn.TabIndex = 8;
            this.Close_btn.Text = "Close";
            this.Close_btn.UseVisualStyleBackColor = true;
            this.Close_btn.Click += new System.EventHandler(this.Close_btn_Click);
            // 
            // ShowKey_btn
            // 
            this.ShowKey_btn.Location = new System.Drawing.Point(1093, 449);
            this.ShowKey_btn.Name = "ShowKey_btn";
            this.ShowKey_btn.Size = new System.Drawing.Size(217, 84);
            this.ShowKey_btn.TabIndex = 9;
            this.ShowKey_btn.Text = "Show PlayFair Key";
            this.ShowKey_btn.UseVisualStyleBackColor = true;
            this.ShowKey_btn.Click += new System.EventHandler(this.ShowKey_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1667, 1092);
            this.Controls.Add(this.ShowKey_btn);
            this.Controls.Add(this.Close_btn);
            this.Controls.Add(this.Clear_btn);
            this.Controls.Add(this.translate_btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.endPhrase_txt);
            this.Controls.Add(this.startPhrase_txt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.magicWord_txt);
            this.Name = "Form1";
            this.Text = "Anthony\'s PlayFair Cipher Encoder/Decoder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox magicWord_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox startPhrase_txt;
        private System.Windows.Forms.TextBox endPhrase_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button translate_btn;
        private System.Windows.Forms.Button Clear_btn;
        private System.Windows.Forms.Button Close_btn;
        private System.Windows.Forms.Button ShowKey_btn;
    }
}

