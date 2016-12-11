<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


@model MVCEntityLogin.Models.LoginModel
@using(Html.BeginForm())
{
<!DOCTYPE html>
<html>
<head runat="server">
	<title></title>
	<link rel="stylesheet" href="~/Content/style.css">	
</head>
<body>
<div>
	<script type="text/javascript">
    function myFunc() { 
        
        var temp = '<%= TempData["message"] %>';
         if(temp!=null && temp!="")
            alert(temp);
    }


  
    myFunc();
</script>
	 <div class="body"></div>
		<div class="grad"></div>
		<div class="header">
			<div>Online Counseling</div>
		</div>
		<br>
		<div class="login">
				<form method="post" action="/Home/Login" runat="server">
				<input type="text" placeholder="username" name="UserName"><br>
				<input type="password" placeholder="password" name="password"><br>
				<input type="submit" value="Login">
					</form>
		</div>
       </div>
		
</body>

	</html>




}
