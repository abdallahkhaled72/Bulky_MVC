//var dataTable;

//$(document).ready(function () {
//    // Properly extract the status parameter from URL
//    const urlParams = new URLSearchParams(window.location.search);
//    const status = urlParams.get('status') || 'all';

//    console.log('Extracted status from URL:', status); // Debug log

//    loadDataTable(status);
//});

//function loadDataTable(status) {
//    console.log('Loading data table with status:', status);

//    // Destroy existing DataTable if it exists
//    if ($.fn.DataTable.isDataTable('#tblData')) {
//        $('#tblData').DataTable().destroy();
//        $('#tblData').empty(); // Clear the table
//    }

//    dataTable = $('#tblData').DataTable({
//        "ajax": {
//            url: '/admin/order/getall?status=' + status,
//            type: 'GET',
//            dataType: 'json'
//        },
//        "columns": [
//            { data: 'id', "width": "5%" },
//            { data: 'name', "width": "25%" },
//            { data: 'phoneNumber', "width": "20%" },
//            { data: 'applicationUser.email', "width": "20%" },
//            { data: 'orderStatus', "width": "10%" },
//            {
//                data: 'orderTotal',
//                "width": "10%",
//                "render": function (data) {
//                    return '$' + parseFloat(data).toFixed(2);
//                }
//            },
//            {
//                data: 'id',
//                "render": function (data) {
//                    return `<div class="w-75 btn-group" role="group">
//                        <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2">
//                            <i class="bi bi-pencil-square"></i> Details
//                        </a>
//                    </div>`;
//                },
//                "width": "10%"
//            }
//        ],
//        "language": {
//            "emptyTable": "No orders found for the selected status."
//        }
//    });
//}







var dataTable; // Declare variable

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    }
    else {
        if (url.includes("completed")) {
            loadDataTable("completed");
        }
        else {
            if (url.includes("pending")) {
                loadDataTable("pending");
            }
            else {
                if (url.includes("approved")) {
                    loadDataTable("approved");
                }
                else {
                    loadDataTable("all");
                }
            }
        }
    }
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/order/getall?status=' + status },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "25%" },
            { data: 'phoneNumber', "width": "20%" },
            { data: 'applicationUser.email', "width": "20%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i></a>               
                    
                    </div>`
                },
                "width": "10%"
            }
        ]
    });
}