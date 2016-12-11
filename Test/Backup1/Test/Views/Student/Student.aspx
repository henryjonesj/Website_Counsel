<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


@model MVCEntityLogin.Models.StudentModel
@using(Html.BeginForm())
{
<!DOCTYPE html>
 <style>
        .select_join {
   
width: 170px;
   right: 900px;
   height: 28px;
   overflow: hidden;
   background: url('http://s24.postimg.org/lyhytocf5/dropdown.png') no-repeat right #FEFEFE;
   border: #FEFEFE 1px solid;
-webkit-border-radius: 3px;
border-radius: 3px;
-webkit-box-shadow: inset 0px 0px 10px 1px #FEFEFE;
box-shadow: inset 0px 0px 10px 1px #FEFEFE;
   }
.select_join select {
  
   background: transparent;
   width: 170px;
   font-size:10pt;
   color:grey;
      right: 900px;
   border: 0;
   border-radius: 0;
   height: 28px;
   -webkit-appearance: none;
   
   }
   .select_join select:focus {
    outline: none;

}
    </style>
<html>
<head runat="server">
	<title></title>
	<link rel="stylesheet" href="~/Content/style.css">	
    <link href="https://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet">
    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>
    <script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
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
			<div>Welcome Student</div>
		</div>
		<br>
		<div class="login">
				<form method="post" action="/Student/Student" runat="server">

				
				<input type="text" id ="datepicker" placeholder="Choose a Date Time" name="Date" >	
				<label ID="Time" runat="server" Text="Choose a Time"/>
          
                 <br /><br/></br/>
                 <div class ="select_join" style="margin-left:15px">
                 <select id="TimeList" onchange="run()" name="Time" > 
                 <option value="09:00:00 AM">09:00</option>
                 <option value="09:30:00 AM">09:30</option>
                 <option value="10:00:00 AM">10:00</option>
                 <option value="10:30:00 AM">10:30</option>
                 <option value="03:00:00 PM">03:00</option>
                 <option value="03:30:00 PM">03:30</option>
                 <option value="04:00:00 PM">04:00</option>
                 <option value="04:30:00 PM">04:30</option>
                    
                 </select> </div> <br><br>

                    <script>
                        function run()
                        {
                            @model.Time= document.getElementById("TimeList").value;
                         
                         }       
                    </script>

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

				<input type="submit" value="Schedule" >
					</form>
		</div>
       </div>
		
</body>

	</html>




}