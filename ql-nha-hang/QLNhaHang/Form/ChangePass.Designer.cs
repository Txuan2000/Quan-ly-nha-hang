
namespace QLNhaHang
{
    partial class ChangePass
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
            this.chkhienthi = new System.Windows.Forms.CheckBox();
            this.txtnhaplaimkmoi = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtmkmoi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtmkcu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnthoat = new System.Windows.Forms.Button();
            this.btnxacnhan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkhienthi
            // 
            this.chkhienthi.AutoSize = true;
            this.chkhienthi.Location = new System.Drawing.Point(204, 174);
            this.chkhienthi.Name = "chkhienthi";
            this.chkhienthi.Size = new System.Drawing.Size(184, 29);
            this.chkhienthi.TabIndex = 12;
            this.chkhienthi.Text = "Hiển thị mật khẩu";
            this.chkhienthi.UseVisualStyleBackColor = true;
            this.chkhienthi.CheckedChanged += new System.EventHandler(this.chkhienthi_CheckedChanged);
            // 
            // txtnhaplaimkmoi
            // 
            this.txtnhaplaimkmoi.Location = new System.Drawing.Point(204, 139);
            this.txtnhaplaimkmoi.Name = "txtnhaplaimkmoi";
            this.txtnhaplaimkmoi.Size = new System.Drawing.Size(163, 30);
            this.txtnhaplaimkmoi.TabIndex = 3;
            this.txtnhaplaimkmoi.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nhập lại mật khẩu mới:";
            // 
            // txtmkmoi
            // 
            this.txtmkmoi.Location = new System.Drawing.Point(204, 83);
            this.txtmkmoi.Name = "txtmkmoi";
            this.txtmkmoi.Size = new System.Drawing.Size(163, 30);
            this.txtmkmoi.TabIndex = 2;
            this.txtmkmoi.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Nhập mật khẩu mới:";
            // 
            // txtmkcu
            // 
            this.txtmkcu.Location = new System.Drawing.Point(204, 23);
            this.txtmkcu.Name = "txtmkcu";
            this.txtmkcu.Size = new System.Drawing.Size(163, 30);
            this.txtmkcu.TabIndex = 1;
            this.txtmkcu.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Nhập mật khẩu cũ:";
            // 
            // btnthoat
            // 
            this.btnthoat.Location = new System.Drawing.Point(230, 245);
            this.btnthoat.Name = "btnthoat";
            this.btnthoat.Size = new System.Drawing.Size(140, 82);
            this.btnthoat.TabIndex = 5;
            this.btnthoat.Text = "Thoát";
            this.btnthoat.UseVisualStyleBackColor = true;
            this.btnthoat.Click += new System.EventHandler(this.btnthoat_Click);
            // 
            // btnxacnhan
            // 
            this.btnxacnhan.Location = new System.Drawing.Point(38, 245);
            this.btnxacnhan.Name = "btnxacnhan";
            this.btnxacnhan.Size = new System.Drawing.Size(140, 82);
            this.btnxacnhan.TabIndex = 4;
            this.btnxacnhan.Text = "Xác nhận";
            this.btnxacnhan.UseVisualStyleBackColor = true;
            this.btnxacnhan.Click += new System.EventHandler(this.btnxacnhan_Click);
            // 
            // ChangePass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 360);
            this.Controls.Add(this.chkhienthi);
            this.Controls.Add(this.txtnhaplaimkmoi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtmkmoi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtmkcu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnthoat);
            this.Controls.Add(this.btnxacnhan);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ChangePass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đổi mật khẩu";
            this.Load += new System.EventHandler(this.ChangePass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkhienthi;
        private System.Windows.Forms.TextBox txtnhaplaimkmoi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtmkmoi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtmkcu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnthoat;
        private System.Windows.Forms.Button btnxacnhan;
    }
}