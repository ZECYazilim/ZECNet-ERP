﻿using System.ComponentModel;

namespace AbcYazilim.OgrenciTakip.Common.Enums
{
    public enum EposKartTuru:byte
    {
        [Description("Visa")]
        Visa=1,
        [Description("Master")]
        Master=2,
        [Description("American Express")]
        AmericanExpress=3
    }
}