const callRemote = mp.events.callRemote;
const call = mp.events.call;
const callRemoteUnreliable = mp.events.callRemoteUnreliable;
const browsers = mp.browsers;
const _callRemote = mp._events.callRemote ;
const _call = mp._events.call;
let voiceZoneId = 0;

gm.events.add("playerEnterPolygon", (polygon) => {
    if (polygon.isGreenZone)
        return;

    voiceZoneId = polygon.id;
    callRemote("set_polygon", polygon.id);
    mp.gui.emmit(`window.hudStore.polygon (1)`);
});

gm.events.add("playerLeavePolygon", (polygon) => {
    if (polygon.isGreenZone)
        return;

    if (polygon.id !== voiceZoneId)
        return;

    voiceZoneId = 0;
    callRemote("set_polygon", 0);
    mp.gui.emmit(`window.hudStore.polygon (0)`);
});

mp.polygons.add(7, [new mp.Vector3(315.6049, -564.9975, 42.45059+0.12),
    new mp.Vector3(311.5501, -575.8205, 42.14121+0.12),
    new mp.Vector3(309.255, -575.3992, 42.17855+0.12),
    new mp.Vector3(310.4486, -563.4799, 42.92852+0.12)], 2);

mp.polygons.add(8, [new mp.Vector3(339.5966, -579.1045, 42.61717+0.12),
    new mp.Vector3(324.8404, -571.7209, 42.14068+0.12),
    new mp.Vector3(326.7254, -566.2316, 42.14068+0.12),
    new mp.Vector3(342.3383, -572.0438, 42.14074+0.12)], 2);

mp.polygons.add(9, [new mp.Vector3(340.9141, -579.6977, 42.14071+0.12),
    new mp.Vector3(343.0812, -572.3118, 42.14071+0.12),
    new mp.Vector3(351.6045, -575.3718, 42.11224+0.12),
    new mp.Vector3(348.9979, -582.5307, 42.1407+0.12)], 2);

mp.polygons.add(10, [new mp.Vector3(351.4391, -587.0585, 42.14122+0.12),
    new mp.Vector3(355.3854, -577.6539, 41.98508+0.12),
    new mp.Vector3(365.7201, -581.3804, 42.61423+0.12),
    new mp.Vector3(362.6744, -590.405, 42.40226+0.12)], 2);

mp.polygons.add(11, [new mp.Vector3(341.6498, -589.5436, 42.14123+0.12),
    new mp.Vector3(338.3994, -597.1334, 41.805+0.12),
    new mp.Vector3(326.5258, -593.2487, 41.74664+0.12),
    new mp.Vector3(329.6247, -584.679, 41.73644+0.12)], 2);

mp.polygons.add(12, [new mp.Vector3(319.7712, -591.998, 42.14123+0.12),
    new mp.Vector3(321.9934, -585.8993, 43.00157+0.12),
    new mp.Vector3(327.5618, -587.9268, 43.12412+0.12),
    new mp.Vector3(325.1992, -594.3756, 42.13934+0.12)], 2);

mp.polygons.add(13, [new mp.Vector3(341.0464, -592.3315, 46.30448+1.12),
    new mp.Vector3(336.0068, -590.5549, 46.30447+1.12),
    new mp.Vector3(334.5612, -590.8784, 46.3045+1.12),
    new mp.Vector3(333.1888, -594.5042, 46.30448+1.12),
    new mp.Vector3(339.3922, -596.8737, 46.30447+1.12)], 2);

mp.polygons.add(14, [new mp.Vector3(333.0742, -581.988, 47.25868+0.12),
    new mp.Vector3(329.268, -593.2637, 47.22856+0.12),
    new mp.Vector3(321.1326, -590.3027, 47.24335+0.12),
    new mp.Vector3(325.2606, -579.1515, 47.23902+0.12)], 2);

mp.polygons.add(15, [new mp.Vector3(336.4686, -585.7498, 46.30447+1.12),
    new mp.Vector3(335.2428, -588.8788, 46.30447+1.12),
    new mp.Vector3(336.2151, -589.905, 46.30448+1.12),
    new mp.Vector3(341.2379, -591.8182, 46.30447+1.12),
    new mp.Vector3(342.6471, -588.0389, 46.30447+1.12)], 2);

mp.polygons.add(16, [new mp.Vector3(338.3871, -580.4295, 46.30445+1.12),
    new mp.Vector3(344.5835, -582.6675, 46.30447+1.12),
    new mp.Vector3(342.8, -587.6549, 46.30447+1.12),
    new mp.Vector3(336.5765, -585.472, 46.30447+1.12)], 2);

/*mp.polygons.add(19, [new mp.Vector3(123.1502, -757.3854, 240.108+0.12),
    new mp.Vector3(128.6254, -760.3926, 240.108+0.12),
    new mp.Vector3(131.9438, -753.5497, 240.3448+0.12),
    new mp.Vector3(125.6814, -750.9192, 240.108+0.12)], 2);

mp.polygons.add(20, [new mp.Vector3(158.4738, -738.3853, 242.1539+0.12),
    new mp.Vector3(157.0544, -742.1476, 242.1589+0.12),
    new mp.Vector3(152.2691, -740.2751, 240.108+0.12),
    new mp.Vector3(154.129, -735.9755, 240.1082+0.12)], 2);

mp.polygons.add(21, [new mp.Vector3(152.2458, -741.1715, 240.108+0.12),
    new mp.Vector3(156.7976, -742.8094, 240.108+0.12),
    new mp.Vector3(156.8031, -742.8311, 240.108+0.12),
    new mp.Vector3(154.8387, -748.4108, 242.1589+0.12),
    new mp.Vector3(150.0137, -746.6754, 240.108+0.12)], 2);

mp.polygons.add(22, [new mp.Vector3(147.6699, -754.0633, 242.1539+0.12),
    new mp.Vector3(145.3745, -759.9457, 240.1079+0.12),
    new mp.Vector3(149.0484, -761.5856, 240.108+0.12),
    new mp.Vector3(152.1834, -755.439, 240.108+0.12)], 2); 

mp.polygons.add(23, [new mp.Vector3(139.709, -763.9958, 257.0318+0.12),
    new mp.Vector3(141.0377, -758.4249, 257.0318+0.12),
    new mp.Vector3(118.2396, -750.1272, 257.0321+0.12),
    new mp.Vector3(115.687, -755.2542, 257.0321+0.12)], 2); */

mp.polygons.add(2, [new mp.Vector3(463.81955, -976.2152, 28.645697+1.12),
    new mp.Vector3(463.882, -983.60333, 28.635307+1.12),
    new mp.Vector3(458.9425, -983.53235, 28.646479+1.12),
    new mp.Vector3(458.92532, -976.2469, 28.645689+1.12)], 2.5);
mp.polygons.add(3, [new mp.Vector3(458.88785, -984.0406, 28.645693+1.12),
    new mp.Vector3(463.92435, -983.91864, 28.614044+1.12),
    new mp.Vector3(463.84137, -991.2844, 28.645689+1.12),
    new mp.Vector3(458.94208, -991.3323, 28.693768+1.12)], 2.5);
mp.polygons.add(4, [new mp.Vector3(464.0739, -995.7645, 28.645521+1.12),
    new mp.Vector3(464.15686, -1000.0229, 28.606705+1.12),
    new mp.Vector3(457.00253, -1000.2925, 28.666092+1.12),
    new mp.Vector3(456.9259, -997.11383, 28.645523+1.12),
    new mp.Vector3(458.6314, -995.3678, 28.645523+1.12),
    new mp.Vector3(464.0481, -995.65643, 28.645517+1.12)], 2.5);
mp.polygons.add(5, [new mp.Vector3(445.87546, -995.36816, 28.64555+1.12),
    new mp.Vector3(445.93515, -998.46246, 28.645515+1.12),
    new mp.Vector3(453.34833, -1000.2137, 28.679409+1.12),
    new mp.Vector3(453.3095, -996.95746, 28.645515+1.12),
    new mp.Vector3(451.75674, -995.3608, 28.645517+1.12)], 2.5);

mp.polygons.add(6, [new mp.Vector3(471.76892, -984.1787, 28.679125+1.12),
    new mp.Vector3(471.59375, -979.7701, 28.645527+1.12),
    new mp.Vector3(479.4254, -979.83105, 28.648428+1.12),
    new mp.Vector3(479.46057, -984.165, 28.758556+1.12)], 2.5);

mp.polygons.add(28, [new mp.Vector3(476.53864, -999.85, 28.646223+1.12),
    new mp.Vector3(471.8232, -999.98706, 28.645649+1.12),
    new mp.Vector3(471.7486, -990.1492, 28.645649+1.12),
    new mp.Vector3(476.3643, -990.0182, 28.64565+1.12)], 2.5);

mp.polygons.add(29, [new mp.Vector3(480.11102, -983.2806, 28.715958+1.12),
    new mp.Vector3(488.32626, -983.22815, 28.627136+1.12),
    new mp.Vector3(488.2883, -994.26, 28.78023+1.12),
    new mp.Vector3(479.95035, -994.2003, 28.896833+1.12)], 2.5);

mp.polygons.add(30, [new mp.Vector3(488.36435, -1003.2119, 28.64552+1.12),
    new mp.Vector3(488.33096, -1015.5712, 28.64552+1.12),
    new mp.Vector3(472.71527, -1015.6113, 28.64552+1.12),
    new mp.Vector3(472.6603, -1003.2197, 28.64552+1.12)], 2.5);

mp.polygons.add(31, [
    new mp.Vector3(439.5542, -999.98004, 32.926144+1.12),
    new mp.Vector3(439.50385, -995.4786, 32.92615+1.12),
    new mp.Vector3(448.3634, -995.364, 32.857037+1.12),
    new mp.Vector3(450.04907, -996.8777, 32.926144+1.12),
    new mp.Vector3(450.1702, -1000.04926, 32.926147+1.12)], 2.5);

mp.polygons.add(32, [
    new mp.Vector3(439.53775, -990.29767, 32.926212+1.12),
    new mp.Vector3(439.6309, -981.2688, 32.917755+1.12),
    new mp.Vector3(448.08765, -981.3758, 32.926212+1.12),
    new mp.Vector3(450.09842, -983.2884, 32.926212+1.12),
    new mp.Vector3(449.9822, -988.7847, 32.926212+1.12),
    new mp.Vector3(448.17712, -990.4117, 32.92622+1.12),
    new mp.Vector3(439.613, -990.37915, 32.926212+1.12)], 2.5);

mp.polygons.add(33, [new mp.Vector3(467.1256, -1014.1672, 25.462612),
    new mp.Vector3(467.08347, -1001.0749, 25.34913),
    new mp.Vector3(470.3706, -1000.95685, 25.349129),
    new mp.Vector3(470.40738, -1006.06433, 25.34913),
    new mp.Vector3(471.167, -1006.38446, 25.34914),
    new mp.Vector3(471.1705, -1011.8402, 25.349133),
    new mp.Vector3(470.2287, -1012.2793, 25.349133),
    new mp.Vector3(470.28436, -1014.1373, 25.462606)], 2.5);

mp.polygons.add(34, [new mp.Vector3(471.7318, -1011.0575, 25.348703),
    new mp.Vector3(471.77127, -1015.6693, 25.34931),
    new mp.Vector3(476.21655, -1015.6406, 25.351078),
    new mp.Vector3(476.34396, -1011.1463, 25.34932)], 2.5);

mp.polygons.add(35, [new mp.Vector3(476.87076, -1004.2775, 25.391588),
    new mp.Vector3(476.7507, -1015.56464, 25.27499),
    new mp.Vector3(488.2927, -1015.61945, 25.349155),
    new mp.Vector3(488.3527, -1004.3589, 25.349155)], 2.5);
/*mp.polygons.add(33, [
    new mp.Vector3(456.90497, -981.36145, 32.926212+1.12),
    new mp.Vector3(461.8853, -981.40894, 32.926212+1.12),
    new mp.Vector3(463.889, -983.2958, 32.92622+1.12),
    new mp.Vector3(463.98715, -988.392, 32.92622+1.12),
    new mp.Vector3(461.88095, -990.41907, 32.92622+1.12),
    new mp.Vector3(456.90494, -990.4254, 32.926216+1.12),
    new mp.Vector3(454.9761, -988.46436, 32.92622+1.12),
    new mp.Vector3(456.88623, -981.35425, 32.926216+1.12),
    new mp.Vector3(461.75192, -981.29736, 32.926216+1.12)], 2.5);  */

/*mp.polygons.add(34, [
    new mp.Vector3(488.37723, -990.9118, 24.229284+1.12),
    new mp.Vector3(483.0137, -991.01135, 24.229288+1.12),
    new mp.Vector3(482.94965, -986.59485, 24.229296+1.12),
    new mp.Vector3(488.31348, -986.5816, 24.229284+1.12)], 2.5);

mp.polygons.add(35, [
    new mp.Vector3(488.29404, -994.66345, 24.23059+1.12),
    new mp.Vector3(488.32834, -998.89355, 24.229288+1.12),
    new mp.Vector3(482.956, -998.9168, 24.229288+1.12),
    new mp.Vector3(483.02017, -994.66724, 24.239626+1.12)], 2.5);*/

mp.polygons.add(36, [
    new mp.Vector3(471.62213, -989.53143, 24.226507+1.12),
    new mp.Vector3(474.40317, -989.4912, 24.229286+1.12),
    new mp.Vector3(476.0551, -991.10815, 24.229288+1.12),
    new mp.Vector3(476.12326, -997.4998, 24.229284+1.12),
    new mp.Vector3(471.71356, -997.6558, 24.229288+1.12)], 2.5);

mp.polygons.add(37, [
    new mp.Vector3(423.15097, -973.0328, 24.579817),
    new mp.Vector3(463.71725, -973.0191, 24.579117),
    new mp.Vector3(463.69263, -999.48956, 24.573435),
    new mp.Vector3(423.18005, -1000.26624, 24.578838)], 3.5);

mp.polygons.add(38, [
    new mp.Vector3(-574.2353, -931.5149, 30.480762+1.12),
    new mp.Vector3(-565.28827, -931.48975, 30.480764+1.12),
    new mp.Vector3(-565.33984, -939.57043, 30.480762+1.12),
    new mp.Vector3(-574.20886, -939.5488, 30.480762+1.12)], 2.5);

mp.polygons.add(39, [new mp.Vector3(471.7006, -1003.8353, 25.349287),
    new mp.Vector3(471.552, -998.2623, 25.349295),
    new mp.Vector3(478.71957, -998.1427, 25.349287),
    new mp.Vector3(478.85727, -1003.72925, 25.349287)], 2.5);
mp.polygons.add(40, [new mp.Vector3(483.02368, -983.22125, 25.360104),
    new mp.Vector3(483.0548, -990.9809, 25.349285),
    new mp.Vector3(488.29257, -990.9795, 25.349287),
    new mp.Vector3(488.29068, -983.3943, 25.385654)], 2.5);
/*mp.polygons.add(39, [
    new mp.Vector3(-570.9161, -928.35803, 26.250547+1.12),
    new mp.Vector3(-562.8891, -928.02155, 26.104017+1.12),
    new mp.Vector3(-562.9654, -939.67444, 26.119263+1.12),
    new mp.Vector3(-574.6751, -939.4717, 26.136677+1.12),
    new mp.Vector3(-574.7288, -934.5888, 26.13668+1.12),
    new mp.Vector3(-571.0466, -934.2905, 26.136683+1.12)], 2.5);

mp.polygons.add(40, [
    new mp.Vector3(-570.9161, -928.35803, 26.250547+1.12),
    new mp.Vector3(-562.8891, -928.02155, 26.104017+1.12),
    new mp.Vector3(-562.9654, -939.67444, 26.119263+1.12),
    new mp.Vector3(-574.6751, -939.4717, 26.136677+1.12)], 2.5);*/


mp.polygons.add(41, [
    new mp.Vector3(-579.3085, -919.808, 26.174656+1.12),
    new mp.Vector3(-576.6211, -919.7962, 26.136677+1.12),
    new mp.Vector3(-572.21674, -919.0632, 26.067997+1.12),
    new mp.Vector3(-572.7536, -911.50946, 26.136677+1.12),
    new mp.Vector3(-576.6718, -911.5268, 26.136677+1.12),
    new mp.Vector3(-581.4543, -911.47034, 26.148142+1.12),
    new mp.Vector3(-581.3078, -915.94806, 26.136679+1.12),
    new mp.Vector3(-579.24054, -918.68176, 26.136677+1.12)], 2.5);




mp.polygons.add(42, [
    new mp.Vector3(443.43756, -985.96515, 28.645555+1.12),
    new mp.Vector3(435.0867, -986.0293, 28.645527+1.12),
    new mp.Vector3(434.99252, -977.90515, 28.65035+1.12),
    new mp.Vector3(443.43643, -977.8503, 28.64552+1.12)], 2.5);














mp.polygons.add(43, [new mp.Vector3(-578.9846, -931.9496, 27.256683),
    new mp.Vector3(-571.6187, -931.90234, 27.306923),
    new mp.Vector3(-571.6522, -926.99866, 27.256685),
    new mp.Vector3(-572.86993, -927.03107, 27.256678),
    new mp.Vector3(-572.8362, -923.2543, 27.256683),
    new mp.Vector3(-578.2077, -923.1038, 27.256678),
    new mp.Vector3(-578.3332, -926.31287, 27.25668),
    new mp.Vector3(-578.1732, -927.0206, 27.256678),
    new mp.Vector3(-578.8859, -926.8683, 27.25668)], 2.5);

mp.polygons.add(44, [new mp.Vector3(-574.738, -939.46246, 27.256685),
    new mp.Vector3(-564.3467, -939.7504, 27.25668),
    new mp.Vector3(-562.92065, -938.24634, 27.256683),
    new mp.Vector3(-562.9882, -929.5217, 27.25668),
    new mp.Vector3(-564.3437, -928.01385, 27.25668),
    new mp.Vector3(-569.2061, -927.98615, 27.256681),
    new mp.Vector3(-570.915, -929.3879, 27.25668),
    new mp.Vector3(-570.9996, -934.4858, 27.256678),
    new mp.Vector3(-574.6583, -934.4891, 27.256678),
], 2.5);


mp.polygons.add(45, [
    new mp.Vector3(-597.61304, -931.58154, 30.435398+1.12),
    new mp.Vector3(-591.4582, -931.66864, 30.435398+1.12),
    new mp.Vector3(-591.48706, -925.65234, 30.435398+1.12),
    new mp.Vector3(-597.5841, -925.6326, 30.435398+1.12)], 2.5);


mp.polygons.add(46, [
    new mp.Vector3(-573.84357, -921.6523, 30.524286+1.12),
    new mp.Vector3(-579.1525, -921.59503, 30.524286+1.12),
    new mp.Vector3(-579.205, -925.8314, 30.524286+1.12),
    new mp.Vector3(-573.89575, -925.90076, 30.524286+1.12)], 2.5);

mp.polygons.add(47, [
    new mp.Vector3(454.9886, -983.28644, 32.926212+1.12),
    new mp.Vector3(456.9852, -981.41254, 32.926212+1.12),
    new mp.Vector3(461.85782, -981.29553, 32.926212+1.12),
    new mp.Vector3(463.925, -983.3601, 32.926212+1.12),
    new mp.Vector3(463.84546, -988.42993, 32.926212+1.12),
    new mp.Vector3(461.92392, -990.4229, 32.926212+1.12),
    new mp.Vector3(456.92587, -990.42365, 32.926212+1.12),
    new mp.Vector3(454.9759, -988.40625, 32.926212+1.12)], 2.5);

mp.polygons.add(41, [new mp.Vector3(483.02747, -991.4592, 25.349382),
    new mp.Vector3(482.9575, -998.96606, 25.349285),
    new mp.Vector3(488.28412, -998.9798, 25.349287),
    new mp.Vector3(488.30612, -991.6008, 25.388554)], 2.5);

mp.polygons.add(48, [new mp.Vector3(-1296.8745, -573.01935, 33),
    new mp.Vector3(-1300.6438, -576.221, 33),
    new mp.Vector3(-1306.7866, -569.0392, 33),
    new mp.Vector3(-1303.0221, -565.7179, 33)], 2.5);

mp.polygons.add(49, [new mp.Vector3(-1293.5725, -577.0744, 33),
    new mp.Vector3(-1297.2592, -580.25977, 33),
    new mp.Vector3(-1291.4288, -587.17377, 33),
    new mp.Vector3(-1287.7863, -583.863, 33)], 2.5);

mp.polygons.add(50, [new mp.Vector3(-1287.0134, -584.7201, 33),
    new mp.Vector3(-1290.8452, -587.9595, 33),
    new mp.Vector3(-1286.7443, -592.86804, 33),
    new mp.Vector3(-1283.1353, -589.46387, 33)], 2.5);

mp.polygons.add(51, [new mp.Vector3(-1287.0134, -584.7201, 37),
    new mp.Vector3(-1290.8452, -587.9595, 37),
    new mp.Vector3(-1286.7443, -592.86804, 37),
    new mp.Vector3(-1283.1353, -589.46387, 37)], 2.5);

mp.polygons.add(52, [new mp.Vector3(-1287.7928, -583.8429, 36.4),
    new mp.Vector3(-1291.4908, -587.104, 36.4),
    new mp.Vector3(-1295.3412, -582.4574, 36.4),
    new mp.Vector3(-1291.6519, -579.2759, 36.4)], 2.5);

mp.polygons.add(53, [new mp.Vector3(-1298.6984, -570.74005, 36.4),
    new mp.Vector3(-1303.492, -573.9662, 36.4),
    new mp.Vector3(-1306.7535, -569.0234, 36.4),
    new mp.Vector3(-1302.9742, -565.7555, 36.4)], 2.5);

mp.polygons.add(54, [new mp.Vector3(-1285.496, -571.5084, 40.2),
    new mp.Vector3(-1281.6373, -575.826, 40.2),
    new mp.Vector3(-1292.9008, -585.36316, 40.2),
    new mp.Vector3(-1297.0779, -580.3809, 40.2)], 2.5);

mp.polygons.add(55, [new mp.Vector3(-1292.647, -585.718, 40.2),
    new mp.Vector3(-1287.1697, -592.2836, 40.2),
    new mp.Vector3(-1284.4331, -590.1323, 40.2),
    new mp.Vector3(-1289.8628, -583.58057, 40.2)], 2.5);

mp.polygons.add(56, [new mp.Vector3(-1292.4384, -567.98535, 40.2),
    new mp.Vector3(-1296.0514, -563.6697, 40.2),
    new mp.Vector3(-1304.966, -571.1594, 40.2),
    new mp.Vector3(-1300.7878, -576.1239, 40.2)], 2.5);

mp.polygons.add(57, [new mp.Vector3(-1302.6077, -568.4051, 40.2),
    new mp.Vector3(-1306.4243, -564.4234, 40.2),
    new mp.Vector3(-1308.8601, -566.37976, 40.2),
    new mp.Vector3(-1305.2242, -570.42096, 40.2)], 2.5);

mp.polygons.add(58, [new mp.Vector3(-1295.3772, -563.75946, 40.2),
    new mp.Vector3(-1292.7676, -561.5972, 40.2),
    new mp.Vector3(-1289.5072, -565.6214, 40.2),
    new mp.Vector3(-1292.0557, -567.7687, 40.2)], 2.5);

mp.polygons.add(59, [new mp.Vector3(234.15002, -1093.4779, 28),
    new mp.Vector3(234.16595, -1103.5369, 28),
    new mp.Vector3(243.68831, -1103.5447, 28),
    new mp.Vector3(243.71852, -1094.0366, 28)], 2.5);

mp.polygons.add(60, [new mp.Vector3(244.38013, -1093.9453, 28),
    new mp.Vector3(244.25426, -1111.4028, 28),
    new mp.Vector3(259.94843, -1111.3873, 28),
    new mp.Vector3(259.8753, -1094.0597, 28)], 2.5);

mp.polygons.add(61, [new mp.Vector3(254.52356, -1093.4827, 35),
    new mp.Vector3(246.57808, -1093.4672, 35),
    new mp.Vector3(246.60887, -1101.5516, 35),
    new mp.Vector3(254.51984, -1101.5969, 35)], 2.5);

mp.polygons.add(62, [new mp.Vector3(233.48732, -1094.0405, 35),
    new mp.Vector3(243.308, -1094.0693, 35),
    new mp.Vector3(243.29169, -1101.5046, 35),
    new mp.Vector3(233.49852, -1101.5721, 35)], 2.5);

mp.polygons.add(63, [new mp.Vector3(1855.1271, 3695.6245, 30.259407),
    new mp.Vector3(1860.2059, 3686.8, 30.259424),
    new mp.Vector3(1865.4286, 3689.911, 30.73259),
    new mp.Vector3(1860.6471, 3698.4934, 30.186737)], 2.5);

mp.polygons.add(64, [new mp.Vector3(1854.5493, 3688.0305, 31.186308),
    new mp.Vector3(1851.6293, 3693.1812, 30.259407),
    new mp.Vector3(1853.6799, 3694.3872, 30.25941),
    new mp.Vector3(1857.1948, 3689.6995, 30.145426)], 2.5);

mp.polygons.add(65, [new mp.Vector3(1844.6736, 3680.8982, 30.259415),
    new mp.Vector3(1849.2716, 3683.7566, 30.259415),
    new mp.Vector3(1850.6907, 3681.3096, 30.25941),
    new mp.Vector3(1845.6818, 3678.4436, 30.25149)], 2.5);

mp.polygons.add(66, [new mp.Vector3(1859.1284, 3690.8494, 34.10884),
    new mp.Vector3(1861.5464, 3692.2346, 34.262344),
    new mp.Vector3(1863.6395, 3689.262, 34.266933),
    new mp.Vector3(1861.0468, 3687.6497, 34.25934)], 2.5);

mp.polygons.add(67, [new mp.Vector3(-446.91508, 6016.0903, 36.86763),
    new mp.Vector3(-441.6883, 6010.9663, 36.822784),
    new mp.Vector3(-447.3137, 6005.462, 36.470142),
    new mp.Vector3(-452.51514, 6010.304, 36.524986)], 2.5);

mp.polygons.add(68, [new mp.Vector3(-445.37418, 6007.4917, 27.985617),
    new mp.Vector3(-449.39108, 6011.4683, 27.985512),
    new mp.Vector3(-444.0255, 6016.611, 26.976952),
    new mp.Vector3(-440.35925, 6012.81, 27.98561)], 2.5);

mp.polygons.add(69, [new mp.Vector3(-435.0152, 6003.834, 28.771164),
    new mp.Vector3(-431.07648, 6000.277, 28.026466),
    new mp.Vector3(-428.18497, 6003.276, 27.955122),
    new mp.Vector3(-431.98846, 6006.864, 27.549774)], 2.5);