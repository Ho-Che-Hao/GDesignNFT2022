$(document).ready(function () {
    //Register();
    InitForm();
});

var VueForm;

var Register = function () {
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
                VueForm.error = error.responseJSON.Form;
            }
        }
    });
    //console.log(Userdata);
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
    };
    VueForm = new Vue({
        el: '#form_data',
        data: {
            data: Userdata,
            error:[]
        },
        /*mounted: {

        },*/
        watch: {
            identyTitle: function () {

            }
        },
        computed: {
            identyTitle: function () {
                if (this.data.IsForeigner === "Native") {
                    return "身分證字號";
                } else {
                    return "居留證號碼";
                }
            }
        },
        methods: {
            Submit: function () {
                Register();
            }
        }

    });
}