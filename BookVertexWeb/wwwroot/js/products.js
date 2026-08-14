var productDataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    productDataTable = $('#tblData').DataTable({
        ajax: {
            url: '/Admin/Product/GetAll',
            type: 'GET',
            dataSrc: 'data'
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

        order: [[0, 'asc']],

        language: {
            search: '',
            searchPlaceholder: 'Search by title, author, or ISBN...',
            info: 'Showing _START_ to _END_ of _TOTAL_ products',
            infoEmpty: 'Showing 0 to 0 of 0 products',
            emptyTable: 'No products found',
            zeroRecords: 'No matching products found',
            lengthMenu: '_MENU_ per page',
            processing: 'Loading products...',

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
                    placeholder: 'Search by title, author, or ISBN...'
                }
            },

            bottomStart: null,

            bottom: {
                features: [
                    'info',
                    'paging',
                    'pageLength'
                ],
                className: 'products-footer'
            },

            bottomEnd: null
        },

        columns: [
            {
                data: 'title',
                width: '25%'
            },
            {
                data: 'isbn',
                width: '17%'
            },
            {
                data: 'price',
                width: '10%',
                render: function (data) {
                    return '$' + Number(data).toFixed(2);
                }
            },
            {
                data: 'author',
                width: '18%'
            },
            {
                data: 'category.name',
                width: '15%',
                render: function (data) {
                    if (!data) {
                        return '';
                    }

                    return `
                        <span class="product-category">
                            <i class="bi bi-bookmark"></i>
                            ${data}
                        </span>
                    `;
                }
            },
            {
                data: 'id',
                width: '15%',
                orderable: false,
                searchable: false,
                render: function (data) {
                    return `
                        <div class="product-actions">
                            <a href="/Admin/Product/Upsert?id=${data}"
                               class="btn btn-sm btn-outline btn-primary product-action">
                                <i class="bi bi-pencil-square"></i>
                                <span>Edit</span>
                            </a>

                            <button type="button"
                                    onclick="Delete('/Admin/Product/Delete/${data}')"
                                    class="btn btn-sm btn-outline btn-error product-action">
                                <i class="bi bi-trash"></i>
                                <span>Delete</span>
                            </button>
                        </div>
                    `;
                }
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel',
        buttonsStyling: false,
        customClass: {
            confirmButton: 'btn btn-error mx-1',
            cancelButton: 'btn btn-ghost mx-1'
        }
    }).then(function (result) {
        if (!result.isConfirmed) {
            return;
        }

        $.ajax({
            url: url,
            type: 'DELETE',

            success: function (response) {
                if (response.success) {
                    productDataTable.ajax.reload(null, false);

                    Swal.fire({
                        title: 'Deleted!',
                        text: response.message || 'Product deleted successfully.',
                        icon: 'success',
                        buttonsStyling: false,
                        customClass: {
                            confirmButton: 'btn btn-primary'
                        }
                    });
                } else {
                    Swal.fire({
                        title: 'Error!',
                        text: response.message || 'Unable to delete product.',
                        icon: 'error',
                        buttonsStyling: false,
                        customClass: {
                            confirmButton: 'btn btn-primary'
                        }
                    });
                }
            },

            error: function () {
                Swal.fire({
                    title: 'Error!',
                    text: 'Something went wrong while deleting the product.',
                    icon: 'error',
                    buttonsStyling: false,
                    customClass: {
                        confirmButton: 'btn btn-primary'
                    }
                });
            }
        });
    });
}