﻿using AbcYazilim.OgrenciTakip.Model.Entities;
using AbcYazilim.OgrenciTakip.Model.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbcYazilim.OgrenciTakip.Model.Dto
{
    [NotMapped]
    public class EvrakBilgileriL:EvrakBilgileri,IBaseHareketEntity
    {
        public string Kod { get; set; }
        public string EvrakAdi { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}