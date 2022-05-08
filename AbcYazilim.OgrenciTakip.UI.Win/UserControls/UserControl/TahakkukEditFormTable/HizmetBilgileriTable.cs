﻿using AbcYazilim.OgrenciTakip.Bll.Functions;
using AbcYazilim.OgrenciTakip.Bll.General;
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Model.Dto;
using AbcYazilim.OgrenciTakip.Model.Entities;
using AbcYazilim.OgrenciTakip.UI.Win.Forms.HizmetForms;
using AbcYazilim.OgrenciTakip.UI.Win.Forms.ServisForms;
using AbcYazilim.OgrenciTakip.UI.Win.Forms.TahakkukForms;
using AbcYazilim.OgrenciTakip.UI.Win.Functions;
using AbcYazilim.OgrenciTakip.UI.Win.GeneralForms;
using AbcYazilim.OgrenciTakip.UI.Win.Show;
using AbcYazilim.OgrenciTakip.UI.Win.UserControls.UserControl.Base;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AbcYazilim.OgrenciTakip.UI.Win.UserControls.UserControl.TahakkukEditFormTable
{
    public partial class HizmetBilgileriTable : BaseTablo
    {
        public HizmetBilgileriTable()
        {
            InitializeComponent();
            Bll = new HizmetBilgileriBll();
            Tablo = tablo;
            EventsLoad();
            ShowItems = new BarItem[] { btnIptalEt, btnIptalGeriAl };
        }
        protected override void Listele()
        {
            tablo.GridControl.DataSource = ((HizmetBilgileriBll)Bll).List(x => x.TahakkukId == OwnerForm.Id).ToBindingList<HizmetBilgileriL>();
        }
        protected override void HareketEkle()
        {
            var source = tablo.DataController.ListSource;
            ListeDisiTutulacakKayitlar = source.Cast<HizmetBilgileriL>().Where(x => !x.IptalEdildi && !x.Delete).Select(x => x.HizmetId).ToList();
            var entities = ShowListForms<HizmetListForm>.ShowDialogListForm(KartTuru.Hizmet, ListeDisiTutulacakKayitlar, true, false).EntityListConvert<HizmetL>();
            if (entities == null) return;
            foreach (var entity in entities)
            {
                Servis servis = null;
                if(entity.HizmetTipi==HizmetTipi.Servis)
                {
                    servis = (Servis)ShowListForms<ServisListForm>.ShowDialogListForm(KartTuru.Servis, -1);
                    if (servis == null) continue;
                }
                var row = new HizmetBilgileriL
                {

                    TahakkukId = OwnerForm.Id,
                    HizmetId=entity.Id,
                    HizmetAdi=entity.HizmetAdi,
                    HizmetTuruId=entity.HizmetTuruId,
                    HizmetTipi=entity.HizmetTipi,
                    IslemTarihi=DateTime.Now.Date,
                    BaslamaTarihi=entity.BaslamaTarihi,
                    BrutUcret=entity.Ucret,
                    KistDusulenUcret=0,
                    IptalEdildi=false,
                    Insert = true
                };
                if (servis != null)
                {
                    row.ServisId = servis.Id;
                    row.ServisYeriAdi = servis.ServisYeri;
                    row.BrutUcret = servis.Ucret;                
                }
                UcretHesapla(row);
                source.Add(row);
            }
            tablo.Focus();
            tablo.RefreshDataSource();
            tablo.FocusedRowHandle = tablo.DataRowCount - 1;
            insUptNavigator.Navigator.Buttons.DoClick(insUptNavigator.Navigator.Buttons.EndEdit);
            tablo.FocusedColumn = colHizmetAdi;
            ButtonEnabledDurumu(true);
        }

        private void UcretHesapla(HizmetBilgileriL entity)
        {
            var egitimBaslamaTarihi = AnaForm.EgitimBaslamaTarihi;
            var egitimBitisTarihi = AnaForm.DonemBitisTarihi;

            var toplamGunSayisi = (int)(egitimBitisTarihi - egitimBaslamaTarihi).TotalDays+1;
            var gunlukUcret = entity.BrutUcret / toplamGunSayisi;
            var alinanHizmetGunSayisi = entity.IptalTarihi==null ?(int)(egitimBitisTarihi - entity.BaslamaTarihi).TotalDays + 1:(int)(entity.IptalTarihi-entity.BaslamaTarihi).Value.TotalDays+1;
            var odenecekUcret = alinanHizmetGunSayisi > 0 ? gunlukUcret * alinanHizmetGunSayisi : 0;
            var kistDonemDusulenUcret = entity.BrutUcret - odenecekUcret;
            kistDonemDusulenUcret = Math.Round(kistDonemDusulenUcret, AnaForm.HizmetTahakkukKurusKullan ? 2 : 0);

            if (entity.BaslamaTarihi > egitimBaslamaTarihi || entity.IptalEdildi)
                entity.KistDusulenUcret = kistDonemDusulenUcret;
            else
                entity.KistDusulenUcret = 0;

            entity.NetUcret = entity.BrutUcret - entity.KistDusulenUcret;
            entity.EgitimDonemiGunSayisi = toplamGunSayisi;
            entity.AlinanHizmetGunSayisi = alinanHizmetGunSayisi;
            entity.GunlukUcret = gunlukUcret;
        }
        protected override void SutunGizleGoster()
        {
            if (tablo.DataRowCount == 0) return;
            var entity = tablo.GetRow<HizmetBilgileriL>();

            if (entity.HizmetTipi == HizmetTipi.Servis)
            {
                colServisYeriAdi.Visible = true;
                colServisYeriAdi.VisibleIndex = 1;
            }
            else
                colServisYeriAdi.Visible = false;
        }
        protected override void RowCellAllowEdit()
        {
            if (tablo.DataRowCount == 0) return;
            var entity = tablo.GetRow<HizmetBilgileriL>();

            colIptalTarihi.OptionsColumn.AllowEdit = entity.IptalEdildi;
            ColIptalNedeniAdi.OptionsColumn.AllowEdit = entity.IptalEdildi;
            colGittigiOkulAdi.OptionsColumn.AllowEdit = entity.IptalEdildi;
            colIptalAciklama.OptionsColumn.AllowEdit = entity.IptalEdildi;

            if (entity.HizmetTipi != HizmetTipi.Egitim)
                colGittigiOkulAdi.OptionsColumn.AllowEdit = false;
        }
        protected override void Tablo_MouseUp(object sender, MouseEventArgs e)
        {
            base.Tablo_MouseUp(sender, e);

            var entity = (HizmetBilgileriL)tablo.GetRow(tablo.FocusedRowHandle);
            if (entity == null) return;

            btnHareketSil.Enabled = !entity.IptalEdildi;
            btnIptalEt.Enabled = !entity.IptalEdildi && !entity.Insert;
            btnIptalGeriAl.Enabled = entity.IptalEdildi;
        }
        protected override void Tablo_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            base.Tablo_FocusedColumnChanged(sender, e);

            if (e.FocusedColumn == ColIptalNedeniAdi)
                e.FocusedColumn.Sec(Tablo, insUptNavigator.Navigator, repositoryIptalNedeni, colIptalNedeniId);
            else if (e.FocusedColumn == colGittigiOkulAdi)
                e.FocusedColumn.Sec(Tablo, insUptNavigator.Navigator, repositoryGittigiOkul, colGittigiOkulId);
            else if(e.FocusedColumn==colIptalTarihi)
            {
                var entity = tablo.GetRow<HizmetBilgileriL>();
                if (entity.IptalTarihi == null) return;

                repositoryIptalTarihi.MinValue = AnaForm.GunTarihininOncesineIptalTarihiGirilebilir ? entity.BaslamaTarihi : DateTime.Now.Date;
                repositoryIptalTarihi.MaxValue = AnaForm.GunTarihininSonrasinaIptalTarihiGirilebilir ? AnaForm.DonemBitisTarihi.AddDays(-1) : DateTime.Now.Date;
            }
        }
        protected override void Tablo_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            base.Tablo_CellValueChanged(sender, e);

            var entity = tablo.GetRow<HizmetBilgileriL>();

            if (e.Column == ColIptalNedeniAdi)
            {
                ((TahakkukEditForm)OwnerForm).indirimBilgileriTable.Tablo.DataController.ListSource
                    .Cast<IndirimBilgileriL>()
                    .Where(x => x.IptalEdildi && x.HizmetHareketId == entity.Id)
                    .ForEach(x => x.IptalNedeniId = entity.IptalNedeniId);

                ((TahakkukEditForm)OwnerForm).indirimBilgileriTable.Tablo.DataController.ListSource
                    .Cast<IndirimBilgileriL>()
                    .Where(x => x.IptalEdildi && x.HizmetHareketId == entity.Id)
                    .ForEach(x => x.IptalNedeniAdi = entity.IptalNedeniAdi);
            }
            else if (e.Column == colIptalAciklama)
            {
                ((TahakkukEditForm)OwnerForm).indirimBilgileriTable.Tablo.DataController.ListSource
                    .Cast<IndirimBilgileriL>()
                    .Where(x => x.IptalEdildi && x.HizmetHareketId == entity.Id)
                    .ForEach(x => x.IptalAciklama = entity.IptalAciklama);
            }
            else if (e.Column == colIptalTarihi)
            {
                UcretHesapla(entity);

                ((TahakkukEditForm)OwnerForm).indirimBilgileriTable.Tablo.DataController.ListSource
                    .Cast<IndirimBilgileriL>()
                    .Where(x => x.IptalEdildi && x.HizmetHareketId == entity.Id)
                    .ForEach(x => x.IptalTarihi = entity.IptalTarihi);

                ((TahakkukEditForm)OwnerForm).indirimBilgileriTable.TopluIndirimHesapla();
            }
        }
    }
}