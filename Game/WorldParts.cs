using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueRuby
{
    public static class WorldParts
    {
        public static (int WD, Point Location, Sector.TypeSector Type)[,] Level
        {
            get
            {
                int[,] LevelWD = new int[27, 22]
                {
                    { 40, 21, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 22, 40 },
                    { 21, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 22 },
                    { 11, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 13 },
                    { 11, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 13 },
                    { 11, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 13 },
                    { 11, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 13 },
                    { 11, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 13 },
                    { 20, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 23 },
                    { 40, 20, 10, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 10, 13, 40 },
                    { 40, 40, 40, 11, 10, 10, 10, 10, 10, 10, 10, 00, 10, 10, 10, 00, 00, 00, 23, 40, 25, 32 },
                    { 21, 12, 22, 25, 40, 40, 40, 40, 40, 40, 40, 25, 40, 40, 40, 20, 10, 23, 40, 40, 11, 13 },
                    { 11, 00, 00, 00, 12, 22, 40, 40, 21, 12, 12, 00, 12, 22, 40, 40, 40, 40, 40, 21, 00, 13 },
                    { 11, 00, 00, 00, 00, 13, 40, 40, 20, 10, 00, 00, 00, 00, 12, 33, 40, 40, 21, 00, 00, 13 },
                    { 11, 00, 00, 00, 10, 23, 40, 40, 40, 40, 20, 00, 10, 10, 23, 40, 40, 31, 10, 00, 00, 13 },
                    { 11, 00, 10, 23, 40, 40, 21, 12, 12, 22, 40, 25, 40, 40, 40, 40, 40, 40, 40, 20, 10, 23 },
                    { 11, 23, 40, 40, 40, 21, 00, 00, 00, 00, 12, 00, 12, 12, 12, 12, 12, 22, 40, 40, 40, 40 },
                    { 30, 40, 40, 21, 12, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 12, 12, 12, 22 },
                    { 40, 21, 12, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 13 },
                    { 21, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 13 },
                    { 11, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 13 },
                    { 11, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 13 },
                    { 11, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 13 },
                    { 11, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 13 },
                    { 11, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 13 },
                    { 11, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 13 },
                    { 20, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 23 },
                    { 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40 }
                };
                int[,] LevelType = new int[27, 22]
                {
                    { 6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6 },
                    { 6,  6,  4,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  3,  6,  6 },
                    { 6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6 },
                    { 6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6 },
                    { 6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6 },
                    { 6,  6,  6,  6,  6,  6,  6,  6,  6,  5,  5, 14,  5,  5,  6,  6,  6,  6,  6,  6,  6,  6 },
                    { 6,  6,  6,  6,  6,  6,  6,  6,  5,  5,  5,  5,  5,  5,  5,  5,  6,  6,  6,  6,  6,  6 },
                    { 7,  5,  5,  5,  5,  5,  5,  5,  5, 13,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  7 },
                    { 8,  7,  7,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  7,  5,  8 },
                    { 8,  8,  8,  5,  7,  7,  7,  7,  7,  7,  7,  5,  7,  7,  7,  5, 12,  5,  7,  8,  9,  7 },
                    { 7,  7,  7,  9,  8,  8,  8,  8,  8,  8,  8, 11,  8,  8,  8,  7,  7,  7,  8,  8,  5,  5 },
                    { 5,  5,  5,  5,  7,  7,  8,  8, 10,  7,  7,  5,  7,  7,  8,  8,  8,  8,  8,  7,  5,  5 },
                    { 5, 13,  5,  5,  5, 10,  8,  8,  7,  7,  5,  5,  5, 12,  7, 10,  8,  8, 10,  5, 13,  5 },
                    { 5,  5,  5, 12,  7,  7,  8,  8,  8,  8,  7,  5,  7,  7,  7,  8,  8,  7,  7,  5,  5,  5 },
                    { 5, 14,  7,  7,  8,  8,  7,  7,  7,  7,  8,  9,  8,  8,  8,  8,  8,  8,  8,  7,  7,  7 },
                    { 5,  7,  8,  8,  8,  7,  5,  5,  5,  5,  7,  5,  7,  7,  7,  7,  7,  7,  8,  8,  8,  8 },
                    { 7,  8,  8,  7,  7,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  7,  7,  7,  7 },
                    { 8,  7,  7,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5, 12,  5,  5,  5,  5,  5,  5 },
                    { 7,  5,  5,  5,  5,  5,  5, 12,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5, 13,  5,  5 },
                    { 5, 13,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5, 13,  5,  5,  5,  5,  5,  5,  5,  5,  5 },
                    { 5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5 },
                    { 5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  6,  6,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5 },
                    { 5,  5,  5,  5,  5,  5,  5,  5,  6,  6,  6,  6,  6,  6,  5,  5,  5,  5,  5,  5,  5,  5 },
                    { 5,  5,  5,  5,  5,  5,  5,  5,  6,  6,  6,  6,  6,  6,  6,  6,  5,  5,  5,  5,  5,  5 },
                    { 5,  1,  5,  5,  5,  5,  6,  6,  6,  6,  6, 14,  6,  6,  6,  6,  6,  5,  5,  5,  2,  5 },
                    { 5,  5,  5,  5,  5,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  6,  5,  5,  5,  5,  5 },
                    { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 15, 15, 15,  0,  0,  0,  0,  0,  0,  0,  0,  0 }
                };
                (int x, int y)[,] LevelLocat = new (int x, int y)[27, 22]//(5,53) - (669,870)
                {
                    {  (6,61),  (34,61),  (63,61),   (94,61),  (124,62),  (155,63),  (185,63),  (214,61),  (244,57),  (274,58),  (303,57),  (334,60),  (364,58),  (394,58),  (424,58),  (455,53),  (485,53),  (515,53),  (544,53),  (575,53),  (605,53),  (633,53) },
                    {  (6,91),  (34,91),  (63,90),   (95,90),  (124,90),  (155,90),  (185,90),  (215,89),  (244,89),  (275,88),  (304,87),  (335,87),  (364,87),  (395,86),  (425,86),  (455,86),  (485,86),  (515,85),  (545,85),  (575,85),  (605,84),  (633,84) },
                    {  (6,121), (34,121), (64,120),  (95,120), (125,119), (155,119), (185,119), (215,119), (244,118), (275,117), (304,117), (335,116), (364,116), (395,115), (425,115), (456,115), (485,114), (515,114), (545,114), (576,113), (606,113), (633,113) },
                    {  (6,151), (35,151), (64,151),  (95,151), (125,151), (155,150), (185,150), (215,150), (244,149), (276,148), (304,148), (335,147), (365,147), (395,147), (425,146), (456,146), (485,145), (516,145), (545,145), (576,144), (606,144), (634,144) },
                    {  (6,181), (35,181), (64,181),  (95,180), (125,180), (156,180), (185,180), (215,180), (245,179), (276,178), (305,177), (335,177), (365,177), (396,176), (426,176), (456,176), (485,175), (516,175), (546,174), (576,174), (606,173), (634,173) },
                    {  (6,212), (35,212), (65,212),  (96,211), (125,211), (156,211), (185,211), (216,210), (245,210), (277,209), (305,208), (336,208), (365,208), (396,207), (426,207), (457,206), (486,206), (516,206), (546,205), (577,205), (607,205), (635,204) },
                    {  (5,241), (35,241), (65,241),  (96,241), (125,240), (156,240), (186,240), (216,239), (245,238), (277,238), (306,237), (336,237), (366,237), (396,236), (426,236), (457,236), (486,235), (517,235), (546,234), (577,234), (607,234), (635,233) },
                    {  (6,272), (36,272), (65,272),  (96,272), (126,272), (156,271), (186,271), (216,271), (246,269), (277,269), (306,269), (336,269), (366,268), (397,267), (427,267), (457,267), (487,266), (517,266), (547,266), (578,265), (607,265), (635,264) },
                    {  (8,303), (36,303), (66,302),  (97,302), (126,301), (157,301), (186,301), (216,300), (246,300), (275,300), (305,298), (335,298), (365,298), (395,297), (425,297), (456,296), (485,296), (516,296), (546,296), (576,295), (606,295), (636,294) },
                    {  (8,333), (37,333), (67,333),  (98,332), (127,332), (157,332), (187,332), (217,332), (245,332), (276,330), (305,330), (335,330), (364,329), (395,328), (425,328), (456,328), (485,327), (516,326), (545,326), (576,326), (606,326), (635,326) },
                    {  (8,361), (37,361), (67,361),  (98,361), (128,361), (157,361), (187,361), (217,361), (246,361), (277,361), (306,360), (334,360), (365,360), (395,360), (426,358), (456,357), (485,357), (517,357), (545,356), (576,356), (606,355), (635,355) },
                    {  (8,392), (38,392), (67,391),  (98,391), (128,391), (158,391), (188,391), (217,391), (245,391), (276,391), (306,391), (336,391), (366,391), (396,391), (426,391), (457,391), (484,391), (517,391), (546,391), (577,385), (606,385), (636,385) },
                    {  (9,423), (38,422), (67,422),  (98,422), (128,421), (158,421), (188,421), (217,420), (245,420), (276,419), (306,419), (336,419), (366,418), (397,418), (427,418), (458,418), (485,417), (517,417), (547,416), (577,416), (607,415), (636,415) },
                    {  (9,453), (38,453), (68,452),  (99,452), (128,452), (158,451), (188,451), (217,450), (245,450), (276,450), (306,449), (337,449), (366,448), (397,448), (427,448), (458,448), (485,447), (517,447), (547,446), (578,446), (608,446), (637,445) },
                    { (10,484), (39,484), (68,484),  (99,484), (129,483), (159,482), (189,481), (218,480), (249,480), (277,480), (307,479), (337,479), (367,479), (398,479), (428,479), (459,479), (488,479), (519,479), (548,478), (578,478), (608,477), (637,477) },
                    { (11,514), (39,514), (69,514),  (99,514), (129,514), (160,513), (189,512), (219,512), (249,512), (277,511), (308,511), (338,511), (368,510), (398,510), (428,509), (459,508), (488,509), (519,509), (549,509), (579,509), (609,509), (638,509) },
                    { (11,544), (39,545), (69,545),  (99,545), (130,544), (160,544), (190,543), (219,543), (249,543), (278,542), (308,542), (338,541), (368,541), (399,540), (428,540), (459,540), (489,539), (520,539), (549,539), (579,539), (609,539), (639,539) },
                    { (10,575), (40,575), (69,575),  (99,574), (130,574), (160,574), (190,573), (220,573), (249,572), (278,571), (308,571), (338,571), (369,571), (399,570), (429,570), (460,570), (489,570), (520,569), (549,569), (580,568), (609,568), (639,568) },
                    { (10,605), (40,605), (69,604),  (99,604), (130,604), (160,603), (190,603), (220,603), (249,604), (279,603), (309,603), (339,603), (370,603), (400,603), (430,603), (460,603), (490,603), (521,602), (550,602), (581,602), (610,602), (640,602) },
                    { (10,635), (40,635), (70,634),  (99,634), (130,634), (160,633), (191,633), (221,633), (250,634), (279,633), (310,633), (340,633), (370,633), (400,633), (430,633), (460,633), (490,632), (521,632), (550,632), (581,632), (611,632), (640,632) },
                    { (10,664), (40,664), (70,664),  (99,664), (131,663), (160,663), (191,663), (221,663), (250,663), (280,663), (310,662), (340,662), (371,662), (400,662), (430,662), (461,662), (491,662), (521,662), (550,662), (581,661), (611,661), (641,661) },
                    { (12,694), (41,694), (70,693), (100,693), (131,693), (161,693), (192,692), (222,692), (251,693), (280,692), (310,692), (340,692), (371,692), (401,692), (431,692), (461,692), (491,692), (521,692), (551,692), (581,692), (611,691), (641,691) },
                    { (10,724), (41,724), (71,724), (100,724), (131,724), (161,723), (192,723), (222,723), (251,723), (280,723), (310,722), (340,722), (371,722), (401,722), (431,722), (461,722), (491,722), (522,722), (551,722), (582,722), (611,722), (641,722) },
                    { (10,755), (41,755), (71,754), (101,754), (132,753), (161,753), (193,753), (222,753), (252,753), (280,753), (310,752), (340,752), (371,752), (401,752), (431,752), (461,752), (491,752), (522,752), (551,752), (582,752), (611,752), (641,752) },
                    { (10,784), (42,784), (72,784), (101,784), (132,784), (162,783), (193,783), (223,782), (252,782), (280,782), (311,782), (340,783), (371,783), (402,782), (431,782), (461,782), (491,782), (522,782), (552,782), (582,782), (611,782), (642,781) },
                    { (10,814), (42,814), (72,814), (101,813), (132,813), (162,813), (193,812), (223,812), (252,812), (280,812), (311,811), (340,811), (372,812), (402,812), (431,812), (461,812), (491,812), (523,812), (552,811), (582,811), (612,811), (642,811) },
                    { (10,844), (42,844), (72,844), (101,843), (132,843), (162,843), (193,842), (223,842), (252,842), (280,842), (311,841), (340,841), (372,842), (402,842), (431,842), (461,842), (491,842), (523,842), (552,841), (582,841), (612,841), (642,841) }
                };
                var Level = new (int WD, Point Location, Sector.TypeSector Type)[27, 22];
                for (int i = 0; i < 27; i++)
                    for (int j = 0; j < 22; j++)
                        if (Enum.IsDefined(typeof(Sector.TypeSector), LevelType[i, j]))
                            Level[i, j] = (LevelWD[i, j], LevelLocat[i, j].ToPoint() - new Size(5, 53), (Sector.TypeSector)LevelType[i, j]);
                        else
                            throw new Exception($"Ошибка в названии типа [{i}, {j}]");
                return Level;
            }
        }
        public static Dictionary<Team, Point> UndergroundPosition => new Dictionary<Team, Point>()
        {
            { Team.Light, new Point(26, 10) },
            { Team.Dark, new Point(26, 12) },
            { Team.Wizard, new Point(26, 11) }
        };
        public static Point[][][] WaterPaths => new (int x, int y)[4][][]
        {
            new (int x, int y)[21][]
            {
                new (int x, int y)[] { (17, 0) },
                new (int x, int y)[] { (16, 1) },
                new (int x, int y)[] { (16, 2), (15, 2) },
                new (int x, int y)[] { (15, 3) },
                new (int x, int y)[] { (15, 4), (14, 4) },
                new (int x, int y)[] { (14, 5) },
                new (int x, int y)[] { (13, 6) },
                new (int x, int y)[] { (13, 7) },
                new (int x, int y)[] { (13, 8) },
                new (int x, int y)[] { (13, 9) },
                new (int x, int y)[] { (14, 10) },
                new (int x, int y)[] { (14, 12) },
                new (int x, int y)[] { (14, 13) },
                new (int x, int y)[] { (14, 14) },
                new (int x, int y)[] { (14, 15), (13, 15) },
                new (int x, int y)[] { (14, 16) },
                new (int x, int y)[] { (14, 17) },
                new (int x, int y)[] { (14, 18), (15, 18) },
                new (int x, int y)[] { (15, 19) },
                new (int x, int y)[] { (15, 20) },
                new (int x, int y)[] { (15, 21) },
            },
            new (int x, int y)[3][]
            {
                new (int x, int y)[] { (13, 7) },
                new (int x, int y)[] { (12, 7) },
                new (int x, int y)[] { (11, 6), (10, 6) },
            },
            new (int x, int y)[4][]
            {
                new (int x, int y)[] { (14, 15), (13, 15), (14, 16) },
                new (int x, int y)[] { (13, 16) },
                new (int x, int y)[] { (12, 17) },
                new (int x, int y)[] { (11, 17) },
            },
            new (int x, int y)[19][]
            {
                new (int x, int y)[] { (8, 0), (9, 0) },
                new (int x, int y)[] { (9, 1) },
                new (int x, int y)[] { (9, 2) },
                new (int x, int y)[] { (10, 4) },
                new (int x, int y)[] { (10, 5) },
                new (int x, int y)[] { (10, 6), (11, 6) },
                new (int x, int y)[] { (10, 7) },
                new (int x, int y)[] { (10, 8) },
                new (int x, int y)[] { (10, 9) },
                new (int x, int y)[] { (10, 10) },
                new (int x, int y)[] { (10, 12) },
                new (int x, int y)[] { (10, 13) },
                new (int x, int y)[] { (10, 14), (11, 14) },
                new (int x, int y)[] { (11, 15) },
                new (int x, int y)[] { (11, 16) },
                new (int x, int y)[] { (11, 17) },
                new (int x, int y)[] { (10, 18), (11, 18) },
                new (int x, int y)[] { (9, 19), (10, 19) },
                new (int x, int y)[] { (8, 21) },
            }
        }.Select(P1 => P1.Select(P2 => P2.Select(P3 => P3.ToPoint()).ToArray()).ToArray()).ToArray();
        private static (int Dir, int I)[][] WaterPathsCrossing => new (int Dir, int I)[][]
        {
            new (int Dir, int I)[2] { (0, 7), (1, 0) },
            new (int Dir, int I)[3] { (0, 15), (0, 16), (2, 0) },
            new (int Dir, int I)[2] { (3, 5), (1, 2) },
            new (int Dir, int I)[2] { (3, 15), (2, 3) },
        };
        private static int[] FillIndices(int index, int Dir, int I) =>
            Enumerable.Repeat(index, WaterPaths[Dir][I].Length).ToArray();
        public static Point[] GetWaterPath(Point Start)
        {
            return CreateWaterPath(Start, out _);
        }
        public static int[][][] GetWaterTable(Point Start)
        {
            CreateWaterPath(Start, out int[][][] WaterTable);
            return WaterTable;
        }
        private static Point[] CreateWaterPath(Point Start, out int[][][] WaterTable)
        {
            WaterTable = new int[4][][]
            {
                new int[21][],
                new int[3][],
                new int[4][],
                new int[19][]
            };
            List<(int Dir, int I)> cords = new List<(int Dir, int I)>();
            for (int d = 0; d < WaterPaths.Length; d++)
                for (int i = 0; i < WaterPaths[d].Length; i++)
                    for (int n = 0; n < WaterPaths[d][i].Length; n++)
                        if (WaterPaths[d][i][n] == Start)
                            cords.Add((d, i));
            foreach ((int Dir, int I) in cords)
            {
                WaterTable[Dir][I] = FillIndices(0, Dir, I);
                for (int k = 1; k <= 10; k++)
                {
                    if (I > k - 1)
                        WaterTable[Dir][I - k] = FillIndices(k, Dir, I - k);
                    if (I < WaterTable[Dir].Length - k)
                        WaterTable[Dir][I + k] = FillIndices(k, Dir, I + k);
                }
            }
            void DoubleOnotherDir(ref int[][][] WaterTable, int dir)
            {
                foreach (var Crds in WaterPathsCrossing)
                {
                    (int Dir, int I) fDIr = (-1, -1);
                    for (int c = 0; c < Crds.Length; c++)
                    {
                        (int Dir, int I) cross = Crds[c];
                        if (cross.Dir == dir && fDIr == (-1, -1))
                        {
                            fDIr = cross;
                            c = -1;
                        }
                        if (cross.Dir != fDIr.Dir && fDIr != (-1, -1) && WaterTable[fDIr.Dir][fDIr.I] != null)
                            WaterTable[cross.Dir][cross.I] = FillIndices(WaterTable[fDIr.Dir][fDIr.I][0], cross.Dir, cross.I);
                    }
                }
            }
            int DirNotFill(int[][][] WaterTable, out int index)
            {
                for (int d = 0; d < WaterTable.Length; d++)
                {
                    if (WaterTable[d].All(P => P == null))
                        continue;
                    bool HaveNull = false;
                    bool FillStarted = false;
                    for (int i = 0; i < WaterTable[d].Length; i++)
                    {
                        if (HaveNull && !FillStarted && WaterTable[d][i] != null && WaterTable[d][i][0] != 10)
                        {
                            index = i;
                            return d;
                        }
                        else if (!HaveNull && !FillStarted && WaterTable[d][i] == null)
                        {
                            HaveNull = true;
                            continue;
                        }
                        else if (i == 0 && WaterTable[d][i] != null)
                        {
                            FillStarted = true;
                            continue;
                        }
                        else if (HaveNull && WaterTable[d][i] != null && WaterTable[d][i][0] == 10)
                        {
                            FillStarted = true;
                            HaveNull = false;
                            continue;
                        }
                        else if (FillStarted && WaterTable[d][i] != null && WaterTable[d][i][0] == 10)
                        {
                            FillStarted = false;
                            continue;
                        }
                    }
                    if (FillStarted && WaterTable[d].Last() == null && WaterTable[d].Last(I => I != null)[0] != 10)
                    {
                        index = -1;
                        return d;
                    }
                }
                index = -1;
                return -1;
            }
            foreach ((int Dir, int I) in cords)
            {
                DoubleOnotherDir(ref WaterTable, Dir);
                int ind = -1;
                int d = DirNotFill(WaterTable, out ind);
                do
                {
                    if (ind == -1)
                        for (int m = 0; m < WaterTable[d].Length; m++)
                            if (WaterTable[d][m] != null)
                            {
                                ind = m;
                                break;
                            }
                    if (ind != -1)
                    {
                        int step = WaterTable[d][ind][0];
                        for (int k = step + 1; k <= 10; k++)
                        {
                            int realI = k - step;
                            if (ind > realI - 1 && (WaterTable[d][ind - realI] == null || WaterTable[d][ind - realI][0] > WaterTable[d][ind - realI + 1][0] + 1))
                                WaterTable[d][ind - realI] = FillIndices(WaterTable[d][ind - realI + 1][0] + 1, d, ind - realI);
                            if (ind < WaterTable[d].Length - realI && (WaterTable[d][ind + realI] == null || WaterTable[d][ind + realI][0] > WaterTable[d][ind + realI - 1][0] + 1))
                                WaterTable[d][ind + realI] = FillIndices(WaterTable[d][ind + realI - 1][0] + 1, d, ind + realI);
                        }
                        DoubleOnotherDir(ref WaterTable, d);
                        d = DirNotFill(WaterTable, out ind);
                    }
                } while (d != -1);
            }
            List<Point> path = new List<Point>();
            for (int d = 0; d < WaterTable.Length; d++)
                for (int i = 0; i < WaterTable[d].Length; i++)
                    if (WaterTable[d][i] != null && WaterTable[d][i][0] == 10)
                        path.AddRange(WaterPaths[d][i]);
            return path.ToArray();
        }

        public class BotMaps
        {
            public enum Keys
            {
                FinishLight,
                FinishDark,
                MagicTower,
                Fortress,
                Catacomb,
                Player
            }

            private Dictionary<Sector.TypeSector, (Point coordinates, GameTable.GameMap map)[]> Maps;
            private Dictionary<Sector.TypeSector, (Point coordinates, GameTable.GameMap map)[]> DragonMaps;
            private Dictionary<Sector.TypeSector, (Point coordinates, GameTable.GameMap map)[]> GoldMaps;

            public static int InteractiviesCount(GameTable game)
            {
                int Max = 0;
                for (int i = 0; i < game.SectorsSize.Width; i++)
                {
                    for (int j = 0; j < game.SectorsSize.Height; j++)
                    {
                        if ((new Sector.TypeSector[]
                        {
                            Sector.TypeSector.FinishLight,
                            Sector.TypeSector.FinishDark,
                            Sector.TypeSector.Catacomb,
                            Sector.TypeSector.Fortress,
                            Sector.TypeSector.MagicTower
                        }).Contains(game[i, j].Type))
                            Max += 3;
                    }
                }
                return Max;
            }
            public BotMaps(GameTable game, BackgroundWorker bw)
            {
                int progress = 0;
                Maps = new Dictionary<Sector.TypeSector, (Point, GameTable.GameMap)[]>();
                DragonMaps = new Dictionary<Sector.TypeSector, (Point, GameTable.GameMap)[]>();
                GoldMaps = new Dictionary<Sector.TypeSector, (Point, GameTable.GameMap)[]>();
                void MapsAdd(Sector.TypeSector type, Point sector)
                {
                    Add(ref Maps, nameof(Maps), type, sector);
                    Add(ref DragonMaps, nameof(DragonMaps), type, sector);
                    Add(ref GoldMaps, nameof(GoldMaps), type, sector);
                }
                void Add(ref Dictionary<Sector.TypeSector, (Point, GameTable.GameMap)[]> maps, string nameofMaps, Sector.TypeSector type, Point sector)
                {
                    string mapsType = nameofMaps.Replace("Maps", "");
                    var gameMap = new GameTable.GameMap(game.Sectors, sector, mapsType == "Dragon", mapsType == "Gold");
                    if (maps.ContainsKey(type))
                        maps[type] = maps[type].Append((sector, gameMap)).ToArray();
                    else
                        maps.Add(type, new (Point, GameTable.GameMap)[] { (sector, gameMap) });
                    progress++;
                    bw.ReportProgress(0, progress);
                }
                for (int i = 0; i < game.SectorsSize.Width; i++)
                {
                    for (int j = 0; j < game.SectorsSize.Height; j++)
                    {
                        Sector.TypeSector sectorType = game[i, j].Type;
                        if (new Sector.TypeSector[]
                        {
                            Sector.TypeSector.FinishLight,
                            Sector.TypeSector.FinishDark,
                            Sector.TypeSector.Catacomb,
                            Sector.TypeSector.Fortress,
                            Sector.TypeSector.MagicTower
                        }.Contains(sectorType))
                            MapsAdd(sectorType, new Point(i, j));
                    }
                }
                bw.ReportProgress(100, progress);
            }

            public GameTable.GameMap GetMap(Keys key, Point location, bool UseDragon, bool HaveGold, Point visitedLocation)
            {
                GameTable.GameMap FindMaps(Sector.TypeSector type)
                {
                    if (UseDragon)
                        return Find(DragonMaps, type);
                    else if (HaveGold)
                        return Find(GoldMaps, type);
                    else
                        return Find(Maps, type);
                }
                GameTable.GameMap Find(Dictionary<Sector.TypeSector, (Point coordinates, GameTable.GameMap map)[]> maps, Sector.TypeSector type)
                    => maps[type].Length == 1 ?
                       maps[type].Single().map :
                       maps[type].Where(V => V.coordinates != visitedLocation)
                                 .MinElement(V => location.StepCount(V.coordinates)).map;
                return key switch
                {
                    Keys.FinishLight => FindMaps(Sector.TypeSector.FinishLight),
                    Keys.FinishDark => FindMaps(Sector.TypeSector.FinishDark),
                    Keys.MagicTower => FindMaps(Sector.TypeSector.MagicTower),
                    Keys.Fortress => FindMaps(Sector.TypeSector.Fortress),
                    Keys.Catacomb => FindMaps(Sector.TypeSector.Catacomb),
                    Keys.Player => throw new Exception("Use GetMap(Player player, bool UseDragon, bool HaveGold)"),
                    _ => null,
                };
            }
            public GameTable.GameMap GetMap(Team team, bool UseDragon, bool HaveGold)
                => team switch
                {
                    Team.Light => GetMap(Keys.FinishLight, new Point(), UseDragon, HaveGold, new Point()),
                    Team.Dark => GetMap(Keys.FinishDark, new Point(), UseDragon, HaveGold, new Point()),
                    _ => null,
                };
            public GameTable.GameMap GetMap(Player bot, bool UseDragon, bool HaveGold)
            {
                GameTable game = bot.OwnerGame;
                Point location = bot.TblLocation;
                Team team = bot is Wizard ? (bot as Wizard).Owner.Team : bot.Team;
                var pls = game.PlayersOf(team.Opponent());
                var deadPls = pls.Where(P => P.IsDead && P.BlueRuby.Count > 0).Select(P => P.TblLocation);
                Point Locate;
                if (deadPls.Any())
                    Locate = deadPls.MinElement(L => location.StepCount(L));
                else
                {
                    var livePls = pls.Where(P => !P.IsDead).Select(P => P.TblLocation);
                    Locate = livePls.MinElement(L => location.StepCount(L));
                }
                return new GameTable.GameMap(game.Sectors, Locate, UseDragon, HaveGold);
            }
        }
    }
}