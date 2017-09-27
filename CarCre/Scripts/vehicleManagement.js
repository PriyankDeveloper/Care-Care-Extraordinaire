$(function () {
    $("#grid").jqGrid({
        url: "/Vehicle/GetTodoLists",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['OwnerId', 'VechicleDealer', 'VechicleYear', 'VehicleId', 'VehicleMark', 'VehicleModel', 'VINNumber'],
        colModel: [
            { hidden: true, name: 'OwnerId', index: 'OwnerId', editable: true },
            { name: 'VechicleDealer', index: 'VechicleDealer', width: 150, stype: 'text', sortable: true, editable: true },
            { name: 'VechicleYear', index: 'VechicleYear', width: 150, stype: 'text',editable: true },
            { name: 'VehicleId', index: 'VehicleId', width: 60, editable: true },
            { name: 'VehicleMark', index: 'VehicleMark', width: 80, stype: 'text', align: "right", editable: true },
            { name: 'VehicleModel', index: 'VehicleModel', width: 40, stype: 'text', align: "right", editable: true },
            { name: 'VINNumber', index: 'VINNumber', width: 150, stype: 'text', sortable: false, editable: true }],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        height: '100%',
        viewrecords: true,
        caption: 'Todo List',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "1"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: true, add: true, del: true, search: false, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: '/Vehicle/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/Vehicle/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "/Vehicle/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });
});