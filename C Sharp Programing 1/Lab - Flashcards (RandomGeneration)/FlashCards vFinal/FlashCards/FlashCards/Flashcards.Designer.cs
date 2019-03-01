namespace FlashCards
{
    partial class Flashcards
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
            this.birdImage = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.answerBoxText = new System.Windows.Forms.TextBox();
            this.solutionBoxText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_StartButton = new System.Windows.Forms.Button();
            this.button_ShowAnswer = new System.Windows.Forms.Button();
            this.button_NewFlashcard = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.lableAnswerCorrect = new System.Windows.Forms.Label();
            this.testLabel = new System.Windows.Forms.Label();
            this.flashcardNumberLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.birdImage)).BeginInit();
            this.SuspendLayout();
            // 
            // birdImage
            // 
            this.birdImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.birdImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.birdImage.Location = new System.Drawing.Point(47, 42);
            this.birdImage.Name = "birdImage";
            this.birdImage.Size = new System.Drawing.Size(600, 400);
            this.birdImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.birdImage.TabIndex = 0;
            this.birdImage.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(704, 289);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(602, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = "What is the Name of this Bird?";
            // 
            // answerBoxText
            // 
            this.answerBoxText.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.answerBoxText.Location = new System.Drawing.Point(712, 361);
            this.answerBoxText.Name = "answerBoxText";
            this.answerBoxText.Size = new System.Drawing.Size(580, 46);
            this.answerBoxText.TabIndex = 2;
            // 
            // solutionBoxText
            // 
            this.solutionBoxText.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.solutionBoxText.Location = new System.Drawing.Point(712, 607);
            this.solutionBoxText.Name = "solutionBoxText";
            this.solutionBoxText.Size = new System.Drawing.Size(580, 46);
            this.solutionBoxText.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(706, 555);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "The Answer is: ";
            // 
            // button_StartButton
            // 
            this.button_StartButton.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_StartButton.Location = new System.Drawing.Point(734, 59);
            this.button_StartButton.Name = "button_StartButton";
            this.button_StartButton.Size = new System.Drawing.Size(538, 100);
            this.button_StartButton.TabIndex = 5;
            this.button_StartButton.Text = "Start Flashcards";
            this.button_StartButton.UseVisualStyleBackColor = true;
            this.button_StartButton.Click += new System.EventHandler(this.button_StartButton_Click);
            // 
            // button_ShowAnswer
            // 
            this.button_ShowAnswer.Location = new System.Drawing.Point(1044, 542);
            this.button_ShowAnswer.Name = "button_ShowAnswer";
            this.button_ShowAnswer.Size = new System.Drawing.Size(248, 50);
            this.button_ShowAnswer.TabIndex = 6;
            this.button_ShowAnswer.Text = "Show Answer";
            this.button_ShowAnswer.UseVisualStyleBackColor = true;
            this.button_ShowAnswer.Click += new System.EventHandler(this.button_ShowAnswer_Click);
            // 
            // button_NewFlashcard
            // 
            this.button_NewFlashcard.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_NewFlashcard.Location = new System.Drawing.Point(58, 565);
            this.button_NewFlashcard.Name = "button_NewFlashcard";
            this.button_NewFlashcard.Size = new System.Drawing.Size(537, 103);
            this.button_NewFlashcard.TabIndex = 7;
            this.button_NewFlashcard.Text = "New Flash Card";
            this.button_NewFlashcard.UseVisualStyleBackColor = true;
            this.button_NewFlashcard.Click += new System.EventHandler(this.button_NewFlashcard_Click);
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(1078, 719);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(156, 45);
            this.button_Close.TabIndex = 8;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // lableAnswerCorrect
            // 
            this.lableAnswerCorrect.AutoSize = true;
            this.lableAnswerCorrect.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableAnswerCorrect.ForeColor = System.Drawing.Color.Green;
            this.lableAnswerCorrect.Location = new System.Drawing.Point(716, 445);
            this.lableAnswerCorrect.Name = "lableAnswerCorrect";
            this.lableAnswerCorrect.Size = new System.Drawing.Size(0, 55);
            this.lableAnswerCorrect.TabIndex = 9;
            // 
            // testLabel
            // 
            this.testLabel.AutoSize = true;
            this.testLabel.BackColor = System.Drawing.SystemColors.Control;
            this.testLabel.Location = new System.Drawing.Point(801, 205);
            this.testLabel.Name = "testLabel";
            this.testLabel.Size = new System.Drawing.Size(229, 32);
            this.testLabel.TabIndex = 10;
            this.testLabel.Text = "Flashcard      of 5";
            // 
            // flashcardNumberLabel
            // 
            this.flashcardNumberLabel.AutoSize = true;
            this.flashcardNumberLabel.Location = new System.Drawing.Point(939, 205);
            this.flashcardNumberLabel.Name = "flashcardNumberLabel";
            this.flashcardNumberLabel.Size = new System.Drawing.Size(31, 32);
            this.flashcardNumberLabel.TabIndex = 11;
            this.flashcardNumberLabel.Text = "1";
            // 
            // Flashcards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2099, 993);
            this.Controls.Add(this.flashcardNumberLabel);
            this.Controls.Add(this.testLabel);
            this.Controls.Add(this.lableAnswerCorrect);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.button_NewFlashcard);
            this.Controls.Add(this.button_ShowAnswer);
            this.Controls.Add(this.button_StartButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.solutionBoxText);
            this.Controls.Add(this.answerBoxText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.birdImage);
            this.Name = "Flashcards";
            this.Text = "Anthony\'s Flashcard GUI";
            this.Load += new System.EventHandler(this.Flashcards_Load);
            ((System.ComponentModel.ISupportInitialize)(this.birdImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox birdImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox answerBoxText;
        private System.Windows.Forms.TextBox solutionBoxText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_StartButton;
        private System.Windows.Forms.Button button_ShowAnswer;
        private System.Windows.Forms.Button button_NewFlashcard;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.Label lableAnswerCorrect;
        private System.Windows.Forms.Label testLabel;
        private System.Windows.Forms.Label flashcardNumberLabel;
    }
}

