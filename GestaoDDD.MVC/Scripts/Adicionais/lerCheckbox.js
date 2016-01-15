    // essa função recebe o nome "comun" ao checkboxes e a quantidade dos mesmos
    function verificar() {
        var listaMarcados = document.getElementsByTagName("div_check");
        for (loop = 0; loop < listaMarcados.length; loop++) {
            var item = listaMarcados[loop];
            if (item.type == "checkbox" && item.checked) {
                alert(item.id);
            }
        }
    }
