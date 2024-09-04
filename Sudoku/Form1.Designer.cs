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
            System.Windows.Forms.TextBox infoTextBoxGenerator;
            this.Grid = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.difficultyInput = new System.Windows.Forms.TextBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonVerify = new System.Windows.Forms.Button();
            this.infoTextBoxSolver = new System.Windows.Forms.TextBox();
            this.solveButton = new System.Windows.Forms.Button();
            this.buttonClearGrid = new System.Windows.Forms.Button();
            this.sudokuSize = new System.Windows.Forms.ComboBox();
            this.infoTextBoxSize = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            infoTextBoxGenerator = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // infoTextBoxGenerator
            // 
            infoTextBoxGenerator.BackColor = System.Drawing.SystemColors.ButtonFace;
            infoTextBoxGenerator.BorderStyle = System.Windows.Forms.BorderStyle.None;
            infoTextBoxGenerator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            infoTextBoxGenerator.Location = new System.Drawing.Point(474, 206);
            infoTextBoxGenerator.Multiline = true;
            infoTextBoxGenerator.Name = "infoTextBoxGenerator";
            infoTextBoxGenerator.RightToLeft = System.Windows.Forms.RightToLeft.No;
            infoTextBoxGenerator.Size = new System.Drawing.Size(314, 61);
            infoTextBoxGenerator.TabIndex = 5;
            infoTextBoxGenerator.Text = "To start the Sudoku game enter the number of keys you want to play with and press" +
    " the \"Generate!\" button. Then press the \"Verify!\" button to check your result.\r\n" +
    "";
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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(471, 279);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Difficulty:";
            // 
            // difficultyInput
            // 
            this.difficultyInput.Location = new System.Drawing.Point(536, 279);
            this.difficultyInput.Name = "difficultyInput";
            this.difficultyInput.Size = new System.Drawing.Size(34, 20);
            this.difficultyInput.TabIndex = 2;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGenerate.Location = new System.Drawing.Point(590, 273);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(78, 33);
            this.buttonGenerate.TabIndex = 3;
            this.buttonGenerate.Text = "Generate!";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonVerify
            // 
            this.buttonVerify.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonVerify.Location = new System.Drawing.Point(674, 273);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(78, 33);
            this.buttonVerify.TabIndex = 4;
            this.buttonVerify.Text = "Verify!";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.buttonVerify_Click);
            // 
            // infoTextBoxSolver
            // 
            this.infoTextBoxSolver.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.infoTextBoxSolver.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.infoTextBoxSolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoTextBoxSolver.Location = new System.Drawing.Point(474, 325);
            this.infoTextBoxSolver.Multiline = true;
            this.infoTextBoxSolver.Name = "infoTextBoxSolver";
            this.infoTextBoxSolver.Size = new System.Drawing.Size(284, 74);
            this.infoTextBoxSolver.TabIndex = 6;
            this.infoTextBoxSolver.Text = "To solve existing Sudoku game enter your keys manually and press the \"Solve!\" but" +
    "ton. If you want to solve another game clear the grid by pressing the \"Clear!\" b" +
    "utton.";
            // 
            // solveButton
            // 
            this.solveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.solveButton.Location = new System.Drawing.Point(474, 407);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(78, 33);
            this.solveButton.TabIndex = 7;
            this.solveButton.Text = "Solve!";
            this.solveButton.UseVisualStyleBackColor = true;
            this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
            // 
            // buttonClearGrid
            // 
            this.buttonClearGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClearGrid.Location = new System.Drawing.Point(558, 407);
            this.buttonClearGrid.Name = "buttonClearGrid";
            this.buttonClearGrid.Size = new System.Drawing.Size(78, 33);
            this.buttonClearGrid.TabIndex = 9;
            this.buttonClearGrid.Text = "Clear!";
            this.buttonClearGrid.UseVisualStyleBackColor = true;
            this.buttonClearGrid.Click += new System.EventHandler(this.buttonClearGrid_Click);
            // 
            // sudokuSize
            // 
            this.sudokuSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sudokuSize.FormattingEnabled = true;
            this.sudokuSize.Items.AddRange(new object[] {
            "Sudoku 4 x 4",
            "Sudoku 6 x 6",
            "Sudoku 8 x 8",
            "Sudoku 9 x 9",
            "Sudoku 10 x 10",
            "Sudoku 12 x 12",
            "Sudoku 16 x 16"});
            this.sudokuSize.Location = new System.Drawing.Point(474, 43);
            this.sudokuSize.Name = "sudokuSize";
            this.sudokuSize.Size = new System.Drawing.Size(121, 24);
            this.sudokuSize.TabIndex = 10;
            this.sudokuSize.Text = "Sudoku Size";
            this.sudokuSize.SelectedIndexChanged += new System.EventHandler(this.sudokuSize_SelectedIndexChanged);
            // 
            // infoTextBoxSize
            // 
            this.infoTextBoxSize.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.infoTextBoxSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.infoTextBoxSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoTextBoxSize.Location = new System.Drawing.Point(474, 12);
            this.infoTextBoxSize.Multiline = true;
            this.infoTextBoxSize.Name = "infoTextBoxSize";
            this.infoTextBoxSize.Size = new System.Drawing.Size(273, 25);
            this.infoTextBoxSize.TabIndex = 11;
            this.infoTextBoxSize.Text = "Please choose Sudoku size you want to play.";
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(618, 38);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(78, 33);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Save!";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.Location = new System.Drawing.Point(710, 38);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(78, 33);
            this.loadButton.TabIndex = 13;
            this.loadButton.Text = "Load!";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 472);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.infoTextBoxSize);
            this.Controls.Add(this.sudokuSize);
            this.Controls.Add(this.buttonClearGrid);
            this.Controls.Add(this.solveButton);
            this.Controls.Add(this.infoTextBoxSolver);
            this.Controls.Add(infoTextBoxGenerator);
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
        private System.Windows.Forms.TextBox infoTextBoxSolver;
        private System.Windows.Forms.Button solveButton;
        private System.Windows.Forms.Button buttonClearGrid;
        private System.Windows.Forms.ComboBox sudokuSize;
        private System.Windows.Forms.TextBox infoTextBoxSize;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
    }
}

