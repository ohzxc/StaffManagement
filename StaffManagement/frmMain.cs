﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffManagement
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            showInfo();
        }
        //记录DataGridView控件中指定单元格信息的字段
        public static string str = "";
        //数据库连接字符串
        public static string strConn = @"Data Source=(localdb)\ProjectsV12;Initial Catalog=db_Staff;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; 
        /// <summary>
        /// 在DataGridView控件上显示记录
        /// </summary>
        private void showInfo()
        {
            using (SqlConnection con = new SqlConnection(strConn))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT No AS 员工编号,Name AS 员工姓名,Salary as 基本工资,Evaluation AS 工作评价 FROM tb_Staff ORDER BY No", con);
                sda.Fill(dt);
                this.dgvInfo.DataSource = dt.DefaultView;
            }
        }
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 添加纪录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.txtNo.Text == "" | this.txtName.Text == "" | this.txtSalary.Text == "" | this.txtEvaluation.Text == "")
            {
                MessageBox.Show("信息不完整！");
                return;
            }
            if (IsNumeric(this.txtSalary.Text.Trim()) == false)
            {
                MessageBox.Show("薪水信息不完整！");
                return;
            }
            //判断是否有相同记录
            if (IsSameRecord() == ture)
                return;
        }
        public bool IsNumeric(string strCode)
        {
            if (strCode == null || strCode.Length == 0)
                return false;
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] byteStr = ascii.GetBytes(strCode);
            foreach (byte code in byteStr)
            {
                if (code < 48 || code > 57)
                    return false;
            }
            return false;
        }
    }
}
