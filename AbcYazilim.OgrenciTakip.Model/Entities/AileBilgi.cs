﻿using AbcYazilim.OgrenciTakip.Model.Attributes;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbcYazilim.OgrenciTakip.Model.Entities
{
    public class AileBilgi:BaseEntityDurum
    {
        [Index("IX_Kod",IsUnique =true)]
        public override string Kod { get; set; }
        [Required,StringLength(50),ZorunluAlan("Bilgi Adı","txtBilgiAdi")]
        public string BilgiAdi { get; set; }
        [StringLength(500)]
        public string Aciklama { get; set; }
    }
}
