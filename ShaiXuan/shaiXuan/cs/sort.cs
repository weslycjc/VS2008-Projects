using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace shaiXuan
{
    public class sort
    {
        /// <summary>
        /// 存放资料DataTable属性的变量
        /// </summary>
        private static DataTable dtValue = new DataTable();

        /// <summary>
        /// 存放资料DataView属性的变量
        /// </summary>
        private static DataView dvValue = new DataView();


        /// <summary>
        /// 排序方式（正向＼反向）
        /// </summary>
        private static string sdValue;

        public static string SdValue
        {
            get { return sort.sdValue; }
            set { sort.sdValue = value; }
        }
        /// <summary>
        /// 排序集合
        /// </summary>
        private static string sdSort;

        public static string SdSort
        {
            get { return sort.sdSort; }
            set { sort.sdSort = value; }
        }
        /// <summary>
        /// 存放资料DataView属性
        /// </summary>
        public static DataView dv
        {
            get { return dvValue; }
            set { dvValue = value; }
        }
        /// <summary>
        /// 存放资料DataTable属性
        /// </summary>
        public static DataTable dt
        {
            get { return dtValue; }
            set { dtValue = value; }
        }

        /// <summary>    
        /// 排序方向属性    
        /// </summary>
        public void dataGridView1_Sorting(object sender, string e)
        {
            string sortExpression = e.ToUpper();
            if (GridViewSD == "ASC")
            {
                GridViewSD = "DESC";
                SortGridView(sortExpression, GridViewSD);
            }
            else
            {
                GridViewSD =  "ASC";
                SortGridView(sortExpression, GridViewSD);
            }
        }

        /// <summary>
        /// 取得排序方式（正向＼反向）
        /// </summary>
        public string GridViewSD
        {
            get
            {
                if (sdValue == null)
                {
                    sdValue = "ASC";//SortDirection.Ascending;
                }
                    return sdValue.ToUpper();// (SortDirection)ViewState["sortDirection"];
            }
            set
            {
                sdValue = value; 
            }
        }


        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sortExpression">列头</param>
        /// <param name="direction">方向</param>
        private void SortGridView(string sortExpression, string direction)
        {
            // You can cache the DataTable for improving performance
            /*if (dv == null)
            {
                dv = dt.DefaultView;
                dv.Sort = "T1 ASC";
                if (sortExpression == "T1" && direction == "DESC")
                {
                    dv.Sort = "T1 DESC";
                }
                else if (sortExpression != "T1")
                {
                    dv.Sort += "," + sortExpression + " " + direction;
                }

            }
            else
            {
                dv = dt.DefaultView;
                
                string sortdir =  sortExpression + " " + direction;
                string sr = sortExpression + " DESC";
                string sa = sortExpression + " ASC";
                if (dv.Sort.IndexOf(sa) > -1 && direction == "DESC")
                {
                    dv.Sort = dv.Sort.Replace(sa, sortdir);
                }
                else if (dv.Sort.IndexOf(sr) > -1 && direction == "ASC")
                {
                    dv.Sort = dv.Sort.Replace(sr, sortdir);
                }
                else
                {
                    dv.Sort += "," + sortExpression + " " + direction;
                }
            }*/

            dv = dt.DefaultView;
            dv.Sort = SdSort.ToString();

            string sortdir = sortExpression + " " + direction;
            string sdesc = sortExpression + " DESC";
            string sasc = sortExpression + " ASC";

            if (dv.Sort.IndexOf(sasc) > -1 && direction == "DESC")
            {
                dv.Sort = dv.Sort.Replace(sasc, sortdir);
            }
            else if (dv.Sort.IndexOf(sdesc) > -1 && direction == "ASC")
            {
                dv.Sort = dv.Sort.Replace(sdesc, sortdir);
            }
            SdSort = dv.Sort;
        }

    }
}
