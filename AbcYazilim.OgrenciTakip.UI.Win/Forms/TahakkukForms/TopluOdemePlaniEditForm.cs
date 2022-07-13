﻿using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Common.Functions;
using AbcYazilim.OgrenciTakip.Common.Message;
using AbcYazilim.OgrenciTakip.Model.Dto;
using AbcYazilim.OgrenciTakip.UI.Win.Forms.BaseForms;
using AbcYazilim.OgrenciTakip.UI.Win.Functions;
using AbcYazilim.OgrenciTakip.UI.Win.GeneralForms;
using AbcYazilim.OgrenciTakip.UI.Win.UserControls.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Windows.Forms;

namespace AbcYazilim.OgrenciTakip.UI.Win.Forms.TahakkukForms
{
    public partial class TopluOdemePlaniEditForm : BaseEditForm
    {
        #region Variables
        private OdemeTipi _odemeTipi;
        private byte _blokeGunSayisi;

        private readonly IList _source;
        private readonly long _tahakkukId;
        private readonly decimal _bakiye;
        private readonly DateTime _kayitTarihi;
        private readonly int _dahaOnceGirilenTaksitSayisi;
        #endregion
        public TopluOdemePlaniEditForm(params object[] prm)//(IList source,long tahakkukId,decimal bakiye,DateTime kayitTarihi,int dahaOnceGirilenTaksitSayisi,bool maksimumTaksitSayisinaUlasildi)
        {
            InitializeComponent();

            _source = (IList)prm[0];
            _tahakkukId = (long)prm[1];
            _bakiye = (decimal)prm[2];
            _kayitTarihi = (DateTime)prm[3];
            _dahaOnceGirilenTaksitSayisi = (int)prm[4];

            DataLayoutControl = myDataLayoutControl;
            EventsLoad();
            ShowItems = new BarItem[] { btnTaksitOlustur};
            HideItems = new BarItem[] {btnYeni,btnGeriAl,btnKaydet,btnFarkliKaydet,btnSil};
        }
        protected internal override void Yukle()
        {
            ControlEnableChange(OdemeTipi.Acik);
            txtIlkTaksitTarihi.DateTime = _kayitTarihi;
            txtIlkTaksitTarihi.Properties.MinValue = _kayitTarihi;
            txtIlkTaksitTarihi.Properties.MaxValue = AnaForm.MaksimumTaksitTarihi;
            txtSabitTaksit.Value = 0;
            txtBakiye.Value = _bakiye;
            txtTaksitSayisi.Properties.MinValue = 1;
            txtTaksitSayisi.Properties.MaxValue = AnaForm.MaksimumTaksitSayisi-_dahaOnceGirilenTaksitSayisi;
            if (AnaForm.MaksimumTaksitSayisi - _dahaOnceGirilenTaksitSayisi > 0)
                ShowDialog();
            else
                Messages.HataMesaji("Maksimum Taksit Sayısı Aşılıyor.");
        }
        private void ControlEnableChange(OdemeTipi odemeTipi)
        {
            txtBankaHesap.Enabled = odemeTipi == OdemeTipi.Epos || odemeTipi == OdemeTipi.Ots || odemeTipi == OdemeTipi.Pos;
            txtAsilBorclu.Enabled = odemeTipi == OdemeTipi.Cek || odemeTipi == OdemeTipi.Senet;
            txtCiranta.Enabled = odemeTipi == OdemeTipi.Cek || odemeTipi == OdemeTipi.Senet;
            txtBanka.Enabled = odemeTipi == OdemeTipi.Cek;
            txtBankaSube.Enabled = odemeTipi == OdemeTipi.Cek;
            txtHesapNo.Enabled = odemeTipi == OdemeTipi.Cek;
            txtIlkBelgeNo.Enabled = odemeTipi == OdemeTipi.Cek;
            txtBanka.ControlEnabledChange(txtBankaSube);
        }

        private bool HataliGiris()
        {
            if (txtIlkTaksitTarihi.DateTime.Date.AddMonths((int)txtTaksitSayisi.Value-1) > AnaForm.MaksimumTaksitTarihi)
            {
                Messages.HataMesaji("Maximum Taksit Tarihi Aşılıyor.");
                return true;
            }
            if (txtOdemeTuru.Id == null)
            {
                Messages.HataMesaji("Ödeme Türü Seçimi Yapmalısınız.");
                txtOdemeTuru.Focus();
                return true;

            }
            if ((_odemeTipi == OdemeTipi.Epos || _odemeTipi == OdemeTipi.Ots || _odemeTipi == OdemeTipi.Pos) && txtBankaHesap.Id == null)
            {
                Messages.SecimHataMesaji("Banka Hesap");
                txtBankaHesap.Focus();
                return true;
            }
            if (txtSabitTaksit.Value == 0 && txtBakiye.Value == 0 || txtSabitTaksit.Value > 0 && txtBakiye.Value > 0)
            {
                Messages.HataMesaji("Sabit Taksit Veya Bakiye Alanlarından Sadece Birinin Değeri Sıfıra Eşit Veya Sıfırdan Büyük Olmalıdır.");
                txtSabitTaksit.Focus();
                return true;
            }
            if ((_odemeTipi == OdemeTipi.Senet || _odemeTipi == OdemeTipi.Cek) && string.IsNullOrEmpty(txtAsilBorclu.Text))
            {
                Messages.HataliVeriMesaji("Asıl Borçlu");
                txtAsilBorclu.Focus();
                return true;
            }
            switch (_odemeTipi)
            {
                case OdemeTipi.Cek when txtBanka.Id==null:
                    Messages.SecimHataMesaji("Banka");
                    txtBanka.Focus();
                    return true;

                case OdemeTipi.Cek when txtBankaSube.Id == null:
                    Messages.SecimHataMesaji("Banka Şube");
                    txtBankaSube.Focus();
                    return true;
                case OdemeTipi.Cek when txtIlkBelgeNo.Value==0:
                    Messages.HataliVeriMesaji("İlk Belge No");
                    txtIlkBelgeNo.Focus();
                    return true;


            }
            return false;
        }
        protected override void TaksitOlustur()
        {
            if (HataliGiris()) return;
            txtOdemeTuru.Focus();

            var tutar = txtSabitTaksit.Value != 0 ? txtSabitTaksit.Value : Math.Round(txtBakiye.Value / txtTaksitSayisi.Value, AnaForm.OdemePlaniKurusKullan ? 2 : 0);
            decimal toplamGirilenTutar = 0;


            for (int t = 0; t < txtTaksitSayisi.Value; t++)
            {
                var taksit = new OdemeBilgileriL
                {
                    Id = 0,
                    TahakkukId = _tahakkukId,
                    OdemeTipi = _odemeTipi,
                    OdemeTuruId = (long)txtOdemeTuru.Id,
                    OdemeTuruAdi = txtOdemeTuru.Text,
                    GirisTarihi = DateTime.Now.Date,
                    Vade = txtIlkTaksitTarihi.DateTime.Date.AddMonths(t),
                    HesabaGecisTarihi = txtIlkTaksitTarihi.DateTime.Date.AddMonths(t),
                    Tutar = t == txtTaksitSayisi.Value - 1 && txtSabitTaksit.Value == 0 ? txtBakiye.Value - toplamGirilenTutar : tutar,
                    BelgeDurumu = BelgeDurumu.Portfoyde,
                    Insert = true
                };
                taksit.TutarYazi = taksit.Tutar.YaziIleTutar();
                taksit.VadeYazi = taksit.Vade.YaziIleVade();
                taksit.Kalan = taksit.Tutar;
                toplamGirilenTutar += taksit.Tutar;

                switch (_odemeTipi)
                {
                    case OdemeTipi.Epos:
                    case OdemeTipi.Ots:
                    case OdemeTipi.Pos:
                        taksit.BankaHesapId = txtBankaHesap.Id;
                        taksit.BankaHesapAdi = txtBankaHesap.Text;
                        taksit.BlokeGunSayisi = _blokeGunSayisi;
                        taksit.HesabaGecisTarihi = taksit.Vade.Date.AddDays(_blokeGunSayisi);
                        break;
                    case OdemeTipi.Senet:
                        taksit.AsilBorclu = txtAsilBorclu.Text;
                        taksit.Ciranta = txtCiranta.Text;                     
                        break;
                    case OdemeTipi.Cek:
                        taksit.AsilBorclu = txtAsilBorclu.Text;
                        taksit.Ciranta = txtCiranta.Text;
                        taksit.BankaId = txtBanka.Id;
                        taksit.BankaAdi = txtBanka.Text;
                        taksit.BankaSubeId = txtBankaSube.Id;
                        taksit.BankaSubeAdi = txtBankaSube.Text;
                        taksit.HesapNo = txtHesapNo.Text;
                        taksit.BelgeNo = ((int)txtIlkBelgeNo.Value+t).ToString();
                        break;
                }
                _source.Add(taksit);
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        protected override void SecimYap(object sender)
        {
            if (!(sender is ButtonEdit)) return;
            using (var sec = new SelectFunctions())
            {
                if (sender == txtOdemeTuru)
                    sec.Sec(txtOdemeTuru);

                else if (sender == txtBankaHesap)
                    sec.Sec(txtBankaHesap, _odemeTipi);
                else if (sender == txtBanka)
                    sec.Sec(txtBanka);
                else if (sender == txtBankaSube)
                    sec.Sec(txtBankaSube,txtBanka);
            }             
        }
        protected override void Control_IdChanged(object sender, IdChangedEventArgs e)
        {
            if (sender == txtOdemeTuru)
            {
                _odemeTipi = txtOdemeTuru.Id == null ? OdemeTipi.Acik : txtOdemeTuru.Tag.ToString().GetEnum<OdemeTipi>();
                ControlEnableChange(_odemeTipi);
                txtBankaHesap.Id = null;
                txtBankaHesap.Text = null;
            }
            else if (sender == txtBankaHesap)
                _blokeGunSayisi = Convert.ToByte(txtBankaHesap.Tag);
            else if (sender == txtBanka)
            {
                txtBankaSube.Id = null;
                txtBankaSube.Text = null;
            }
        }
        protected override void Control_EnabledChange(object sender, EventArgs e)
        {
            if (sender != txtBanka) return;
            txtBanka.ControlEnabledChange(txtBankaSube);
        }
        protected override void BaseEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SablonKaydet();
            if (DialogResult != DialogResult.Cancel) return;

            if (Messages.HayirSeciliEvetHayir("Taksit Oluşturma Ekranı Kapatılacaktır. Onaylıyor Musunuz?", "Kapatma Onay") == DialogResult.No)
                e.Cancel = true;
        }
    }
}