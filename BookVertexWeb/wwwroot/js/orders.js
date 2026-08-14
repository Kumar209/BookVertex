var orderDataTable;

$(document).ready(function () {

    var url = window.location.search.toLowerCase();

    if (url.includes("cancelled")) {
        loadDataTable("cancelled");
    }
    else if (url.includes("shipped")) {
        loadDataTable("shipped");
    }
    else if (url.includes("pending")) {
        loadDataTable("pending");
    }
    else if (url.includes("processing")) {
        loadDataTable("processing");
    }
    else if (url.includes("approved")) {
        loadDataTable("approved");
    }
    else {
        loadDataTable("all");
    }

});


function loadDataTable(status) {

    orderDataTable = $('#tblData').DataTable({

        ajax: {
            url: '/Admin/Order/GetAll?status=' + encodeURIComponent(status),
            type: 'GET',
            dataSrc: function (json) {

                var orders = json.data || [];

                $('#orderCount').text(orders.length);

                return orders;
            }
        },

        processing: true,
        searching: true,
        ordering: true,
        paging: true,
        stateSave: true,

        pageLength: 10,

        lengthMenu: [
            [5, 10, 25, 50, 100],
            [5, 10, 25, 50, 100]
        ],

        order: [[0, 'desc']],

        language: {

            search: '',

            searchPlaceholder:
                'Search orders by name, email, or phone...',

            info:
                'Showing _START_ to _END_ of _TOTAL_ orders',

            infoEmpty:
                'Showing 0 to 0 of 0 orders',

            emptyTable:
                'No orders found',

            zeroRecords:
                'No matching orders found',

            lengthMenu:
                '_MENU_ per page',

            processing:
                'Loading orders...',

            paginate: {
                first: '«',
                previous: '‹',
                next: '›',
                last: '»'
            }

        },

        layout: {

            topStart: null,

            topEnd: {
                search: {
                    placeholder:
                        'Search orders by name, email, or phone...'
                }
            },

            bottomStart: null,

            bottom: {
                features: [
                    'info',
                    'paging',
                    'pageLength'
                ],
                className: 'orders-footer'
            },

            bottomEnd: null

        },

        columns: [

            // ================= ORDER =================

            {
                data: 'id',
                width: '12%',

                render: function (data) {

                    return `
                        <div class="order-number">

                            <div class="order-icon">
                                <i class="bi bi-box"></i>
                            </div>

                            <div>
                                <span class="order-label">
                                    Order
                                </span>

                                <span class="order-id">
                                    #${data}
                                </span>
                            </div>

                        </div>
                    `;
                }
            },


            // ================= CUSTOMER =================

            {
                data: 'name',
                width: '18%',

                render: function (data) {

                    return `
                        <span class="order-customer">
                            ${data || '—'}
                        </span>
                    `;
                }
            },


            // ================= PHONE =================

            {
                data: 'phoneNumber',
                width: '17%',

                render: function (data) {

                    if (!data) {
                        return `<span class="order-muted">—</span>`;
                    }

                    return `
                        <span class="order-phone">
                            <i class="bi bi-telephone"></i>
                            ${data}
                        </span>
                    `;
                }
            },


            // ================= EMAIL =================

            {
                data: 'applicationUser.email',
                width: '21%',

                render: function (data) {

                    if (!data) {
                        return `<span class="order-muted">—</span>`;
                    }

                    return `
                        <span class="order-email">
                            ${data}
                        </span>
                    `;
                }
            },


            // ================= STATUS =================

            {
                data: 'orderStatus',
                width: '14%',

                render: function (data) {

                    if (!data) {
                        return '';
                    }

                    var icon = 'bi-circle';
                    var className = 'order-status-default';

                    switch (data) {

                        case 'Pending':
                            icon = 'bi-clock';
                            className = 'order-status-pending';
                            break;

                        case 'Approved':
                            icon = 'bi-check-circle';
                            className = 'order-status-approved';
                            break;

                        case 'Processing':
                            icon = 'bi-arrow-repeat';
                            className = 'order-status-processing';
                            break;

                        case 'Shipped':
                            icon = 'bi-truck';
                            className = 'order-status-shipped';
                            break;

                        case 'Cancelled':
                            icon = 'bi-x-circle';
                            className = 'order-status-cancelled';
                            break;

                        case 'Refunded':
                            icon = 'bi-arrow-counterclockwise';
                            className = 'order-status-refunded';
                            break;
                    }

                    return `
                        <span class="order-status ${className}">
                            <i class="bi ${icon}"></i>
                            ${data}
                        </span>
                    `;
                }
            },


            // ================= TOTAL =================

            {
                data: 'orderTotal',
                width: '10%',

                render: function (data) {

                    return `
                        <span class="order-total">
                            $${Number(data || 0).toFixed(2)}
                        </span>
                    `;
                }
            },


            // ================= ACTIONS =================

            {
                data: 'id',
                width: '12%',
                orderable: false,
                searchable: false,

                render: function (data) {

                    return `
                        <div class="order-actions">

                            <a href="/Admin/Order/Details?orderId=${data}"
                               class="btn btn-sm btn-outline btn-primary order-action">

                                <i class="bi bi-eye"></i>

                                <span>
                                    Details
                                </span>

                            </a>

                        </div>
                    `;
                }
            }

        ]

    });

}