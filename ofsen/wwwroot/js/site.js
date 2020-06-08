
const GET = "GET";
const POST = "POST";

// #region Aias
function OzAjax(url, method, sendData, cllBckFnSuccess, cllBckFnError, cllBckFnProcess, multipart) {
    this.url = url;
    this.method = method || GET;
    this.sendData = sendData;
    this.cllBckFnSuccess = cllBckFnSuccess;
    this.cllBckFnProcess = cllBckFnProcess;
    this.cllBckFnError = cllBckFnError;
    this.multipart = multipart;
    this.Running = false;
}
OzAjax.prototype.BasitGonder = function () {
    this.Running = true;
    $.ajax({
        url: this.url,
        method: this.method,
        data: this.sendData,
        dataType: "text",
        global:false,
        success: this.cllBckFnSuccess,
        error: this.cllBckFnError || this.Error
    });
    this.Running = false;
};
OzAjax.prototype.MultiGonder = function () {
    var that = this;
    this.Running = true;
    $.ajax({
        url: this.url,
        method: "POST",
        data: this.sendData,
        dataType: "text", 
        global: false,
        enctype: "multipart/form-data",
        timeout: 180000,
        contentType: false,
        processData: false,
        cache: false,
        async: true,
        success: this.cllBckFnSuccess,
        error: this.Error,
        xhr: function () {
            var myxhr = $.ajaxSettings.xhr();
            if (myxhr.upload) {
                myxhr.upload.addEventListener("progress", that.cllBckFnProcess || that.Process);// function (event) { AyakMultiFormProgress(event, processCallBack); });
            }
            return myxhr;
        }
    });
    this.Running = false;
};
OzAjax.prototype.Error = function (hata) {
    new OzModal().Bilgi(hata, 1200);
};
OzAjax.prototype.Process = function (event) {
    var percent = 0;
    var position = event.loaded || event.position;
    var total = event.total;
    if (event.lengthComputable) {
        percent = Math.ceil(position / total * 100);
    }
    this.cllBckFnProcess(percent);
};

$().addClass("");
// #endregion

// #region Ozmodal
function OzModal() {
    this.ozmodal = $("#ozModal");
    this.ozicerik = $("#ozIcerik");
    this.bilgiUsed = 0;
}
OzModal.prototype.Bilgi = function (bilgi, sure) {
    this.ozicerik.html(bilgi);
    this.ozmodal.modal("show");
    setTimeout(this.Kapat, sure || 1200);
    this.bilgiUsed++;
};
OzModal.prototype.Sayfa = function (icerik) {
    this.ozicerik.html(icerik);
    this.ozmodal.modal("show");
};
OzModal.prototype.Kapat = function () {
    $("#ozModal").modal("hide");
    this.bilgiUsed--;
    if (this.bilgiUsed < 1)
        $("#ozModal").modal("hide");
};
// #endregion

// #region OzOnay
function OzOnay(soru, evetBtnText, hayirBtnText, callEvet, callHayir) {

    this.soru = soru;
    this.evetBtnText = evetBtnText;
    this.hayirBtnText = hayirBtnText;
    this.callEvet = callEvet;
    this.callHayir = callHayir;

    this.ozmodal = new Ozmodal();

    this.mModal = $("#ozOnayDiv");
    this.mSoru = $("#ozOnaySoruDiv");
    this.mEvet = $("#ozOnayEvetBtn");
    this.mHayir = $("#ozOnayHayirBtn");
}
OzOnay.prototype.Sor = function () {
    this.mSoru.html(this.soru);
    this.mEvet.html(this.evetBtnText);
    this.mHayir.html(this.hayirBtnText);
    this.ozmodal.Sayfa(this.mModal);

    this.mEvet.click( this.callEvet).click(this.ozmodal.Kapat);
    this.mHayir.click(this.callHayir).click(this.ozmodal.Kapat);
};
// #endregion




    

