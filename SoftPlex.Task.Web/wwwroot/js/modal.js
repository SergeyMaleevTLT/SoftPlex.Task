function openModalToUpdate(parameters) {
    const id = parameters.data;
    const url = parameters.url;
    const modal = $('#modal');

    if (id == undefined && url === undefined) {
        alert('Something went wrong')
        return;
    }

    $.ajax({
        type: 'GET',
        url: url,
        data: { "id": id },
        success: function (response) {
            modal.find(".modal-body").html(response);
            modal.modal('show')
        },
        failure: function () {
            modal.modal('hide')
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};

function openModalToCreate(parameters) {
    const url = parameters.url;
    const modal = $('#modal');

    if (url === undefined) {
        alert('Something went wrong')
        return;
    }

    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            modal.find(".modal-body").html(response);
            modal.modal('show')
        },
        failure: function () {
            modal.modal('hide')
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};