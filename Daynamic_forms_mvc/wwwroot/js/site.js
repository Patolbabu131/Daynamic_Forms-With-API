$(document).ready(function () {
    bindDatatable();
});

var partial = function () {
    $.ajax({

        url: "/Home/GetData/",

        success: function (data) {
            alert(data);
        }
    })
}

//data table
function bindDatatable() {
    datatable = $('#Forms_Table')
        .DataTable
        ({
            "sAjaxSource": "/Home/GetData",
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "filter": true,
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },
            "columns": [
                {

                    "data": "fid",
                    render: function (data, type, row, meta) {
                        return row.fid
                    }
                },
                {
                    "data": "subject",
                    render: function (data, type, row, meta) {
                        return '<a href="#" data-bs-toggle="tooltip" style="text-decoration:none; color: black" onclick="Emp_Edit(' + row.subject + ')">' + row.subject + '</a>';
                    }
                },
                {

                    "data": "description",
                    render: function (data, type, row, meta) {
                        return row.description
                    }
                },


                {
                    render: function (data, type, row, meta) {
                        return '<button type="button" class="btn btn-primary" onclick ="DeleteCall(' + row.fid + ')">Delete </button> | <a type="button" class="btn btn-secondary" onclick ="EditCall(' + row.fid + ')">Edit</a>';
                    }
                },
                //{
                //    "searchable": true,
                //    render: function (data, type, row, meta) {
                //        return '<a href="home/delete/>delete</a> || <button type="button" class="button2" onclick = "Add_Salary(' + row.emp_No + ')" > Add Salary</button> | <button type="button" class="button2" onclick = "sal_details(' + row.emp_No + ')" > Salary Details</button>';
                //    }
                //},
            ]
        });
}

//list table
$("#addToList").click(function () {
    if ($.trim($("#qquestionsv").val()) == "" || $.trim($("#qtype").val()) == "") return;
    var qquestionsv = $("#qquestionsv").val(),
        qtype = $("#qtype").val(),
        ItemTable = $("#ItemTable tbody");
    var productItem = '<tr><td>' + qquestionsv + '</td><td>' + qtype + '</td><td><a data-itemId="0" href="#" class="deleteItem">Remove</a></td></tr>';
    ItemTable.append(productItem);
    clearItem();
});

function saveForm(data) {
    $.ajax({
        type: "POST",
        url: "/Home/SaveForms",
        data: data,
        success: function (result) {
            alert(result);
            window.location.replace("/home/index");
        },
        error: function () {
            alert("Error!")
        }
    });
}

function DeleteCall(id) {
    var confirmation = confirm("Are you sure to delete this Employee...");
    if (confirmation) {
        $.ajax({
            url: '/home/Delete/?id=' + id,
            success: function (result) {
                alert(result);
                window.location.reload("/home/index");
            },
            error: function (error) {
                alert('error; ' + eval(error));
            }
        })
    }
}

function EditCall(id) {
    $.ajax({
        url: '/home/GetEdit/?id=' + id,
        success: function (data) {
            $.ajax({
                url: "/home/Edit",
                success: function (form) {
                    $('#CreateContainer').html(form);
                    $("#Editpars").modal('show');
                    $(".fid").val(data[0].fid);
                    $(".subject").val(data[0].subject);
                    $(".description").val(data[0].description);
                    var length = data[0].question.length
                    for (var i = 1; i < length; i++) {
                        $('#parent .questiondiv').clone().find('input').val('').end().appendTo("#morequestion");
                    }
                    for (var i = 0; i < length; i++) {
                     
                        $($(".qid")[i]).val(data[0].question[i].qid) 
                        $($(".question")[i]).val(data[0].question[i].questions) 
                        $($(".questiontype")[i]).val(data[0].question[i].anstype) 
                    }
                    
                }
            });
        }
    }); 
}

function Edit() {
    var input=$(".fid")
    var inputs = $(".question");
    var inputs2 = $(".questiontype");
    var quesionslist = [];
    for (var i = 0; i < inputs.length; i++) {
        quesionslist.push({
            FormsId: $(input).val(),
            questions: $(inputs[i]).val(),
            anstype: $(inputs2[i]).val()
        });
    }
    var data = {

        fid:$(".fid").val(),
        subject: $(".subject").val(),
        description: $(".description").val(),
        Question: quesionslist
    };
    $.ajax({
        type: "POST",
        url: "/Home/PostEdit",
        data: data,
        success: function (result) {
            alert(result);
            window.location.replace("/home/index");
        },
        error: function () {
            alert("Error!")
        }
    });
}

//remove item
function clearItem() {
    $("#qquestionsv").val('');
    $("#qtype").val('');
}

$(document).on('click', 'a.deleteItem', function (e) {
    e.preventDefault();
    var $self = $(this);
    if ($(this).attr('data-itemId') == "0") {
        $(this).parents('tr').css("background-color", "#ff6347").fadeOut(800, function () {
            $(this).remove();
        });
    }
});

function Create_Form() {
    var inputs = $(".question");
    var inputs2 = $(".questiontype");
    var quesionslist = [];
    for (var i = 0; i < inputs.length; i++) {
        quesionslist.push({
            questions: $(inputs[i]).val(),
            anstype: $(inputs2[i]).val()
        });
    }

    var data = {
        subject: $("#fsubject").val(),
        description: $("#fdescription").val(),
        Question: quesionslist
    };

    $.when(saveForm(data)).then(function (response) {
        console.log(response);
    }).fail(function (err) {
        console.log(err);
    });
}

function addquestion() {
    $('#parent .questiondiv').clone().find('input').val('').end().appendTo("#morequestion");
}


$(document).on('click', 'a.deleteItem', function (e) {
    e.preventDefault();
    var $self = $(this);
    if ($(this).attr('data-itemId') == "0") {
        $(this).parents('.questiondiv').css("background-color", "#ff6347").fadeOut(800, function () {
            $(this).remove();
        });
    }
});