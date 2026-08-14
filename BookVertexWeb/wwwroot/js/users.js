var userDataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    userDataTable = $('#tblData').DataTable({

        ajax: {
            url: '/Admin/User/GetAll',
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
            searchPlaceholder: 'Search users by name, email, or phone...',
            info: 'Showing _START_ to _END_ of _TOTAL_ users',
            infoEmpty: 'Showing 0 to 0 of 0 users',
            emptyTable: 'No users found',
            zeroRecords: 'No matching users found',
            lengthMenu: '_MENU_ per page',
            processing: 'Loading users...',

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
                    placeholder: 'Search users by name, email, or phone...'
                }
            },

            bottomStart: null,

            bottom: {
                features: [
                    'info',
                    'paging',
                    'pageLength'
                ],
                className: 'users-footer'
            },

            bottomEnd: null
        },

        columns: [

            {
                data: 'name',
                width: '21%',

                render: function (data) {

                    return `
                        <div class="user-person">
                            <div class="user-avatar">
                                <i class="bi bi-person"></i>
                            </div>

                            <span class="user-name">
                                ${escapeHtml(data || 'Unknown User')}
                            </span>
                        </div>
                    `;
                }
            },

            {
                data: 'email',
                width: '24%',

                render: function (data) {

                    if (!data) {
                        return `<span class="user-muted">—</span>`;
                    }

                    return `
                        <span class="user-email">
                            <i class="bi bi-envelope"></i>
                            ${escapeHtml(data)}
                        </span>
                    `;
                }
            },

            {
                data: 'phoneNumber',
                width: '17%',

                render: function (data) {

                    if (!data) {
                        return `<span class="user-muted">—</span>`;
                    }

                    return `
                        <span class="user-phone">
                            <i class="bi bi-telephone"></i>
                            ${escapeHtml(data)}
                        </span>
                    `;
                }
            },

            {
                data: 'role',
                width: '13%',

                render: function (data) {

                    if (!data) {
                        return `<span class="user-role user-role-default">No Role</span>`;
                    }

                    var roleClass = 'user-role-default';
                    var icon = 'bi-person';

                    if (data === 'Admin') {
                        roleClass = 'user-role-admin';
                        icon = 'bi-shield-check';
                    }
                    else if (data === 'Employee') {
                        roleClass = 'user-role-employee';
                        icon = 'bi-person-badge';
                    }
                    else if (data === 'Customer') {
                        roleClass = 'user-role-customer';
                        icon = 'bi-person';
                    }

                    return `
                        <span class="user-role ${roleClass}">
                            <i class="bi ${icon}"></i>
                            ${escapeHtml(data)}
                        </span>
                    `;
                }
            },

            {
                data: {
                    id: 'id',
                    lockoutEnd: 'lockoutEnd'
                },
                width: '25%',
                orderable: false,
                searchable: false,

                render: function (data) {

                    var isLocked = false;

                    if (data.lockoutEnd) {
                        var lockout = new Date(data.lockoutEnd).getTime();
                        var today = new Date().getTime();

                        isLocked = lockout > today;
                    }

                    return `
                        <div class="user-actions">

                            <button type="button"
                                    onclick="LockUnlock('${data.id}')"
                                    class="btn btn-sm btn-outline ${isLocked ? 'btn-error' : 'btn-primary'} user-action">

                                <i class="bi bi-${isLocked ? 'lock-fill' : 'unlock-fill'}"></i>

                                <span>
                                    ${isLocked ? 'Unlock' : 'Lock'}
                                </span>

                            </button>

                            <a href="/Admin/User/RoleManagment?userId=${data.id}"
                               class="btn btn-sm btn-outline btn-secondary user-action">

                                <i class="bi bi-person-badge"></i>

                                <span>
                                    Role
                                </span>

                            </a>

                            <a href="/Admin/User/ChangePassword?userId=${data.id}"
                               class="btn btn-sm btn-outline btn-warning user-action">

                                <i class="bi bi-key-fill"></i>

                                <span>
                                    Password
                                </span>

                            </a>

                        </div>
                    `;
                }
            }

        ]

    });
}


function LockUnlock(id) {

    $.ajax({
        type: 'POST',
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: 'application/json',

        success: function (response) {

            if (response.success) {

                toastr.success(response.message);

                userDataTable.ajax.reload(null, false);

            }
            else {

                toastr.error(
                    response.message || 'Unable to update user status.'
                );

            }
        },

        error: function () {

            toastr.error(
                'Something went wrong while updating the user.'
            );

        }
    });
}


function escapeHtml(value) {

    return String(value)
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#039;');
}