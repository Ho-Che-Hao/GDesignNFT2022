$(document).ready(function () {
    InitForm();
});

var VueForm;
window.issent = false;
var Register = function () {
	
	var message = VueForm.ErrorMsg;
	if(!VueForm.data.checkRead){
		message = message + "請詳細閱讀個資條款<br/>";
	}
	if(message === ""){
		VueForm.error = [];
		$.ajax({
			type: "POST",
			contentType: 'application/json',
			dataType: 'json',
			data: JSON.stringify({ Entity: VueForm.data}),
			url: "/api/User/Add",
			success: function (data) {
				bootbox.alert("報名成功", function () {
					location.reload();
				});
			},
			error: function (error) {
				if (error.responseJSON && error.responseJSON.Form) {
					window.issent = false;
					VueForm.error = error.responseJSON.Form;
				}
			}
		});
	}else{
		bootbox.alert(message, function () {
					
				});
	}
}

var InitForm = function () {
    var Userdata = {
        Name: "",
        IdentyCode: "",
        Email: "",
        Phone: "",
        IsForeigner: "Native",
        SchoolName: "",
        SchoolDepartment: "",
        SchoolGrade: "",
		checkRead : false,
    };
    VueForm = new Vue({
        el: '#form_data',
        data: {
            data: Userdata,
            error:[]
        },
        mounted: function() {
			var submit = this.Submit;
			$("#form_data").find("tbody").find("input").keyup(function(event) {
				if (event.keyCode === 13) {
					event.preventDefault();
					event.stopPropagation();
					if(window.issent === false){
						submit();
						setTimeout(function(){
							window.issent = false;
						}, 800);
					}
					
				}
			});
        },
        watch: {
            identyTitle: function () {

            },
			ErrorMsg:function(){
			}
        },
        computed: {
            identyTitle: function () {
                if (this.data.IsForeigner === "Native") {
                    return "身分證字號";
                } else {
                    return "居留證號碼";
                }
            },
			ErrorMsg:function(){
				var message = "";
				var that = this;
				if(that.data.Name.trim() === ""){
					message = message + "請輸入姓名<br/>"
				}
				if(that.data.IdentyCode.trim() === ""){
					message = message + "請輸入"+ this.identyTitle +"<br/>"
				}
				if(that.data.Email.trim() === ""){
					message = message + "請輸入Email<br/>"
				}
				if(that.data.Phone.trim() === ""){
					message = message + "請輸入連絡電話<br/>"
				}
				if(that.data.SchoolName.trim() === ""){
					message = message + "請輸入就讀學校<br/>"
				}
				if(that.data.SchoolDepartment.trim() === ""){
					message = message + "請輸入就讀系所<br/>"
				}
				if(that.data.SchoolGrade.trim() === ""){
					message = message + "請輸入就讀年級<br/>"
				}
				return message;
			}
			
        },
        methods: {
            Submit: function () {
				window.issent = true;
                Register();
            }
        }

    });
}