﻿using jrascraping.Models;
using jrascraping.Regexs;
using Microsoft.EntityFrameworkCore;

namespace jrascraping.Query
{
    public class PayBackQuery
    {
        private static JraDbContext context;
        public static void DbContext()
        {
            //初期化
            var options = new DbContextOptionsBuilder<JraDbContext>();
            options.UseSqlite("Data Source=Jra.db");
            context = new JraDbContext(options.Options);
        }
        public PayBack CreatePayBack(string html)
        {
            DbContext();
            var regex = new PayBackCname();
            var win = regex.win.Matches(html);
            var wideBefore = regex.wideBefore.Matches(html);
            var wideAfter = regex.wideAfter.Matches(html);
            var tripleBefor = regex.tripleBefor.Matches(html);
            var tripleCenter = regex.tripleCenter.Matches(html);
            var tripleAfter = regex.tripleAfter.Matches(html);
            var refund = regex.refund.Matches(html);

            var payback = new PayBack();
            var count = win.Count + wideBefore.Count + wideAfter.Count + tripleBefor.Count + tripleCenter.Count + tripleAfter.Count;
            if (count == 22)
            {
                payback.TanshoNum = int.Parse(win[0].Value);
                payback.Fuku1Num = int.Parse(win[1].Value);
                payback.Fuku2Num = int.Parse(win[2].Value);
                payback.Fuku3Num = int.Parse(win[3].Value);
                payback.Wakuren1Waku = int.Parse(wideBefore[0].Value);
                payback.Wakuren2Waku = int.Parse(wideAfter[0].Value);
                payback.Wide1_1Num = int.Parse(wideBefore[1].Value);
                payback.Wide1_2Num = int.Parse(wideAfter[1].Value);
                payback.Wide2_1Num = int.Parse(wideBefore[2].Value);
                payback.Wide2_2Num = int.Parse(wideAfter[2].Value);
                payback.Wide3_1Num = int.Parse(wideBefore[3].Value);
                payback.Wide3_2Num = int.Parse(wideAfter[3].Value);
                payback.Umaren1Num = int.Parse(wideBefore[4].Value);
                payback.Umaren2Num = int.Parse(wideAfter[4].Value);
                payback.Umatan1Num = int.Parse(wideBefore[5].Value);
                payback.Umatan2Num = int.Parse(wideAfter[5].Value);
                payback.Sanfuku1Num = int.Parse(tripleBefor[0].Value);
                payback.Sanfuku2Num = int.Parse(tripleCenter[0].Value);
                payback.Sanfuku3Num = int.Parse(tripleAfter[0].Value);
                payback.Santan1Num = int.Parse(tripleBefor[1].Value);
                payback.Santan2Num = int.Parse(tripleCenter[1].Value);
                payback.Santan3Num = int.Parse(tripleAfter[1].Value);
            }
            else
            {
                //payback.TanshoNum = int.Parse(win[0].Value);
                //payback.Wide1_1Num = int.Parse(widebefore[1].Value);
                //payback.Wide1_2Num = int.Parse(wideafter[1].Value);
                //payback.Wide2_1Num = int.Parse(widebefore[2].Value);
                //payback.Wide2_2Num = int.Parse(wideafter[2].Value);
                //payback.Wide3_1Num = int.Parse(widebefore[3].Value);
                //payback.Wide3_2Num = int.Parse(wideafter[3].Value);
                //payback.Umaren1Num = int.Parse(widebefore[4].Value);
                //payback.Umaren2Num = int.Parse(wideafter[4].Value);
                //payback.Umatan1Num = int.Parse(widebefore[5].Value);
                //payback.Umatan2Num = int.Parse(wideafter[5].Value);
                //payback.Sanfuku1Num = int.Parse(triplebefor[0].Value);
                //payback.Sanfuku2Num = int.Parse(triplecenter[0].Value);
                //payback.Sanfuku3Num = int.Parse(tripleafter[0].Value);
                //payback.Santan1Num = int.Parse(triplebefor[1].Value);
                //payback.Santan2Num = int.Parse(triplecenter[1].Value);
                //payback.Santan3Num = int.Parse(tripleafter[1].Value);
            }

            if (refund.Count == 12)
            {
                payback.TanshoRe = int.Parse(refund[0].Value, System.Globalization.NumberStyles.AllowThousands);
                payback.Fuku1Re = int.Parse(refund[1].Value, System.Globalization.NumberStyles.AllowThousands);
                payback.Fuku2Re = int.Parse(refund[2].Value, System.Globalization.NumberStyles.AllowThousands);
                payback.Fuku3Re = int.Parse(refund[3].Value, System.Globalization.NumberStyles.AllowThousands);
                payback.WakurenRe = int.Parse(refund[4].Value, System.Globalization.NumberStyles.AllowThousands);
                payback.Wide1Re = int.Parse(refund[5].Value, System.Globalization.NumberStyles.AllowThousands);
                payback.Wide2Re = int.Parse(refund[6].Value, System.Globalization.NumberStyles.AllowThousands);
                payback.Wide3Re = int.Parse(refund[7].Value, System.Globalization.NumberStyles.AllowThousands);
                payback.UmarenRe = int.Parse(refund[8].Value, System.Globalization.NumberStyles.AllowThousands);
                payback.UmatanRe = int.Parse(refund[9].Value, System.Globalization.NumberStyles.AllowThousands);
                payback.SanfukuRe = int.Parse(refund[10].Value, System.Globalization.NumberStyles.AllowThousands);
                payback.SantanRe = int.Parse(refund[11].Value, System.Globalization.NumberStyles.AllowThousands);
            }
            else
            {
                //payback.TanshoRe = int.Parse(refund[0].Value, System.Globalization.NumberStyles.AllowThousands);
                //payback.Wide1Re = int.Parse(refund[5].Value, System.Globalization.NumberStyles.AllowThousands);
                //payback.Wide2Re = int.Parse(refund[6].Value, System.Globalization.NumberStyles.AllowThousands);
                //payback.Wide3Re = int.Parse(refund[7].Value, System.Globalization.NumberStyles.AllowThousands);
                //payback.UmarenRe = int.Parse(refund[8].Value, System.Globalization.NumberStyles.AllowThousands);
                //payback.UmatanRe = int.Parse(refund[9].Value, System.Globalization.NumberStyles.AllowThousands);
                //payback.SanfukuRe = int.Parse(refund[10].Value, System.Globalization.NumberStyles.AllowThousands);
                //payback.SantanRe = int.Parse(refund[11].Value, System.Globalization.NumberStyles.AllowThousands);
            }
            context.PayBack.Add(payback);
            return payback;
        }
    }
}
