using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace jrascraping
{
    public class Jra
    {
        static void Main()
        {
            //���[�X���ʂ̃g�b�v�y�[�W��html���擾����
            string html = GetHtml("pw01sli00/AF");
            //�擾�������[�X���ʂ�TOP�y�[�W����A���[�X��:cname�̑g�ݍ��킹�𐳋K�\���Ŏ擾���e�[�u���Ɋi�[����
            Dictionary<string, string> table = ParseRaceLinkTable(html);

            //SQLite�̃t�@�C����I��
            string db_file = "JRA.db";
            using SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file);

            //SQLite��Insert
            conn.Open();
            using SQLiteTransaction trans = conn.BeginTransaction();
            SQLiteCommand cmd = conn.CreateCommand();

            foreach (var pair in table)
            {
                // ���K�\���Ŏ擾����cname���g�p���āA�ʂ̃��[�X���ʂ�html�𓾂�
                var raceResultHtml = GetHtml(pair.Value);

                //Insert to CnameTable
                cmd.CommandText = "INSERT INTO CnameTable (racename, cname) VALUES (@racename, @cname)";
                cmd.Parameters.Add("racename", System.Data.DbType.String);
                cmd.Parameters.Add("cname", System.Data.DbType.String);
                cmd.Parameters["racename"].Value = pair.Key;
                cmd.Parameters["cname"].Value = pair.Value;
                cmd.ExecuteNonQuery();

                //����
                Regex RegexPlace = new Regex("(?<=<td class=\"place\">).*?(?=</td>)",
                   RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
                var MatchesPlace = RegexPlace.Matches(raceResultHtml);
                foreach (Match place in MatchesPlace)
                {
                    Debug.WriteLine(place);
                    cmd.CommandText = "INSERT INTO RaceResults (place,racename) VALUES (@place,@racename)";
                    cmd.Parameters.Add("place", System.Data.DbType.String);
                    cmd.Parameters["place"].Value = place;
                    cmd.ExecuteNonQuery();
                }
                //�g
                Regex RegexWaku = new Regex("(?<=<td class=\"waku\"><img src=\"/JRADB/img/waku/).*?(?=.png\" alt=\")",
                   RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
                var MatchesWaku = RegexWaku.Matches(raceResultHtml);
                foreach (Match waku in MatchesWaku)
                {
                    Debug.WriteLine(waku);
                    cmd.CommandText = "INSERT INTO RaceResults (place,racename) VALUES (@place,@racename)";
                    cmd.Parameters.Add("place", System.Data.DbType.String);
                    cmd.Parameters["place"].Value = waku;
                    cmd.ExecuteNonQuery();
                }
                //�n��
                Regex RegexNum = new Regex("(?<=<td class=\"num\">).*?(?=</td>)",
                   RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
                var MatchesNum = RegexNum.Matches(raceResultHtml);
                foreach (Match num in MatchesNum)
                {
                    //Debug.WriteLine(num);
                }

                //�n��
                Regex RegexHorse = new Regex("(?<=\\('/JRADB/accessU.html','pw.{20,20}'\\);\">).*?(?=</a>)",
                   RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
                var MatchesHorse = RegexHorse.Matches(raceResultHtml).Cast<Match>().ToList();
                for (int i = 0; i < MatchesPlace.Count; i++)
                {
                    cmd.Parameters.Add("horse", System.Data.DbType.String);
                    cmd.Parameters["horse"].Value = MatchesHorse[i].Value;
                    cmd.CommandText = "update RaceResults set horse=@horse where racename = '" + pair.Key + "' and place= " + (i + 1);
                    cmd.ExecuteNonQuery();
                }
                cmd.ExecuteNonQuery();
                //����
                Regex RegexAge = new Regex("(?<=<td class=\"age\">).*?(?=</td>)",
                   RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
                var MatchesAge = RegexAge.Matches(raceResultHtml);
                foreach (Match Age in MatchesAge)
                {
                    //Debug.WriteLine(Age);
                }
                //���S�d��
                Regex RegexWeight = new Regex("(?<=<td class=\"weight\">).*?(?=</td>)",
                   RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
                var MatchesWeight = RegexWeight.Matches(raceResultHtml);
                foreach (Match Weight in MatchesWeight)
                {
                    //Debug.WriteLine(Weight);
                }
                //�R��
                Regex RegexJockey = new Regex("(?<=\\('/JRADB/accessK.html','pw.{14,14}'\\);\">).*?(?=</a>)",
                   RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
                var MatchesJockey = RegexJockey.Matches(raceResultHtml);
                foreach (Match Jockey in MatchesJockey)
                {
                    //Debug.WriteLine(Jockey);
                }
                //�^�C��
                Regex RegexTime = new Regex("(?<=<td class=\"time\">).*?(?=</td>)",
                   RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
                var MatchesTime = RegexTime.Matches(raceResultHtml);
                foreach (Match Time in MatchesTime)
                {
                    //Debug.WriteLine(Time);
                }
                //������
                Regex RegexMargin = new Regex("(?<=<td class=\"margin\">).*?(?=</td>)",
                   RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
                var MatchesMargin = RegexMargin.Matches(raceResultHtml);
                foreach (Match Margin in MatchesMargin)
                {
                    //Debug.WriteLine(Margin);
                }
                //�R�[�i�[�A�ʉߏ��ʁ�

                //������
                Regex RegexFtime = new Regex("(?<=<td class=\"f_time\">).*?(?=</td>)",
                   RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
                var MatchesFtime = RegexFtime.Matches(raceResultHtml);
                foreach (Match Ftime in MatchesFtime)
                {
                    //Debug.WriteLine(Ftime);
                }
                //�n�̏d��

                //�����t
                Regex RegexTrainer = new Regex("(?<=\\('/JRADB/accessC.html','pw.{14,14}'\\);\">).*?(?=</a>)",
                   RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
                var MatchesTrainer = RegexTrainer.Matches(raceResultHtml);
                foreach (Match Trainer in MatchesTrainer)
                {
                    //Debug.WriteLine(Trainer);
                }
                //�P���l�C
                Regex RegexPop = new Regex("(?<=<td class=\"pop\">).*?(?=</td>)",
                   RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
                var MatchesPop = RegexPop.Matches(raceResultHtml);
                foreach (Match Pop in MatchesPop)
                {
                    //Debug.WriteLine(Pop);
                }
            }
            trans.Commit();
        }
        private static Dictionary<string, string> ParseRaceLinkTable(string html)
        {
            Dictionary<string, string> table = new Dictionary<string, string>();
            // ������html���p�[�X���ĉ��݂����Ȃ��̂𔲂��o���R�[�h�����
            // �v�f�� Regex
            //https://docs.microsoft.com/ja-jp/dotnet/standard/base-types/regular-expressions
            //https://www.mnet.ne.jp/~nakama/
            //https://ja.wikipedia.org/wiki/���K�\��
            // ���C�����[�X
            Regex regex = new Regex(
                "(?<cname>pw.{30,30})\\'\\);.*?\\</span\\>\\</span\\>(?<racename>.{0,40}?)\\<span class=\"grade_icon",
                RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var matches = regex.Matches(html);
            foreach (Match match in matches)
            {
                table.Add(match.Groups["racename"].Value, match.Groups["cname"].Value);
            }
            // ����ȊO
            regex = new Regex(
                "(?<cname>pw.{30,30})\\'\\);\\\">(?<racename>.{0,40}?)\\</a\\>\\<span class=\"grade_icon",
                RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            matches = regex.Matches(html);
            foreach (Match match in matches)
            {
                table.Add(match.Groups["racename"].Value, match.Groups["cname"].Value);
            }
            return table;
        }

        private static string GetHtml(string V)
        {
            //http���擾����
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var content = new FormUrlEncodedContent(
                        new Dictionary<string, string>
                        {
                            { "cname", V },
                        });
                    HttpResponseMessage response = client.PostAsync("http://www.jra.go.jp/JRADB/accessS.html", content).Result;
                    response.EnsureSuccessStatusCode();     //���URL���Ăяo������
                    System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    string responseBody = new StreamReader(response.Content.ReadAsStreamAsync().Result, Encoding.GetEncoding("shift_jis")).ReadToEnd();
                    return responseBody;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
            return string.Empty;
        }
    }
}