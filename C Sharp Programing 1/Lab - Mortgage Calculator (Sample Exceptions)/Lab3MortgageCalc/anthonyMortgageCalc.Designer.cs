namespace Lab3MortgageCalc
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.applicantNameText = new System.Windows.Forms.TextBox();
            this.homePurchasePriceText = new System.Windows.Forms.TextBox();
            this.downPaymentText = new System.Windows.Forms.TextBox();
            this.interestRateText = new System.Windows.Forms.TextBox();
            this.loanTermText = new System.Windows.Forms.TextBox();
            this.button_Calculate = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.monthlyPaymentText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.totalLoanText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Applicant Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(289, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Home Purchase Price";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(311, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "Down Payment Amount";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(291, 32);
            this.label4.TabIndex = 3;
            this.label4.Text = "Interest Rate (Annual)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 311);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(269, 32);
            this.label5.TabIndex = 4;
            this.label5.Text = "Loan Term (Months)";
            // 
            // applicantNameText
            // 
            this.applicantNameText.Location = new System.Drawing.Point(438, 54);
            this.applicantNameText.Name = "applicantNameText";
            this.applicantNameText.Size = new System.Drawing.Size(338, 38);
            this.applicantNameText.TabIndex = 5;
            // 
            // homePurchasePriceText
            // 
            this.homePurchasePriceText.Location = new System.Drawing.Point(438, 116);
            this.homePurchasePriceText.Name = "homePurchasePriceText";
            this.homePurchasePriceText.Size = new System.Drawing.Size(338, 38);
            this.homePurchasePriceText.TabIndex = 6;
            // 
            // downPaymentText
            // 
            this.downPaymentText.Location = new System.Drawing.Point(438, 182);
            this.downPaymentText.Name = "downPaymentText";
            this.downPaymentText.Size = new System.Drawing.Size(338, 38);
            this.downPaymentText.TabIndex = 7;
            // 
            // interestRateText
            // 
            this.interestRateText.Location = new System.Drawing.Point(438, 242);
            this.interestRateText.Name = "interestRateText";
            this.interestRateText.Size = new System.Drawing.Size(338, 38);
            this.interestRateText.TabIndex = 8;
            // 
            // loanTermText
            // 
            this.loanTermText.Location = new System.Drawing.Point(438, 304);
            this.loanTermText.Name = "loanTermText";
            this.loanTermText.Size = new System.Drawing.Size(338, 38);
            this.loanTermText.TabIndex = 9;
            // 
            // button_Calculate
            // 
            this.button_Calculate.Location = new System.Drawing.Point(438, 384);
            this.button_Calculate.Name = "button_Calculate";
            this.button_Calculate.Size = new System.Drawing.Size(337, 174);
            this.button_Calculate.TabIndex = 10;
            this.button_Calculate.Text = "Calculate";
            this.button_Calculate.UseVisualStyleBackColor = true;
            this.button_Calculate.Click += new System.EventHandler(this.button_Calculate_Click);
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(438, 713);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(338, 80);
            this.button_Close.TabIndex = 11;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // monthlyPaymentText
            // 
            this.monthlyPaymentText.Location = new System.Drawing.Point(438, 582);
            this.monthlyPaymentText.Name = "monthlyPaymentText";
            this.monthlyPaymentText.Size = new System.Drawing.Size(337, 38);
            this.monthlyPaymentText.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.label6.Location = new System.Drawing.Point(79, 588);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(249, 32);
            this.label6.TabIndex = 13;
            this.label6.Text = "Monthly Payment: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(79, 654);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(211, 32);
            this.label7.TabIndex = 14;
            this.label7.Text = "Total Financing";
            // 
            // totalLoanText
            // 
            this.totalLoanText.Location = new System.Drawing.Point(438, 648);
            this.totalLoanText.Name = "totalLoanText";
            this.totalLoanText.Size = new System.Drawing.Size(337, 38);
            this.totalLoanText.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 994);
            this.Controls.Add(this.totalLoanText);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.monthlyPaymentText);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.button_Calculate);
            this.Controls.Add(this.loanTermText);
            this.Controls.Add(this.interestRateText);
            this.Controls.Add(this.downPaymentText);
            this.Controls.Add(this.homePurchasePriceText);
            this.Controls.Add(this.applicantNameText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Anthony Morgage Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox applicantNameText;
        private System.Windows.Forms.TextBox homePurchasePriceText;
        private System.Windows.Forms.TextBox downPaymentText;
        private System.Windows.Forms.TextBox interestRateText;
        private System.Windows.Forms.TextBox loanTermText;
        private System.Windows.Forms.Button button_Calculate;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.TextBox monthlyPaymentText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox totalLoanText;
    }
}

