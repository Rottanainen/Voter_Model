namespace Symulacja
{
    partial class Form5
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
            this.lblMean = new System.Windows.Forms.Label();
            this.lblStdDev = new System.Windows.Forms.Label();
            this.lblUncertainty = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMean
            // 
            this.lblMean.AutoSize = true;
            this.lblMean.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblMean.Location = new System.Drawing.Point(349, 87);
            this.lblMean.Name = "lblMean";
            this.lblMean.Size = new System.Drawing.Size(79, 29);
            this.lblMean.TabIndex = 0;
            this.lblMean.Text = "label1";
            // 
            // lblStdDev
            // 
            this.lblStdDev.AutoSize = true;
            this.lblStdDev.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStdDev.Location = new System.Drawing.Point(349, 171);
            this.lblStdDev.Name = "lblStdDev";
            this.lblStdDev.Size = new System.Drawing.Size(79, 29);
            this.lblStdDev.TabIndex = 1;
            this.lblStdDev.Text = "label2";
            // 
            // lblUncertainty
            // 
            this.lblUncertainty.AutoSize = true;
            this.lblUncertainty.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblUncertainty.Location = new System.Drawing.Point(349, 269);
            this.lblUncertainty.Name = "lblUncertainty";
            this.lblUncertainty.Size = new System.Drawing.Size(79, 29);
            this.lblUncertainty.TabIndex = 2;
            this.lblUncertainty.Text = "label3";
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblUncertainty);
            this.Controls.Add(this.lblStdDev);
            this.Controls.Add(this.lblMean);
            this.Name = "Form5";
            this.Text = "Form5";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMean;
        private System.Windows.Forms.Label lblStdDev;
        private System.Windows.Forms.Label lblUncertainty;
    }
}