namespace Sudoku
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
            this.Grid = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.difficultyInput = new System.Windows.Forms.TextBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonVerify = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Grid
            // 
            this.Grid.Location = new System.Drawing.Point(25, 12);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(430, 430);
            this.Grid.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(507, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Difficulty:";
            // 
            // difficultyInput
            // 
            this.difficultyInput.Location = new System.Drawing.Point(563, 27);
            this.difficultyInput.Name = "difficultyInput";
            this.difficultyInput.Size = new System.Drawing.Size(34, 20);
            this.difficultyInput.TabIndex = 2;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(614, 25);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 3;
            this.buttonGenerate.Text = "Generate!";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonVerify
            // 
            this.buttonVerify.Location = new System.Drawing.Point(614, 67);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(75, 23);
            this.buttonVerify.TabIndex = 4;
            this.buttonVerify.Text = "Verify!";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.buttonVerify_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonVerify);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.difficultyInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Grid);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Grid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox difficultyInput;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Button buttonVerify;
    }
}

