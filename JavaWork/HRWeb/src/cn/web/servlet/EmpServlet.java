package cn.web.servlet;

import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import cn.web.dao.EmpDao;
import cn.web.pojo.Employee;

/**
 * Servlet implementation class EmpServlet
 */
@WebServlet("/EmpServlet")
public class EmpServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
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

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		request.setCharacterEncoding("UTF-8");
		response.setCharacterEncoding("UTF-8");
		response.setContentType("text/html;charset=UTF-8");
		doGet(request, response);
	}
	
	protected void findLogin(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		HttpSession session=request.getSession();
		String employeeNumber=request.getParameter("employeeNumber");
		int num=Integer.parseInt(employeeNumber);
		String pwd=request.getParameter("password");
		EmpDao dao=new EmpDao();
		Employee emp=dao.selectLogin(num, pwd);
		if(emp==null){
			//登录失败
			response.sendRedirect("login.jsp");
		}else{
			session.setAttribute("emp", emp);
			request.getRequestDispatcher("/index.jsp").forward(request, response);
		}
	}
	protected void findEmpList(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		EmpDao dao=new EmpDao();
		List<Employee> list=dao.selectEmpList();
		request.setAttribute("empList", list);
		request.getRequestDispatcher("/employee_list.jsp").forward(request, response);
	}

	protected void delEmp(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		EmpDao dao=new EmpDao();
		String sid=request.getParameter("id");
		int id=Integer.parseInt(sid);
		int i=dao.delEmp(id);
		System.out.println("删除结果:"+i);
		request.getRequestDispatcher("/EmpServlet?method=findEmpList").forward(request, response);
		
	}
	protected void addEmp(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		String snum=request.getParameter("employeeNumber");
		int num=Integer.parseInt(snum);
		String name=request.getParameter("name");
		String password=request.getParameter("password");
		String gender=request.getParameter("gender");
		String birthday=request.getParameter("date");
		String telephone=request.getParameter("telephone");
		String email=request.getParameter("email");
		String address=request.getParameter("address");
		String education=request.getParameter("education");
		int dnum=Integer.parseInt(request.getParameter("departmentNumber"));
		int pnum=Integer.parseInt(request.getParameter("positionNumber"));
		
		Employee emp=new  Employee(num, name, gender, birthday, telephone, email, address, education, dnum, pnum, password);
		
		EmpDao dao=new EmpDao();
		int i=dao.insertEmp(emp);
		System.out.println("增加结果:"+i);
		request.getRequestDispatcher("/EmpServlet?method=findEmpList").forward(request, response);
		
	}
	protected void findEmpById(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		String sid=request.getParameter("id");
		int id=Integer.parseInt(sid);
		EmpDao dao=new EmpDao();
		Employee emp=dao.selectEmpById(id);
		if(emp!=null){
			request.setAttribute("emp", emp);
			request.getRequestDispatcher("/employee_detail.jsp").forward(request, response);
		}else{
			request.getRequestDispatcher("/EmpServlet?method=findEmpList").forward(request, response);
		}
	}
	protected void toUpdateEmp(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		String sid=request.getParameter("id");
		int id=Integer.parseInt(sid);
		EmpDao dao=new EmpDao();
		Employee emp=dao.selectEmpById(id);
		if(emp!=null){
			request.setAttribute("emp", emp);
			request.getRequestDispatcher("/employee_update.jsp").forward(request, response);
		}else{
			request.getRequestDispatcher("/EmpServlet?method=findEmpList").forward(request, response);
		}
	}
	
	protected void doUpdateEmp(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		String sid=request.getParameter("id");
		int id=Integer.parseInt(sid);
		String name=request.getParameter("name");
		String password=request.getParameter("password");
		String gender=request.getParameter("gender");
		String date=request.getParameter("date");
		String telephone=request.getParameter("telephone");
		String email=request.getParameter("email");
		String address=request.getParameter("address");
		String education=request.getParameter("education");
		Employee emp=new Employee(id, name, gender, date, telephone, email, address, education, password);
		EmpDao dao=new EmpDao();
		int i=dao.updateEmp(emp);
		request.getRequestDispatcher("/EmpServlet?method=findEmpList").forward(request, response);
	}


}
