var Precintos = [];

function insertarPrecinto() {
    try {
        var PrecintoToAdd = document.getElementById("txtPrecinto").value;

        if (PrecintoToAdd != "" && (Precintos.length == 0 || Precintos.indexOf(PrecintoToAdd) < 0)) {

            Precintos.push(PrecintoToAdd);
            renderPrecintosLista();
            document.getElementById("txtPrecinto").value = "";
        }

        return false
    }
    catch (err) {
        return false;
    }
}

function removerPrecinto(precinto) {

    try {

        index = Precintos.indexOf(precinto);

        if (index > -1) {
            Precintos.splice(index, 1);
        }

        renderPrecintosLista();

        return false;
    }
    catch (err) {
        return false;
    }
}

function renderPrecintosLista() {

    var div = document.getElementById('dvPrecintos');
    var hdfPrecintos = document.getElementById('hdfprecinto');

    hdfPrecintos.value = "";
    div.innerHTML = "";

    for (i = 0; i < Precintos.length; i++) {

        var divPrecinto = document.createElement('div');

        divPrecinto.style = 'width: 200px'
        divPrecinto.innerHTML = '<input readonly="readonly" type="text" name="focus" required class="search-box" value="' + Precintos[i] + '"/>\
                    <button id="' + Precintos[i] + '" class="close-icon" type="submit"  onclick="return removerPrecinto(this.id)"></button><br />';
        div.appendChild(divPrecinto);

        if (hdfPrecintos.value == "") {
            hdfPrecintos.value = Precintos[i];
        } else {
            hdfPrecintos.value += ',' + Precintos[i];
        }
    }
}