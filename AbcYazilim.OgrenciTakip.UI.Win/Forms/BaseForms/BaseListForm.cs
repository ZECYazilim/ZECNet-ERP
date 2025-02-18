﻿using AbcYazilim.OgrenciTakip.Bll.Interfaces;
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;
using AbcYazilim.OgrenciTakip.UI.Win.Functions;
using AbcYazilim.OgrenciTakip.UI.Win.Show.Interfaces;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
using DevExpress.XtraPrinting.Native;
using DevExpress.Utils.Extensions;
using AbcYazilim.OgrenciTakip.UI.Win.Show;
using AbcYazilim.OgrenciTakip.UI.Win.Forms.FiltreForms;
using AbcYazilim.OgrenciTakip.Model.Entities;
using AbcYazilim.OgrenciTakip.UI.Win.GeneralForms;
using System.Collections.Generic;
using AbcYazilim.OgrenciTakip.Common.Message;

namespace AbcYazilim.OgrenciTakip.UI.Win.Forms.BaseForms
{
    public partial class BaseListForm : RibbonForm
    {
        private long _filtreId;
        private bool _formSablonKayitEdilecek;
        private bool _tabloSablonKayitEdilecek;
        protected IBaseFormShow FormShow;
        protected KartTuru kartTuru;
        protected internal GridView Tablo;
        protected bool AktifKartlariGoster = true;
        protected internal bool AktifPasifButonGoster=false;
        protected internal bool MultiSelect;
        protected internal BaseEntity SelectedEntity;
        protected IBaseBll Bll;
        protected ControlNavigator Navigator;
        protected internal long? SeciliGelecekId;
        protected BarItem[] ShowItems;
        protected BarItem[] HideItems;
        protected internal IList<long> ListeDisiTutulacakKayitlar;
        protected internal SelectRowFunctions RowSelect;
        protected internal bool EklenebilecekEntityVar = false;
        protected internal IList<BaseEntity> SelectedEntities;

        public BaseListForm()
        {
            InitializeComponent();
        }
        private void EventsLoad()
        {
            /* foreach (var item in ribbonControl.Items)
            {
                switch (item)
                {
                    case BarItem button:
                        button.ItemClick += Button_ItemClick;
                        break;
                }
            }*/  //FARKLI BİR KULLANIM ÖRNEĞİ birden fazla tür olması durumunda

            //Button Events
            foreach (BarItem button in ribbonControl.Items)
                button.ItemClick += Button_ItemClick;
            //Table Events
            Tablo.DoubleClick += Tablo_DoubleClick;
            Tablo.KeyDown += Tablo_KeyDown;
            Tablo.MouseUp += Tablo_MouseUp;
            Tablo.ColumnWidthChanged += Tablo_ColumnWidthChanged;
            Tablo.ColumnPositionChanged += Tablo_ColumnPositionChanged;
            Tablo.EndSorting += Tablo_EndSorting;
            Tablo.FilterEditorCreated += Tablo_FilterEditorCreated;
            Tablo.ColumnFilterChanged += Tablo_ColumnFilterChanged;
            Tablo.CustomDrawFooterCell += Tablo_CustomDrawFooterCell;
            Tablo.FocusedRowObjectChanged += Tablo_FocusedRowObjectChanged;
            Tablo.RowDeleted += Tablo_RowDeleted;
            //Form Events
            Shown += BaseListForm_Shown;
            Load += BaseListForm_Load;
            FormClosing += BaseListForm_FormClosing;
            LocationChanged += BaseListForm_LocationChanged;
            SizeChanged += BaseListForm_SizeChanged;
        }
        private void Tablo_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            SutunGizleGoster();
        }
        private void Tablo_ColumnFilterChanged(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(Tablo.ActiveFilterString))
                _filtreId = 0;
        }
        private void Tablo_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (!Tablo.OptionsView.ShowFooter) return;
            if (e.Column.Summary.Count > 0)
                e.Appearance.TextOptions.HAlignment = e.Column.ColumnEdit.Appearance.HAlignment;
        }
        private void Tablo_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            SutunGizleGoster();
        }
        private void Tablo_FilterEditorCreated(object sender, DevExpress.XtraGrid.Views.Base.FilterControlEventArgs e)
        {
            e.ShowFilterEditor = false;
            ShowEditForms<FiltreEditForm>.ShowDialogEditForm(KartTuru.Filtre, _filtreId, kartTuru, Tablo.GridControl);
        }

        private void BaseListForm_SizeChanged(object sender, System.EventArgs e)
        {
            if(!IsMdiChild)
                _formSablonKayitEdilecek = true;
        }

        private void BaseListForm_LocationChanged(object sender, System.EventArgs e)
        {
            if (!IsMdiChild)
                _formSablonKayitEdilecek = true;
        }

        private void Tablo_EndSorting(object sender, System.EventArgs e)
        {
            _tabloSablonKayitEdilecek = true;
        }

        private void Tablo_ColumnPositionChanged(object sender, System.EventArgs e)
        {
            _tabloSablonKayitEdilecek = true;
        }

        private void Tablo_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            _tabloSablonKayitEdilecek = true;
        }

        private void BaseListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SablonKaydet();
        }

        private void BaseListForm_Load(object sender, System.EventArgs e)
        {
            SablonYukle();
        }

        private void Tablo_MouseUp(object sender, MouseEventArgs e)
        {
            e.SagMenuGoster(sagMenu);
        }

        private void BaseListForm_Shown(object sender, System.EventArgs e)
        {
            Tablo.Focus();
            ButonGizleGoster();
            SutunGizleGoster();

            if (IsMdiChild || !SeciliGelecekId.HasValue) return;
            Tablo.RowFocus("Id",SeciliGelecekId);
        }

        private void ButonGizleGoster()
        {
            btnSec.Visibility = AktifPasifButonGoster ? BarItemVisibility.Never : IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;
            barEnter.Visibility = IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;
            barEnterAciklama.Visibility = IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;
            btnAktifPasifKartlar.Visibility = AktifPasifButonGoster ? BarItemVisibility.Always : !IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;

            ShowItems?.ForEach(x => x.Visibility = BarItemVisibility.Always);
            HideItems?.ForEach(x => x.Visibility = BarItemVisibility.Never);
        }
        protected virtual void SutunGizleGoster(){}
        private void SablonKaydet()
        {
            if (_formSablonKayitEdilecek)
                Name.FormSablonKaydet(Left, Top, Width, Height, WindowState);

            if (_tabloSablonKayitEdilecek)
                Tablo.TabloSablonKaydet(IsMdiChild ? Name + " Tablosu" : Name + " TablosuMDI");
        }
        private void SablonYukle()
        {
            if (IsMdiChild)
                Tablo.TabloSablonYukle(Name + " Tablosu");
            else
            {
                Name.FormSablonYukle(this);
                Tablo.TabloSablonYukle(Name + " TablosuMDI");
            }

        }
        protected internal void Yukle()
        {
            DegiskenleriDoldur();
            EventsLoad();

            Tablo.OptionsSelection.MultiSelect = MultiSelect;
            Navigator.NavigatableControl = Tablo.GridControl; //ilgili tablonun navigatörü atandı.
            Cursor.Current = Cursors.WaitCursor;
            Listele();
            Cursor.Current = DefaultCursor;

            //Güncellenecek.

        }
        protected virtual void BelgeHareketleri() { }
        protected virtual void DegiskenleriDoldur() { }

        protected virtual void ShowEditForm(long id)
        {
            var result = FormShow.ShowDialogEditForm(kartTuru, id);
            ShowEditFormDefault(result);
        }

        protected void ShowEditFormDefault(long id)
        {
            if (id <= 0) return;
            AktifKartlariGoster = true;
            FormCaptionAyarla();
            Tablo.RowFocus("Id", id);
        }
        protected virtual void EntityDelete()
        {
            var entity = Tablo.GetRow<BaseEntity>();
            if (entity == null) return;
            if (!((IBaseCommonBll)Bll).Delete(entity)) return;

            Tablo.DeleteSelectedRows();
            Tablo.RowFocus(Tablo.FocusedRowHandle);
        }
        protected virtual void SelectEntity()
        {
            if(MultiSelect)
            {
                SelectedEntities = new List<BaseEntity>();
                if(RowSelect.SelectedRowCount==0)
                {
                    Messages.KartSecmemeUyariMesaji();
                    return;
                }
                SelectedEntities = RowSelect.GetSelectedRows();
            }
            else
            {
                SelectedEntity = Tablo.GetRow<BaseEntity>();
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        protected virtual void Listele(){}
        private void FiltreSec()
        {
            var entity = (Filtre)ShowListForms<FiltreListForm>.ShowDialogListForm(KartTuru.Filtre, _filtreId, kartTuru, Tablo.GridControl);
            if (entity == null) return;

            _filtreId = entity.Id;
            Tablo.ActiveFilterString = entity.FiltreMetni;
        }
        protected virtual void Yazdir()
        {
            TablePrintingFunctions.Yazdir(Tablo, Tablo.ViewCaption, AnaForm.SubeAdi);
        }
        private void FormCaptionAyarla()
        {
            if (btnAktifPasifKartlar == null)
            {
                Listele();
                return;
            }
            if (AktifKartlariGoster == true)
            {
                btnAktifPasifKartlar.Caption = "Pasif Kartlar";
                Tablo.ViewCaption = Text;
            }
            else
            {
                btnAktifPasifKartlar.Caption = "Aktif Kartlar";
                Tablo.ViewCaption = Text+" - Pasif Kartlar";
            }
            Listele();
        }
        private void IslemTuruSec()
        {
            if (!IsMdiChild)
            {
                //Güncellenecek.
                SelectEntity();
            }
            else
                btnDuzelt.PerformClick();
        }
        protected virtual void Button_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (e.Item == btnGonder)
            {
                var link = (BarSubItemLink)e.Item.Links[0];
                link.Focus();
                link.OpenMenu();
                link.Item.ItemLinks[0].Focus();
            }
            else if (e.Item == btnStandartExcelDosyasi)
                Tablo.TabloDisariAktar(DosyaTuru.ExcelStandart, e.Item.Caption, Text);
            else if (e.Item == btnFormatliExcelDosyasi)
                Tablo.TabloDisariAktar(DosyaTuru.ExcelFormatli, e.Item.Caption, Text);
            else if (e.Item == btnFormatsizExcelDosyasi)
                Tablo.TabloDisariAktar(DosyaTuru.ExcelFormatsiz, e.Item.Caption);
            else if (e.Item == btnWordDosyasi)
                Tablo.TabloDisariAktar(DosyaTuru.WordDosyasi, e.Item.Caption);
            else if (e.Item == btnPdfDosyasi)
                Tablo.TabloDisariAktar(DosyaTuru.PdfDosyasi, e.Item.Caption);
            else if (e.Item == btnTxtDosyasi)
                Tablo.TabloDisariAktar(DosyaTuru.TxtDosyasi, e.Item.Caption);
            else if (e.Item == btnYeni)
            {
                //Yetki Kontrolü Yapılacak.
                ShowEditForm(-1);
            }
            else if (e.Item == btnDuzelt)
                ShowEditForm(Tablo.GetRowId());

            else if (e.Item == btnSil)
            {
                //yetki kontrolü
                EntityDelete();
            }

            else if (e.Item == btnSec)
                SelectEntity();

            else if (e.Item == btnYenile)
                Listele();

            else if (e.Item == btnFiltrele)
                FiltreSec();

            else if (e.Item == btnKolonlar)
            {
                if (Tablo.CustomizationForm == null)
                    Tablo.ShowCustomization();
                else
                    Tablo.HideCustomization();
            }
            else if (e.Item == btnBelgeHareketleri)
                BelgeHareketleri();
            else if (e.Item == btnTahakkukYap)
            {
                TahakkukYap();
            }
            else if (e.Item == btnBagliKartlar)
            {
                BagliKartAc();
            }

            else if (e.Item == btnYazdir)
                Yazdir();

            else if (e.Item == btnCikis)
                Close();
            else if (e.Item == btnAktifPasifKartlar)
            {
                AktifKartlariGoster = !AktifKartlariGoster;

                FormCaptionAyarla();
            }
            Cursor.Current = DefaultCursor;
        }

        protected virtual void TahakkukYap(){ }

        protected virtual void BagliKartAc(){}

        private void Tablo_DoubleClick(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            IslemTuruSec();
            Cursor.Current = DefaultCursor;
        }    
        private void Tablo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    IslemTuruSec();
                    break;
                case Keys.Escape:
                    Close();                    
                    break;
            }
        }
    }
}