﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jrascraping.Models
{
    public class RaceResults
    {
        public string Date { get; set; }
        public string NumberOfTime { get; set; }
        public string Racename { get; set; }
        public string Place { get; set; }
        public int Waku { get; set; }
        public int Num { get; set; }
        public HorseInfo Horse { get; set; }
        public string Weight { get; set; }
        public string Jockey { get; set; }
        public string Margin { get; set; }
        public string Time { get; set; }
        public string ArrivalDifference { get; set; }
        public string Corner { get; set; }
        public string HalongTime { get; set; }
        public string HorseWeight { get; set; }
        public string Trainer { get; set; }
        public int Pop { get; set; }

    }
}
