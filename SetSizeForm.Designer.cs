namespace BoundsTest
{
    partial class SetSizeForm
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
            this._sizeLabel = new System.Windows.Forms.Label();
            this._heightLabel = new System.Windows.Forms.Label();
            this._widthLabel = new System.Windows.Forms.Label();
            this._widthTextBox = new System.Windows.Forms.TextBox();
            this._heightTextBox = new System.Windows.Forms.TextBox();
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _sizeLabel
            // 
            this._sizeLabel.AutoSize = true;
            this._sizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._sizeLabel.Location = new System.Drawing.Point(12, 9);
            this._sizeLabel.Name = "_sizeLabel";
            this._sizeLabel.Size = new System.Drawing.Size(126, 16);
            this._sizeLabel.TabIndex = 0;
            this._sizeLabel.Text = "Set the window size:";
            // 
            // _heightLabel
            // 
            this._heightLabel.AutoSize = true;
            this._heightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._heightLabel.Location = new System.Drawing.Point(27, 63);
            this._heightLabel.Name = "_heightLabel";
            this._heightLabel.Size = new System.Drawing.Size(47, 16);
            this._heightLabel.TabIndex = 1;
            this._heightLabel.Text = "Height";
            // 
            // _widthLabel
            // 
            this._widthLabel.AutoSize = true;
            this._widthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._widthLabel.Location = new System.Drawing.Point(27, 35);
            this._widthLabel.Name = "_widthLabel";
            this._widthLabel.Size = new System.Drawing.Size(42, 16);
            this._widthLabel.TabIndex = 1;
            this._widthLabel.Text = "Width";
            // 
            // _widthTextBox
            // 
            this._widthTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._widthTextBox.Location = new System.Drawing.Point(80, 32);
            this._widthTextBox.MaxLength = 5;
            this._widthTextBox.Name = "_widthTextBox";
            this._widthTextBox.Size = new System.Drawing.Size(70, 22);
            this._widthTextBox.TabIndex = 3;
            // 
            // _heightTextBox
            // 
            this._heightTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._heightTextBox.Location = new System.Drawing.Point(80, 59);
            this._heightTextBox.MaxLength = 5;
            this._heightTextBox.Name = "_heightTextBox";
            this._heightTextBox.Size = new System.Drawing.Size(70, 22);
            this._heightTextBox.TabIndex = 4;
            // 
            // _okButton
            // 
            this._okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._okButton.Location = new System.Drawing.Point(12, 91);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(78, 32);
            this._okButton.TabIndex = 5;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._cancelButton.Location = new System.Drawing.Point(96, 91);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(78, 32);
            this._cancelButton.TabIndex = 6;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // SetSizeForm
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(184, 134);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._heightTextBox);
            this.Controls.Add(this._widthTextBox);
            this.Controls.Add(this._widthLabel);
            this.Controls.Add(this._heightLabel);
            this.Controls.Add(this._sizeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetSizeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Bounds Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _sizeLabel;
        private System.Windows.Forms.Label _heightLabel;
        private System.Windows.Forms.Label _widthLabel;
        private System.Windows.Forms.TextBox _widthTextBox;
        private System.Windows.Forms.TextBox _heightTextBox;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Button _cancelButton;
    }
}