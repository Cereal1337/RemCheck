﻿
namespace RemCheck
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectorState = new System.Windows.Forms.ComboBox();
            this.minsState = new System.Windows.Forms.TextBox();
            this.runButton = new System.Windows.Forms.Button();
            this.timerBox = new System.Windows.Forms.TextBox();
            this.abortButton = new System.Windows.Forms.Button();
            this.errorMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectorState
            // 
            this.selectorState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectorState.FormattingEnabled = true;
            this.selectorState.Items.AddRange(new object[] {
            "Tweet",
            "Custom Program"});
            this.selectorState.Location = new System.Drawing.Point(54, 41);
            this.selectorState.Name = "selectorState";
            this.selectorState.Size = new System.Drawing.Size(138, 23);
            this.selectorState.TabIndex = 0;
            // 
            // minsState
            // 
            this.minsState.Location = new System.Drawing.Point(226, 212);
            this.minsState.Name = "minsState";
            this.minsState.Size = new System.Drawing.Size(33, 23);
            this.minsState.TabIndex = 5;
            this.minsState.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(594, 91);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 7;
            this.runButton.Text = "Go";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // timerBox
            // 
            this.timerBox.BackColor = System.Drawing.SystemColors.Window;
            this.timerBox.Location = new System.Drawing.Point(403, 212);
            this.timerBox.Name = "timerBox";
            this.timerBox.PlaceholderText = "00:00:00";
            this.timerBox.ReadOnly = true;
            this.timerBox.Size = new System.Drawing.Size(100, 23);
            this.timerBox.TabIndex = 8;
            this.timerBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // abortButton
            // 
            this.abortButton.Location = new System.Drawing.Point(412, 336);
            this.abortButton.Name = "abortButton";
            this.abortButton.Size = new System.Drawing.Size(75, 23);
            this.abortButton.TabIndex = 9;
            this.abortButton.Text = "Abort";
            this.abortButton.UseVisualStyleBackColor = true;
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
            // 
            // errorMessage
            // 
            this.errorMessage.AutoSize = true;
            this.errorMessage.Location = new System.Drawing.Point(286, 324);
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.Size = new System.Drawing.Size(32, 15);
            this.errorMessage.TabIndex = 10;
            this.errorMessage.Text = "error";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 684);
            this.Controls.Add(this.errorMessage);
            this.Controls.Add(this.abortButton);
            this.Controls.Add(this.timerBox);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.minsState);
            this.Controls.Add(this.selectorState);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "s";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox selectorState;
        private System.Windows.Forms.TextBox minsState;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.TextBox timerBox;
        private System.Windows.Forms.Button abortButton;
        private System.Windows.Forms.Label errorMessage;
    }
}
