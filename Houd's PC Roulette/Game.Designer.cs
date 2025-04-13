namespace Houd_s_PC_Roulette
{
    partial class Game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            label1 = new Label();
            guessBox = new MaskedTextBox();
            button1 = new Button();
            resultLabel = new Label();
            pointsLabel = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(38, 20);
            label1.Name = "label1";
            label1.Size = new Size(303, 24);
            label1.TabIndex = 0;
            label1.Text = "Pick a number between 1 and 8";
            // 
            // guessBox
            // 
            guessBox.Location = new Point(38, 127);
            guessBox.Name = "guessBox";
            guessBox.Size = new Size(303, 23);
            guessBox.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(145, 252);
            button1.Name = "button1";
            button1.Size = new Size(89, 23);
            button1.TabIndex = 2;
            button1.Text = "Check Guess";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // resultLabel
            // 
            resultLabel.AutoSize = true;
            resultLabel.Font = new Font("Arial", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            resultLabel.Location = new Point(46, 197);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new Size(0, 19);
            resultLabel.TabIndex = 3;
            // 
            // pointsLabel
            // 
            pointsLabel.AutoSize = true;
            pointsLabel.Font = new Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pointsLabel.Location = new Point(12, 286);
            pointsLabel.Name = "pointsLabel";
            pointsLabel.Size = new Size(59, 16);
            pointsLabel.TabIndex = 4;
            pointsLabel.Text = "Points: 0";
            // 
            // Game
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(379, 311);
            Controls.Add(pointsLabel);
            Controls.Add(resultLabel);
            Controls.Add(button1);
            Controls.Add(guessBox);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Game";
            Text = "Guess the Number Game";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private MaskedTextBox guessBox;
        private Button button1;
        private Label resultLabel;
        private Label pointsLabel;
    }
}