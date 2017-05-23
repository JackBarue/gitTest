using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StRttmy.BLL;
using StRttmy.Model;

namespace StRttmy.UI
{
    //  进行数据操作后，更新当前列表 OK
    //  分页功能
    //  查询数据后修改、删除按钮增加问题 OK
    //  空数据检测 OK

    public partial class UserListForm : Form
    {
        private UserBLL ub = null;        

        public UserListForm()
        {
            InitializeComponent();            
        }

        //窗体加载
        private void UserListForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(52, 152, 186);
            ShowUserList(txtKeyword.Text);
            txtKeyword.Focus();
        }

        //新建用户
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddUserForm auf = new AddUserForm();
            auf.ulf = this;
            auf.ShowDialog();
        }

        //查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            ShowUserList(txtKeyword.Text);
        }

        //查询方法
        public void ShowUserList(string keyword)
        {
            if (Program.mf.LoginUser != null)
            {
                dgvUserList.DataSource = null;
                dgvUserList.Columns.Clear();
                IList<User> users = null;//用户列表
                ub = new UserBLL();
                users = ub.UserList(keyword);
                this.dgvUserList.DataSource = users;
                //隔行变色
                this.dgvUserList.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
                this.dgvUserList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                SetDgv(users.Count());                
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }            
        }

        //列表表头和操作按钮赋值及属性值
        private void SetDgv(int ListLenght)
        {
            dgvUserList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满dgvUserList
            dgvUserList.Columns[0].HeaderText = "用户ID";
            dgvUserList.Columns[1].HeaderText = "用户类型";
            dgvUserList.Columns[2].HeaderText = "登录名";
            dgvUserList.Columns[2].Width = 250;
            dgvUserList.Columns[3].HeaderText = "用户名";
            dgvUserList.Columns[3].Width = 250;
            dgvUserList.Columns[4].HeaderText = "密码";
            dgvUserList.Columns[5].HeaderText = "工作单位";
            dgvUserList.Columns[5].Width = ListLenght > 23 ? 260 : 280;
            dgvUserList.Columns[6].HeaderText = "创建时间";
            dgvUserList.Columns[6].Width = 245;
            dgvUserList.Columns["UserID"].Visible = false;
            dgvUserList.Columns["UserType"].Visible = false;
            dgvUserList.Columns["Password"].Visible = false;
            dgvUserList.EnableHeadersVisualStyles = false;
            dgvUserList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
            dgvUserList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGridViewButtonColumn dgvButtonColEdit = new DataGridViewButtonColumn();                // 设置列名
            dgvButtonColEdit.Name = "ManageEdit";
            // 设置列标题
            dgvButtonColEdit.HeaderText = "修改";
            // 设置按钮标题
            dgvButtonColEdit.UseColumnTextForButtonValue = true;
            dgvButtonColEdit.Text = "修改";
            dgvButtonColEdit.Width = 100;
            dgvButtonColEdit.HeaderCell.Style.BackColor = Color.FromArgb(97, 147, 170);
            dgvButtonColEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            DataGridViewButtonColumn dgvButtonColDel = new DataGridViewButtonColumn();
            // 设置列名
            dgvButtonColDel.Name = "ManageDel";
            // 设置列标题
            dgvButtonColDel.HeaderText = "删除";
            // 设置按钮标题
            dgvButtonColDel.UseColumnTextForButtonValue = true;
            dgvButtonColDel.Text = "删除";
            dgvButtonColDel.Width = 100;
            dgvButtonColDel.HeaderCell.Style.BackColor = Color.FromArgb(97, 147, 170);
            dgvButtonColDel.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            // 向DataGridView追加
            dgvUserList.Columns.Insert(dgvUserList.Columns.Count, dgvButtonColEdit);
            dgvUserList.Columns.Insert(dgvUserList.Columns.Count, dgvButtonColDel);
        }

        //添加行号
        private void dgvUserList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvUserList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvUserList.RowHeadersDefaultCellStyle.Font, rectangle, dgvUserList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        //单元格点击事件
        private void dgvUserList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //  e.RowIndex为-1时为标题行
            if (e.RowIndex != -1)
            {
                if (dgvUserList.Columns[e.ColumnIndex].Name == "ManageEdit")
                {
                    UI.UpdateUserForm uuf = new UpdateUserForm();//修改用户
                    uuf.userId = (Guid)dgvUserList.Rows[e.RowIndex].Cells["UserId"].Value;
                    uuf.ulf = this;
                    uuf.ShowDialog();
                }

                if (dgvUserList.Columns[e.ColumnIndex].Name == "ManageDel")
                {
                    Guid userId = (Guid)dgvUserList.Rows[e.RowIndex].Cells["UserId"].Value;//删除需要的id
                    DelUser(userId);
                }
            }
        }

        //删除*******************************考核系统做完后要做判断,教师所编辑的试题、试卷、课件、素材是否删除?学员说做的试卷是否删除
        private void DelUser(Guid userId)
        {
            DialogResult dr = MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                ub = new UserBLL();
                if (ub.DelUser(userId))
                {
                    MessageBox.Show("用户已删除。");
                    ShowUserList(txtKeyword.Text);
                }
                else
                {
                    MessageBox.Show("用户删除失败。");
                }
            }
        }

    }      
}
