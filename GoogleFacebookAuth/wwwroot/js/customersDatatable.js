

$(document).ready(function () {
    $('#Customers').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/api/customers",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false,
        },
        {
            "targets": [6],
            "sorting": false,
        }],

        "columns": [
            { "data": "Id", "name": "Id", "autowidth": true },
            { "data": "FirstName", "name": "FirstName", "autowidth": true },
            { "data": "LastName", "name": "LastName", "autowidth": true },
            { "data": "Contact", "name": "Contact", "autowidth": true },
            { "data": "Email", "name": "Email", "autowidth": true },

            { "data": "DateOfBirth", "name": "Date Of Birth", "autowidth": true },
            {
                "render": function (data, row) { return "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.id + "'); >Delete</a>"; }
            },

        ]
    });
});

