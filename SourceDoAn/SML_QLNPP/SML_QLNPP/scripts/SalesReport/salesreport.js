$(document).ready(function () {
    
    var table = $('#order-list').DataTable();
    
    $("#searchListOrder").on('click', function () {
        var idDistributor = $("#idDistributor").val();
        if (!idDistributor) {
            idDistributor = "0";
        }
        var month = $("#month").val();
        if (!month) {
            month = "0";
        }
        var quartar = $("#quartar").val();
        if (!quartar) {
            quartar = "0";
        }
        var year = $("#year").val();
        if (!year) {
            year = "0";
        }
        debugger;
        var url = 'SearchListOrder';
        console.log(status);
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            beforeSend: function () {
                loading = true;
                $('#loading').show();

            },
            //int? idDistributor, int month, int quartar, int year
            data: {
                month: parseInt(month),
                quartar: parseInt(quartar),
                year: parseInt(year),
                idDistributor: parseInt(idDistributor),
            },
            success: function (data) {
                if (data) {
                    debugger;
                    $('#loading').hide();
                    table.clear();
                    var result = data.map(function (item) {
                        var rs = [];
                        var date = new Date(item.CreatedDate);

                        rs.push(item.idOrder);
                        rs.push(date.getHours() + ':' + date.getMinutes() + ' ' + +(date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
                        //rs.push(item.CreateDate);
                        rs.push(item.name);
                        rs.push(item.staffName);
                        rs.push(item.Total);
                        return rs;
                    });

                    table.rows.add(result);
                    table.draw();



                }
                else {
                    $('#loading').hide();
                    alert('Tìm kiếm không thành công!')
                };
            }
        });
    })
    $('#order-list tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input class="FClass" style="width:150px;" type="text" id="' + title.replace(' ', '_') + '" placeholder="Search ' + title + '" />');
    });
   
    table.columns().every(function () {
        var that = this;
        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that.search(this.value).draw();
            }
        });
    });
});

$(document).ready(function () {

    var table = $('#bill-list').DataTable();

    $("#searchListBill").on('click', function () {
        var idDistributor = $("#idDistributor").val();
        if (!idDistributor) {
            idDistributor = "0";
        }
        var month = $("#month").val();
        if (!month) {
            month = "0";
        }
        var quartar = $("#quartar").val();
        if (!quartar) {
            quartar = "0";
        }
        var year = $("#year").val();
        if (!year) {
            year = "0";
        }
        debugger;
        var url = 'SearchListBill';
        console.log(status);
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            beforeSend: function () {
                loading = true;
                $('#loading').show();

            },
            //int? idDistributor, int month, int quartar, int year
            data: {
                month: parseInt(month),
                quartar: parseInt(quartar),
                year: parseInt(year),
                idDistributor: parseInt(idDistributor),
            },
            success: function (data) {
                if (data) {
                    debugger;
                    $('#loading').hide();
                    table.clear();
                    var result = data.map(function (item) {
                        var rs = [];
                        var date = new Date(item.createdDate);

                        rs.push(item.idBill);
                        //rs.push(item.CreateDate);
                        rs.push(item.staffName);
                        rs.push(item.name);
                        rs.push(date.getHours() + ':' + date.getMinutes() + ' ' + +(date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
                        rs.push(item.purchase);
                        rs.push(item.types);
                        rs.push(item.description)
                        return rs;
                        //x.idBill, x.idStaff, x.idDistributor, x.createdDate, x.purchase, x.types, x.description
                    });

                    table.rows.add(result);
                    table.draw();



                }
                else {
                    $('#loading').hide();
                    alert('Tìm kiếm không thành công!')
                };
            }
        });
    })
    //foot search
    $('#bill-list tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input class="form-control" style="width:110px;" type="text" id="' + title.replace(' ', '_') + '" placeholder="Search ' + title + '" />');
    });

    table.columns().every(function () {
        var that = this;
        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that.search(this.value).draw();
            }
        });
    });
});


$(document).ready(function () {

    var table = $('#delivery-order-list').DataTable();

    $("#searchListDeliveryOrder").on('click', function () {
        var idDistributor = $("#idDistributor").val();
        if (!idDistributor) {
            idDistributor = "0";
        }
        var month = $("#month").val();
        if (!month) {
            month = "0";
        }
        var quartar = $("#quartar").val();
        if (!quartar) {
            quartar = "0";
        }
        var year = $("#year").val();
        if (!year) {
            year = "0";
        }
        debugger;
        var url = 'SearchListDeliveryOrder';
        console.log(status);
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            beforeSend: function () {
                loading = true;
                $('#loading').show();

            },
            data: {
                month: parseInt(month),
                quartar: parseInt(quartar),
                year: parseInt(year),
                idDistributor: parseInt(idDistributor),
            },
            success: function (data) {
                if (data) {
                    debugger;
                    $('#loading').hide();
                    table.clear();
                    var result = data.map(function (item) {
                        var rs = [];
                        var date = new Date(item.deliveryDate);

                        rs.push(item.idDeliveryOrder);
                        rs.push(date.getHours() + ':' + date.getMinutes() + ' ' + +(date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
                        rs.push(item.name);
                        rs.push(item.staffName);
                        rs.push(item.totalPurchase);
                        return rs;
                        //x.idOrder, x.deliveryDate, x.idDistributor, x.idStaff, x.totalPurchase
                    });

                    table.rows.add(result);
                    table.draw();



                }
                else {
                    $('#loading').hide();
                    alert('Tìm kiếm không thành công!')
                };
            }
        });
    })
    //foot search
    $('#delivery-order-list tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input class="form-control" style="width:100px;" type="text" id="' + title.replace(' ', '_') + '" placeholder="Search ' + title + '" />');
    });

    table.columns().every(function () {
        var that = this;
        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that.search(this.value).draw();
            }
        });
    });
});

$(document).ready(function () {

    var table = $('#business-list').DataTable({
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                    i : 0;
            };

            // Total over all pages
            totalsp = api
                .column(3)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotalsp = api
                .column(3, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            // Total over all pages
            total = api
                .column(4)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotal = api
                .column(4, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(3).footer()).html(
                 + pageTotalsp + ' (' + totalsp + ' Số lượng All)'
            );
            $(api.column(4).footer()).html(
                '$' + pageTotal + ' ( $' + total + ' Tổng tiền All)'
            );
        }
    });

    $("#searchListOrderDetail").on('click', function () {
        var idDistributor = $("#idDistributor").val();
        if (!idDistributor) {
            idDistributor = "0";
        }
        var month = $("#month").val();
        if (!month) {
            month = "0";
        }
        var quartar = $("#quartar").val();
        if (!quartar) {
            quartar = "0";
        }
        var year = $("#year").val();
        if (!year) {
            year = "0";
        }
        debugger;
        var url = 'SearchListOrderDetail';
        console.log(status);
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            beforeSend: function () {
                loading = true;
                $('#loading').show();

            },
            data: {
                month: parseInt(month),
                quartar: parseInt(quartar),
                year: parseInt(year),
                idDistributor: parseInt(idDistributor),
            },
            success: function (data) {
                if (data) {
                    debugger;
                    $('#loading').hide();
                    table.clear();
                    var result = data.map(function (item) {
                        var rs = [];
                        var date = new Date(item.CreatedDate);

                        rs.push(item.idOrder);
                        rs.push(date.getHours() + ':' + date.getMinutes() + ' ' + +(date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
                        rs.push(item.name);
                        rs.push(item.quantity)
                        rs.push(item.Total);
                        return rs;
                    });

                    table.rows.add(result);
                    table.draw();



                }
                else {
                    $('#loading').hide();
                    alert('Tìm kiếm không thành công!')
                };
            }
        });
    })
    //foot search
    $('#business-list tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input class="FClass" style="width:100px;" type="text" id="' + title.replace(' ', '_') + '" placeholder="Search ' + title + '" />');
    });

    table.columns().every(function () {
        var that = this;
        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that.search(this.value).draw();
            }
        });
    });
});

$(document).ready(function () {

    var table = $('#commodityAllocationReport1').DataTable({
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                    i : 0;
            };

            // Total over all pages
            totalsp = api
                .column(3)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotalsp = api
                .column(3, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            // Total over all pages
            total = api
                .column(4)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotal = api
                .column(4, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(3).footer()).html(
                 +pageTotalsp + ' (' + totalsp + ' Số lượng All)'
            );
            $(api.column(4).footer()).html(
                '$' + pageTotal + ' ( $' + total + ' Tổng tiền All)'
            );
        }
    });

    $("#searchcommodityAllocationReport1").on('click', function () {
        var idDistributor = $("#idDistributor").val();
        if (!idDistributor) {
            idDistributor = "0";
        }
        var month = $("#month").val();
        if (!month) {
            month = "0";
        }
        var quartar = $("#quartar").val();
        if (!quartar) {
            quartar = "0";
        }
        var year = $("#year").val();
        if (!year) {
            year = "0";
        }
        debugger;
        var url = 'SearchListOrderDetail';

        console.log(status);
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            beforeSend: function () {
                loading = true;
                $('#loading').show();

            },
            data: {
                month: parseInt(month),
                quartar: parseInt(quartar),
                year: parseInt(year),
                idDistributor: parseInt(idDistributor),
            },
            success: function (data) {
                if (data) {
                    debugger;
                    $('#loading').hide();
                    table.clear();
                    var result = data.map(function (item) {
                        var rs = [];
                        var date = new Date(item.CreatedDate);

                        rs.push(item.idOrder);
                        rs.push(date.getHours() + ':' + date.getMinutes() + ' ' + +(date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
                        rs.push(item.name);
                        rs.push(item.quantity)
                        rs.push(item.Total);
                        return rs;
                    });

                    table.rows.add(result);
                    table.draw();



                }
                else {
                    $('#loading').hide();
                    alert('Tìm kiếm không thành công!')
                };
            }
        });



        //foot search
        $('#commodityAllocationReport1 tfoot th').each(function () {
            var title = $(this).text();
            $(this).html('<input class="FClass" style="width:100px;" type="text" id="' + title.replace(' ', '_') + '" placeholder="Search ' + title + '" />');
        });

        table.columns().every(function () {
            var that = this;
            $('input', this.footer()).on('keyup change', function () {
                if (that.search() !== this.value) {
                    that.search(this.value).draw();
                }
            });
        });

    });

});

$(document).ready(function () {

    var table = $('#commodityAllocationReport2').DataTable({
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                    i : 0;
            };

            // Total over all pages
            totalsp = api
                .column(3)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotalsp = api
                .column(3, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            // Total over all pages
            total = api
                .column(4)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotal = api
                .column(4, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(3).footer()).html(
                 +pageTotalsp + ' (' + totalsp + ' Số lượng All)'
            );
            $(api.column(4).footer()).html(
                '$' + pageTotal + ' ( $' + total + ' Tổng tiền All)'
            );
        }
    });

    $("#searchcommodityAllocationReport2").on('click', function () {
        var idDistributor = $("#idDistributor").val();
        if (!idDistributor) {
            idDistributor = "0";
        }
        var month = $("#month").val();
        if (!month) {
            month = "0";
        }
        var quartar = $("#quartar").val();
        if (!quartar) {
            quartar = "0";
        }
        var year = $("#year").val();
        if (!year) {
            year = "0";
        }
        debugger;
        var url = 'SearchListDetailDeliveryOrder';
        console.log(status);
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            beforeSend: function () {
                loading = true;
                $('#loading').show();

            },
            data: {
                month: parseInt(month),
                quartar: parseInt(quartar),
                year: parseInt(year),
                idDistributor: parseInt(idDistributor),
            },
            success: function (data) {
                if (data) {
                    debugger;
                    $('#loading').hide();
                    table.clear();
                    var result = data.map(function (item) {
                        var rs = [];
                        var date = new Date(item.deliveryDate);

                        rs.push(item.idOrder);
                        rs.push(date.getHours() + ':' + date.getMinutes() + ' ' + +(date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
                        rs.push(item.name);
                        rs.push(item.quantity)
                        rs.push(item.totalPurchase);
                        return rs;
                    });

                    table.rows.add(result);
                    table.draw();



                }
                else {
                    $('#loading').hide();
                    alert('Tìm kiếm không thành công!')
                };
            }
        });
    })


    $('#commodityAllocationReport2 tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input class="FClass" style="width:100px;" type="text" id="' + title.replace(' ', '_') + '" placeholder="Search ' + title + '" />');
    });

    table.columns().every(function () {
        var that = this;
        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that.search(this.value).draw();
            }
        });
    });
});