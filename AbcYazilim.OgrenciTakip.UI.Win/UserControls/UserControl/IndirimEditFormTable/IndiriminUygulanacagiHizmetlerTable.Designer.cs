﻿
namespace AbcYazilim.OgrenciTakip.UI.Win.UserControls.UserControl.IndirimEditFormTable
{
    partial class IndiriminUygulanacagiHizmetlerTable
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grid = new AbcYazilim.OgrenciTakip.UI.Win.UserControls.Grid.MyGridControl();
            this.tablo = new AbcYazilim.OgrenciTakip.UI.Win.UserControls.Grid.MyGridView();
            this.colHizmetAdi = new AbcYazilim.OgrenciTakip.UI.Win.UserControls.Grid.MyGridColumn();
            this.colIndirimTutari = new AbcYazilim.OgrenciTakip.UI.Win.UserControls.Grid.MyGridColumn();
            this.repositoryTutar = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.colIndirimOrani = new AbcYazilim.OgrenciTakip.UI.Win.UserControls.Grid.MyGridColumn();
            this.repositoryOran = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryTutar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryOran)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.MainView = this.tablo;
            this.grid.Name = "grid";
            this.grid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryTutar,
            this.repositoryOran});
            this.grid.Size = new System.Drawing.Size(387, 211);
            this.grid.TabIndex = 5;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tablo});
            // 
            // tablo
            // 
            this.tablo.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tablo.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Maroon;
            this.tablo.Appearance.FooterPanel.Options.UseFont = true;
            this.tablo.Appearance.FooterPanel.Options.UseForeColor = true;
            this.tablo.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Maroon;
            this.tablo.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.tablo.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.tablo.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tablo.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Maroon;
            this.tablo.Appearance.ViewCaption.Options.UseForeColor = true;
            this.tablo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHizmetAdi,
            this.colIndirimTutari,
            this.colIndirimOrani});
            this.tablo.GridControl = this.grid;
            this.tablo.Name = "tablo";
            this.tablo.OptionsMenu.EnableColumnMenu = false;
            this.tablo.OptionsMenu.EnableFooterMenu = false;
            this.tablo.OptionsMenu.EnableGroupPanelMenu = false;
            this.tablo.OptionsNavigation.EnterMoveNextColumn = true;
            this.tablo.OptionsPrint.AutoWidth = false;
            this.tablo.OptionsPrint.PrintFooter = false;
            this.tablo.OptionsPrint.PrintGroupFooter = false;
            this.tablo.OptionsView.ColumnAutoWidth = false;
            this.tablo.OptionsView.HeaderFilterButtonShowMode = DevExpress.XtraEditors.Controls.FilterButtonShowMode.Button;
            this.tablo.OptionsView.RowAutoHeight = true;
            this.tablo.OptionsView.ShowGroupPanel = false;
            this.tablo.OptionsView.ShowViewCaption = true;
            this.tablo.StatusBarAciklama = "Bu indirimin uygulanacağı hizmet kartlarını seçiniz.";
            this.tablo.StatusBarKisaYol = "( Shift + Insert)";
            this.tablo.StatusBarKisaYolAciklama = " Hizmet Kartı Ekleyiniz.";
            this.tablo.ViewCaption = "İndirimin Uygulanacağı Hizmetler";
            // 
            // colHizmetAdi
            // 
            this.colHizmetAdi.Caption = "Hizmet Adı";
            this.colHizmetAdi.FieldName = "HizmetAdi";
            this.colHizmetAdi.Name = "colHizmetAdi";
            this.colHizmetAdi.OptionsColumn.AllowEdit = false;
            this.colHizmetAdi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colHizmetAdi.OptionsFilter.AllowAutoFilter = false;
            this.colHizmetAdi.OptionsFilter.AllowFilter = false;
            this.colHizmetAdi.StatusBarAciklama = null;
            this.colHizmetAdi.StatusBarKisaYol = null;
            this.colHizmetAdi.StatusBarKisaYolAciklama = null;
            this.colHizmetAdi.Visible = true;
            this.colHizmetAdi.VisibleIndex = 0;
            this.colHizmetAdi.Width = 203;
            // 
            // colIndirimTutari
            // 
            this.colIndirimTutari.Caption = "Tutar";
            this.colIndirimTutari.ColumnEdit = this.repositoryTutar;
            this.colIndirimTutari.FieldName = "IndirimTutari";
            this.colIndirimTutari.Name = "colIndirimTutari";
            this.colIndirimTutari.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colIndirimTutari.OptionsFilter.AllowAutoFilter = false;
            this.colIndirimTutari.OptionsFilter.AllowFilter = false;
            this.colIndirimTutari.StatusBarAciklama = "İndirim Tutarı Giriniz.";
            this.colIndirimTutari.StatusBarKisaYol = "F4:";
            this.colIndirimTutari.StatusBarKisaYolAciklama = " Hesap Makinesi";
            this.colIndirimTutari.Visible = true;
            this.colIndirimTutari.VisibleIndex = 1;
            // 
            // repositoryTutar
            // 
            this.repositoryTutar.AutoHeight = false;
            this.repositoryTutar.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryTutar.DisplayFormat.FormatString = "n2";
            this.repositoryTutar.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryTutar.EditFormat.FormatString = "n2";
            this.repositoryTutar.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryTutar.Mask.EditMask = "n2";
            this.repositoryTutar.Name = "repositoryTutar";
            // 
            // colIndirimOrani
            // 
            this.colIndirimOrani.AppearanceCell.Options.UseTextOptions = true;
            this.colIndirimOrani.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIndirimOrani.Caption = "Oran ( % )";
            this.colIndirimOrani.ColumnEdit = this.repositoryOran;
            this.colIndirimOrani.FieldName = "IndirimOrani";
            this.colIndirimOrani.Name = "colIndirimOrani";
            this.colIndirimOrani.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colIndirimOrani.OptionsFilter.AllowAutoFilter = false;
            this.colIndirimOrani.OptionsFilter.AllowFilter = false;
            this.colIndirimOrani.StatusBarAciklama = "İndirim Oranı Giriniz.";
            this.colIndirimOrani.StatusBarKisaYol = null;
            this.colIndirimOrani.StatusBarKisaYolAciklama = null;
            this.colIndirimOrani.Visible = true;
            this.colIndirimOrani.VisibleIndex = 2;
            // 
            // repositoryOran
            // 
            this.repositoryOran.AutoHeight = false;
            this.repositoryOran.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryOran.DisplayFormat.FormatString = "n0";
            this.repositoryOran.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryOran.EditFormat.FormatString = "n0";
            this.repositoryOran.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryOran.Mask.EditMask = "n0";
            this.repositoryOran.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.repositoryOran.Name = "repositoryOran";
            // 
            // IndiriminUygulanacagiHizmetlerTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grid);
            this.Name = "IndiriminUygulanacagiHizmetlerTable";
            this.Size = new System.Drawing.Size(387, 235);
            this.Controls.SetChildIndex(this.grid, 0);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryTutar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryOran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Grid.MyGridControl grid;
        private Grid.MyGridView tablo;
        private Grid.MyGridColumn colHizmetAdi;
        private Grid.MyGridColumn colIndirimTutari;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryTutar;
        private Grid.MyGridColumn colIndirimOrani;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryOran;
    }
}
