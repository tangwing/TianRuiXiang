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
        string id;
        string lengLiang;
        public static string lengLiangDanWei="";
        string reLiang;
        public static string reLiangDanWei = "";
        string reGongLv;
        public static string reGongLvDanWei = "";
        string shunShiLiuLiang;
        public static string shunShiLiuLiangDanWei = "";
        string leiJiLiuLiang;
        public static string leiJiLiuLiangDanWei = "";
        double gongShuiWenDu;
        double huiShuiWenDu;
        string leiJiGongZuoShiJian;
        string shiShiShiJian;
        string state = null;

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
            unitDic.Add("2C", "m3");
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
            if (lengLiang.Length == 0) lengLiang = "0";

            field = data.Substring(36, 2);//unit
            lengLiangDanWei = unitDic[field];
        //reLiang
            field = data.Substring(38, 8);
            reLiang = inverseData(field);
            reLiang = reLiang.TrimStart(toTrim);
            if (reLiang.Length == 0) reLiang = "0";

            field = data.Substring(46, 2);//unit
            reLiangDanWei = unitDic[field];
        //reGongLv
            field = data.Substring(48, 8);
            reGongLv = inverseData(field);
            reGongLv = reGongLv.TrimStart(toTrim);
            if (reGongLv.Length == 0) reGongLv = "0";

            field = data.Substring(56, 2);//unit
            reGongLvDanWei = unitDic[field];
        //shunShiLvLiang
            field = data.Substring(58, 8);
            shunShiLiuLiang = inverseData(field);
            shunShiLiuLiang = shunShiLiuLiang.TrimStart(toTrim);
            if (shunShiLiuLiang.Length == 0) shunShiLiuLiang = "0";

            field = data.Substring(66, 2);//unit
            shunShiLiuLiangDanWei = unitDic[field];
        //leiJiLiuLiang
            field = data.Substring(68, 8);
            leiJiLiuLiang = inverseData(field);
            leiJiLiuLiang = leiJiLiuLiang.TrimStart(toTrim);
            if (leiJiLiuLiang.Length == 0) leiJiLiuLiang = "0";

            field = data.Substring(76, 2);//unit
            leiJiLiuLiangDanWei = unitDic[field];
        //gongShuiWenDu
            field = data.Substring(78, 2);
            int fraction = int.Parse(field);
            field = data.Substring(80, 2);
            int integer = int.Parse(field);
            gongShuiWenDu = integer + (double)fraction / 100;
        //huiShuiWendu
            field = data.Substring(84, 2);//82,83 not used
            fraction = int.Parse(field);
            field = data.Substring(86, 2);
            integer = int.Parse(field);
            huiShuiWenDu = integer + (double)fraction / 100;
        //leiJiGongWuoShiJian
            field = data.Substring(90, 6);//88,89 not used
            leiJiGongZuoShiJian = inverseData(field);
            leiJiGongZuoShiJian = leiJiGongZuoShiJian.TrimStart(toTrim);// +"h";
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
            if (data[111] == '4') this.state = "电池欠压/";
        //ZhuangTai2
            field = data.Substring(112, 2);
            int state = int.Parse(field);
            if (state % 2 == 1) this.state += "积分仪故障/";
            state/=2;
            if (state % 2 == 1) this.state += "进水温度传感器故障/";
            state /= 2;
            if (state % 2 == 1) this.state += "回水温度传感器故障/";
            state /= 2;
            if (state % 2 == 1) this.state += "流量传感器故障";
            if (this.state == null) this.state = "正常";
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

        public string toCSVString(string delim="\n")
        {
            return id +
                delim + reLiang +
                delim + gongShuiWenDu +
                delim + huiShuiWenDu +
                delim + (gongShuiWenDu-huiShuiWenDu).ToString("F2") +
                delim + leiJiLiuLiang +
                delim + state+
                delim + shiShiShiJian 
                ;
        }
        
        private string extractMeterIdFromDataStream(string stream)
        {
           return inverseData(stream.Substring(4, 14));
        }
    }
}