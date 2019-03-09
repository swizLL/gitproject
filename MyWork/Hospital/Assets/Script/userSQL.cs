using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using UnityEngine.UI;

public class userSQL : MonoBehaviour
{
    public GameObject nnosearchInputPage;//查找护士时输入护士号的输入框
    public GameObject nurInfoPage;//护士信息的显示界面
    public GameObject nnoDeletePage;//删除护士的界面
    public GameObject dnosearchInputPage;//查找医生时输入医生编号的界面
    public GameObject docInfoPage;//医生信息的显示界面
    public GameObject dnoDeletePage;//删除医生的界面

    public Text[] regisInput;//注册时的信息文本数组
    public Text[] nurInfo;//护士信息界面的文本数组
    public Text[] nurInsertinfo;//添加护士的信息文本数组
    public Text[] docInfo;//医生信息界面的文本数组
    public Text[] docInsertinfo;//添加医生的信息文本数组

    public InputField[] regisField;//注册页面的文本输入框数组
    public InputField[] nurInsertField;//添加护士的文本输入框数组
    public InputField[] docInsertField;//添加医生的文本输入框数组
    public InputField searchNurInput;//查找护士的护士号输入框
    public InputField deleteNurInput;//删除护士的护士号输入框
    public InputField searchDocInput;//查找医生的医生编号输入框
    public InputField deleteDocInput;//删除医生的医生编号输入框
    public InputField ensusePassword;//注册界面确认密码的输入框
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void register()//注册的方法
    {
        string value = null;
        for (int i = 0; i < regisInput.Length; i++)
        {
            if (i != regisInput.Length - 1)
            {
                value = value + "'" + regisInput[i].text + "',";
            }
            else value = value + "'" + regisInput[i].text + "'";
        }
        bool caninsert = true;
        foreach (Text txt in regisInput)
        {
            if (txt.text == "" || regisInput[1].text != ensusePassword.text)
                caninsert = false;
        }
        if (caninsert)
        {
            SQL._sql.sqlcon.Open();
            string sql = "INSERT INTO Ser VALUES(" + value + ")";
            SqlCommand com = new SqlCommand(sql, SQL._sql.sqlcon);
            com.ExecuteReader();
            SQL._sql.sqlcon.Close();
            UIcontroller._uiCotl.showInfo("注册完成！");
            clearregisInput();
            UIcontroller._uiCotl.mainpage.SetActive(true);
            UIcontroller._uiCotl.zhucepage.SetActive(false);
        }
        else UIcontroller._uiCotl.showInfo("请检查信息！");
    }
    public void clearregisInput()
    {
        foreach (InputField input in regisField)
        {
            input.text = "";
        }
    }
    public void searchNurInfo()//查找护士
    {
        string Nno = searchNurInput.text;
        string sql = "SELECT * FROM Nurse WHERE Nno='" + Nno + "'";
        SqlDataAdapter sda = new SqlDataAdapter(sql, SQL._sql.sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "nurInfo");
        if (ds.Tables["nurInfo"].Rows.Count != 0)
        {
            for (int i = 0; i < ds.Tables["nurInfo"].Columns.Count; i++)
            {
                nurInfo[i].text = ds.Tables["nurInfo"].Rows[0][i].ToString().Trim();
                nnosearchInputPage.SetActive(false);
                nurInfoPage.SetActive(true);
            }
        }
        else UIcontroller._uiCotl.showInfo("查无此人！");
    }
    public void insertNurse()//添加护士
    {
        string value = null;
        for (int i = 0; i < nurInsertinfo.Length; i++)
        {
            if (i != nurInsertinfo.Length - 1)
            {
                value = value + "'" + nurInsertinfo[i].text + "',";
            }
            else value = value + "'" + nurInsertinfo[i].text + "'";
        }
        bool caninsert = true;
        string Nno = nurInsertinfo[0].text;
        string sql1 = "SELECT * FROM Nurse WHERE Nno='" + Nno + "'";//sql语句
        SqlDataAdapter sda = new SqlDataAdapter(sql1, SQL._sql.sqlcon);//执行sql语句
        DataSet ds = new DataSet();//定义数据集
        sda.Fill(ds, "nurse");//把sql语句查询到的数据添加到数据集里
        if (ds.Tables["nurse"].Rows.Count != 0)//判断数据集是否为空
            caninsert = false;
        int[] cantNulltxt = { 0, 1, 2, 3, 5, 7, 8 };//不能为空的输入框的索引
        for (int i = 0; i < cantNulltxt.Length; i++)
        {
            if (nurInsertinfo[cantNulltxt[i]].text == "")
                caninsert = false;
        }
        if (caninsert)
        {
            SQL._sql.sqlcon.Open();//数据库连接打开
            string sql = "INSERT INTO Nurse VALUES(" + value + ")";
            SqlCommand com = new SqlCommand(sql, SQL._sql.sqlcon);//插入删除修改调用sql语句的方法
            com.ExecuteReader();//执行sql语句
            SQL._sql.sqlcon.Close();
            clearnurInstInput();
            UIcontroller._uiCotl.showInfo("插入完成！");
        }
        else
        {
            SQL._sql.sqlcon.Close();
            UIcontroller._uiCotl.showInfo("请完善信息或已存在此护士！");
        }
    }
    public void clearnurInstInput()
    {
        foreach (InputField input in nurInsertField)
        {
            input.text = "";
        }
    }
    public void deleteNurse()
    {
        string delNno = deleteNurInput.text;
        string sql = "Delete FROM Nurse WHERE Nno='" + delNno + "'";
        SQL._sql.sqlcon.Open();
        SqlCommand com = new SqlCommand(sql, SQL._sql.sqlcon);
        if (com.ExecuteNonQuery() != 0)
        {
            com.ExecuteReader();
            SQL._sql.sqlcon.Close();
            nnoDeletePage.SetActive(false);
            UIcontroller._uiCotl.showInfo("删除完成！");
        }
        else
        {
            SQL._sql.sqlcon.Close();
            UIcontroller._uiCotl.showInfo("查无此人！");
        }
    }
    public void searchDocInfo()//查找医生
    {
        string Dno = searchDocInput.text;
        string sql = "SELECT * FROM Doctor WHERE Dno='" + Dno + "'";
        SqlDataAdapter sda = new SqlDataAdapter(sql, SQL._sql.sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "docInfo");
        if (ds.Tables["docInfo"].Rows.Count != 0)
        {
            for (int i = 0; i < ds.Tables["docInfo"].Columns.Count; i++)
            {
                docInfo[i].text = ds.Tables["docInfo"].Rows[0][i].ToString().Trim();
                dnosearchInputPage.SetActive(false);
                docInfoPage.SetActive(true);
            }
        }
        else UIcontroller._uiCotl.showInfo("查无此人！");
    }
    public void insertDoctor()//添加医生
    {
        string value = null;
        for (int i = 0; i < docInsertinfo.Length; i++)
        {
            if (i != docInsertinfo.Length - 1)
            {
                value = value + "'" + docInsertinfo[i].text + "',";
            }
            else value = value + "'" + docInsertinfo[i].text + "'";
        }
        bool caninsert = true;
        string Dno = docInsertinfo[0].text;
        string sql1 = "SELECT * FROM Doctor WHERE Dno='" + Dno + "'";
        SqlDataAdapter sda = new SqlDataAdapter(sql1, SQL._sql.sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "doctor");
        if (ds.Tables["doctor"].Rows.Count != 0)
            caninsert = false;
        int[] cantNulltxt = { 0, 1, 2, 3, 5, 6, 7 };
        for (int i = 0; i < cantNulltxt.Length; i++)
        {
            if (docInsertinfo[cantNulltxt[i]].text == "")
                caninsert = false;
        }
        if (caninsert)
        {
            SQL._sql.sqlcon.Open();
            string sql = "INSERT INTO Doctor VALUES(" + value + ")";
            Debug.Log(sql);
            SqlCommand com = new SqlCommand(sql, SQL._sql.sqlcon);
            com.ExecuteReader();
            SQL._sql.sqlcon.Close();
            cleardocInstInput();
            UIcontroller._uiCotl.showInfo("插入完成！");
        }
        else
        {
            SQL._sql.sqlcon.Close();
            UIcontroller._uiCotl.showInfo("请完善信息或已存在此医生！");
        }
    }
    public void cleardocInstInput()
    {
        foreach (InputField input in docInsertField)
        {
            input.text = "";
        }
    }
    public void deleteDocotr()
    {
        string delDno = deleteDocInput.text;
        string sql = "Delete FROM Doctor WHERE Dno='" + delDno + "'";
        SQL._sql.sqlcon.Open();
        SqlCommand com = new SqlCommand(sql, SQL._sql.sqlcon);
        if (com.ExecuteNonQuery() != 0)
        {
            com.ExecuteReader();
            SQL._sql.sqlcon.Close();
            dnoDeletePage.SetActive(false);
            UIcontroller._uiCotl.showInfo("删除完成！");
        }
        else
        {
            SQL._sql.sqlcon.Close();
            UIcontroller._uiCotl.showInfo("查无此人！");
        }
    }
}
