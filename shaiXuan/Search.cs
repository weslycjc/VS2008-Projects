using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Data.SqlClient;
namespace shaiXuan
{
    public partial class Change : Form
    {
        public Change()
        {
            InitializeComponent();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
        }
        private void 创建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 fr = new Form1();
            fr.StartPosition = FormStartPosition.CenterScreen;
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Click1();
            dataGridView1.Columns[13].Visible = false;
            int width = 0;
            for (int i = 0; i < 22; i++)
            {
                if (dataGridView1.Columns[i].Visible == true)
                {
                    width += dataGridView1.Columns[i].Width;
                    if (width > 950)
                    {
                        width = 950;
                    }
                }
            }
            dataGridView1.Size = new System.Drawing.Size(width+100, 269);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //dataGridView1.Columns[1].FillWeight = 210;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Click1();
            dataGridView1.ReadOnly = false;
            dataGridView1.Columns[22].Visible = false;
            int width = 90;
            for (int i = 0; i < 22; i++)
            {
                //dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                if (dataGridView1.Columns[i].Visible == true)
                {
                    width += dataGridView1.Columns[i].Width;
                    if (width > 950)
                    {
                        width = 950;
                    }
                }
            }
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Size = new System.Drawing.Size(width+100, 269);
            NoEidtCells();
        }

        public void Click1()
        {
            dataGridView1.Visible = true;
            string batch = txtbatch.Text.Trim();
            int id1; 
            bool bid=int.TryParse(txtid.Text.Trim(),out id1);
            string sql="";
            if (bid == true || id1 != 0)
            {
                 sql = string.Format("select Master.*,QueXianNo.*,Class_Table.* from Master left join QueXianNo on Master.id=QueXianNo.MasterID left join Class_Table on QueXianNo.Quexian=Class_Table.QueXian where Master.ID='{0}'", id1);

            }
            else if(id1==0 && batch.Length>0)
            {
                 sql = string.Format("select Master.*,QueXianNo.*,Class_Table.* from Master left join QueXianNo on Master.id=QueXianNo.MasterID left join Class_Table on QueXianNo.Quexian=Class_Table.QueXian where Master.Batch='{0}'", batch);
            }
            if (sql.Length > 0)
            {
                DataSet ds = SqlDB.GetDataSet(sql);
                DataTable dt = ds.Tables[0];
                dt.Columns[1].ColumnName = "批次号";
                dt.Columns[2].ColumnName = "订单号";
                dt.Columns[3].ColumnName = "物料号";
                dt.Columns[4].ColumnName = "模具号";
                dt.Columns[5].ColumnName = "订单日期";
                dt.Columns[6].ColumnName = "班号";
                dt.Columns[7].ColumnName = "工时";
                dt.Columns[8].ColumnName = "订单录入时间";
                dt.Columns[9].ColumnName = "订单类型";
                dt.Columns[12].ColumnName = "不良数";
                dt.Columns[13].ColumnName = "缺陷代号";
                dt.Columns[15].ColumnName = "工序机台";
                dt.Columns[16].ColumnName = "操作员";
                dt.Columns[17].ColumnName = "产量";
                dt.Columns[22].ColumnName = "缺陷";
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[13].Visible = true;  //缺陷代号
                dataGridView1.Columns[14].Visible = false;
                dataGridView1.Columns[18].Visible = false;
                dataGridView1.Columns[19].Visible = false;
                dataGridView1.Columns[20].Visible = false;
                dataGridView1.Columns[21].Visible = false;
                dataGridView1.Columns[22].Visible = true; //缺陷

            }
            
        }

        public void NoEidtCells()
        {
            int[] Cells = new int[] { 0,1,2,3,4,5,6,8,9,10,11 };
            foreach (int i in Cells)
            {
                dataGridView1.Columns[i].ReadOnly = true;
                dataGridView1.Columns[i].DefaultCellStyle.BackColor=Color.Yellow;
            }
        }



        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex ==7)
            {
                bool[] x = new bool[2];
                int a = 0;
                int masterID, gh;
                x[0] = int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(),out masterID);
                x[1] = int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString(), out gh);
                foreach (bool c in x)
                {
                    if (c == false)
                    {
                        MessageBox.Show("请输入正确的工时", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        a++;
                    }
                }
                if (a == 0)
                {
                    string sql = string.Format("update Master set Time={0} where ID={1}", gh, masterID);
                    SqlDB.ExecuteNonQuery(sql);
                }
            }
            else if (e.ColumnIndex !=7)
            {
                int id, bl,cl;
                int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString(),out id);
                bool x=int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString(),out bl);
                string qx = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                string jt = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                string czy = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                bool y = int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString(), out cl);

                if (x == true || y==true)
                {
                    string sql = string.Format("update QueXianNo set Number1={0},QueXian='{1}',Number2='{2}',P_No='{3}',Number3={4} where ID={5}", bl,qx,jt,czy,cl,id);
                    SqlDB.ExecuteNonQuery(sql);
                }
            }
        }

    }
}
