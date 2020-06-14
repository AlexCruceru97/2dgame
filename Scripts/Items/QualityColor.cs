using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Quality { Common, Rare, Epic, Legendary }
public static class QualityColor
{
    private static Dictionary<Quality, string> colors = new Dictionary<Quality, string>()
    {
        {Quality.Common,"#d6d6d6" },
        {Quality.Rare,"#0F12CD" },
        {Quality.Epic,"#CF079D" },
        {Quality.Legendary,"#F1B100"}
    };

    public static Dictionary<Quality, string> MyColors { get => colors;  }
}

