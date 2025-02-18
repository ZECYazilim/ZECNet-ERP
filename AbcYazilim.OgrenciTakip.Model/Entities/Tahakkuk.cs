﻿using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Model.Attributes;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbcYazilim.OgrenciTakip.Model.Entities
{
    public class Tahakkuk:BaseEntityDurum
    {
        [Index("IX_Kod", IsUnique = false), ZorunluAlan("Öğrenci No", "txtKod")]
        public override string Kod { get; set; }
        public long OgrenciId { get; set; }

        [StringLength(20)]
        public string OkulNo { get; set; }

        [Column(TypeName ="date")]
        public DateTime KayitTarihi { get; set; } = DateTime.Now.Date;
        public KayitSekli KayitSekli { get; set; } = KayitSekli.YeniKayit;
        public KayitDurumu KayitDurumu { get; set; } = KayitDurumu.KesinKayit;

        [ZorunluAlan("Sınıf Adı", "txtSinif")]
        public long SinifId { get; set; }

        public long? GeldigiOkulId { get; set; }
        public long? KontenjanId { get; set; }
        public long? YabanciDilId { get; set; }
        public long? RehberId { get; set; }
        public long? TesvikId { get; set; }
        public SonrakiDonemKayitDurumu SonrakiDonemKayitDurumu { get; set; } = SonrakiDonemKayitDurumu.KayitYenileyecek;

        [StringLength(500)]
        public string SonrakiDonemKayitDurumuAciklama { get; set; }

        public long? OzelKod1Id { get; set; }
        public long? OzelKod2Id { get; set; }
        public long? OzelKod3Id { get; set; }
        public long? OzelKod4Id { get; set; }
        public long? OzelKod5Id { get; set; }
        public long SubeId { get; set; }
        public long DonemId { get; set; }
        public Ogrenci Ogrenci { get; set; }
        public Sinif Sinif { get; set; }
        public Okul GeldigiOkul { get; set; }
        public Kontenjan Kontenjan { get; set; }
        public YabanciDil YabanciDil { get; set; }
        public Rehber Rehber { get; set; }
        public Tesvik Tesvik { get; set; }
        public OzelKod OzelKod1 { get; set; }
        public OzelKod OzelKod2 { get; set; }
        public OzelKod OzelKod3 { get; set; }
        public OzelKod OzelKod4 { get; set; }
        public OzelKod OzelKod5 { get; set; }
        public Sube Sube { get; set; }
        public Donem Donem { get; set; }

    }
}
