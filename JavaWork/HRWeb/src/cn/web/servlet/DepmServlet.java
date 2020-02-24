package cn.web.servlet;

import cn.web.dao.DepmDao;
import cn.web.pojo.Department;
import sun.security.util.ManifestEntryVerifier;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.List;

@WebServlet("/DepmServlet")
public class DepmServlet extends HttpServlet {
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        request.setCharacterEncoding("UTF-8");
        response.setCharacterEncoding("UTF-8");
        response.setContentType("text/html;charset=UTF-8");
        doGet(request, response);
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        //获取【方法】参数名称
        String name=request.getParameter("method");
        try {
            // 通过反射动态获取目标方法的名称，目标方法的参数类型
            Method method=this.getClass().getDeclaredMethod(name, HttpServletRequest.class,HttpServletResponse.class);
            //动态调用目标方法
            method.invoke(this, request,response);
        } catch (NoSuchMethodException | SecurityException | IllegalAccessException | IllegalArgumentException
                | InvocationTargetException e) {
            e.printStackTrace();
        }
    }
    protected void findDepmList(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        DepmDao dao=new DepmDao();
        List<Department> list=dao.selectDepmList();
        request.setAttribute("depmList", list);
        request.getRequestDispatcher("/department_list.jsp").forward(request, response);
    }
    protected void findDepmById(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String sid=request.getParameter("id");
        int id=Integer.parseInt(sid);
        DepmDao dao=new DepmDao();
        Department depm=dao.selectDempById(id);
        if(depm!=null){
            request.setAttribute("depm", depm);
            request.getRequestDispatcher("/department_detail.jsp").forward(request, response);
        }else{
            request.getRequestDispatcher("/DepmServlet?method=findDepmList").forward(request, response);
        }
    }

    protected void toUpdateDepm(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String sid=request.getParameter("id");
        int id=Integer.parseInt(sid);
        DepmDao dao=new DepmDao();
        Department depm=dao.selectDempById(id);
        if(depm!=null){
            request.setAttribute("depm", depm);
            request.getRequestDispatcher("/department_update.jsp").forward(request, response);
        }else{
            request.getRequestDispatcher("/DepmServlet?method=findDepmList").forward(request, response);
        }
    }
    protected void doUpdateDepm(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String sid=request.getParameter("id");
        int id=Integer.parseInt(sid);
        String manager=request.getParameter("manager");
        String telephone=request.getParameter("telephone");
        String address=request.getParameter("address");
        String notes=request.getParameter("notes");
        Department emp=new Department(id,manager, telephone, address,notes);
        DepmDao dao=new DepmDao();
        int i=dao.updateDepm(emp);
        request.getRequestDispatcher("/DepmServlet?method=findDepmList").forward(request, response);
    }
}
