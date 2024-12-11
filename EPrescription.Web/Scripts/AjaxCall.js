
//Basic function for ajax call get data ========================================================================================================================
function ajaxCall(url, paramData, callback, method, obj) {
    method = method == null ? "POST" : method;
    $.ajax({
        type: method,
        url: url,
        data: JSON.stringify(paramData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (callback == "renderComplaintLoad") {
                renderComplaintLoad(response);
            }
              else if (callback == "renderDiseaseLoad") {
                renderDiseaseLoad(response);
            } else if (callback == "rendeDosageTypeLoad") {
                rendeDosageTypeLoad(response);
            } else if (callback == "rendeLoadAvailablity") {
                rendeLoadAvailablity(response);
            } else if (callback == "renderProcedureLoad") {
                renderProcedureLoad(response);
            } else if (callback == "renderLoadInvestigation") {
                renderLoadInvestigation(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + "! please try again", '<i class="fa fa-exclamation-circle" aria-hidden="true"> Alert</i>');
        }
    });
}
function renderRemoveItem(data) {
    location.reload();
}
