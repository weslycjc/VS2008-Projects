using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using shaiXuan.ZRFCPO_01;
using System.Net;
using shaiXuan.cs;
using System.Data.SqlClient;

namespace shaiXuan
{
    public partial class Form1 : Form
    {
        NetworkCredential credentials = new NetworkCredential("VICOBARCODE", "handhand0");
        Z_RFC_PO_01 zmara = new Z_RFC_PO_01();
        Zrfcpo01[] data;
        sort st = new sort();       
        public string sortExpression;
        public int show;
        public int msg;
        public string odType;//订单类型
        string str2; //批次号通过截位获得的_后面的位数
        DialogResult a;//messagebox是否保存
        public Form1()     
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string sql = string.Format("select max(ID) from Master");
            SqlDataReader reader = SqlDB.ExecuteReader(sql);
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                lbl1.Text = "当前序号为  " + id.ToString();
            }
            reader.Dispose();
            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox")
                {
                    
                    TextBox tb1 = c as TextBox;
                    c.KeyDown += new KeyEventHandler(Key_Down);
                }
            }

        }

        #region 回车操作
        private void Key_Down(object sender, KeyEventArgs e)    
        {
                        
            if (e.KeyCode == Keys.Enter)
            {
                Enter1 en = new Enter1();
                if (txtbatch.Focused == true)   //批次号判断
                {
                    string text1 = txtbatch.Text.Trim();
                    List<Enter1> list1 = new List<Enter1>();
                    list1 = en.Batch(text1);
                    msg = list1[0].result1;
                    str2 = list1[0].Str2;
                    txtbatch.Text = text1;
                    textBox1.Text = str2.Substring(0,8);
                    if (txtbatch.Text.Length != 0)
                    {
                        BindData();
                        //string sql =string.Format("select ID,P_No,Number,Time from Master where Batch='{0}'",txtbatch.Text.Trim());
                        //SqlDataReader reader = SqlDB.ExecuteReader(sql);
                        //while (reader.Read())
                        //{
                        //    int a = reader.GetInt32(1);
                        //    caozuoyuan.Text = Convert.ToString(a);
                        //    caozuoyuan.ReadOnly = true;
                        //    int b= reader.GetInt32(2);
                        //    txtcl.Text = Convert.ToString(b);
                        //    txtcl.ReadOnly = true;
                        //    float c = reader.GetFloat(3);
                        //    txtgh.Text = Convert.ToString(c);
                        //    txtgh.ReadOnly = true;
                        //    titleID = reader.GetInt32(0);
                        //    MessageBox.Show("改批次号已录入过", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                    }
                   
                }
                if (msg == 0) 
                {
                    SendKeys.Send("{Tab}");
                }    
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                button1.PerformClick();
            }

        }
        #endregion

        #region 保存后录入新批次
        private void button1_Click(object sender, EventArgs e)  
        {
            bool b = TxtBox();
            if (b == true)              
            {
                a = MessageBox.Show("是否保存(保存后可录入新批次)", "", MessageBoxButtons.OKCancel);
                if(a==DialogResult.OK)
                {
                    Click1();
                    ClearAllTxt();
                    txtbatch.Focus();
                }
            }

        }
        #endregion

        //#region 保存后录入新机台
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    bool b = TxtBox();
        //    if (b == true)
        //    {
        //        a = MessageBox.Show("是否保存(保存后可录入新机台)", "提示", MessageBoxButtons.OKCancel);
        //        if (a == DialogResult.OK)
        //        {
        //            Click1();
        //            txtjitai1.Text = "";
        //            txtjitai1.Focus();
        //            buliang1.Text = "";
        //            buliang2.Text = "";
        //            buliang3.Text = "";
        //            buliang4.Text = "";
        //            quexian1.Text = "";
        //            quexian2.Text = "";
        //            quexian3.Text = "";
        //            quexian4.Text = "";
        //            string sql = string.Format("select ID from Master where Batch='{0}'", txtbatch.Text.Trim());
        //            SqlDataReader reader = SqlDB.ExecuteReader(sql);
        //            while (reader.Read())
        //            {
        //                titleID = reader.GetInt32(0);
        //            }
        //        }
        //    }
        //}
        //#endregion

        #region 获取RFC数据
        public void BindData()  
        {
            
            string T1 = "";
            string T2 = "";
            string T3 = textBox1.Text.ToString().Trim();
            string T4 = "";
            string a1 = "";
            string a2 = "";
            string a3 = "";
            string a4 = "";

            zmara.Credentials = credentials;
            data = new Zrfcpo01[0];

            zmara.ZRfcPo01(T3, T4.ToUpper(), a1, a2, ref data, T2, T1, a3, a4);
            DataTable dt = new DataTable();
            
            //dt.Columns.Add("T1");//订单
            //dt.Columns.Add("T2", typeof(System.Double));//订单数量
            //dt.Columns.Add("T3", typeof(System.Double));//已交货数量
           
            //dt.Columns.Add("T5", typeof(System.Double));//成型基数
            //dt.Columns.Add("T6", typeof(System.Double));//订单模次
            dt.Columns.Add("T7");//物料
            dt.Columns.Add("T8");//订单类型
            dt.Columns.Add("T4");//工装/模具
            dt.Columns.Add("T9", typeof(System.DateTime));//基本开始日期
            //dt.Columns.Add("T10", typeof(System.DateTime));//基本完成日期
            //dt.Columns.Add("T11", typeof(System.DateTime));//实际完成日期
            //dt.Columns.Add("T12", typeof(System.Double));//报废数量

            
            foreach (Zrfcpo01 po in data)
            {
                DataRow dr = dt.NewRow();

                dr[0] = po.Matnr;

                dr[1] = po.Ktext;

                dr[2] = po.Dauat;

                dr[3] = po.Gstrp;

                textBox2.Text = dr[0].ToString();
                textBox3.Text = dr[1].ToString();              
                txtDate.Text = dr[3].ToString().Substring(0, 4) + "/" + str2.Substring(9, 2) + "/" + str2.Substring(11, 2);
                odType = dr[2].ToString();
            }
            if (textBox2.Text.Length==0)
            {
                MessageBox.Show("查询数据为空");
                txtbatch.Focus();
                txtbatch.Text = "";
                msg = 1;
            }


        }
        #endregion

        #region 清空所有文本
        public void ClearAllTxt()   
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = "";
                }
            }
        }
        #endregion

        #region 添加信息事件
        public void Click1()    
        { 
            int i = 0;
            OrderT order1 = new OrderT();
            OrderC oc = new OrderC();
            MingXi mx =new MingXi();
            List<OrderC> olist = new List<OrderC>();
 
           order1.OrdetType(odType);
           odType = order1.Quexian;//订单类型
           string batch =txtbatch.Text.Trim().ToString();
           string orderno=textBox1.Text.ToString();
           string matnr = textBox2.Text.ToString();
           string mold = textBox3.Text.ToString();
           string[] splitStrings = txtDate.Text.Split();
           string datestr = string.Join("",splitStrings);
           DateTime date = Convert.ToDateTime(datestr);             
           string jitai1 = txtjitai1.Text.Trim();
           string jitai2 = txtjitai2.Text.Trim();
           string jitai3 = txtjitai3.Text.Trim();
           string jitai4 = txtjitai4.Text.Trim();
           string jitai5 = txtjitai5.Text.Trim();
           string jitai6 = txtjitai6.Text.Trim();
           string jitai7 = txtjitai7.Text.Trim();
           string jitai8 = txtjitai8.Text.Trim();
           string caozuo1 = txtcz1.Text.Trim();
           string caozuo2 = txtcz2.Text.Trim();
           string caozuo3 = txtcz3.Text.Trim();
           string caozuo4 = txtcz4.Text.Trim();
           string caozuo5 = txtcz5.Text.Trim();
           string caozuo6 = txtcz6.Text.Trim();
           string caozuo7 = txtcz7.Text.Trim();
           string caozuo8 = txtcz8.Text.Trim();
           int chanliang1 = int.Parse(txtcl1.Text.Trim());
           int chanliang2 = int.Parse(txtcl2.Text.Trim());
           int chanliang3 = int.Parse(txtcl3.Text.Trim());
           int chanliang4 = int.Parse(txtcl4.Text.Trim());
           int chanliang5 = int.Parse(txtcl5.Text.Trim());
           int chanliang6 = int.Parse(txtcl6.Text.Trim());
           int chanliang7 = int.Parse(txtcl7.Text.Trim());
           int chanliang8 = int.Parse(txtcl8.Text.Trim());
           int banhao =int.Parse(str2.Substring(8,1));
           float gongshi =float.Parse(txtgh.Text.Trim());
           int bl1, bl2, bl3, bl4, bl5, bl6, bl7, bl8, intqx1, intqx2, intqx3, intqx4, intqx5, intqx6, intqx7, intqx8;
           int.TryParse(buliang1.Text.Trim().ToString(), out bl1);            
           int.TryParse(buliang2.Text.Trim().ToString(), out bl2);             
           int.TryParse(buliang3.Text.Trim().ToString(), out bl3);         
           int.TryParse(buliang4.Text.Trim().ToString(), out bl4);
           int.TryParse(buliang5.Text.Trim().ToString(), out bl5);
           int.TryParse(buliang6.Text.Trim().ToString(), out bl6);
           int.TryParse(buliang7.Text.Trim().ToString(), out bl7);
           int.TryParse(buliang8.Text.Trim().ToString(), out bl8);
           int.TryParse(quexian1.Text.Trim().ToString(), out intqx1);
           int.TryParse(quexian2.Text.Trim().ToString(), out intqx2);
           int.TryParse(quexian3.Text.Trim().ToString(), out intqx3);
           int.TryParse(quexian4.Text.Trim().ToString(), out intqx4);
           int.TryParse(quexian5.Text.Trim().ToString(), out intqx5);
           int.TryParse(quexian6.Text.Trim().ToString(), out intqx6);
           int.TryParse(quexian7.Text.Trim().ToString(), out intqx7);
           int.TryParse(quexian8.Text.Trim().ToString(), out intqx8);
           int[] bl = new int[] { bl1, bl2, bl3, bl4, bl5, bl6, bl7, bl8 };
           int[] intqx = new int[] { intqx1, intqx2, intqx3, intqx4, intqx5, intqx6, intqx7, intqx8 };
           string[] jitai = new string[] { jitai1, jitai2, jitai3, jitai4, jitai5, jitai6, jitai7, jitai8 };
           string[] caozuo = new string[] { caozuo1, caozuo2, caozuo3, caozuo4, caozuo5, caozuo6, caozuo7, caozuo8 };
           int[] changliang = new int[] { chanliang1, chanliang2, chanliang3, chanliang4, chanliang5, chanliang6, chanliang7, chanliang8 };
           string[] strqx = new string[8];
           int mid=0;
           bool x=true;  //判断是否添加数据到数据库
       
           string sql =string.Format("select QueXian,Number from Class_Table where Qclass='{0}'",odType);
           SqlDataReader reader = SqlDB.ExecuteReader(sql);
           while (reader.Read())
           {
               oc = new OrderC() { Quexian = reader.GetString(0), Number = reader.GetInt32(1) };
               olist.Add(oc);
               i++;
           }
           reader.Dispose();//关闭
           sql = string.Format("select max(ID) from Master");
           reader = SqlDB.ExecuteReader(sql);
           while (reader.Read())
           {
               mid = reader.GetInt32(0) + 1;
               break;
           }
           reader.Dispose();//关闭
           for (int j = 0; j < 8; j++)
           {
               if (bl[j] == 0 || jitai[j].Length==0)
               {
                   break;
               }
               if (olist.Count > 0)
               {
                   strqx[j] = mx.PiDui(intqx[j], olist);
                   if (strqx[j].Length == 0)
                   {
                       MessageBox.Show("没有找到" + intqx[j] + "所对应的缺陷", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       x = false;
                   }
                   else
                   {
                       sql = string.Format("insert into QueXIanNo(Number1,QueXian,MasterID,Number2,P_No,Number3) values({0},'{1}','{2}','{3}','{4}',{5})", bl[j], strqx[j], mid, jitai[j],caozuo[j],changliang[j]);
                       SqlDB.ExecuteNonQuery(sql);    //添加明细表信息 QueXianNo
                   }
               }
               else
               {
                   x = false;
               }
           }
           if (x == true)
           {
               DateTime nowdate =DateTime.Now;
               sql = string.Format("insert into Master(Batch,OrderNo,Matnr,Mold,Date,B_No,Time,Date2,OType) values('{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}','{8}')", batch, orderno, matnr, mold, date, banhao, gongshi, nowdate,odType);
               SqlDB.ExecuteNonQuery(sql);    //添加抬头表信息 Master             
           }
        }
        #endregion

        #region 判断输入的文本是否正确
        public bool TxtBox()
        {
            Enter1 en = new Enter1();
            bool result = true;
            bool[] res = new bool[7];
            res[0] = en.CaoZuoyuan(txtcz1.Text.Trim(),ref txtcz1);
            res[1] = en.ChanLiang(txtcl1.Text.Trim(),ref txtcl1);
            res[2] = en.GongShi(txtgh.Text.Trim(),ref txtgh);
            res[3] = en.BuLiang1(buliang1.Text.Trim(),ref buliang1);
            res[4] = true;
            res[5] = true;
            res[6] = true;
            if (buliang2.Text.Trim().Length > 0)
            {
                res[4] = en.BuLiang2(buliang2.Text.Trim(), ref buliang2);
            }
            if (buliang3.Text.Trim().Length > 0)
            {
                res[5] = en.BuLiang2(buliang3.Text.Trim(), ref buliang3);
            }
            if (buliang4.Text.Trim().Length > 0)
            {
                res[6] = en.BuLiang2(buliang4.Text.Trim(), ref buliang4);
            }
            foreach (bool b in res)
            {
                if (b == false)
                {
                    result = false;
                }
                break;
            }


            return result;
        }
        #endregion

        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Change sr = new Change();
            sr.StartPosition = FormStartPosition.CenterScreen;
            sr.Show();
        }





    }
}
