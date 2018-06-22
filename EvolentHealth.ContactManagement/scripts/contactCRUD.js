$.validator.setDefaults({
    debug: true,
    success: "valid"
});

$(document).ready(function () {         
    contactList();
    $("#contactForm").validate({
        rules: {
            firstname: "required",
            lastname: "required",
            email: {
                required: true,
                email: true
            },

            phonenumber: {
                required: true,      
                phoneUS: true
            },

            status: "required"
        },
        messages: {
            firstname: "Please enter first name",
            lastname: "Please enter last name",

            email: "Please enter a valid email address",
            phonenumber: "Please enter a valid phone number",
            status: "Please select status",
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            // Add the `help-block` class to the error element
            error.addClass("help-block");

            if (element.prop("type") == "radio") {
                error.inser(element.parent("div"));
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".col-sm-5").addClass("has-error").removeClass("has-success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".col-sm-5").addClass("has-success").removeClass("has-error");
        }
    });
        });
       
 

function contactList() {
    // Call Web API to get a list of Contact
    $.ajax({
        url: '/api/contacts',
        type: 'GET',
        dataType: 'json',
        success: function (contacts) {
            contactListSuccess(contacts)       
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}


function contactListSuccess(contacts) {
    // Iterate over the collection of data
    $.each(contacts, function (index, contact) {
        // Add a row to the Contact table
        contactAddRow(contact);
    });
}

function contactAddRow(contact) {

    // Check if <tbody> tag exists, add one if not
    if ($("#contactTable tbody").length == 0) {
        $("#contactTable").append("<tbody></tbody>");
    }
    // Append row to <table>
    $("#contactTable tbody").append(
      contactBuildTableRow(contact));
}

function contactBuildTableRow(contact) {
    var ret =
      "<tr>" +
      "<td>" +
    "<button type='button' " +
       "onclick='contactGet(this);' " +
       "class='btn btn-default' " +
       "data-id='" + contact.ContactId + "'>" +
       "<span class='glyphicon glyphicon-edit' />"
     + "</button>" +
  "</td >" +
   "<td>" + contact.ContactId + "</td>" +
       "<td>" + contact.FirstName + "</td>" +
       "<td>" + contact.LastName + "</td>"
        + "<td>" + contact.Email + "</td>"
        + "<td>" + contact.PhoneNumber + "</td>"
        + "<td>" + contact.IsActive + "</td>" +
       "<td>" +
    "<button type='button' " +
       "onclick='contactDelete(this);' " +
       "class='btn btn-default' " +
       "data-id='" + contact.ContactId + "'>" +
       "<span class='glyphicon glyphicon-remove' />" +
    "</button>" +
  "</td>" +
      "</tr>";
    return ret;
}

function contactGet(ctl)
{
    var contactid = $(ctl).data("id");
    // Call Web API to get a particular contact
    $.ajax({
        url: '/api/contacts/' + contactid,
        type: 'GET',
        dataType: 'json',
        success: function (contact) {

            contactToFields(contact);
            $("#updateButton").text("Update");
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });

}




function contactDelete(ctl) {
    if (confirm("Are you sure you want to delete?")) {
        var id = $(ctl).data("id");

        $.ajax({
            url: "/api/Contacts/" + id,
            type: 'DELETE',
            success: function (contact) {
                $(ctl).parents("tr").remove();
            },
            error: function (request, message, error) {
                handleException(request, message, error);
            }
        });
    }
    return false;
}
function handleException(request, message,
                 error) {
    var msg = "";
    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON != null) {
        msg += "Message" +
            request.responseJSON.Message + "\n";
    }
    alert(msg);
}

function contactToFields(contact) {
    debugger;
    $("#contactid").val(contact.ContactId);
    $("#firstname").val(contact.FirstName);
    $("#lastname").val(contact.LastName);
    $("#email").val(contact.Email);
    $("#phonenumber").val(contact.PhoneNumber);
    $("#status").val(contact.IsActive);
    if (contact.IsActive) {
        $("#status_active").prop("checked", true);
      
    }
    else 
    {
        $("#status_inactive").prop("checked", true);
    }
}

var Contact = {
    ContactId: 0,
    FirstName: "",
    LastName: "",
    Email: "",
    PhoneNumber: "",
    IsActive: ""

}

// Handle click event on Update button
function updateClick() {
    // Build Contact object from inputs
    debugger;
    if ($("#contactForm").valid())
        {
            Contact = new Object();
            Contact.ContactId = $("#contactid").val();
            Contact.FirstName = $("#firstname").val();
            Contact.LastName =  $("#lastname").val();
            Contact.Email = $("#email").val();
            Contact.PhoneNumber = $("#phonenumber").val();
            //Contact.IsActive = $("#status").val();

            Contact.IsActive = $("input[name='status']:checked").val();
            if ($("#updateButton").text().trim() ==
                      "Add") {
                contactAdd(Contact);
            }

            else {
                contactUpdate(Contact);
            }
    }
}


function contactAdd(contact) {
    $.ajax({
        url: "/api/Contacts",
        type: 'POST',
        contentType:
           "application/json;charset=utf-8",
        data: JSON.stringify(contact),
        success: function (contact) {
            contactAddSuccess(contact);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function contactUpdate(contact) {
    $.ajax({
        url: "/api/Contacts/"+contact.ContactId,
        type: 'PUT',
        contentType:
           "application/json;charset=utf-8",
        data: JSON.stringify(contact),
        success: function (contact) {
            contactUpdateSuccess(contact);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function contactUpdateSuccess(contact) {
    debugger;
    contactUpdateInTable(contact);
   
}

function contactUpdateInTable(contact) {
    // Find Contact in <table>
    var row = $("#contactTable button[data-id='" +
       contact.ContactId + "']").parents("tr")[0];
    // Add changed Contact to table
    $(row).after(contactBuildTableRow(contact));
    // Remove original Contact
    $(row).remove();
    formClear(); // Clear form fields
    // Change Update Button Text
    $("#updateButton").text("Add");
}
function contactAddSuccess(contact) {
    contactAddRow(contact);
    formClear();
}

function formClear() {
    $("#contactid").val("");
    $("#firstname").val("");
    $("#lastname").val("");
    $("#email").val("");
    $("#phonenumber").val("");
    $('input[name="status"]').prop('checked', false);
}
// Handle click event on Add button
function addClick() {
    formClear();
    $("#updateButton").text("Add");
}

 
    $("#contactForm").validate({
        rules: {
            firstname: "required",
            lastname: "required",
            email: {
                required: true,
                email: true
            },
            phonenumber: {
                required: true,
                minlength: 10
            },

            status: "required"
        },
        messages: {
            firstname: "Please enter firstname",
            lastname: "Please enter lastname",

            email: "Please enter a valid email address",
            phonenumber: "Please enter phone number",
            status: "Please select status",
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            // Add the `help-block` class to the error element
            error.addClass("help-block");

            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.parent("label"));
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".col-sm-5").addClass("has-error").removeClass("has-success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".col-sm-5").addClass("has-success").removeClass("has-error");
        }
    });

 
 