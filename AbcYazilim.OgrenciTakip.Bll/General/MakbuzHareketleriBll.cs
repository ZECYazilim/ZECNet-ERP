﻿using AbcYazilim.OgrenciTakip.Bll.Base;
using AbcYazilim.OgrenciTakip.Bll.Interfaces;
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Data.Contexts;
using AbcYazilim.OgrenciTakip.Model.Dto;
using AbcYazilim.OgrenciTakip.Model.Entities;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AbcYazilim.OgrenciTakip.Bll.General
{
    public class MakbuzHareketleriBll : BaseHareketBll<MakbuzHareketleri, OgrenciTakipContext>, IBaseHareketSelectBll<MakbuzHareketleri>
    {
        public IEnumerable<BaseHareketEntity> List(Expression<Func<MakbuzHareketleri, bool>> filter)
        {
            return List(filter, x => new MakbuzHareketleriL // ref u 1 ->>> ttt
            {
                Id = x.Id,
                MakbuzId=x.MakbuzId,
                OgrenciNo=x.OdemeBilgileri.Tahakkuk.Ogrenci.Kod,
                Adi=x.OdemeBilgileri.Tahakkuk.Ogrenci.Adi,
                Soyadi=x.OdemeBilgileri.Tahakkuk.Ogrenci.Soyadi,
                SinifAdi=x.OdemeBilgileri.Tahakkuk.Sinif.SinifAdi,
                OgrenciSubeAdi=x.OdemeBilgileri.Tahakkuk.Sube.SubeAdi,
                OdemeBilgileriId=x.OdemeBilgileriId,
                OdemeTuruAdi=x.OdemeBilgileri.OdemeTuru.OdemeTuruAdi,
                OdemeTipi=x.OdemeBilgileri.OdemeTipi,
                TakipNo=x.OdemeBilgileri.TakipNo,
                GirisTarihi=x.OdemeBilgileri.GirisTarihi,
                Vade=x.OdemeBilgileri.Vade,
                HesabaGecisTarihi=x.OdemeBilgileri.HesabaGecisTarihi,
                Tutar=x.OdemeBilgileri.Tutar,
                IslemOncesiTutar=x.IslemOncesiTutar,
                IslemTutari=x.IslemTutari,
                KullaniciId=x.KullaniciId,
                EskiSubeId=x.EskiSubeId,
                YeniSubeId=x.YeniSubeId,
                BankaHesapAdi=x.OdemeBilgileri.BankaHesap.HesapAdi,
                BankaAdi=x.OdemeBilgileri.Banka.BankaAdi,
                BankaSubeAdi=x.OdemeBilgileri.BankaSube.SubeAdi,
                BelgeNo=x.OdemeBilgileri.BelgeNo,
                HesapNo=x.OdemeBilgileri.HesapNo,
                AsilBorclu=x.OdemeBilgileri.AsilBorclu,
                Ciranta=x.OdemeBilgileri.Ciranta,
                SonHareketId=x.OdemeBilgileri.MakbuzHareketleri.Where(y=>y.OdemeBilgileriId==x.OdemeBilgileri.Id).Max(y=>y.Id), //son hareket Id
                SonHareketTarihi=x.OdemeBilgileri.MakbuzHareketleri.Where(y=>y.OdemeBilgileriId==x.OdemeBilgileri.Id).Max(y=>y.Makbuz.Tarih), //son tarih
                BelgeDurumu=x.BelgeDurumu==0?BelgeDurumu.Portfoyde:x.BelgeDurumu,
                BelgeSubeAdi=x.OdemeBilgileri.MakbuzHareketleri.Where(y=>y.OdemeBilgileriId==x.OdemeBilgileri.Id).Max(y=>y.EskiSube.SubeAdi),
                SonIslemYeri=x.OdemeBilgileri.MakbuzHareketleri.Where(y=>y.OdemeBilgileriId==x.OdemeBilgileri.Id)
                .Max(y=>y.Makbuz.AvukatHesapId!=null?
                y.Makbuz.AvukatHesap.AdiSoyadi:
                y.Makbuz.BankaHesapId!=null?
                y.Makbuz.BankaHesap.HesapAdi:
                y.Makbuz.CariHesapId!=null?
                y.Makbuz.CariHesap.CariAdi:
                y.Makbuz.KasaHesapId!=null?
                y.Makbuz.KasaHesap.KasaAdi:
                y.Makbuz.SubeHesapId!=null?
                y.Makbuz.SubeHesap.SubeAdi:
                null),
            }).ToList();
        }
    }
}