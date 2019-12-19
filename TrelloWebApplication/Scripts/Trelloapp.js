$('#btnCom').click(function () {
    $("#btnCom").hide();
    $('#AddCom').show();
});



$("#BtnSave").click(function () {
    var path = window.location.pathname; 		// e' l'ultima parte del path, compreso il carattere "/" iniziale
    var basepath = SetupBaseRoute(path);
    if (basepath.indexOf("Select") != -1) {
        // soltanto se e' listino,
        var newpath = basepath + "/SpostaCard";
        var e = document.getElementById("StatoId");     //stato in cui spostare le card selezionate
        var strUser = e.options[e.selectedIndex].text;

        var s2 = document.getElementById("lbprodplid");     // listbox degli id relativo alle card da spostare 
        var idtosend = [];
        var numel = s2.options.length;

        for (var i = 0; i < numel; i++) {
            idtosend.push(s2.options[i].text);
        }

        // serializza l'oggetto
        var jsonlist = JSON.stringify(idtosend);
        $.post(newpath, { idlistino: strUser, jsonids: jsonlist })
    };
});


function addLoadEvent(func) {
    var oldonload = window.onload;
    if (typeof window.onload != "function") {
        window.onload = func;
    }
    else {
        window.onload = function () {
            if (oldonload) {
                oldonload();
            }
            func();
        };
    }
}

addLoadEvent(function () {
    // inserire qui il codice da eseguire al caricamento della pagina
});

// chiamata al completamento del caricamento della pagina.
$(function () {
    try {
        //alert("qua");
    } catch (e) {
        alert(e.message);
    }
});


$("#BtnAdd").click(function () {
    // deve leggere gli elementi selezionati della listbox dei prodotti
    // e poi muoverli nella listbox contenente le card da spostare.
    var selcode = [];       // array contenente i codici prodotti selezionati dalla listbox
    var selid = [];         // array contnente gli id prodotti relativi ai codici selezionati
    var s = document.getElementById("lbprodid");        // listbox degli id
    var s1 = document.getElementById("lbproducts");     // listbox dei nomi
    var s2 = document.getElementById("lbprodplid");     // listbox degli id relativi alle card da spostare.
    var s3 = document.getElementById("lbprodpl");       // listbox i nomi relativi delle card da spostare.


    // in selcode e in selid soltanto i nomi e i relativi id selezionati
    var numel = s1.options.length;
    var i;
    for (i = 0; i < numel; i++) {
        if (s1.options[i].selected) {
            selcode.push(s1.options[i].text);
            selid.push(s.options[i].text);
        }
    }

    // aggiunge le card allo stato selezionato, toglie dalla lista delle card disponibili
    for (i = 0; i < selid.length; i++) {
        var option = document.createElement("option");
        var option1 = document.createElement("option");

        // aggliunge gli elementi ai tag destinazione
        option.text = selcode[i];
        s3.add(option);
        option1.text = selid[i];
        s2.add(option1);

        // rinuove gli elementi dai tag sorgente
        if ((index = indexMatchingText(s1, option.text)) >= 0)
            s1.remove(index);
        if ((index = indexMatchingText(s, option1.text)) >= 0) {
            s.remove(index);
        }
    }
});

$("#BtnRemove").click(function () {
    // inverso di BtnAdd
    // deve leggere gli elementi selezionati della listbox  contenente i prodotti per il listino
    // e poi muoverli nelle listbox contenente i prodotti.
    // deve leggere gli elementi selezionati della listbox dei prodotti
    // e poi muoverli nella listbox contenente i prodotti per listino.
    var selcode = [];       // array contenente i codici prodotti selezionati dalla listbox
    var selid = [];         // array contnente gli id prodotti relativi ai codici selezionati
    var s = document.getElementById("lbprodid");        // listbox degli id
    var s1 = document.getElementById("lbproducts");     // listbox dei codici
    var s2 = document.getElementById("lbprodplid");     // listbox degli id relativi ai prodotti contenuti nel listino (hidden)
    var s3 = document.getElementById("lbprodpl");       // listbox dei codici relativi ai prodotti del listino

    //var s = $("#lbprodid")[0];        // listbox degli id
    //var s1 = $("#lbproducts")[0];     // listbox dei codici
    //var s2 = $("#lbprodplid")[0];     // listbox degli id relativi ai prodotti contenuti nel listino (hidden)
    //var s3 = $("#lbprodpl")[0];       // listbox dei codici relativi ai prodotti del listino

    // in selcode e in selid soltanto i codici e i relativi id selezionati
    var numel = s3.options.length;
    var i;
    for (i = 0; i < numel; i++) {
        if (s3.options[i].selected) {
            selcode.push(s3.options[i].text);
            selid.push(s2.options[i].text);
        }
    }

    // aggiunge i prodotti al listino, toglie dalla lista dei prodotti disponibili
    for (i = 0; i < selid.length; i++) {
        var option = document.createElement("option");
        var option1 = document.createElement("option");

        // aggliunge gli elementi ai tag destinazione
        option.text = selcode[i];
        s1.add(option);
        option1.text = selid[i];
        s.add(option1);

        // rinuove gli elementi dai tag sorgente
        if ((index = indexMatchingText(s3, option.text)) >= 0)
            s3.remove(index);
        if ((index = indexMatchingText(s2, option1.text)) >= 0) {
            s2.remove(index);
        }
    }
});






// Ritorna soltanto l'url del controller. Utilizzato per costruire una nuova
// chiamata a una action dello stesso controller, via javascript
function SetupBaseRoute(partialpath) {
    try {
        var result = "";
        var index;
        if (partialpath == "/") {
            result = "../../Home";
        }
        else {
            while (1) {
                if ((index = partialpath.lastIndexOf("/")) == 0) {
                    result = "../.." + partialpath;
                    break;
                }
                else if (index > 0) {
                    partialpath = partialpath.substring(0, index);
                }
                else {
                    alert("errore: partialpath=" + partialpath);
                    break;
                }
            }
        }
        return result;
    } catch (e) {
        alert(e.message);
    }
}

// trova il testo all'interno dell'elemento, e ne restituisce l'indice
function indexMatchingText(ele, text) {
    for (var i = 0; i < ele.length; i++) {
        if (ele[i].childNodes[0].nodeValue === text) {
            return i;
        }
    }
    return -1;

}

// forza il reload della pagina
function PageReload() {
    window.location.href = window.location.href;
}