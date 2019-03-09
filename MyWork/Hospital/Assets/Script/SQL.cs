using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using UnityEngine.UI;


public class SQL : MonoBehaviour
{
    public static SQL _sql;
    public Text PnoInputtext;//查找病人时的医疗证号的输入框
    public Text delpnoInput;//删除病人时的医疗证号的输入框
    public Text infoText;//医生和护士以及病房的信息文本预设体
    public Text npnoInput;//护士修改病人出入院时间的医疗证号输入框
    public Text dpnoInput;//医生修改病人病情的医疗证号输入框


    public Text[] insertPatientInfo;//插入病人时的信息文本数组
    public InputField[] insertPatientInput;//插入病人时的输入框数组
    public InputField enterTime;//护士修改病人出入院时间时的入院时间输入框
    public InputField outTime;//护士修改病人出入院时间时的出院时间输入框
    public InputField pathogen;//医生修改病人病情的输入框
    public InputField userNumtext;//登录界面输入账号的输入框
    public InputField userPswtext;//登陆界面输入密码的输入框
    public InputField newPswInput;//修改密码时的新密码输入框
    public InputField ensurePswInput;//修改密码时的确认密码输入框

    private string Ipaddress;//本地的IP地址
    private string Sqlname;//数据库的名字
    public  SqlConnection sqlcon;//数据库连接
    private string usernum;//用户名
    private string password;//密码
    private string pno;//医生和护士修改时的医疗证号
    // Use this for initialization
    private void Awake()
    {
        _sql = this;
        Ipaddress = Network.player.ipAddress;
    }
    void Start()
    {
        linkSql(Ipaddress);
    }
    public void linkSql(string Ip)
    {
        Sqlname = "server=" + Ip + ";database=医院病床管理;uid=sa;pwd=123456";
        sqlcon = new SqlConnection(Sqlname);
    }//连接数据库
    public void Login()
    {
        usernum = userNumtext.text;
        password = userPswtext.text;
        string sql = "SELECT Spassword,Spermission FROM Ser WHERE Snumber='" + usernum + "'";
        SqlDataAdapter sda = new SqlDataAdapter(sql, sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "Psw");
        if (ds.Tables["Psw"].Rows.Count != 0)
        {
            string findPassword = ds.Tables["Psw"].Rows[0][0].ToString().Trim();//将object类型的数据转换为string类型,并去掉string的前后的多余的空格。
            string permission = ds.Tables["Psw"].Rows[0][1].ToString().Trim();//获得权限
            if (password == findPassword)
            {
                UIcontroller._uiCotl.logPermission = permission;
                UIcontroller._uiCotl.Login();
            }
            else UIcontroller._uiCotl.showInfo("用户名或密码错误！");
        }
        else UIcontroller._uiCotl.showInfo("用户名或密码错误！");
    }//登录
    public void getNullbed()//得到空余床位
    {
        UIcontroller._uiCotl.destroyChild(UIcontroller._uiCotl.nullBedContent);
        string sql = "SELECT Broom,Bbed,Boffice FROM Bed WHERE Bno IS NULL";
        SqlDataAdapter sda = new SqlDataAdapter(sql, sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "nullBed");
        for (int i = 0; i < ds.Tables["nullBed"].Rows.Count; i++)
        {
            string BedInfo = "";
            for (int j = 0; j < ds.Tables["nullBed"].Columns.Count; j++)
            {
                BedInfo += ds.Tables["nullBed"].Rows[i][j].ToString().Trim() + "           ";
            }
            infoText.text = BedInfo;
            Instantiate(infoText, UIcontroller._uiCotl.nullBedContent.transform);//实例化预设体
        }
    }//得到空床位
    public void getpatientInfo()
    {
        string Pno = PnoInputtext.text;
        string sql = "SELECT * FROM Patient WHERE Pno='" + Pno + "'";
        SqlDataAdapter sda = new SqlDataAdapter(sql, sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "patInfo");
        if (ds.Tables["patInfo"].Rows.Count != 0)
        {
            for (int i = 0; i < ds.Tables["patInfo"].Columns.Count; i++)
            {
                UIcontroller._uiCotl.patientInfo[i].text = ds.Tables["patInfo"].Rows[0][i].ToString().Trim();
                UIcontroller._uiCotl.pnoInputPage.SetActive(false);
                UIcontroller._uiCotl.paitientInfoPage.SetActive(true);
            }
        }
        else UIcontroller._uiCotl.showInfo("查无此人！");
    }//获取病人信息
    public void searchDNInfo(string officeName)//查询各个科室的医生和护士
    {
        UIcontroller._uiCotl.destroyChild(UIcontroller._uiCotl.doctorContent);
        UIcontroller._uiCotl.destroyChild(UIcontroller._uiCotl.nurseContent);
        string sql = "SELECT Dname,Dtitle,Dtel FROM Doctor WHERE Doffice='" + officeName + "'";
        SqlDataAdapter sda = new SqlDataAdapter(sql, sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "doctor");
        string sql1 = "SELECT Nname,Ntitle,Ntel FROM Nurse WHERE Noffice='" + officeName + "'";
        SqlDataAdapter sda1 = new SqlDataAdapter(sql1, sqlcon);
        sda1.Fill(ds, "nurse");
        for (int i = 0; i < ds.Tables["doctor"].Rows.Count; i++)
        {
            string docInfo = "";
            for (int j = 0; j < ds.Tables["doctor"].Columns.Count; j++)
            {
                docInfo += ds.Tables["doctor"].Rows[i][j].ToString().Trim() + "           ";
            }
            infoText.text = docInfo;
            Instantiate(infoText, UIcontroller._uiCotl.doctorContent.transform);
        }
        for (int i = 0; i < ds.Tables["nurse"].Rows.Count; i++)
        {
            string nurInfo = "";
            for (int j = 0; j < ds.Tables["nurse"].Columns.Count; j++)
            {
                nurInfo += ds.Tables["nurse"].Rows[i][j].ToString().Trim() + "           ";
            }
            infoText.text = nurInfo;
            Instantiate(infoText, UIcontroller._uiCotl.nurseContent.transform);
        }
    }
    public void insertPatient()//插入病人数据
    {

        string value = null;
        for (int i = 0; i < insertPatientInfo.Length; i++)
        {
            if (i != insertPatientInfo.Length - 1)
            {
                value = value + "'" + insertPatientInfo[i].text + "',";
            }
            else value = value + "'" + insertPatientInfo[i].text + "'";
        }
        bool caninsert = true;
        int[] cantNUllInput = { 0, 1, 3, 5, 8, 9, 10, 11, 12, 13, 14, 15 };
        string pno = insertPatientInfo[0].text;
        string dno= insertPatientInfo[11].text;
        string nno = insertPatientInfo[12].text;
        string roomNum = insertPatientInfo[13].text;
        string bedNum = insertPatientInfo[14].text;
        string sql1 = "SELECT Bno FROM Bed WHERE Broom='" + roomNum + "' AND " + "Bbed='" + bedNum + "'";
        string sql3 = "SELECT * FROM Patient WHERE Pno='" + pno + "'";
        SqlDataAdapter sda = new SqlDataAdapter(sql1, sqlcon);
        SqlDataAdapter sda1 = new SqlDataAdapter(sql3, sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "Bed");
        sda1.Fill(ds,"patient");
        if (ds.Tables["Bed"].Rows.Count == 0|| ds.Tables["Bed"].Rows[0][0].ToString().Trim()!= ""||ds.Tables["patient"].Rows.Count!=0)
        {
            caninsert = false;       
        }
        for (int i = 0; i < cantNUllInput.Length; i++)
        {
            if (insertPatientInfo[cantNUllInput[i]].text == "")
                caninsert = false;
        }
        if (caninsert)
        {
            sqlcon.Open();
            string sql = "INSERT INTO Patient VALUES(" + value + ")";
            SqlCommand com = new SqlCommand(sql, sqlcon);
            com.ExecuteReader();
            sqlcon.Close();
            sqlcon.Open();
            string sql2="UPDATE Bed SET Bno='"+pno+"',"+"Bnurse='"+nno+"',"+"Bdoctor='"+dno+ "' WHERE Broom='" + roomNum + "' AND " + "Bbed='" + bedNum + "'";
            SqlCommand com1 = new SqlCommand(sql2, sqlcon);
            com1.ExecuteReader();
            sqlcon.Close();
            UIcontroller._uiCotl.showInfo("插入完成！");
            clearPaInput();
        }
        else UIcontroller._uiCotl.showInfo("病人信息有误或床位信息有误！");
    }
    public void clearPaInput()
    {
        foreach (InputField input in insertPatientInput)
        {
            input.text = "";
        }
    }//清空病人输入框
    public void deletePatient()//删除病人
    {
        string delpno = delpnoInput.text;
        string sql = "DELETE FROM Patient WHERE Pno='" + delpno + "'";
        string sql1 = "UPDATE Bed SET Bno=NULL,Bdoctor=NULL,Bnurse=NULL WHERE Bno='" + delpno + "'";
        sqlcon.Open();
        SqlCommand com = new SqlCommand(sql, sqlcon);//执行SQL语句
        if (com.ExecuteNonQuery() != 0)//如果条件满足，则数据库中存在该数据项
        {
            com.ExecuteReader();//把你执行完的结果给保存
            sqlcon.Close();
            sqlcon.Open();
            SqlCommand com1 = new SqlCommand(sql1, sqlcon);
            com1.ExecuteReader();
            sqlcon.Close();
            UIcontroller._uiCotl.deletePage.SetActive(false);
            UIcontroller._uiCotl.showInfo("删除成功！");
        }
        else
        {
            UIcontroller._uiCotl.showInfo("查无此人！");
            sqlcon.Close();
        }
    }
    public void PswChange()//医生修改密码
    {
        string newpsw = newPswInput.text;
        string ensurepsw = ensurePswInput.text;
        if (newpsw == ensurepsw && newpsw != "")
        {
            string sql = "UPDATE Ser SET Spassword='" + newpsw + "'" + "WHERE Snumber='" + usernum + "'";
            sqlcon.Open();
            SqlCommand com = new SqlCommand(sql, sqlcon);
            com.ExecuteReader();
            UIcontroller._uiCotl.pswChangepage.SetActive(false);
            UIcontroller._uiCotl.showInfo("密码修改成功！");
            sqlcon.Close();
        }
        else UIcontroller._uiCotl.showInfo("密码为空或者前后输入不一致！");
    }
    public void todocChangePage()//去到医生的修改界面
    {
        pno = dpnoInput.text;
        string sql = "SELECT Ppathogen FROM Patient WHERE Pno='" + pno + "'";
        SqlDataAdapter sda = new SqlDataAdapter(sql, sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "path");
        if (ds.Tables["path"].Rows.Count != 0)
        {
            pathogen.text = ds.Tables["path"].Rows[0][0].ToString().Trim();
            UIcontroller._uiCotl.dpnoInputPage.SetActive(false);
            UIcontroller._uiCotl.docchangePage.SetActive(true);
        }
        else UIcontroller._uiCotl.showInfo("查无此人!");
    }//dpno输入界面
    public void changePath()
    {
        string newPath = pathogen.text;
        string sql = "UPDATE Patient SET Ppathogen='"+newPath+"'"+" WHERE Pno='" + pno + "'";
        if(newPath!="")
        {
            sqlcon.Open();
            SqlCommand com = new SqlCommand(sql, sqlcon);
            com.ExecuteReader();
            UIcontroller._uiCotl.showInfo("修改成功！");
            UIcontroller._uiCotl.docchangePage.SetActive(false);
            sqlcon.Close();
        }
        else UIcontroller._uiCotl.showInfo("病情不能为空！");
    }//修改病情
    public void tonurChangePage()//npno输入界面
    {
        pno = npnoInput.text;
        string sql = "SELECT Pentertime,Pouttime FROM Patient WHERE Pno='" + pno + "'";
        SqlDataAdapter sda = new SqlDataAdapter(sql, sqlcon);
        DataSet ds = new DataSet();//定义数据集
        sda.Fill(ds, "eotime");//填入查询到的数据
        if (ds.Tables["eotime"].Rows.Count != 0)
        {
            enterTime.text = ds.Tables["eotime"].Rows[0][0].ToString().Trim();
            outTime.text = ds.Tables["eotime"].Rows[0][1].ToString().Trim();
            UIcontroller._uiCotl.npnoInputPage.SetActive(false);
            UIcontroller._uiCotl.nursechangePage.SetActive(true);
        }
        else UIcontroller._uiCotl.showInfo("查无此人！");
    }
    public void changeTime()
    {
        string newEnterTime = enterTime.text;
        string newOutTime = outTime.text;
        string sql = "UPDATE Patient SET Pentertime='" + newEnterTime + "'," + "Pouttime='" + newOutTime + "'WHERE Pno='" + pno + "'";
        if (newEnterTime != "" && newOutTime != "")
        {
            sqlcon.Open();
            SqlCommand com = new SqlCommand(sql, sqlcon);
            com.ExecuteReader();
            UIcontroller._uiCotl.showInfo("修改成功！");
            UIcontroller._uiCotl.nursechangePage.SetActive(false);
            sqlcon.Close();
        }
        else UIcontroller._uiCotl.showInfo("请勿输入空值！");
    }//修改出入院时间
    
    public void clearInputField(InputField input)//清空页面的输入框
    {
        input.text = "";
    }
}
