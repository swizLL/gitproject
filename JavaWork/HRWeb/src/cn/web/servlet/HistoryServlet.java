package cn.web.servlet;

import cn.web.dao.EmpDao;
import cn.web.dao.HistoryDao;
import cn.web.pojo.Employee;
import cn.web.pojo.History;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.List;

@WebServlet("/HistoryServlet")
public class HistoryServlet extends HttpServlet {
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        request.setCharacterEncoding("UTF-8");
        response.setCharacterEncoding("UTF-8");
        response.setContentType("text/html;charset=UTF-8");
        doGet(request, response);
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String name = request.getParameter("method");
        try {
            // 通过反射动态获取目标方法的名称，目标方法的参数类型
            Method method = this.getClass().getDeclaredMethod(name, HttpServletRequest.class, HttpServletResponse.class);
            //动态调用目标方法
            method.invoke(this, request, response);
        } catch (NoSuchMethodException | SecurityException | IllegalAccessException | IllegalArgumentException
                | InvocationTargetException e) {
            e.printStackTrace();
        }
    }

    protected void findHisList(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        HistoryDao dao = new HistoryDao();
        List<History> list = dao.selectHistoryList();
        request.setAttribute("historyList", list);
        request.getRequestDispatcher("/history_list.jsp").forward(request, response);
    }

    protected void findHisById(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String hid = request.getParameter("id");
        int id = Integer.parseInt(hid);
        HistoryDao dao = new HistoryDao();
        History his = dao.selectHisById(id);
        if (his != null) {
            request.setAttribute("his", his);
            request.getRequestDispatcher("/history_detail.jsp").forward(request, response);
        } else {
            request.getRequestDispatcher("/HistoryServlet?method=findHisList").forward(request, response);
        }
    }

    protected void toUpdateHis(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String hid = request.getParameter("id");
        int id = Integer.parseInt(hid);
        HistoryDao dao=new HistoryDao();
        History his=dao.selectHisById(id);
        if(his!=null){
            request.setAttribute("his", his);
            request.getRequestDispatcher("/history_update.jsp").forward(request, response);
        }else{
            request.getRequestDispatcher("/HistoryServlet?method=findHisList").forward(request, response);
        }
    }
    protected void doUpdateHis(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String hid = request.getParameter("id");
        int id = Integer.parseInt(hid);
        String name=request.getParameter("name");
        String gender=request.getParameter("gender");
        String date=request.getParameter("date");
        String telephone=request.getParameter("telephone");
        String email=request.getParameter("email");
        String address=request.getParameter("address");
        String education=request.getParameter("education");
        History his=new History(id, name, gender, date, telephone, email, address, education);
        HistoryDao dao=new HistoryDao();
        int i=dao.updateHistory(his);
        request.getRequestDispatcher("/HistoryServlet?method=findHisList").forward(request, response);
    }
    protected void delHis(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        HistoryDao dao=new HistoryDao();
        String hid=request.getParameter("id");
        int id=Integer.parseInt(hid);
        int i=dao.delHis(id);
        System.out.println("删除结果:"+i);
        request.getRequestDispatcher("/HistoryServlet?method=findHisList").forward(request, response);

    }
    protected void addHis(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String snum=request.getParameter("employeeNumber");
        int num=Integer.parseInt(snum);
        String name=request.getParameter("name");
        String gender=request.getParameter("gender");
        String birthday=request.getParameter("date");
        String telephone=request.getParameter("telephone");
        String email=request.getParameter("email");
        String address=request.getParameter("address");
        String education=request.getParameter("education");
        int dnum=Integer.parseInt(request.getParameter("departmentNumber"));
        int pnum=Integer.parseInt(request.getParameter("positionNumber"));
        History his=new History(num, name, gender, birthday, telephone, email, address, education, dnum, pnum);
        HistoryDao dao=new HistoryDao();
        int i=dao.insertHis(his);
        System.out.println("增加结果:"+i);
        request.getRequestDispatcher("/HistoryServlet?method=findHisList").forward(request, response);
    }
}