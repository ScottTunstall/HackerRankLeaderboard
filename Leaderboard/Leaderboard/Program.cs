﻿//
// This code was written by Scott Tunstall (scott.tunstall@ntlworld.com)
//
// You are NOT permitted to use any of my code, especially any derivatives of my GetRank() function, to cheat on HackerRank tests. 
// This is my solution - write your own!
// I left the commented diagnostic code in, in case it helps you work out how I solved this.
//


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Leaderboard
{
    class Program
    {
        // Complete the climbingLeaderboard function below.
        static int[] climbingLeaderboard(int[] scores, int[] alice)
        {
            var scoresWithoutDuplicates = scores.Distinct()
                                            .OrderByDescending(x=>x)
                                            .ToList();

            var toBeReturned = new List<int>();

            for (int i = 0; i < alice.Length; i++)
            {
                var alicesScore = alice[i];
                var rank = GetRank(alicesScore, scoresWithoutDuplicates);             
                toBeReturned.Add(rank);
            }

            return toBeReturned.ToArray();
        }


        private static int GetRank(int score, IList<int> scores)
        {
            // Quick checks to save CPU time
            if (score >= scores.First())
                return 1;
            
            if (score < scores.Last())
                return scores.Count + 1; 

            int start = scores.Count / 2;
            int end = scores.Count;

            for (; ; )
            {

                if (score == scores[start])
                    return start + 1;

                if (score > scores[start])
                {
                    while (score > scores[start])
                    {
                        //Console.WriteLine("score " + score + "> " + scores[start]);

                        end = start;

                        //Console.WriteLine("score[end] = " + scores[end]);

                        start >>= 1;
                    }
                }

                if (score < scores[start])
                {
                    while (score < scores[start])
                    {
                        //Console.WriteLine("score " + score + "< " + scores[start]);

                        var lastStart = start;
                        start += (end - start) / 2;
                        if (start == lastStart)
                            break;
                    }
                }

                if (end-start ==1 )
                {
                    if (score < scores[start])
                        return start +2;

                    if (score >= scores[start])
                        return start + 1;
                }


                //Console.WriteLine("looking for score " + score + " in this subset: ");
                //for (int i = start; i < end; i++)
                //    Console.Write(scores[i] + " ");

                //Console.WriteLine("Press ENTER");
                //Console.ReadLine();
            }

            return start+1;
        }


        static void Main(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            //TextWriter textWriter = Console.Out;

            int scoresCount = Convert.ToInt32(Console.ReadLine());

            int[] scores = Array.ConvertAll(Console.ReadLine().Split(' '), scoresTemp => Convert.ToInt32(scoresTemp));
            int aliceCount = Convert.ToInt32(Console.ReadLine());

            int[] alice = Array.ConvertAll(Console.ReadLine().Split(' '), aliceTemp => Convert.ToInt32(aliceTemp));

            //var fakeInput = GetFakeInput();

            //int [] scores = Array.ConvertAll(fakeInput[1].Split(' '), scoresTemp => Convert.ToInt32(scoresTemp));
            //int[] alice = Array.ConvertAll(fakeInput[3].Split(' '), aliceTemp => Convert.ToInt32(aliceTemp));

            int[] result = climbingLeaderboard(scores, alice);

            textWriter.WriteLine(string.Join("\n", result));

            textWriter.Flush();
            textWriter.Close();
        }



        private static string[] GetFakeInput()
        {
            return new string[]
                {
                "1000", // number of scores
                "99898857 99889660 99869152 99837681 99736538 99703188 99673092 99438723 99188873 99182024 98941219 98847091 98834424 98552564 98476874 98398348 98383509 98265264 98219491 98191077 98165233 97983638 97981128 97789951 97774327 97754636 97636469 97569914 97540158 97504843 97398178 97148541 96820968 96759214 96380773 96371061 96364322 96289197 96165809 96139504 96134779 96082233 96017200 95906550 95757436 95540407 95534204 95388550 95337193 95235067 95160090 95087785 95065060 95024100 94965051 94845473 94782986 94768896 94242274 94191731 94172664 94163364 94089720 93955021 93774815 93718265 93624880 93556432 93527673 93518041 93486743 93383140 93329526 93261889 93243864 93239358 93132132 93073213 92871536 92735907 92359924 92187449 92185209 92022763 91984664 91911126 91768343 91580168 91006030 90966775 90918276 90865305 90782670 90764511 90588136 90567346 90550011 90539504 90366610 90247430 90163683 90039364 89964884 89770309 89648081 89632081 89394413 89215653 89200873 89043099 89020791 88899309 88765616 88681806 88681734 88664086 88636769 88458223 88320571 88310928 88200336 88147259 88111168 88060354 88051598 88003109 87851868 87680720 87632498 87484779 87463793 87413592 87390682 87347096 87198915 87084884 86981448 86877227 86837432 86730928 86665202 86558222 86479589 86437161 86368585 86313016 85936421 85852296 85758025 85753484 85501928 85383427 85328683 85297887 85277577 85275044 84714041 84644948 84573185 84484722 84483293 84470839 84468342 84445590 84408964 84375901 84351273 84319931 84296547 84250753 84165132 84164342 84156204 83832482 83644580 83494421 83266972 83229813 83153066 83068491 82958792 82529712 82203357 81718051 81642632 81581680 81342609 81294777 81235055 81172242 81106960 81042054 81038341 80883974 80883864 80375157 80205800 80096537 80073936 79989028 79833146 79756091 79591100 79525698 79462246 79392928 79376281 79349218 79346940 79345242 79230696 79102264 79092058 79081507 79029153 78662939 78236803 78213437 78002023 78001933 77899885 77755112 77647274 77531772 77427863 77390200 77380433 77026911 77026739 76992735 76992529 76807965 76803173 76781571 76606476 76500650 76437794 76319750 76298752 76084721 75955818 75934545 75876716 75856659 75836962 75833423 75445682 75289722 75213851 75191474 75147090 75009094 74860476 74854476 74762611 74660586 74651120 74497163 74480821 74416106 74373610 74324213 74211291 73781823 73707013 73701614 73672565 73497954 73441533 73036167 72892708 72889205 72585585 72416845 72303349 72160943 72137913 72119300 72078349 72003299 71941300 71892836 71773532 71632435 71521276 71468615 71466067 71300432 71245146 71081443 71009621 70903214 70776786 70481661 70450989 70442023 70335193 70090900 69985196 69963203 69892252 69704000 69501776 69241044 69132626 68949851 68909586 68901758 68804733 68438409 68403415 68388574 68256706 68233843 68226727 68220953 67718956 67626779 67570376 67476832 67156487 67001481 66950206 66920900 66886650 66879732 66870911 66827552 66699689 66551294 66460650 66415752 66321790 66199025 66130142 66001792 65953283 65759440 65674431 65651956 65612680 65599660 65502905 65395015 65162561 65041922 64897146 64735975 64682717 64537284 64534498 64372715 64331812 64175174 64119892 64036775 64010388 63865508 63765666 63718626 63683869 63670239 63452595 63436897 63278564 63277712 63231578 63158765 63088408 63087647 63080228 63005208 62799030 62756701 62752183 62723523 62637979 62396913 62369503 62311464 62280897 62226940 61942802 61872377 61744815 61725861 61662046 61649427 61374721 61372786 61331183 61285249 61270463 61219483 61030892 60965760 60891206 60880319 60863105 60785546 60775227 60653226 60633286 60618406 60353657 60319274 60279733 60264283 60244877 60051391 60034420 60030599 59976268 59941980 59880911 59843278 59787484 59294838 59268369 59060033 59017480 58809472 58616767 58576790 58480947 58206350 58152517 57845227 57799037 57655592 57574446 57461776 57434424 57399296 57388801 57280876 57240423 57196939 57142288 57073375 56995908 56972421 56871618 56513308 56330312 56319998 56229962 56147438 56060795 56014663 55959180 55950667 55721071 55591040 55481516 55465161 55048486 55035572 54952449 54907503 54859595 54548336 54425407 54295971 54020757 53985644 53853693 53654945 53627873 53591381 53525017 53324983 53314801 53297759 53126919 53084439 52954242 52753107 52702526 52523162 52491052 52371959 52322642 52299154 52287256 52152546 51911108 51790588 51787078 51777437 51616679 51615899 51394126 51283786 50936551 50935981 50723219 50587608 50484287 50136625 49905408 49769792 49736839 49496028 49409832 49338817 49147982 49123195 49119855 48904647 48881538 48816993 48768770 48700767 48582310 48571821 48562828 48458819 48410758 48355213 47962443 47728081 47561353 47553337 47472760 47467255 47330165 47295473 47205907 47078215 46886548 46434898 46307185 46239450 46234631 46130801 45983571 45621141 45593352 45544991 45519872 45420697 45389780 44971870 44865643 44832851 44686680 44595790 44471953 44379467 44346004 44243889 44039168 43965328 43883130 43759219 43533771 43481124 43393242 43348155 43240650 43231410 43217430 43083861 42928063 42877645 42782297 42486326 42459573 42410022 42402617 42389710 42290611 42217530 42115142 42036628 41964111 41944473 41857932 41570274 41514515 41501660 41482470 41299344 41281531 41237553 41057608 40853643 40838474 40788611 40464280 40435960 40367825 40235705 39944724 39821191 39758387 39733157 39712574 39621355 39466346 39423527 39411840 39269151 39169534 39157478 38923832 38850265 38815594 38656097 38575653 38430509 38318919 38233525 38012448 37864075 37808894 37749704 37672921 37658556 37634077 37614934 37475138 37389307 37346787 37238991 37233004 37227673 37157726 37132959 37131336 36951768 36746439 36618354 36565937 36515932 36494798 36088099 35973514 35940782 35872411 35855416 35677252 35587597 35576546 35033889 35022404 34914914 34804545 34582879 34501579 34478060 34246493 34236026 34136600 34095909 33873149 33851288 33840443 33651721 33616225 33486324 33409108 33371160 33081859 33026057 33019125 32957948 32776462 32672287 32647271 32620090 32597483 32564531 32559706 32525711 32256339 32222258 32183771 32178557 32151507 31541478 31500641 31383209 31296990 31221308 31129807 30894716 30880372 30814823 30624157 30538757 30201893 29889150 29883393 29840852 29831711 29752592 29688606 29633121 29456864 29406441 29307117 29183024 29085993 28970471 28710917 28562054 28534876 28421090 28360002 28345962 28291238 28287305 28273053 28206329 28141374 27790473 27728924 27560382 27502928 27493362 27432892 27428865 27407130 27381053 27239902 27165414 27016781 26968193 26953106 26544750 26515061 26464181 26437273 26045559 25994868 25883050 25866525 25843732 25646508 25643866 25622009 25451541 25433144 25093795 25060964 24900394 24824269 24652066 24627100 24505252 24467041 24327220 24284080 24268364 24245022 24184127 24165262 24097674 24094525 24027993 24023578 24019400 23869765 23704694 23632244 23452624 23373000 23152366 23100343 23013666 22882819 22820548 22819120 22812759 22644278 22637065 22438481 22347122 22314017 22275894 22271876 22239077 22135838 21928432 21867816 21731527 21615197 21571005 21548136 21153248 21138892 20953111 20943846 20913526 20911737 20820781 20779829 20765805 20677838 20498116 20227184 19951249 19795707 19635324 19576223 19475704 19457546 19401964 19347489 19320004 19299890 19283479 19243393 19200019 19175472 19016178 18960046 18901876 18887114 18836976 18801779 18636309 18570974 18208201 18164084 17795609 17460119 17390455 17161179 16835265 16718182 16632317 16613652 16384221 16381153 16229361 16169721 16050119 16022852 16021832 15785833 15660755 15588145 15439694 15416322 15250856 15206441 15192236 15143584 14952547 14947330 14871408 14856257 14784585 14751899 14620420 14455379 14440612 14418513 14214261 14122792 14117795 14102138 13855944 13796399 13789045 13408789 13256891 13094473 13066038 12964764 12956892 12758003 12704778 12586847 12027757 11953943 11920947 11822425 11580194 11267534 11007929 10777024 10625199 10563359 10545245 9988234 9932041 9877402 9458507 9442671 9369805 9284741 9234715 9168390 9162165 9040438 8768324 8602686 8546271 8526715 8519247 8415976 8361529 8342931 8303364 8291180 7848549 7847864 7823851 7773936 7699327 7602749 7540526 7385441 7366433 7337630 7272373 7187504 7175749 7023343 7016216 7010723 6889607 6863558 6591329 6531546 6426286 6407031 6365870 6311121 6221977 5801839 5747997 5730472 5724599 5424544 5354783 5296128 5295458 5238523 5149248 4850739 4791108 4763665 4750185 4719109 4526021 4494678 4449061 4362767 4237908 4213229 4207655 4169092 4138237 3938541 3790285 3766792 3754053 3632939 3617582 3608983 3521123 3330943 3279031 3227047 3094550 3025172 3009534 2943676 2904654 2809765 2551266 2428570 2301374 2254113 2231034 2228132 2208756 1810975 1702807 1698924 1668930 1654148 1536080 1477819 1328232 1000898 925673 874028 630208 524009 502548 387784 283346 59336 17498",
                "500", // number of alices scores
                "391846 801454 932294 992081 1432552 1433244 1436692 1463582 1703613 1762894 1794359 1800192 1894975 1913203 1964838 2005948 2047999 2383858 2684319 2743219 2836582 3152591 3238095 3318511 3402656 3496157 3506024 3597120 3638481 3642843 3670712 3726377 4150638 4554272 4802824 4857664 4898431 5012543 5280270 5364626 5676136 5876874 6022361 6028227 6245061 6328848 6580175 6589494 6791072 6972786 7219001 7219169 8449989 8535620 8608383 8655386 8825649 8850774 9058610 9100221 9119377 9234359 9366618 9950180 9994385 10435500 10550362 10745332 11153289 11278740 11299917 11662805 11693685 11887990 12148439 12230424 12286089 12335688 12589537 13116895 13456869 13573727 13668838 13780348 14028223 14141947 14354840 15166110 15718836 15951581 15983035 16179427 16217776 16461748 16690227 17136624 17586139 17746429 17962226 17976610 18107913 18422389 18642912 18661809 18695793 18926655 19190917 19235936 19387084 19436183 19439737 19706074 19865961 19934195 20219123 20253953 20679961 20979273 21159394 21334369 21367965 21407073 21482635 21644357 21705470 21820171 21899801 22233185 22312056 22448820 22575323 22576884 22832750 22925897 22988414 23392266 23470835 23493224 23511294 23769257 23872661 24347501 24751711 25926015 25976461 26190491 26499847 26522588 26776156 27014398 27100658 27113794 27289744 27413026 27423490 27542904 27575610 27841647 27891639 28860977 29184032 29300814 29323478 29470586 29475131 29536195 29675966 29798114 29945456 30678564 30798410 30909184 31111832 31716591 31971458 32354071 32380194 32686984 32735533 32742174 33232711 33263849 33359246 33450766 33742615 34244707 34408627 34527993 34578289 34917461 34954364 35086580 35124049 36047971 36158047 36278447 36483806 36511452 36594736 36988057 37098103 37355315 37828700 37886907 37979515 39008036 39178417 39267111 39683129 41081657 41110149 41230016 41409716 41474286 41764335 41840751 41915121 42066987 42136136 42186378 42200312 42565789 42614495 42828837 43070356 43241588 43571452 43630492 43751492 43821686 43841447 44213463 44392895 44626622 44631738 44772111 44791612 45091523 45122311 45627790 45650997 45819713 45821875 46533603 47032899 47230938 47565467 48211152 48637237 49409974 49466277 49807998 49944381 50055869 50944886 51075111 51236335 51489853 51922722 52068578 52531969 52827202 52902185 53188237 53418920 53844116 53967731 53970796 54241430 54608737 54638091 55341442 55362257 55669391 55670566 55756085 56770750 57045767 57361589 57736703 57751872 57793340 57876186 57920259 58518626 58729518 58760383 58818749 59052638 59169255 59212162 59262169 59380743 59385422 59511588 59786438 60094793 60356579 60661450 60909937 61221713 61457378 61751276 61769572 61890267 62028912 62193882 62218889 62547818 62815742 63026778 63172740 63189901 63213453 63264117 63403888 63432460 63503450 63792181 64004709 64112481 64361073 64431988 64636323 64840580 64977367 65367763 65378535 65512394 65955648 66272078 66288732 66483638 66613842 66677724 66783787 67223950 67350642 67519846 67586032 67938430 68129393 68417488 68615186 68701158 68715796 68754853 69017618 69089954 69490011 70259135 70285622 70311047 70413341 70616446 70668056 70748454 71123056 71255410 71569348 71632167 71667195 71820726 71910315 72211877 72406989 72718992 72751655 72880692 72900352 73209188 73572088 73618781 73825126 73867960 74044920 74051822 74100021 74175166 74191009 74341000 74397247 74547982 74852706 74855211 75107218 75274368 75470880 75471475 75714063 76004935 76134622 77212808 77395184 77502677 77819877 77861211 78180910 78603376 78656266 79600155 79875556 80080049 80245069 80611415 80657896 80764361 80839022 81511536 81843732 82619530 82650778 82709002 82758650 83028684 83047085 83098533 83229037 83645665 83679182 84078703 84079858 84179092 84349602 84394883 84461237 84633029 85032215 85082307 85282973 85584791 85937367 86031498 86404209 86505858 86511677 86537930 86628248 86691770 86872063 87217864 87533048 87663322 87821642 87850570 87998684 88050144 88089743 88325890 88328010 88333427 88365184 88476933 88606543 89192223 89439977 89522427 89698014 89958399 90090987 91015721 91075926 91122860 91534189 91754922 92051794 92129438 92537300 92548200 92693695 92718229 93219133 93601907 93629599 94057033 94146637 94554957 95337874 95516680 95733720 95758719 95776623 95792891 95877293 95895748 96196373 96246973 96318708 96537266 96699900 96822480 97539936 97749924 98041206 98094483 98252132 98294927 98412917 98572075 99944335"
                };
        }


    }
}
