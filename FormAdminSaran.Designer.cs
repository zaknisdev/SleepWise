namespace SleepWise
{
    partial class FormAdminSaran
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdminSaran));
            this.dgvHarian = new System.Windows.Forms.DataGridView();
            this.dgvMingguan = new System.Windows.Forms.DataGridView();
            this.txtSaranHarian = new System.Windows.Forms.RichTextBox();
            this.txtSaranMingguan = new System.Windows.Forms.RichTextBox();
            this.btnSimpanHarian = new System.Windows.Forms.Button();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnSimpanMingguan = new System.Windows.Forms.Button();
            this.btnLoadHarian = new System.Windows.Forms.Button();
            this.btnLoadMinguan = new System.Windows.Forms.Button();
            this.btnKembali = new System.Windows.Forms.Button();
            this.lblHarian = new System.Windows.Forms.Label();
            this.lblMingguan = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHarian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMingguan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHarian
            // 
            this.dgvHarian.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHarian.Location = new System.Drawing.Point(12, 209);
            this.dgvHarian.Name = "dgvHarian";
            this.dgvHarian.RowHeadersWidth = 62;
            this.dgvHarian.RowTemplate.Height = 28;
            this.dgvHarian.Size = new System.Drawing.Size(308, 229);
            this.dgvHarian.TabIndex = 0;
            this.dgvHarian.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dgvMingguan
            // 
            this.dgvMingguan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMingguan.Location = new System.Drawing.Point(480, 209);
            this.dgvMingguan.Name = "dgvMingguan";
            this.dgvMingguan.RowHeadersWidth = 62;
            this.dgvMingguan.RowTemplate.Height = 28;
            this.dgvMingguan.Size = new System.Drawing.Size(308, 229);
            this.dgvMingguan.TabIndex = 1;
            // 
            // txtSaranHarian
            // 
            this.txtSaranHarian.Location = new System.Drawing.Point(12, 59);
            this.txtSaranHarian.Name = "txtSaranHarian";
            this.txtSaranHarian.Size = new System.Drawing.Size(308, 65);
            this.txtSaranHarian.TabIndex = 2;
            this.txtSaranHarian.Text = "";
            // 
            // txtSaranMingguan
            // 
            this.txtSaranMingguan.Location = new System.Drawing.Point(480, 59);
            this.txtSaranMingguan.Name = "txtSaranMingguan";
            this.txtSaranMingguan.Size = new System.Drawing.Size(308, 65);
            this.txtSaranMingguan.TabIndex = 3;
            this.txtSaranMingguan.Text = "";
            // 
            // btnSimpanHarian
            // 
            this.btnSimpanHarian.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSimpanHarian.Location = new System.Drawing.Point(12, 130);
            this.btnSimpanHarian.Name = "btnSimpanHarian";
            this.btnSimpanHarian.Size = new System.Drawing.Size(121, 70);
            this.btnSimpanHarian.TabIndex = 4;
            this.btnSimpanHarian.Text = "Simpan";
            this.btnSimpanHarian.UseVisualStyleBackColor = false;
            this.btnSimpanHarian.Click += new System.EventHandler(this.btnSimpanHarian_Click);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(800, 38);
            this.bindingNavigator1.TabIndex = 5;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(34, 33);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(54, 33);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(34, 33);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(34, 33);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(34, 33);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 38);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 31);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(34, 33);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(34, 33);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // btnSimpanMingguan
            // 
            this.btnSimpanMingguan.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSimpanMingguan.Location = new System.Drawing.Point(490, 130);
            this.btnSimpanMingguan.Name = "btnSimpanMingguan";
            this.btnSimpanMingguan.Size = new System.Drawing.Size(121, 70);
            this.btnSimpanMingguan.TabIndex = 6;
            this.btnSimpanMingguan.Text = "Simpan";
            this.btnSimpanMingguan.UseVisualStyleBackColor = false;
            this.btnSimpanMingguan.Click += new System.EventHandler(this.btnSimpanMingguan_Click);
            // 
            // btnLoadHarian
            // 
            this.btnLoadHarian.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLoadHarian.Location = new System.Drawing.Point(139, 130);
            this.btnLoadHarian.Name = "btnLoadHarian";
            this.btnLoadHarian.Size = new System.Drawing.Size(121, 70);
            this.btnLoadHarian.TabIndex = 7;
            this.btnLoadHarian.Text = "Load";
            this.btnLoadHarian.UseVisualStyleBackColor = false;
            this.btnLoadHarian.Click += new System.EventHandler(this.btnLoadHarian_Click);
            // 
            // btnLoadMinguan
            // 
            this.btnLoadMinguan.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLoadMinguan.Location = new System.Drawing.Point(617, 130);
            this.btnLoadMinguan.Name = "btnLoadMinguan";
            this.btnLoadMinguan.Size = new System.Drawing.Size(121, 70);
            this.btnLoadMinguan.TabIndex = 8;
            this.btnLoadMinguan.Text = "Simpan";
            this.btnLoadMinguan.UseVisualStyleBackColor = false;
            this.btnLoadMinguan.Click += new System.EventHandler(this.btnLoadMingguan_Click);
            // 
            // btnKembali
            // 
            this.btnKembali.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnKembali.Location = new System.Drawing.Point(347, 303);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(110, 63);
            this.btnKembali.TabIndex = 9;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = false;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // lblHarian
            // 
            this.lblHarian.AutoSize = true;
            this.lblHarian.Location = new System.Drawing.Point(135, 36);
            this.lblHarian.Name = "lblHarian";
            this.lblHarian.Size = new System.Drawing.Size(56, 20);
            this.lblHarian.TabIndex = 10;
            this.lblHarian.Text = "Harian";
            // 
            // lblMingguan
            // 
            this.lblMingguan.AutoSize = true;
            this.lblMingguan.Location = new System.Drawing.Point(613, 36);
            this.lblMingguan.Name = "lblMingguan";
            this.lblMingguan.Size = new System.Drawing.Size(79, 20);
            this.lblMingguan.TabIndex = 11;
            this.lblMingguan.Text = "Mingguan";
            // 
            // FormAdminSaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblMingguan);
            this.Controls.Add(this.lblHarian);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.btnLoadMinguan);
            this.Controls.Add(this.btnLoadHarian);
            this.Controls.Add(this.btnSimpanMingguan);
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.btnSimpanHarian);
            this.Controls.Add(this.txtSaranMingguan);
            this.Controls.Add(this.txtSaranHarian);
            this.Controls.Add(this.dgvMingguan);
            this.Controls.Add(this.dgvHarian);
            this.Name = "FormAdminSaran";
            this.Text = "FormAdminSaran";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHarian)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMingguan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHarian;
        private System.Windows.Forms.DataGridView dgvMingguan;
        private System.Windows.Forms.RichTextBox txtSaranHarian;
        private System.Windows.Forms.RichTextBox txtSaranMingguan;
        private System.Windows.Forms.Button btnSimpanHarian;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button btnSimpanMingguan;
        private System.Windows.Forms.Button btnLoadHarian;
        private System.Windows.Forms.Button btnLoadMinguan;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Label lblHarian;
        private System.Windows.Forms.Label lblMingguan;
    }
}