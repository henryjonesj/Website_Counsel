<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


@model MVCEntityLogin.Models.StudentModel
@using(Html.BeginForm())
{
<!DOCTYPE html>

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

<script type="text/javascript">
    function myFunc() { 
        var username = '<%= Session["UserName"] %>';
        if(username==null || username=="")
            window.location.href = "/Home/Login";  
        
        var temp = '<%= TempData["message"] %>';
         if(temp!=null && temp!="")
            alert(temp);
    }


  
    myFunc();
</script>
        
	 <div class="body"></div>
		<div class="grad"></div>
		<div class="header">
			<div>Welcome Student</div>
		</div>
		<br/>
		<div class="login">
				<form method="post" action="/Student/Student" runat="server">

				
				<input type="text" id ="datepicker" placeholder="Choose a Date" name="Date" >	
          
                 <br /> <br /> <br />
           
                 <select name="Time" id="TimeList" onchange="run()">
                 <option selected>Choose Time</option>
                 <option value="09:00:00 AM">09:00</option>
                 <option value="09:30:00 AM">09:30</option>
                 <option value="10:00:00 AM">10:00</option>
                 <option value="10:30:00 AM">10:30</option>
                 <option value="03:00:00 PM">03:00</option>
                 <option value="03:30:00 PM">03:30</option>
                 <option value="04:00:00 PM">04:00</option>
                 <option value="04:30:00 PM">04:30</option>
                    
                 </select> <br><br>

                    <script>
                        function run()
                        {
                            @model.Time= document.getElementById("TimeList").value;
                         
                         }       
                    </script>

				 <label><input type="checkbox" id="CheckBox"/>Check here to enter additional info</label>

				<textarea id ="MoreInfoBox" name="MoreInfo" placeholder="Additional Information" style="display:none">
                    </textarea>
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