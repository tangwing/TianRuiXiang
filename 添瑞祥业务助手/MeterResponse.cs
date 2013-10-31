using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 Test:
 MBUS 68 20 51 21 31 17 00 11 11 81 2E 1F 90 12 00 00
 MBUS 68 20 51 21 31 17 00 11 11 81 2E 1F 90 12 00 00 00 00 05 00 00 00 00 05 00 00 00 00 14 00 00 00 00 35 19 00 00 00 2C 76 30 00 68 30 00 73 02 00 32 41 11 12 09 07 20 04 00 E9 16
 */
namespace 添瑞祥业务助手
{
    class MeterResponse
    {
        string data;
        public string id;
        public string lengLiang;
        public string reLiang;
        string reGongLv;
        string shunShiLiuLiang;
        string leiJiLiuLiang;
        string gongShuiWenDu;
        string huiShuiWenDu;
        string leiJiGongWuoShiJian;
        string shiShiShiJian;
        string dianChiDianYa="Normal";
        string jiFenYi="Normal";
        string jinShuiWenDuChuanGanQi = "Normal";
        string huiShuiWenDuChuanGanQi = "Normal";
        string liuLiangChuanGanQi = "Normal";


        static Dictionary<string, string> unitDic = new Dictionary<string, string>(16);
        static MeterResponse()
        {
            unitDic.Add("02", "Wh");
            unitDic.Add("05", "kWh");
            unitDic.Add("08", "MWh");
            unitDic.Add("0A", "MWh*100");
            unitDic.Add("01", "J");
            unitDic.Add("0B", "kJ");
            unitDic.Add("0E", "MJ");
            unitDic.Add("11", "GJ");
            unitDic.Add("13", "GJ*100");
            unitDic.Add("14", "W");
            unitDic.Add("17", "kW");
            unitDic.Add("1A", "MW");
            unitDic.Add("29", "L");
            unitDic.Add("2C", "m^3");
            unitDic.Add("32", "L/h");
            unitDic.Add("35", "m^3/h");
        }

        public MeterResponse(string dataStr)
        {
            if (dataStr.Length != 118) throw new Exception("The length of response should be 118!");
            data = dataStr;
            translate(data);
        }
        private void translate(string data)
        {
            char[] toTrim = { '0' };
            string field;
        //id
            field = data.Substring(4, 14);
            id = inverseData(field); 
        //lengLiang
            field = data.Substring(28,8);
            lengLiang=inverseData(field);
            lengLiang=lengLiang.TrimStart(toTrim);
            field = data.Substring(36, 2);//unit
            lengLiang += unitDic[field];
        //reLiang
            field = data.Substring(38, 8);
            reLiang = inverseData(field);
            reLiang = reLiang.TrimStart(toTrim);
            field = data.Substring(46, 2);//unit
            reLiang += unitDic[field];
        //reGongLv
            field = data.Substring(48, 8);
            reGongLv = inverseData(field);
            reGongLv = reGongLv.TrimStart(toTrim);
            field = data.Substring(56, 2);//unit
            reGongLv += unitDic[field];
        //shunShiLvLiang
            field = data.Substring(58, 8);
            shunShiLiuLiang = inverseData(field);
            shunShiLiuLiang = shunShiLiuLiang.TrimStart(toTrim);
            field = data.Substring(66, 2);//unit
            shunShiLiuLiang += unitDic[field];
        //leiJiLiuLiang
            field = data.Substring(68, 8);
            leiJiLiuLiang = inverseData(field);
            leiJiLiuLiang = leiJiLiuLiang.TrimStart(toTrim);
            field = data.Substring(76, 2);//unit
            leiJiLiuLiang += unitDic[field];
        //gongShuiWenDu
            field = data.Substring(78, 6);
            field = field.Insert(2, ". ");
            gongShuiWenDu = inverseData(field);
            gongShuiWenDu = gongShuiWenDu.TrimStart(toTrim)+"C";
        //huiShuiWendu
            field = data.Substring(84, 6);
            field = field.Insert(2, ". ");
            huiShuiWenDu = inverseData(field);
            huiShuiWenDu = huiShuiWenDu.TrimStart(toTrim) + "C";
        //leiJiGongWuoShiJian
            field = data.Substring(90, 6);
            leiJiGongWuoShiJian = inverseData(field);
            leiJiGongWuoShiJian = leiJiGongWuoShiJian.TrimStart(toTrim)+"h";
        //shiShiGongWuoShiJian
            field = data.Substring(96, 14);
            shiShiShiJian = inverseData(field);
            shiShiShiJian = shiShiShiJian.TrimStart(toTrim);
            shiShiShiJian = shiShiShiJian.Insert(12, ":");
            shiShiShiJian = shiShiShiJian.Insert(10, ":");
            shiShiShiJian = shiShiShiJian.Insert(8, " ");
            shiShiShiJian = shiShiShiJian.Insert(6, "-");
            shiShiShiJian = shiShiShiJian.Insert(4, "-");
        //ZhuangTai1 (dianChiDianYa)
            if (data[111] == '4') dianChiDianYa = "欠压";
        //ZhuangTai2
            field = data.Substring(112, 2);
            int state = int.Parse(field);
            if (state % 2 == 1) jiFenYi = "故障";
            state/=2;
            if (state % 2 == 1) jinShuiWenDuChuanGanQi = "故障";
            state /= 2;
            if (state % 2 == 1) huiShuiWenDuChuanGanQi = "故障";
            state /= 2;
            if (state % 2 == 1) liuLiangChuanGanQi = "故障";
        }

        //Inverse the data order
        static string inverseData(string stream)
        {
            char[] id = stream.ToCharArray();
            char tmp;
            for (int i = 0; i < stream.Length/4; i++)
            {
                int left = 2 * i;
                int right = id.Length - (i + 1) * 2;
                tmp = id[left];
                id[left] = id[right];
                id[right] = tmp;

                tmp = id[left + 1];
                id[left + 1] = id[right + 1];
                id[right + 1] = tmp;
            }
            return new string(id);
        }

        public string toString(string delim="\n")
        {
            return id + delim + lengLiang + delim + reLiang+
                delim+reGongLv+
                delim+shunShiLiuLiang+
                delim+leiJiLiuLiang+
                delim+gongShuiWenDu+
                delim+huiShuiWenDu+
                delim+leiJiGongWuoShiJian+
                delim+shiShiShiJian+
                delim+dianChiDianYa+
                delim+jiFenYi+
                delim+jinShuiWenDuChuanGanQi +
                delim+huiShuiWenDuChuanGanQi +
                delim+liuLiangChuanGanQi;
        }
        
        private string extractMeterIdFromDataStream(string stream)
        {
           return inverseData(stream.Substring(4, 14));
        }
    }
}
