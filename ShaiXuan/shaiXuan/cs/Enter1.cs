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
namespace shaiXuan.cs
{
    class Enter1
    {
        /// <summary>
        /// 
        /// </summary>
        public string Str1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Str2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int result1 { get; set; }

        /// <summary>
        /// 判断操作员textBox
        /// </summary>
        /// <param name="x">textBox.text</param>
        /// <returns>msg</returns>
        public bool CaoZuoyuan(string x,ref TextBox tb)
        {
            bool result=true;
            int a=0;
            if (x.Length==0)
            {
                MessageBox.Show("请输入操作员工号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                result = false;
            }
            else if (int.TryParse(x, out a) == false || x.Length!=5)
            {
                MessageBox.Show("请输入正确的工号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb.Select();
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 判断产量textBox
        /// </summary>
        /// <param name="x">textBox.text</param>
        /// <returns>msg</returns>
        public bool ChanLiang(string x, ref TextBox tb)
        { 
            bool result=true;
            int a = 0;
            if (x.Length == 0)
            {
                MessageBox.Show("请输入产量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                result=false;
            }
            else if (int.TryParse(x, out a) == false)
            {
                MessageBox.Show("请输入正确的产量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb.Select();
                result=false;
            }
            return result; 
        }
        /// <summary>
        /// 判断筛选工时
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public bool GongShi(string x,ref TextBox tb)
        {
            bool result=true;
            int a = 0;
            if (x.Length == 0)
            {
                MessageBox.Show("请输入筛选工时", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                result=false;
            }
            else if (int.TryParse(x, out a) == false)
            {
                MessageBox.Show("请输入正确的筛选工时", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb.Select();
                result = false;
            }
            //增加的验证
            else if (float.Parse(x) > 20)
            {
                MessageBox.Show("工时不能超过20小时", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb.Select();
                result = false;
            }

            return result; 
        }
        public List<Enter1> Batch(string x)
        {
            List<Enter1> result = new List<Enter1>();
            if (x.Length == 0)
            {
                MessageBox.Show("请输入批次号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Enter1 ent = new Enter1() { Str1 = "", Str2 = "", result1 = 1 };
                result.Add(ent);
            }
            else
            {
                string[] splitStrings = x.Split('_');
                string str2 = splitStrings[1];
                Enter1 ent = new Enter1() { Str1 = "", Str2 = str2, result1 = 0 };
                result.Add(ent);
            }
            return result; 
        }
        /// <summary>
        /// 判断不良数textBox
        /// </summary>
        /// <param name="x">textBox.text</param>
        /// <returns>msg</returns>
        public bool BuLiang1(string x,ref TextBox tb)
        {
            bool result =true;
            int a = 0;
            if (x.Length == 0)
            {
                MessageBox.Show("请输入不良数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                result =false;
            }
            else if (int.TryParse(x, out a) == false)
            {
                MessageBox.Show("请输入正确的不良数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb.Select();
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 判断不良数textbox 234
        /// </summary>
        /// <param name="x"></param>
        /// <param name="tb"></param>
        /// <returns></returns>
        public bool BuLiang2(string x, ref TextBox tb)
        {
            bool result = true;
            int a = 0;
            if (int.TryParse(x, out a) == false)
            {
                MessageBox.Show("请输入正确的不良数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb.Select();
                result = false;
            }
            return result;
        }
    }

    class OrderT
    {
        private string quexian;

        public string Quexian { get { return this.quexian; } set { quexian = value; } }

        public int OrdetType(string x)
        {
            int result = 0;
            if (x == "ZP11" || x=="M0")
            {
                quexian = "M0"; 
            }
            else if (x == "ZP13"|| x=="S0")
            {
                quexian = "S0";
            }
            else if (x == "ZP15" || x=="A0")
            {
                quexian = "A0";
            }
            else
            {
                MessageBox.Show("订单类型不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                result = 1;
            }
            return result;
        }

    }

    class OrderC
    {
        public string Quexian { get; set; }
        public int Number{get;set;}
    }

    class MingXi 
    { 


        public string PiDui(int x, List<OrderC> list)
        {
            string result="";
            for (int i = 0; i < list.Count; i++)
            {
                if (x == list[i].Number)
                {
                    result = list[i].Quexian;
                    
                }
            }
            return result;
        }       
    }


}
