function load() {

        // si tiene salto de pagina en data.json le dice que no existe ese archivo
        // TODO EN UNA SOLA FILA

	var mydata = JSON.parse(data);

	alert(mydata[0].name);
        alert(mydata[1].name);

}
