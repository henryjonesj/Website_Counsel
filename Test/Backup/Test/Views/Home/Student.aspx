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
	<script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>

  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
  <script>
  $( function() {
    $( "#datepicker" ).datepicker();
  } );
  </script>	
	 <div class="body"></div>
		<div class="grad"></div>
		<div class="header">
			<div>Online<span>Counseling</span></div>
		</div>
		<br>
		<div class="login">
				<form method="post" action="/Home/Login" runat="server">
				<asp:Label ID="Date" runat="server" Text="Choose a Date"></asp:Label>
				<input type="text" id ="datepicker">	
				<asp:Label ID="Time" runat="server" Text="Choose a Time"></asp:Label>
					
				<asp:DropDownList id="TimeList"
                    AutoPostBack="True"
                    runat="server">

                  <asp:ListItem Selected="True" Value="09:00"> 09:00 </asp:ListItem>
                  <asp:ListItem Value="09:30"> 09:30 </asp:ListItem>
                  <asp:ListItem Value="10:00"> 10:00 </asp:ListItem>
                  <asp:ListItem Value="10:30"> 10:30 </asp:ListItem>
                  <asp:ListItem Value="11:00"> 11:00 </asp:ListItem> 
						
               </asp:DropDownList> <br>

				<asp:CheckBox id="CheckBox" Text="Click here to additional information" TextAlign="Right"
                											 runat="server"/>	
				<asp:TextBox ID="MoreInfoBox" runat="server" Height="35" style="display:none;"/>	
				<script>
						 $(document).ready(function () {
     					$('#CheckBox').click(function () {
         					var $this = $(this);
         				if ($this.is(':checked')) {
             				$('#MoreInfoBox').show();
         					} else 
						{
             				$('#MoreInfoBox').hide();
         				}
     				});
 				});
				</script>		

				<input type="submit" value="Login">
					</form>
		</div>
       </div>
		
</body>

	</html>




}