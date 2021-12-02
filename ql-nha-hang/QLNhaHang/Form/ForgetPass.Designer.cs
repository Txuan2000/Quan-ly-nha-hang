
namespace QLNhaHang
{
    partial class ForgetPass
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
            this.txtsdt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txttennhanvien = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtmanv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnthoat = new System.Windows.Forms.Button();
            this.btnxacnhan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtsdt
            // 
            this.txtsdt.Location = new System.Drawing.Point(214, 181);
            this.txtsdt.Name = "txtsdt";
            this.txtsdt.Size = new System.Drawing.Size(208, 30);
            this.txtsdt.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Số điện thoại:";
            // 
            // txttennhanvien
            // 
            this.txttennhanvien.Location = new System.Drawing.Point(214, 124);
            this.txttennhanvien.Name = "txttennhanvien";
            this.txttennhanvien.Size = new System.Drawing.Size(208, 30);
            this.txttennhanvien.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tên nhân viên:";
            // 
            // txtmanv
            // 
            this.txtmanv.Location = new System.Drawing.Point(214, 75);
            this.txtmanv.Name = "txtmanv";
            this.txtmanv.Size = new System.Drawing.Size(208, 30);
            this.txtmanv.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mã nhân viên:";
            // 
            // btnthoat
            // 
            this.btnthoat.Location = new System.Drawing.Point(284, 267);
            this.btnthoat.Name = "btnthoat";
            this.btnthoat.Size = new System.Drawing.Size(140, 49);
            this.btnthoat.TabIndex = 5;
            this.btnthoat.Text = "Thoát";
            this.btnthoat.UseVisualStyleBackColor = true;
            this.btnthoat.Click += new System.EventHandler(this.btnthoat_Click);
            // 
            // btnxacnhan
            // 
            this.btnxacnhan.Location = new System.Drawing.Point(44, 267);
            this.btnxacnhan.Name = "btnxacnhan";
            this.btnxacnhan.Size = new System.Drawing.Size(140, 49);
            this.btnxacnhan.TabIndex = 4;
            this.btnxacnhan.Text = "Xác nhận";
            this.btnxacnhan.UseVisualStyleBackColor = true;
            this.btnxacnhan.Click += new System.EventHandler(this.btnxacnhan_Click);
            // 
            // ForgetPass
            // 
            this.AcceptButton = this.btnxacnhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 415);
            this.Controls.Add(this.txtsdt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txttennhanvien);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtmanv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnthoat);
            this.Controls.Add(this.btnxacnhan);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ForgetPass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quên mật khẩu";
            this.Load += new System.EventHandler(this.ForgetPass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtsdt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txttennhanvien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtmanv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnthoat;
        private System.Windows.Forms.Button btnxacnhan;
    }
}