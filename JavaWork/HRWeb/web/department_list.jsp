<%@ page language="java" contentType="text/html; charset=UTF-8"
         pageEncoding="UTF-8"%>
<%@ page import="java.util.*"%>
<%@ page import="cn.web.pojo.Department" %>
<% String path = request.getContextPath(); %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>HR医院 - 部门管理列表</title>
    <meta name="keywords" content="">
    <meta name="description" content="">

    <link rel="shortcut icon" href="favicon.ico">
    <link href="<%=path %>/css/bootstrap.min.css?v=3.3.6" rel="stylesheet">
    <link href="<%=path %>/css/font-awesome.css?v=4.4.0" rel="stylesheet">

    <!-- Data Tables -->
    <link href="<%=path %>/css/plugins/dataTables/dataTables.bootstrap.css"
          rel="stylesheet">
    <link href="<%=path %>/css/animate.css" rel="stylesheet">
    <link href="<%=path %>/css/style.css?v=4.1.0" rel="stylesheet">

    <link rel="stylesheet" type="text/css" href="<%=path %>/dist/sweetalert.css">
</head>
<body class="gray-bg">
<div class="wrapper wrapper-content animated fadeInRight">
    <div class="row">
        <div class="col-sm-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>部门信息列表</h5>
                </div>
                <div class="ibox-content">
                    <table class="table table-striped table-bordered table-hover dataTables-example">
                        <thead>
                        <tr>
                            <th>序号</th>
                            <th>部门编号</th>
                            <th>部门</th>
                            <th>负责人</th>
                            <th>电话</th>
                            <th>地址</th>
                        </tr>
                        </thead>
                        <tbody>
                        <%
                            List<Department> pe=(List<Department>)request.getAttribute("depmList");
                            int index=1;
                            for(Department department : pe){
                        %>
                        <tr class="gradeA">
                            <td><%=index++ %></td>
                            <td><%=department.getDepartmentnumber() %></td>
                            <td><%=department.getName() %></td>
                            <td><%=department.getManager()%></td>
                            <td><%=department.getTelephone()%></td>
                            <td><%=department.getAddress() %></td>
                            <td>
                                <a href="<%=path %>/DepmServlet?method=findDepmById&id=<%=department.getId()%>" class="btn btn-info">查看</a>&nbsp;&nbsp;
                                <a href="<%=path %>/DepmServlet?method=toUpdateDepm&id=<%=department.getId()%>" class="btn btn-primary">修改</a>
                        </tr>
                        <%
                            }
                        %>
                        </tbody>
                    </table>

                </div>
            </div>
        </div>
    </div>
</div>

<!-- 全局js -->
<script src="<%=path %>/js/jquery.min.js?v=2.1.4"></script>
<script src="<%=path %>/js/bootstrap.min.js?v=3.3.6"></script>
<script src="<%=path %>/js/plugins/jeditable/jquery.jeditable.js"></script>

<!-- Data Tables -->
<script src="<%=path %>/js/plugins/dataTables/jquery.dataTables.js"></script>
<script src="<%=path %>/js/plugins/dataTables/dataTables.bootstrap.js"></script>

<!-- 自定义js -->
<script src="<%=path %>/js/content.js?v=1.0.0"></script>

<!-- layer javascript -->
<script src="js/plugins/layer/layer.min.js"></script>
</body>
</html>
