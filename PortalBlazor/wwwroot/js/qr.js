window.addEventListener("load", () => generateQr());

function generateQr() {
    if(document.getElementById("qrCode")){
        const uri = document.getElementById("qrCodeData").getAttribute('data-url');
        new QRCode(document.getElementById("qrCode"),
            {
                text: uri,
                width: 150,
                height: 150
            });
    }
    else {
        setTimeout(() => {
            generateQr();
        },100);
    }
}