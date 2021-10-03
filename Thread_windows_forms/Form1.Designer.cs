namespace Thread_windows_forms
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
            this.startTimerButton = new System.Windows.Forms.Button();
            this.stopTimerButton = new System.Windows.Forms.Button();
            this.timerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startTimerButton
            // 
            this.startTimerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.startTimerButton.Location = new System.Drawing.Point(3, 65);
            this.startTimerButton.Name = "startTimerButton";
            this.startTimerButton.Size = new System.Drawing.Size(75, 23);
            this.startTimerButton.TabIndex = 0;
            this.startTimerButton.Text = "Start";
            this.startTimerButton.UseVisualStyleBackColor = true;
            this.startTimerButton.Click += new System.EventHandler(this.startTimerButton_Click);
            // 
            // stopTimerButton
            // 
            this.stopTimerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.stopTimerButton.Location = new System.Drawing.Point(84, 65);
            this.stopTimerButton.Name = "stopTimerButton";
            this.stopTimerButton.Size = new System.Drawing.Size(75, 23);
            this.stopTimerButton.TabIndex = 1;
            this.stopTimerButton.Text = "Stop";
            this.stopTimerButton.UseVisualStyleBackColor = true;
            this.stopTimerButton.Click += new System.EventHandler(this.stopTimerButton_Click);
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.timerLabel.ForeColor = System.Drawing.Color.Lime;
            this.timerLabel.Location = new System.Drawing.Point(12, 19);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(143, 37);
            this.timerLabel.TabIndex = 2;
            this.timerLabel.Text = "00:00:00";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(167, 101);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.stopTimerButton);
            this.Controls.Add(this.startTimerButton);
            this.Name = "Form1";
            this.Text = "Timer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

private System.Windows.Forms.Button startTimerButton;
private System.Windows.Forms.Button stopTimerButton;
private System.Windows.Forms.Label timerLabel;
    }
}

