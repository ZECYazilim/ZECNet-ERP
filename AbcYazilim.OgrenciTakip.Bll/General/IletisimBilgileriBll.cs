﻿using AbcYazilim.OgrenciTakip.Bll.Base;
using AbcYazilim.OgrenciTakip.Bll.Interfaces;
using AbcYazilim.OgrenciTakip.Data.Contexts;
using AbcYazilim.OgrenciTakip.Model.Dto;
using AbcYazilim.OgrenciTakip.Model.Entities;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AbcYazilim.OgrenciTakip.Bll.General
{
    public class IletisimBilgileriBll : BaseHareketBll<IletisimBilgileri, OgrenciTakipContext>, IBaseHareketSelectBll<IletisimBilgileri>
    {
        public IEnumerable<BaseHareketEntity> List(Expression<Func<IletisimBilgileri, bool>> filter)
        {
            return List(filter, x => new IletisimBilgileriL
            {
                Id = x.Id,
                TahakkukId = x.TahakkukId,
                IletisimId=x.IletisimId,
                TcKimlikNo=x.Iletisim.TcKimlikNo,
                Adi=x.Iletisim.Adi,
                Soyadi=x.Iletisim.Soyadi,
                EvTel=x.Iletisim.EvTel,
                IsTel1=x.Iletisim.IsTel1,
                IsTel2=x.Iletisim.IsTel2,
                CepTel1=x.Iletisim.CepTel1,
                CepTel2=x.Iletisim.CepTel2,
                EvAdres=x.Iletisim.EvAdres,
                EvAdresIlAdi=x.Iletisim.EvAdresIl.IlAdi,
                EvAdresIlceAdi=x.Iletisim.EvAdresIlce.IlceAdi,
                IsAdres=x.Iletisim.IsAdres,
                IsAdresIlAdi=x.Iletisim.IsAdresIl.IlAdi,
                IsAdresIlceAdi=x.Iletisim.IsAdresIlce.IlceAdi,
                YakinlikId=x.YakinlikId,
                YakinlikAdi=x.Yakinlik.YakinlikAdi,
                MeslekAdi=x.Iletisim.Meslek.MeslekAdi,
                IsyeriAdi=x.Iletisim.Isyeri.IsYeriAdi,
                GorevAdi=x.Iletisim.Gorev.GorevAdi,
                Veli=x.Veli,
                FaturaAdresi=x.FaturaAdresi
            }).ToList();
        }
    }
}
